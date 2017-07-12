using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatos;
using System.Security.Cryptography;
using System.Text;

namespace materialDesing
{
    public partial class A_Usuarios : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                roles.DataSource = logicaNegocio.ListadoRolesOTS();
                roles.DataTextField = "nombre";
                roles.DataValueField = "elm_cve";
                roles.DataBind();
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            string user_cve = Request["cu"];
            string user_nom = Request["nu"] + " " + Request["pu"] + " " + Request["mu"];
            string user_pass = GetSHA1HashData(Request["co"]);
            short? user_status = short.Parse(Request["status"]);
            string user_email = Request["eu"];
            string user_rol = roles.SelectedValue;
            if (string.IsNullOrEmpty(user_cve.TrimEnd(' ')) == false && string.IsNullOrEmpty(user_nom.TrimEnd(' ')) == false && string.IsNullOrEmpty(user_pass.TrimEnd(' ')) == false)
            {
                Entidades.sp_WebAppOTSAdmUsers_Result insertaUser = logicaNegocio.admUserOTS(user_cve, user_nom, user_pass, user_status, user_email, user_rol, "alta");
                if (insertaUser != null)
                {
                    error = insertaUser.error;
                    mensaje = insertaUser.mensaje;
                    if (Convert.ToInt32(error) == 0)
                    {
                        Response.Write("<script type=\"text/javascript\">alert('El Usuario: " + user_nom.ToUpper() + " \\nHa Sido Registrado.');  window.location.href = 'A_Usuarios.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Usuarios.aspx';</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('No puedes dejar campos vacios, ni espacios en blanco'); window.location.href = 'A_Usuarios.aspx';</script>");
            }
        }
        private string GetSHA1HashData(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }
    }
}