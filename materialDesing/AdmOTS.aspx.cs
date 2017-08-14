using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class AdmOTS : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        string nomImagenes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                string clave = Session["user_cve"].ToString();
                user_cve.Text = clave;

                int num_OTSU = variables.Num_OTS;
                string tip_OTSU = variables.Tipo_OTS;
                string userResp = variables.User_OTS;

                List<Entidades.otsemov> OTSEncabezado = logicaNegocio.consulta_OTS(num_OTSU, tip_OTSU, userResp);

                string sub_sistema = OTSEncabezado[0].ss_cve.ToString();
                if (tip_OTSU.Equals("SOP") == true)
                {
                    Session["checkSop"] = "checked";
                    Session["checkPen"] = "";
                }
                else if(tip_OTSU.Equals("PEN") == true)
                {
                    Session["checkPen"] = "checked";
                    Session["checkSop"] = "";
                }
                else
                {
                    Session["checkSop"] = "";
                    Session["checkPen"] = "";
                }
                txtDescrip.Text = OTSEncabezado[0].descripcion;

                string status = OTSEncabezado[0].sts_prog.ToString().TrimEnd();
                if (status.Equals("1") == true)
                {
                    Session["checkAct"] = "checked";
                    Session["checkDes"] = "";
                }
                else if (status.Equals("0") == true)
                {
                    Session["checkDes"] = "checked";
                    Session["checkAct"] = "";
                }
                else
                {
                    Session["checkAct"] = "";
                    Session["checkDes"] = "";
                }

                if (OTSEncabezado[0].fec_prom != null)
                {
                    fechaIni.Text = OTSEncabezado[0].fec_prom.Value.ToString("yyyy-MM-dd");
                }

                if (string.IsNullOrEmpty(OTSEncabezado[0].nomImg) == true)
                {
                    /* si no trae informacion se muestra el bloque para agregar imagenes */
                    Session["visibleImagenes"] = "";
                }
                else
                {
                    Session["visibleImagenes"] = "style='display:none'";
                    nomImagenes = OTSEncabezado[0].nomImg.ToString().TrimEnd();
                }

                
                if (!IsPostBack)
                {
                    prg_res.DataSource = logicaNegocio.ListadoProgramadores(1, "PRG");
                    prg_res.DataTextField = "nombre";
                    prg_res.DataValueField = "user_cve";
                    prg_res.DataBind();
                    prg_res.SelectedValue = userResp;

                    apl_para2.DataSource = logicaNegocio.ListadoPaises();
                    apl_para2.DataTextField = "nombre";
                    apl_para2.DataValueField = "elm_cve";
                    apl_para2.DataBind();


                    if (string.IsNullOrEmpty(OTSEncabezado[0].aplica) == false)
                    {
                        string[] roles = OTSEncabezado[0].aplica.ToString().Split(',');
                        for (int i = 0; i < apl_para2.Items.Count; i++)
                        {
                            for (int y = 0; y < roles.Length; y++)
                            {
                                if (apl_para2.Items[i].Value.Equals(roles[y].TrimEnd(' ').TrimStart(' ')) == true)
                                {
                                    apl_para2.Items[i].Selected = true;
                                }
                            }
                        }
                    }

                    ss.DataSource = logicaNegocio.ListadoSubSistemas();
                    ss.DataTextField = "nombre";
                    ss.DataValueField = "elm_cve";
                    ss.DataBind();
                    ss.SelectedValue = sub_sistema;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void btnGuardarOTS_Click(object sender, EventArgs e)
        {
            string sub_sistema = ss.SelectedValue;
            string tip_OTS = variables.Tipo_OTS;
            string asigna = user_cve.Text.ToUpper();
            string nom_OTS = ""; //Request["nomOTS"].ToString().TrimEnd(' ');
            string responsable = prg_res.SelectedValue;
            string status = Request["status"];
            string descripcion = Request["descripcion"].ToString().TrimEnd(' ');
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaProm = Convert.ToDateTime(fechaIni.Text.ToString());
            
            HttpFileCollection files = Request.Files;
            for (int i = 0; i <= files.Count - 1; i++)
            {
                HttpPostedFile postedFile = files[i];
                if (postedFile.ContentLength > 0)
                {
                    postedFile.SaveAs(Server.MapPath(@"Media\Upload\") + Path.GetFileName(postedFile.FileName));
                    nomImagenes = nomImagenes + postedFile.FileName + ";";
                }
            }

            string selectedValue = "";
            foreach (ListItem item in apl_para2.Items)
            {
                if (item.Selected)
                {
                    selectedValue += item.Value + ", ";
                }
            }
            string aplica = selectedValue;
            
            if (/*string.IsNullOrEmpty(nom_OTS) == false && */string.IsNullOrEmpty(descripcion) == false && string.IsNullOrEmpty(aplica) == false)
            {
                Entidades.sp_WebAppOTSAdmOTS_Result insertOTSE = logicaNegocio.admOTS(sub_sistema, tip_OTS, asigna, variables.Num_OTS, "", "", responsable, status, descripcion, fechaInicio.ToString("yyyy/MM/dd"), "", aplica, variables.Tipo_OTS, nom_OTS, "", "", "", "", "actOTS", nomImagenes, fechaProm);
                if (insertOTSE != null)
                {
                    error = insertOTSE.error;
                    mensaje = insertOTSE.mensaje;
                    if (Convert.ToInt32(error) == 0)
                    {
                        //sendEmail(responsable, nom_OTS, "", descripcion, fechaIni, aplica);
                        if (variables.Tipo_OTS.Substring(0, 3).Equals("SOP") == true)
                        {
                            sendEmail(responsable, "", "", descripcion, fechaProm, aplica);
                            variables.Num_OTS = 0;
                            variables.Tipo_OTS = null;
                            variables.User_OTS = null;
                            Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "'); window.location.href = 'C_Soportes.aspx';</script>");

                        }
                        else if (variables.Tipo_OTS.Substring(0, 3).Equals("PEN") == true)
                        {
                            Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "'); window.location.href = 'C_Pendientes.aspx';</script>");
                        }                        
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Encabezado.aspx';</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('No puedes dejar campos vacios, ni espacios en blanco');</script>");
            }
        }
        public void sendEmail(string responsable, string nom_OTS, string operacion, string descripcion, DateTime fechaFin, string aplica)
        {
            string emailRespon = emailResponsable(responsable);
            var to = emailRespon;
            var cc = "fernando.garcia@skytex.com.mx";
            var bcc = "fernando.garcia@skytex.com.mx";
            var emailRemitente = "soludin@skytex.com.mx";

            var eMailSubject = "Actualizacion OTS";
            var eMailMessage =
                "<html lang='en'>" +
                "<head>" +
                    "<style>" +
                        "html { font-family: sans-serif; font-size: 11px -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;}" +
                        "body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.428571429; color: #333333; background-color: #ffffff; } " +
                    "</style>" +
                "</head>" +
                    "<body>" +
                    "<h4> Actualizacion OTS - Skytex México</h4>" +
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
                            "<strong>Se a actualizado la informacion de un OTS</strong>" +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "" +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Responsable:</strong> " + responsable +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Descripcion:</strong> " + descripcion +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Fecha Prometida:</strong> " + fechaFin.ToString("dd/MM/yyyy") +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Aplica para:</strong> " + entiFinancieras(aplica) +
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string emailResponsable(string responsable)
        {
            string emailRespon;
            SqlConnection _conn = new SqlConnection(variables.Conexion);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select mail from usuarios where user_cve = '{0}'", responsable);
            _conn.Open();
            emailRespon = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            _conn.Close();
            return emailRespon;
        }
        public string entiFinancieras(string idEntidades)
        {
            string nomEntidades = "";
            string[] entidades = idEntidades.Split(',');
            SqlConnection _conn = new SqlConnection(variables.Conexion);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            for (int i = 0; i < entidades.Length; i++)
            {
                _cmd.CommandText = String.Format("select nombre from otscatlgos where elm_cve = '{0}'", entidades[i].TrimStart(' ').TrimEnd(' '));
                _conn.Open();
                nomEntidades = nomEntidades + Convert.ToString(_cmd.ExecuteScalar()) + ", ";
                _cmd.ExecuteNonQuery();
                _conn.Close();
            }
            nomEntidades = nomEntidades.Substring(0, nomEntidades.Length - 2);
            return nomEntidades;
        }
    }
}