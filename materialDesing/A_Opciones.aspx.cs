using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class A_Opciones : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                if (!IsPostBack)
                {
                    cmbOpciones.DataSource = logicaNegocio.opcionesCatalogo();
                    cmbOpciones.DataTextField = "nom_catlgo";
                    cmbOpciones.DataValueField = "cve_catlgo";
                    cmbOpciones.DataBind();
                    cmbOpciones.Items.Insert(0, new ListItem("----Selecciona opcion----", null));
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnGuardarOpc_Click(object sender, EventArgs e)
        {

        }
    }
}