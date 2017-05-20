using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using System.Security.Cryptography;
using System.Text;

namespace materialDesing
{
    public partial class Login : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //llena el combo con los usuarios activos 
                llenaUsuarios();
            }
        }
        //metodo que ejecuta el procedimiento almacenado sp_WebAppOTSConsultaUser para obtener el listado de los usuarios activos
        protected void llenaUsuarios()
        {
            List<AccesoDatos.sp_WebAppOTSConsultaUser_Result> usuario = logicaNegocio.ListadoProgramadores(1,"");
            //el resultado de la ejecucion la asignamos al combobox 
            claveUser.DataSource = usuario;
            claveUser.DataTextField = "nombre";
            claveUser.DataValueField = "user_cve";
            claveUser.DataBind();
            //agregamos una opcion estatica al comienzo del listado
            claveUser.Items.Insert(0, new ListItem("------Selecciona un usuario------", "NA"));
        }
        //metodo utilizado para validar los datos ingresados por el usuario
        protected void ValidateUser(object sender, EventArgs e)
        {
            //recibo la clave del usuario seleccionado en el combobox
            String usuario = claveUser.SelectedValue;
            //recibo la contraseña ingresada
            string pass = Request["password"];
            //envio la contraseña al metodo encargado de encriptar la cadena de texto y la asigno a una variable
            String pw = GetSHA1HashData(pass);
            //envio los parametros para ejecutar el metodo que consulta los datos ingresados
            var res = logicaNegocio.LoginOTS(usuario, pw);
            String nomUser;
            //si el resultado es null envio los siguientes mensajes en los labels del diseño
            if (res == null)
            {
                nomUser = " El Usuario/Contraseña no se encontraron";
                Label1.Text = "<div class='chip red darken-4 white-text'> " + nomUser + ", Intente de nuevo<i class='material-icons'>close</i></div>";
            }else
            {
                //el resultado de la ejecucion del sp se lo asigno a la variable nombre
                nomUser = res;
                //creo las variables de session con los datos ingresados
                Session["nombre"] = nomUser;
                Session["user_cve"] = usuario;
                Response.Redirect("Inicio.aspx");
            }
        }
        private string GetSHA1HashData(string data)
        {
            //instancio la clase que utiliza o implementa md5
            SHA1 sha1 = SHA1.Create();

            //convierto la cadena de texto en un arreglo de letras
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

            //instancio una clase encargada de generar strings
            StringBuilder returnValue = new StringBuilder();

            //creo un ciclo en el cual encripto cada letra de la contraseña
            for (int i = 0; i < hashData.Length; i++)
            {
                //concateno el resultado de la encriptacion de cada letra
                returnValue.Append(hashData[i].ToString());
            }

            //regreso el valor
            return returnValue.ToString();
        }

        protected void password_TextChanged(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowStatus", "capLock(event);", true);
        }
    }
}