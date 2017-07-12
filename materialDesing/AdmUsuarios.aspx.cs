using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class AdmUsuarios : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUser_cve.Text = variables.M_User_cve;
                txtNombre.Text = variables.M_Nombre;
                txtEmail.Text = variables.M_Email;
                
                rol_cve.DataSource = logicaNegocio.ListadoRolesOTS();
                rol_cve.DataTextField = "nombre";
                rol_cve.DataValueField = "elm_cve";
                rol_cve.DataBind();

                string[] roles = variables.M_Roles.Split(',');
                for (int i = 0; i < rol_cve.Items.Count; i++)
                {
                    for (int y = 0; y < roles.Length; y++)
                    {
                        if (rol_cve.Items[i].Value.Equals(roles[y].TrimEnd(' ').TrimStart(' ')) == true)
                        {
                            rol_cve.Items[i].Selected = true;
                        }
                    }
                } 
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string user_cve = txtUser_cve.Text;
            short? status = short.Parse(Request["status"]);
            string email = txtEmail.Text;
            if (string.IsNullOrEmpty(nombre.TrimEnd(' ')) == false && string.IsNullOrEmpty(user_cve.TrimEnd(' ')) == false && string.IsNullOrEmpty(email.TrimEnd(' ')) == false)
            {
                Entidades.sp_WebAppOTSAdmUsers_Result actualizaUser = logicaNegocio.admUserOTS(user_cve, nombre, "", status, email, "", "actualiza");
                if (actualizaUser != null)
                {
                    error = actualizaUser.error;
                    mensaje = actualizaUser.mensaje;
                    //si no se regreso ningun error
                    if (Convert.ToInt32(error) == 0)
                    {
                        int rols = 0;
                        foreach (ListItem item in rol_cve.Items)
                        {
                            if (item.Selected)
                            {
                                rols++;
                            }
                        }
                        if (rols>0)
                        {
                            foreach (ListItem item in rol_cve.Items)
                            {
                                if (item.Selected)
                                {
                                    Entidades.sp_WebAppOTSAdmUsers_Result RolUser = logicaNegocio.admUserOTS(user_cve.ToUpper(), "", "", 0, "", item.Value, "nRol");
                                    if (RolUser != null)
                                    {
                                        error = RolUser.error;
                                        mensaje = RolUser.mensaje;
                                    }
                                }
                                else if (item.Selected == false)
                                {
                                    Entidades.sp_WebAppOTSAdmUsers_Result RolUser = logicaNegocio.admUserOTS(user_cve.ToUpper(), "", "", 0, "", item.Value, "eRol");
                                    if (RolUser != null)
                                    {
                                        error = RolUser.error;
                                        mensaje = RolUser.mensaje;
                                    }
                                }
                            }
                            Response.Write("<script type=\"text/javascript\">alert('El Usuario: " + nombre.ToUpper() + " \\nHa Sido Modificado.');  window.location.href = 'C_Usuarios.aspx';</script>");
                        }
                        else
                        {
                            Response.Write("<script type=\"text/javascript\">alert('Seleccione al menos 1 rol para el usuario \\nIntente De Nuevo.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_Usuarios.aspx';</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script type=\"text/javascript\">alert('No puedes dejar campos vacios, ni espacios en blanco'); window.location.href = 'AdmUsuarios.aspx';</script>");
            }
        }
    }
}