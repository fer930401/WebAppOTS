using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class CambiaContraseña : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(variables.Email) == false)
                {
                    txtEmail.Text = variables.Email;
                    txtEmailConfirm.Text = variables.EmailConfirm;
                }
            }         
        }

        
        protected void CerrarSession(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void btnCambiarPass_Click(object sender, EventArgs e)
        {
            variables.Email = txtEmail.Text;
            variables.EmailConfirm = txtEmailConfirm.Text;
            variables.Pass = GetSHA1HashData(txtPass.Text);

            if (variables.Email.Equals(variables.EmailConfirm) == true)
            {
                try
                {
                    Entidades.sp_WebAppOTSAdmUsers_Result cambioPass = logicaNegocio.admUserOTS("", "", variables.Pass, 0, variables.Email, "", "actPass");
                    if (cambioPass != null)
                    {
                        error = cambioPass.error;
                        mensaje = cambioPass.mensaje;
                    }
                    if(error == 0)
                    {
                        variables.Email = "";
                        variables.EmailConfirm = "";
                        variables.Pass = "";
                        Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "'); window.location.href = 'Login.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "'); window.location.href = 'CambiaContraseña.aspx';</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script type=\"text/javascript\">alert('Ocurrio un error, favor de revisar'); window.location.href = 'CambiaContraseña.aspx';</script>");
                }               
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('Los emails ingresados no coinciden, favor de revisar'); window.location.href = 'CambiaContraseña.aspx';</script>");
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