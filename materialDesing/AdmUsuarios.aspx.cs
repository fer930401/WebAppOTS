﻿using LogicaNegocio;
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

                /*roles.DataSource = logicaNegocio.ListadoRolesOTS();
                roles.DataTextField = "nombre";
                roles.DataValueField = "elm_cve";
                roles.DataBind();*/

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
            //string roles  = "";
            //foreach (ListItem item in rol_cve.Items)
            //{
            //    if (item.Selected)
            //    {
            //        roles += item.Value + ", ";
            //    }
            //}
            //roles.Remove(roles.Length - 1);
            AccesoDatos.sp_WebAppOTSAdmUsers_Result actualizaUser = logicaNegocio.admUserOTS(user_cve, nombre, "", status, email, "", "actualiza");
            if (actualizaUser != null)
            {
                error = actualizaUser.error;
                mensaje = actualizaUser.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    foreach (ListItem item in rol_cve.Items)
                    {
                        if (item.Selected)
                        {
                            AccesoDatos.sp_WebAppOTSAdmUsers_Result RolUser = logicaNegocio.admUserOTS(user_cve.ToUpper(), "", "", 0, "", item.Value, "nRol");
                            if (RolUser != null)
                            {
                                error = RolUser.error;
                                mensaje = RolUser.mensaje;
                                //si no se regreso ningun error
                                /*if (Convert.ToInt32(error) == 0)
                                {
                                    Response.Write("<script type=\"text/javascript\">alert('Se Agrego El Nuevo Rol Al Usuario Con User_cve: " + user_cve.ToUpper() + "');  window.location.href = 'C_Usuarios.aspx';</script>");
                                }
                                else
                                {
                                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_Usuarios.aspx';</script>");
                                }*/
                            }
                        }
                    }
                    variables.M_User_cve = null;
                    variables.M_Nombre = null;
                    variables.M_Email = null;
                    variables.M_Status = null;
                    variables.M_Roles = null;
                    Response.Write("<script type=\"text/javascript\">alert('El Usuario: " + nombre.ToUpper() + " \\nHa Sido Modificado.');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
            }
        }
    }
}