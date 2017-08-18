using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class CambiaContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(variables.Email) == false)
                {
                    txtEmail.Text = variables.Email;
                    txtEmailConfirm.Text = variables.EmailConfirm;
                }
            }         
        }

        protected void CerrarSession(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void btnCambiarPass_Click(object sender, EventArgs e)
        {
            variables.Email = txtEmail.Text;
            variables.EmailConfirm = txtEmailConfirm.Text;
            variables.Pass = txtPass.Text;

            if (variables.Email.Equals(variables.EmailConfirm) == true)
            {
                sendEmail(variables.Email, variables.Pass);
                Response.Write("<script type=\"text/javascript\">alert('Se envio un email de confirmacion'); window.location.href = 'Login.aspx';</script>");           
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('Los emails ingresados no coinciden, favor de revisar'); window.location.href = 'CambiaContraseña.aspx';</script>");
            }
            
        }
        

        public void sendEmail(string responsable, string pass)
        {
            string emailEncode = Base64Encode(responsable);
            string passEncode = Base64Encode(pass);
            string hash = emailEncode + "@" + passEncode;
            /* url para pruebas locales */
            //string url = "http://" + HttpContext.Current.Request.Url.Authority + "/valPas.aspx?token=" + hash;
            /* url para servidor IIS */
            string url = "http://" + HttpContext.Current.Request.Url.Authority + "/" + HttpContext.Current.Request.ApplicationPath + "/valPas.aspx?token=" + hash;

            string email = emailResponsable(responsable);
            if (string.IsNullOrEmpty(email) == false)
            {
                string emailRespon = emailResponsable(responsable);
                var to = emailRespon;
                var cc = "fernando.garcia@skytex.com.mx";
                var bcc = "fernando.garcia@skytex.com.mx";
                var emailRemitente = "soludin@skytex.com.mx";

                var eMailSubject = "Cambio de contraseña";
                var eMailMessage =
                    "<html lang='es'>" +
                    "<head>" +
                        "<style>" +
                            "html { font-family: sans-serif; font-size: 11px -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;}" +
                            "body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.428571429; color: #333333; background-color: #ffffff; } " +
                        "</style>" +
                    "</head>" +
                        "<body>" +
                        "<h4> Cambio de Contraseña</h4>" +
                        "<table cellpadding='0' cellspacing='0' width='700'>" +
                         "<tr>" +
                          "<td>" +
                            "<img src='http://i64.tinypic.com/2cwph5l.jpg' width='190' height='90' />" +
                          "</td>" +
                         "</tr>" +
                         "<tr>" +
                          "<td style='padding: 40px 30px 40px 30px;'>" +
                           "<table cellpadding='0' cellspacing='0' width='100%'>" +
                              "<tr>" +
                               "<td width='60%'>" +
                                "<strong>Realiza el siguiente proceso</strong>" +
                               "</td>" +
                              "</tr>" +
                              "<tr>" +
                               "<td width='60%'>" +
                                "" +
                               "</td>" +
                              "</tr>" +
                              "<tr>" +
                               "<td width='60%'>" +
                                "Se a solicitado el cambio de contraseña: fecha de solicitud: <strong>" + DateTime.Now + "</strong>" +
                               "</td>" +
                              "</tr>" +
                              "<tr>" +
                               "<td width='60%'>" +
                                "<strong>Da click en el siguiente enlace para confirmar el cambio: </strong> <a href='" + url + "'>Aqui</a>" +
                               "</td>" +
                              "</tr>" +
                             "</table>" +
                          "</td>" +
                         "</tr>" +
                         "<tr>" +
                          "<td bgcolor='#222222'>" +
                           "<p align='center'><font color= '#ffffff'></font></p>" +
                          "</td>" +
                         "</tr>" +
                        "</table>" +
                        "</body>" +
                    "</html>";

                MailMessage mail = new MailMessage();
                mail.To.Add(new System.Net.Mail.MailAddress(to));
                mail.From = new System.Net.Mail.MailAddress(emailRemitente, "OTS Web", System.Text.Encoding.UTF8);
                mail.CC.Add(new System.Net.Mail.MailAddress(cc));
                mail.Bcc.Add(new System.Net.Mail.MailAddress(bcc));
                mail.Subject = eMailSubject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = eMailMessage;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                // Configuración SMTP
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("outlook.skytex.com.mx", 25);

                // Crear Credencial de Autenticacion
                smtp.Credentials = new System.Net.NetworkCredential("soludin", "pluma");
                smtp.EnableSsl = false;

                try
                {
                    smtp.Send(mail);
                    variables.Email = "";
                    variables.EmailConfirm = "";
                    variables.Pass = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('El correo ingresado no es valido, favor de revisar'); window.location.href = 'CambiaContraseña.aspx';</script>");
            }            
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
         
        public string emailResponsable(string responsable)
        {
            string emailRespon;
            SqlConnection _conn = new SqlConnection(variables.Conexion);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select mail from usuarios where mail = '{0}'", responsable);
            _conn.Open();
            emailRespon = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            _conn.Close();
            return emailRespon;
        }
    }
}