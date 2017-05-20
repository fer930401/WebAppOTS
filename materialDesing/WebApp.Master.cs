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
                String user = Session["nombre"].ToString();
                String clave = Session["user_cve"].ToString().ToUpper();
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
    }
}