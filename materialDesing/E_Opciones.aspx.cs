using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class E_Opciones : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                claveCat.Text = logicaNegocio.BuscaClave(variables.Cve_catlgo);
                claveOpc.Text = variables.Elm_cve;
                nom_catlgo.Text = variables.Nombre_cve;
            }
        }
        protected void btnGuardarOpc_Click(object sender, EventArgs e)
        {
            string newCve_catlgo = variables.Cve_catlgo;
            string newElm_cve = variables.Elm_cve;
            string newNombre = nom_catlgo.Text;
            short? newStatus = short.Parse(Request["status"].ToString());
            string opcion = "modifica";
            Entidades.sp_WebAppOTSAdmOpc_Result insOpcOTS = logicaNegocio.opcOTS(newCve_catlgo, newElm_cve, newNombre, newStatus, opcion);
            if (insOpcOTS != null)
            {
                error = insOpcOTS.error;
                mensaje = insOpcOTS.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    variables.Cve_catlgo = "";
                    variables.Elm_cve = "";
                    variables.Nombre_cve = "";
                    variables.Status_cve = "";
                    Response.Write("<script type=\"text/javascript\">alert('La nueva opcion a sido modificada.'); window.location.href = 'AdmOpciones.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente mas tarde.');  window.location.href = 'AdmOpciones.aspx';</script>");
                }
            }
        }
    }
}