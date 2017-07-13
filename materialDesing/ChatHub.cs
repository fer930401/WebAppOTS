using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Entidades;

namespace materialDesing
{
    public class ChatHub : Hub
    {
        public static string emailIDLoaded = "";

        #region Connect
        public void Connect(string user_cve, string user, string email)
        {
            emailIDLoaded = email;
            var id = Context.ConnectionId;
            using (DBOTSEntities dc = new DBOTSEntities())
            {
                var item = dc.ChatUserDetalle.FirstOrDefault(x => x.mail.Equals(email) && x.user_cve.Equals(user_cve));
                if (item != null)
                {
                    dc.ChatUserDetalle.Remove(item);
                    dc.SaveChanges();

                    // Disconnect
                    Clients.All.onUserDisconnectedExisting(item.connection_id, item.user_cve);
                }

                var Users = dc.ChatUserDetalle.ToList();
                if (Users.Where(x => x.user_cve.Equals(user_cve) && x.mail.Equals(email)).ToList().Count == 0)
                {
                    var userdetails = new ChatUserDetalle
                    {
                        connection_id = id,
                        user_cve = user_cve,
                        user_nom = user,
                        mail = email
                    };
                    dc.ChatUserDetalle.Add(userdetails);
                    dc.SaveChanges();

                    // send to caller
                    var connectedUsers = dc.ChatUserDetalle.ToList();
                    var CurrentMessage = dc.ChatsGrupales.ToList();
                    Clients.Caller.onConnected(id, user_cve, connectedUsers, CurrentMessage);
                }

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, user_cve, user, email);
            }
        }
        #endregion

        #region Disconnect
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            using (DBOTSEntities dc = new DBOTSEntities())
            {
                var item = dc.ChatUserDetalle.FirstOrDefault(x => x.connection_id == Context.ConnectionId);
                if (item != null)
                {
                    dc.ChatUserDetalle.Remove(item);
                    dc.SaveChanges();

                    var id = Context.ConnectionId;
                    Clients.All.onUserDisconnected(id, item.user_cve);
                }
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region Send_To_All
        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            AddAllMessageinCache(userName, message);

            // Broad cast message
            Clients.All.messageReceived(userName, message);
        }
        #endregion

        #region Private_Messages
        public void SendPrivateMessage(string toUserId, string message, string status)
        {
            string fromUserId = Context.ConnectionId;
            using (DBOTSEntities dc = new DBOTSEntities())
            {
                var toUser = dc.ChatUserDetalle.FirstOrDefault(x => x.connection_id == toUserId);
                var fromUser = dc.ChatUserDetalle.FirstOrDefault(x => x.connection_id == fromUserId);
                if (toUser != null && fromUser != null)
                {
                    if (status == "Click")
                        AddPrivateMessageinCache(fromUser.mail, toUser.mail, fromUser.user_cve, message);

                    // send to 
                    Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.user_cve, message, fromUser.mail, toUser.mail, status, fromUserId);

                    // send to caller user
                    Clients.Caller.sendPrivateMessage(toUserId, fromUser.user_cve, message, fromUser.mail, toUser.mail, status, fromUserId);
                }
            }
        }
        public List<PrivateChatMessage> GetPrivateMessage(string fromid, string toid, int take)
        {
            using (DBOTSEntities dc = new DBOTSEntities())
            {
                List<PrivateChatMessage> msg = new List<PrivateChatMessage>();

                var v = (from a in dc.ChatsPrivados
                         join b in dc.ChatsPrivadosDetalles on a.mail equals b.emisor_chat into cc
                         from c in cc
                         where (c.emisor_chat.Equals(fromid) && c.receptor_chat.Equals(toid)) || (c.emisor_chat.Equals(toid) && c.receptor_chat.Equals(fromid))
                         orderby c.emisor_chat descending
                         select new
                         {
                             UserName = a.user_cve,
                             Message = c.mensaje,
                             ID = c.ID
                         }).Take(take).ToList();
                v = v.OrderBy(s => s.ID).ToList();

                foreach (var a in v)
                {
                    var res = new PrivateChatMessage()
                    {
                        userName = a.UserName,
                        message = a.Message
                    };
                    msg.Add(res);
                }
                return msg;
            }
        }

        private int takeCounter = 0;
        private int skipCounter = 0;
        public List<PrivateChatMessage> GetScrollingChatData(string fromid, string toid, int start = 10, int length = 1)
        {
            takeCounter = (length * start); // 20
            skipCounter = ((length - 1) * start); // 10

            using (DBOTSEntities dc = new DBOTSEntities())
            {
                List<PrivateChatMessage> msg = new List<PrivateChatMessage>();
                var v = (from a in dc.ChatsPrivados
                         join b in dc.ChatsPrivadosDetalles on a.mail equals b.emisor_chat into cc
                         from c in cc
                         where (c.emisor_chat.Equals(fromid) && c.receptor_chat.Equals(toid)) || (c.emisor_chat.Equals(toid) && c.receptor_chat.Equals(fromid))
                         orderby c.emisor_chat descending
                         select new
                         {
                             UserName = a.user_cve,
                             Message = c.mensaje,
                             ID = c.emisor_chat
                         }).Take(takeCounter).Skip(skipCounter).ToList();

                foreach (var a in v)
                {
                    var res = new PrivateChatMessage()
                    {
                        userName = a.UserName,
                        message = a.Message
                    };
                    msg.Add(res);
                }
                return msg;
            }
        }
        #endregion

        #region Save_Cache
        private void AddAllMessageinCache(string userName, string message)
        {
            using (DBOTSEntities dc = new DBOTSEntities())
            {
                var messageDetail = new ChatsGrupales
                {
                    user_cve = userName,
                    mensaje = message,
                    mail = emailIDLoaded,
                    fecha = DateTime.Now
                };
                dc.ChatsGrupales.Add(messageDetail);
                dc.SaveChanges();
            }
        }

        private void AddPrivateMessageinCache(string fromEmail, string chatToEmail, string userName, string message)
        {
            using (DBOTSEntities dc = new DBOTSEntities())
            {
                // Save master
                var master = dc.ChatsPrivados.ToList().Where(a => a.mail.Equals(fromEmail)).ToList();
                if (master.Count == 0)
                {
                    var result = new ChatsPrivados
                    {
                        mail = fromEmail,
                        user_cve = userName
                    };
                    dc.ChatsPrivados.Add(result);
                    dc.SaveChanges();
                }

                // Save details
                var resultDetails = new ChatsPrivadosDetalles
                {
                    emisor_chat = fromEmail,
                    receptor_chat = chatToEmail,
                    mensaje = message
                };
                dc.ChatsPrivadosDetalles.Add(resultDetails);
                dc.SaveChanges();
            }
        }
        #endregion
    }

    public class PrivateChatMessage
    {
        public string userName { get; set; }
        public string message { get; set; }
    }
}