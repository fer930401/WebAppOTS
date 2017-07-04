using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class materialDesing : System.Web.UI.MasterPage
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombre"] != null)
            {
                string user = Session["nombre"].ToString();
                string clave = Session["user_cve"].ToString().ToUpper();
                List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> OTSActivo = logicaNegocio.ListadoOTS(clave, "2", 5, "", "", "");
                if (OTSActivo.Count != 0)
                {
                    lblNum_OTS.Text = OTSActivo[0].userAsig.ToString();
                    lblReng_OTS.Text = OTSActivo[0].num_OTS.ToString();
                    lblTip_OTS.Text = OTSActivo[0].tipo_OTS.ToString();
                    lblOperacion.Text = OTSActivo[0].operacion.ToString();
                    lblDescripcion.Text = OTSActivo[0].descripcion.ToString();
                    lblStatus.Text = OTSActivo[0].sts_prog.ToString();
                    DateTime fecAsig = Convert.ToDateTime(OTSActivo[0].fec_asig.ToString());
                    DateTime fecActual = DateTime.Now;
                    TimeSpan ts = fecActual - fecAsig;
                    string dias = "0";
                    string horas = "0";
                    string minutos = "0";

                    if (ts.Days >= 1)
                    {
                        if (ts.Days < 10)
                        {
                            dias = "0" + ts.Days.ToString();
                        }
                        else
                        {
                            dias = ts.Days.ToString();
                        }
                    }
                    if (ts.Hours >= 1)
                    {
                        if (ts.Hours < 10)
                        {
                            horas = "0" + ts.Hours.ToString();
                        }
                        else
                        {
                            horas = ts.Hours.ToString();
                        }
                    }
                    if (ts.Minutes >= 1)
                    {
                        if (ts.Minutes < 10)
                        {
                            minutos = "0" + ts.Minutes.ToString();
                        }
                        else
                        {
                            minutos = ts.Minutes.ToString();
                        }
                    }

                    lblFechaIni.Text = dias + " Dias.," + horas+" Horas.," + minutos + " Min.";
                    if (lblStatus.Text.Equals("Iniciada") == true)
                    {
                        btnContinuar.Visible = false;
                    }
                    else if (lblStatus.Text.Equals("Pausa") == true)
                    {
                        btnTerminar.Visible = false;
                        btnParo.Visible = false;
                    }
                    Session["flotante"] = "";
                }
                else
                {
                    Session["flotante"] = "style = 'display:none'";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void CerrarSession(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
        public string validarRol(string cve_user,string rol)
        {
            return logicaNegocio.validarRol(cve_user,rol);
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            short? error;
            string mensaje;
            int num_OTS = Int32.Parse(lblNum_OTS.Text);
            int num_SubOTS = Int32.Parse(lblReng_OTS.Text);
            string tipo_SubOTS = lblTip_OTS.Text.Substring(0,3).ToUpper();
            string userRespSubOTS = Session["user_cve"].ToString().ToUpper();
            string sts_SubOTS = "3";

            AccesoDatos.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", tipo_SubOTS, "", num_OTS, num_SubOTS.ToString(), "", userRespSubOTS, sts_SubOTS, "", "", "", "", "", "", "", "", "", "", "terminaDet", "", DateTime.Now);
            if (insertOTSD != null)
            {
                error = insertOTSD.error;
                mensaje = insertOTSD.mensaje;
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('SubOTS Finalizado'); window.location.href = window.location.href;</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = window.location.href;</script>");
                }
            }
        }

        protected void btnParo_Click(object sender, EventArgs e)
        {
            variables.Sts_OTS = lblStatus.Text;
            variables.Num_OTS = Int32.Parse(lblNum_OTS.Text);
            variables.Tipo_OTS = lblTip_OTS.Text;
            variables.Num_rengOTS = Int32.Parse(lblReng_OTS.Text);
            
            Response.Redirect("A_Paros.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            variables.Sts_OTS = lblStatus.Text;
            variables.Num_OTS = Int32.Parse(lblNum_OTS.Text);
            variables.Tipo_OTS = lblTip_OTS.Text;
            variables.Num_rengOTS = Int32.Parse(lblReng_OTS.Text);

            Response.Redirect("A_Paros.aspx");
        }
    }
}