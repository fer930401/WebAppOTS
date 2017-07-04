using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class AdmOpciones : System.Web.UI.Page
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
                    string clave = Session["user_cve"].ToString();
                    string nombre = Session["nombre"].ToString();
                    user_cve.Text = clave;
                    usuario.Text = nombre;
                    variables.M_StatusOpc = "2";
                    Literal1.Text = "style='display:none'";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnBuscaClave_Click(object sender, EventArgs e)
        {
            
            string elmto_cve = elm_cve.Text;
            if (string.IsNullOrEmpty(elmto_cve.TrimEnd(' ')) == false)
            {
                List<AccesoDatos.otscatlgos> catOTS = logicaNegocio.infoOpc(elmto_cve);
                if (catOTS != null && catOTS.Count > 0)
                {
                    Literal1.Text = "";
                    foreach (var elemento in catOTS)
                    {
                        variables.M_StatusOpc = elemento.status.ToString();
                        nom_catlgo.Text = elemento.nombre;
                        claveCat.Text = elemento.cve_catlgo;
                    }
                }
                else
                {
                    Literal1.Text = "style='display:none'";
                }
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('No puedes buscar campos vacios, ni espacios en blanco'); window.location.href = 'AdmOpciones.aspx';</script>");
            }
            
        }

        protected void btnGuardarOpc_Click(object sender, EventArgs e)
        {
            string newCve_catlgo = claveCat.Text;
            string newElm_cve = elm_cve.Text;
            string newNombre = nom_catlgo.Text;
            short? newStatus = short.Parse(Request["status"].ToString());
            string opcion = "modifica";
            AccesoDatos.sp_WebAppOTSAdmOpc_Result insOpcOTS = logicaNegocio.opcOTS(newCve_catlgo, newElm_cve, newNombre, newStatus, opcion);
            if (insOpcOTS != null)
            {
                error = insOpcOTS.error;
                mensaje = insOpcOTS.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
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