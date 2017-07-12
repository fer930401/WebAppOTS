using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class Reportes : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                String user = Session["nombre"].ToString();
                String clave = Session["user_cve"].ToString();
                Label1.Text = user;
                user_cve.Text = clave;
                OTS(Session["user_cve"].ToString().ToUpper(), "consultaOTS");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        public void OTS(string user, string opcion)
        {
            int sopTerminados = 0;
            int penTerminados = 0;
            Entidades.sp_WebAppOTSAdmOTS_Result contadorSopT = logicaNegocio.admOTS("", "SOP", "", 3, "", "", user, "3", "", "", "", "", "", "", "", "", "", "", opcion, "", DateTime.Now); 
            if (contadorSopT != null)
            {
                error = contadorSopT.error;
                mensaje = contadorSopT.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    sopTerminados = Int32.Parse(mensaje);
                    soportesT.Text = sopTerminados.ToString();
                }
            }

            Entidades.sp_WebAppOTSAdmOTS_Result contadorPenT = logicaNegocio.admOTS("", "PEN", "", 3, "", "", user, "3", "", "", "", "", "", "", "", "", "", "", opcion, "", DateTime.Now); 
            if (contadorPenT != null)
            {
                error = contadorPenT.error;
                mensaje = contadorPenT.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    penTerminados = Int32.Parse(mensaje);
                    pendientesT.Text = penTerminados.ToString();
                }
            }
        }
    }
}