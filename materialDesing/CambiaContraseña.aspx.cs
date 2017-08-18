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
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
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
            variables.Pass = GetSHA1HashData(txtPass.Text);

            if (variables.Email.Equals(variables.EmailConfirm) == true)
            {
                try
                {
                    Entidades.sp_WebAppOTSAdmUsers_Result cambioPass = logicaNegocio.admUserOTS("", "", variables.Pass, 0, variables.Email, "", "actPass");
                    if (cambioPass != null)
                    {
                        error = cambioPass.error;
                        mensaje = cambioPass.mensaje;
                    }
                    if(error == 0)
                    {
                        variables.Email = "";
                        variables.EmailConfirm = "";
                        variables.Pass = "";
                        Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "'); window.location.href = 'Login.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "'); window.location.href = 'CambiaContraseña.aspx';</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script type=\"text/javascript\">alert('Ocurrio un error, favor de revisar'); window.location.href = 'CambiaContraseña.aspx';</script>");
                }               
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('Los emails ingresados no coinciden, favor de revisar'); window.location.href = 'CambiaContraseña.aspx';</script>");
            }
            
        }
        private string GetSHA1HashData(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }

        public void sendEmail(string responsable, string nom_OTS, string operacion, string descripcion, DateTime fechaFin, string aplica)
        {
            string emailRespon = responsable;
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
                            "<strong>Realiza el sigueinte proceso</strong>" +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "" +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "Se a solicitado el cambio de contraseña: fecha de solicitud" + DateTime.Now +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Da click en el siguiente enlace:</strong> " + fechaFin.ToString("dd/MM/yyyy") +
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
    }
}