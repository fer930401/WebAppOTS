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
    public partial class valPas : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request["token"].ToString();
            string email = token.Substring(0, token.IndexOf('@'));
            string pass = token.Substring(token.IndexOf('@') + 1);
            //Response.Write(pass);
            //Response.Write(Base64Decode(pass));

            variables.Email = Base64Decode(email);
            variables.Pass = GetSHA1HashData(Base64Decode(pass));
            try
            {
                Entidades.sp_WebAppOTSAdmUsers_Result cambioPass = logicaNegocio.admUserOTS("", "", variables.Pass, 0, variables.Email, "", "actPass");
                if (cambioPass != null)
                {
                    error = cambioPass.error;
                    mensaje = cambioPass.mensaje;
                }
                if (error == 0)
                {
                    variables.Email = "";
                    variables.EmailConfirm = "";
                    variables.Pass = "";
                    Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "'); window.location.href = 'Login.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Ocurrio un error, intente mas tarde'); window.location.href = 'Login.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script type=\"text/javascript\">alert('Ocurrio un error, favor de revisar'); window.location.href = 'Login.aspx';</script>");
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
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