using System;
using LogicaNegocio;
using AccesoDatos;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;

namespace materialDesing
{
    public partial class ReasignaOTS : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int num_OTSU = Int32.Parse(Request["num_OTS"].ToString());
                string tip_OTSU = Request["tip_OTS"].ToString().ToUpper();
                tip_OTSU = tip_OTSU.Substring(0, 3);
                txtNumOTS.Text = num_OTSU.ToString();
                DataTable dt_OTS = reasignacion(num_OTSU, tip_OTSU);
                txtResponsable.Text = dt_OTS.Rows[0][2].ToString();
                txtTipoOTS.Text = Request["tip_OTS"].ToString();
                DataTable datos = new DataTable();
                cmbResponsable.Items.Clear();
                cmbResponsable.Items.Insert(0, new ListItem("Selecciona una opción", String.Empty));
                cmbResponsable.SelectedIndex = 0;
                cmbResponsable.AppendDataBoundItems = true;
                cmbResponsable.DataSource = logicaNegocio.ListadoProgramadores(1, "PRG");
                cmbResponsable.DataValueField = "user_cve";
                cmbResponsable.DataTextField = "nombre";
                cmbResponsable.DataBind();
            }
            
        }
        public DataTable reasignacion(int numOTS, string tip_OTSU)
        {
            DataTable dt = new DataTable();
            try
            {
                string CadenaConecta = @"Data Source=192.168.100.133;Initial Catalog=DBOTS;User ID=soludin;Password=pluma";
                SqlConnection _conn = new SqlConnection(CadenaConecta);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select num_OTS,otscatlgos2.nombre,usuarios.nombre,descripcion from otsemov join otscatlgos otscatlgos2 on otscatlgos2.elm_cve = otsemov.tipo_OTS and otscatlgos2.cve_catlgo in ('OTS') join usuarios on usuarios.user_cve = otsemov.userResp and usuarios.status_usr = 1 where num_OTS = '{0}' and tipo_OTS = '{1}'", numOTS, tip_OTSU);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return dt;
        }

        protected void btnReasignar_Click(object sender, EventArgs e)
        {
            int numOTS = Int32.Parse(txtNumOTS.Text);
            string tipo_OTSU = Request["tip_OTS"].ToString();
            /* variable para la busqueda de valores extra en el mail */
            string tip_OTSU = Request["tip_OTS"].ToString().ToUpper();
            tip_OTSU = tip_OTSU.Substring(0, 3);
            DateTime fechaReasignado = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy"));
            DataTable dt_OTS = reasignacion(numOTS, tip_OTSU);
            string nuevoRespo = cmbResponsable.SelectedValue.ToString();
            sp_WebAppOTSAdmOTS_Result updateOTSE = logicaNegocio.admOTS("", tip_OTSU, nuevoRespo, numOTS, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "reasignarOTS", "", DateTime.Now);
            if (updateOTSE != null)
            {
                error = updateOTSE.error;
                mensaje = updateOTSE.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    sendEmail(nuevoRespo, tipo_OTSU, dt_OTS.Rows[0][1].ToString(), dt_OTS.Rows[0][3].ToString(), fechaReasignado);
                    string s = "<SCRIPT language='javascript'>alert('" + mensaje + "'); window.location.href =  'C_Soportes.aspx';</SCRIPT>";
                    Type cstype = this.GetType();
                    ClientScriptManager cs = this.Page.ClientScript;
                    cs.RegisterClientScriptBlock(cstype, s, s.ToString());
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_Soportes.aspx';</script>");
                }
            }
        }
        public void sendEmail(string responsable, string tip_OTS, string operacion, string descripcion, DateTime fechaFin)
        {
            string emailRespon = emailResponsable(responsable);
            var to = emailRespon;
            var cc = "Programador.Web1@Skytex.com.mx";
            var bcc = "Programador.Web1@Skytex.com.mx";
            var emailRemitente = "soludin@skytex.com.mx";

            var eMailSubject = "Reasignacion de OTS";
            var eMailMessage =
                "<html lang='en'>" +
                "<head>" +
                    "<style>" +
                        "html { font-family: sans-serif; font-size: 11px -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;}" +
                        "body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.428571429; color: #333333; background-color: #ffffff; } " +
                    "</style>" +
                "</head>" +
                    "<body>" +
                    "<h4> Reasignacion de OTS - Skytex México</h4>" +
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
                            "<strong>Se le ha reasigando el siguiente OTS</strong>" +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "" +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Tipo OTS:</strong> " + tip_OTS +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Operacion:</strong> " + operacion +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Descripcion:</strong> " + descripcion +
                           "</td>" +
                          "</tr>" +
                          "<tr>" +
                           "<td width='60%'>" +
                            "<strong>Fecha Reasignado:</strong> " + fechaFin.ToString("dd/MM/yyyy") +
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

            // Agregar el Adjunto si deseamos hacerlo
            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(@"c:\Users\fernando.garcia\Documents\Proyectos Skytex\AplicacionWeb\Prueba.Presentacion\Activo\Reporte Ordenes.xls");
            //mail.Attachments.Add(attachment);

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
            string CadenaConecta = @"Data Source=192.168.100.133;Initial Catalog=DBOTS;User ID=soludin;Password=pluma";
            SqlConnection _conn = new SqlConnection(CadenaConecta);
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
    }
}