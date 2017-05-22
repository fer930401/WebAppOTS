using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class C_Usuarios : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                DataRow dr;
                List<AccesoDatos.sp_WebAppOTSConsultaUser_Result> listaUsuarios = logicaNegocio.ListadoProgramadores(1, "USR");
                dt.Columns.Add("Clave", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Status", typeof(string));

                if (listaUsuarios != null)
                {
                    int i = 0;
                    foreach (var elemento in listaUsuarios)
                    {
                        dr = dt.NewRow();
                        dr["Clave"] = elemento.user_cve;
                        dr["Nombre"] = elemento.nombre;
                        dr["Email"] = elemento.mail;
                        dr["Status"] = elemento.status_usr;
                        dt.Rows.Add(dr);
                        i++;
                    }
                }

                dt.PrimaryKey = new DataColumn[] { dt.Columns["Clave"] };
                ViewState["dt"] = dt;
                this.BindGrid();

                rol_cve.DataSource = logicaNegocio.ListadoRolesOTS();
                rol_cve.DataTextField = "nombre";
                rol_cve.DataValueField = "elm_cve";
                rol_cve.DataBind();
            }
        }
        private void llenaGrid()
        {
            this.GridView1.DataSource = logicaNegocio.ListadoUsuarios();
            this.GridView1.DataBind();
        }
        protected void BindGrid()
        {
            GridView1.Columns[4].Visible = false;
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this,this.GetType(), "Eliminar", "confirmar();", true);
        }
        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
            GridView1.Columns[4].Visible = true;
        }
        protected void OnUpdate(object sender, EventArgs e)
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            string user_cve = (row.Cells[0].Controls[0] as TextBox).Text;
            string nombre = (row.Cells[1].Controls[0] as TextBox).Text;
            string email = (row.Cells[2].Controls[0] as TextBox).Text;
            string status = (row.Cells[3].Controls[0] as TextBox).Text;
            short status2 = short.Parse((row.Cells[3].Controls[0] as TextBox).Text);
            DataTable dt = ViewState["dt"] as DataTable;
            dt.Rows[row.RowIndex]["Clave"] = user_cve;
            dt.Rows[row.RowIndex]["Nombre"] = nombre;
            dt.Rows[row.RowIndex]["Email"] = email;
            dt.Rows[row.RowIndex]["Status"] = status;
            ViewState["dt"] = dt;
            GridView1.EditIndex = -1;
            this.BindGrid();
            AccesoDatos.sp_WebAppOTSAdmUsers_Result actualizaUser = logicaNegocio.admUserOTS(user_cve, nombre, "", status2, email, "", "actualiza");
            if (actualizaUser != null)
            {
                error = actualizaUser.error;
                mensaje = actualizaUser.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('El Usuario: " + nombre.ToUpper() + " \\nHa Sido Modificado.');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
            }
        }
        protected void OnCancel(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnDelete(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            string nombre = (row.Cells[1].Controls[0] as TextBox).Text;
            string user_cve = GridView1.DataKeys[rowIndex].Values[0].ToString();
            AccesoDatos.sp_WebAppOTSAdmUsers_Result EliminaUser = logicaNegocio.admUserOTS(user_cve, nombre, "", 0, "", "", "elimina");
            if (EliminaUser != null)
            {
                error = EliminaUser.error;
                mensaje = EliminaUser.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('El Usuario: " + nombre.ToUpper() + " \\nHa Sido Eliminado.');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
            }
        }

        protected void Nrol_TextChanged(object sender, EventArgs e)
        {
            string user_cve = Ncve.Text;
            string rol = Nrol.Text;

            string script = string.Format("document.getElementById('TextBox4').value = '{0}';", mensaje);
            script += string.Format("document.getElementById('valor').focus();", mensaje);
            script += string.Format("document.getElementById('TextBox4').focus();", mensaje);
        }

        protected void btnRol_Click(object sender, EventArgs e)
        {
            Ncve.Text = cve_usr.Text;
            Nrol.Text = rol_cve.Text;
            string user_cve = Ncve.Text;
            string rol = Nrol.Text;
            AccesoDatos.sp_WebAppOTSAdmUsers_Result RolUser = logicaNegocio.admUserOTS(user_cve.ToUpper(), "", "", 0, "", rol, "nRol");
            if (RolUser != null)
            {
                error = RolUser.error;
                mensaje = RolUser.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Agrego El Nuevo Rol Al Usuario Con User_cve: " + user_cve.ToUpper() + "');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
            }
        }

        protected void btnERol_Click(object sender, EventArgs e)
        {
            Ncve.Text = cve_usr.Text;
            Nrol.Text = rol_cve.Text;
            string user_cve = Ncve.Text;
            string rol = Nrol.Text;
            AccesoDatos.sp_WebAppOTSAdmUsers_Result RolUser = logicaNegocio.admUserOTS(user_cve.ToUpper(), "", "", 0, "", rol, "eRol");
            if (RolUser != null)
            {
                error = RolUser.error;
                mensaje = RolUser.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('El Rol Del Usuario Con User_cve: " + user_cve.ToUpper() + " A Sido Eliminado');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_Usuarios.aspx';</script>");
                }
            }
        }
    }
}