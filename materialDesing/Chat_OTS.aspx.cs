using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class Chat_OTS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = Session["nombre"].ToString();
            string clave = Session["user_cve"].ToString().ToUpper();
            lblUsuario.Text = user.Substring(0, user.IndexOf(' '));
            txtUser_cve.Text = clave;
            txtUser.Text = user;
        }
    }
}