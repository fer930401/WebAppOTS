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
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}