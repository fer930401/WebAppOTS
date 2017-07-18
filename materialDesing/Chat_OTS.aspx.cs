using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class Chat_OTS1 : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                string user = Session["nombre"].ToString();
                string clave = Session["user_cve"].ToString().ToUpper();
                string email = logicaNegocio.consultaEmail(clave);
                lblUsuario.Text = user.Substring(0, user.IndexOf(' '));
                lblUsuarioOTS.Text = user.Substring(0, user.IndexOf(' '));
                txtUser_cve.Text = clave;
                txtUser.Text = user;
                txtEmail.Text = email;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        public string validarRol(string cve_user, string rol)
        {
            return logicaNegocio.validarRol(cve_user, rol);
        }
        protected void CerrarSession(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}