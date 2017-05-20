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
                if (!IsPostBack)
                {
                    OTS(Session["user_cve"].ToString().ToUpper(), "consultaOTS");
                    rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG");
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
            AccesoDatos.sp_WebAppOTSAdmOTS_Result contadorSop = logicaNegocio.admOTS("", "SOP", "", 1, "", "", user, "1", "", "", "", "", "", "", "", "", "", "", opcion, "", DateTime.Now); 
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

            AccesoDatos.sp_WebAppOTSAdmOTS_Result contadorSopT = logicaNegocio.admOTS("", "SOP", "", 3, "", "", user, "3", "", "", "", "", "", "", "", "", "", "", opcion, "", DateTime.Now); ;
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
            AccesoDatos.sp_WebAppOTSAdmOTS_Result contadorPen = logicaNegocio.admOTS("", "PEN", "", 1, "", "", user, "1", "", "", "", "", "", "", "", "", "", "", opcion, "", DateTime.Now);
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
            AccesoDatos.sp_WebAppOTSAdmOTS_Result contadorPenT = logicaNegocio.admOTS("", "PEN", "", 3, "", "", user, "3", "", "", "", "", "", "", "", "", "", "", opcion, "", DateTime.Now);
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
        public void OTSGraf(string tipoOTS, int statusOTS, string userResp, string mes)
        {
            LogicaNegocio.LogicaNegocioCls logicaNegocio = new LogicaNegocio.LogicaNegocioCls();
            /*string fecha = "";
            fecha = DateTime.Now.ToString("yyyy") + "-" + mes;*/        //Se elimina por cambio de grafica 

            AccesoDatos.sp_WebAppOTSAdmOTS_Result contadorPenT = logicaNegocio.admOTS("", tipoOTS, "", statusOTS, "", "", userResp, "", "", /*fecha*/"", "", "", "", "", "", "", "", "", "consultaOTSGraf", "", DateTime.Now);
            Response.Write(contadorPenT.mensaje);
        }
    }
}