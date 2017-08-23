using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using LogicaNegocio;

namespace materialDesing
{
    public partial class materialDesing1 : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                DateTime fechaActual = DateTime.Now;
                DateTime fechaDefault = DateTime.MinValue;
                fechaDefault = fechaActual.AddDays(1 - Convert.ToDouble(fechaActual.DayOfWeek));
                DateTime fechaInicioSemana = fechaDefault.Date;
                DateTime fechaFinSemana = fechaInicioSemana.AddDays(6);

                if (!IsPostBack)
                {
                   

                    if (string.IsNullOrEmpty(variables.Fec_ini) == false)
                    {
                        fec_iniI.Text = variables.Fec_ini;
                        fec_finI.Text = variables.Fec_fin;
                        fec_ini.Text = variables.Fec_ini;
                        fec_fin.Text = variables.Fec_fin;
                    }
                    else
                    {
                        fec_iniI.Text = fechaInicioSemana.ToString();
                        fec_finI.Text = fechaFinSemana.ToString();
                        /*fec_ini.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        fec_fin.Text = DateTime.Now.ToString("yyyy-MM-dd");*/
                    }
                    

                    OTS(Session["user_cve"].ToString().ToUpper(), "consultaOTS");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
           
        }
        public void OTS(string user, string opcion)
        {
            int sopNoIniciados = 0;
            int sopTerminados = 0;
            int penNoIniciados = 0;
            int penTerminados = 0;
            DateTime fechaInicial = DateTime.Parse(fec_iniI.Text);
            DateTime fechaFinal = DateTime.Parse(fec_finI.Text);
            Entidades.sp_WebAppOTSAdmOTS_Result contadorSop = logicaNegocio.admOTS("", "SOP", "", 1, "", "", user, "", "", fechaInicial.ToString("MM/dd/yyyy"), fechaFinal.ToString("MM/dd/yyyy"), "", "", "", "", "", "", "", opcion, "", DateTime.Now); 
            if (contadorSop != null)
            {
                error = contadorSop.error;
                mensaje = contadorSop.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    sopNoIniciados = Int32.Parse(mensaje);
                    soportesNoIniciados.Text = sopNoIniciados.ToString();
                }
            }

            Entidades.sp_WebAppOTSAdmOTS_Result contadorSopT = logicaNegocio.admOTS("", "SOP", "", 3, "", "", user, "", "", fechaInicial.ToString("MM/dd/yyyy"), fechaFinal.ToString("MM/dd/yyyy"), "", "", "", "", "", "", "", opcion, "", DateTime.Now); ;
            if (contadorSopT != null)
            {
                error = contadorSopT.error;
                mensaje = contadorSopT.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    sopTerminados = Int32.Parse(mensaje);
                    soportesTerminados.Text = sopTerminados.ToString();
                }
            }
            Entidades.sp_WebAppOTSAdmOTS_Result contadorPen = logicaNegocio.admOTS("", "PEN", "", 1, "", "", user, "", "", fechaInicial.ToString("MM/dd/yyyy"), fechaFinal.ToString("MM/dd/yyyy"), "", "", "", "", "", "", "", opcion, "", DateTime.Now);
            if (contadorPen != null)
            {
                error = contadorPen.error;
                mensaje = contadorPen.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    penNoIniciados = Int32.Parse(mensaje);
                    pendientesNoIniciados.Text = penNoIniciados.ToString();
                }
            }
            Entidades.sp_WebAppOTSAdmOTS_Result contadorPenT = logicaNegocio.admOTS("", "PEN", "", 3, "", "", user, "", "", fechaInicial.ToString("MM/dd/yyyy"), fechaFinal.ToString("MM/dd/yyyy"), "", "", "", "", "", "", "", opcion, "", DateTime.Now);
            if (contadorPenT != null)
            {
                error = contadorPenT.error;
                mensaje = contadorPenT.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    penTerminados = Int32.Parse(mensaje);
                    pendientesTerminados.Text = penTerminados.ToString();
                }
            }
        }
        public void OTSGraf(string tipoOTS, int statusOTS, string userResp, DateTime fechaInicio, DateTime fechaFin)
        {
            Entidades.sp_WebAppOTSAdmOTS_Result contadorPenT = logicaNegocio.admOTS("", tipoOTS, "", statusOTS, "", "", userResp, "", "", fechaInicio.ToString("MM/dd/yyyy"), fechaFin.ToString("MM/dd/yyyy"), "", "", "", "", "", "", "", "consultaOTSGraf", "", DateTime.Now);
            Response.Write(contadorPenT.mensaje);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime fecInicial = DateTime.Parse(fec_ini.Text);
            DateTime fecFinal = DateTime.Parse(fec_fin.Text);
            if (fecInicial < DateTime.Now)
            {
                if (fecFinal < DateTime.Now)
                {
                    variables.Fec_ini = fecInicial.ToString("yyyy-MM-dd");
                    variables.Fec_fin = fecFinal.ToString("yyyy-MM-dd");
                    Response.Write("<script type=\"text/javascript\">window.location.href = 'Inicio.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('La fecha final no es valida, seleccione una fecha menor');  window.location.href = 'Inicio.aspx';</script>");
                }
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('La fecha inicial no es valida, seleccione una fecha menor');  window.location.href = 'Inicio.aspx';</script>");
            }
        }
    }
}