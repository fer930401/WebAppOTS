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
                dt.Columns.Add("Rol", typeof(string));

                if (listaUsuarios != null)
                {
                    int i = 0;
                    foreach (var elemento in listaUsuarios)
                    {
                        dr = dt.NewRow();
                        dr["Clave"] = elemento.user_cve.TrimEnd(' ');
                        dr["Nombre"] = elemento.nombre.TrimEnd(' ');
                        dr["Email"] = elemento.mail.TrimEnd(' ');
                        dr["Status"] = elemento.status_usr.TrimEnd(' ');
                        dr["Rol"] = elemento.cve_rol.TrimEnd(' ').Remove(elemento.cve_rol.TrimEnd(' ').Length - 1);
                        dt.Rows.Add(dr);
                        i++;
                    }
                }
                dt.PrimaryKey = new DataColumn[] { dt.Columns["Clave"] };
                ViewState["dt"] = dt;
                this.BindGrid();
            }
        }
        private void llenaGrid()
        {
            this.GridView1.DataSource = logicaNegocio.ListadoUsuarios();
            this.GridView1.DataBind();
        }
        protected void BindGrid()
        {
            //GridView1.Columns[5].Visible = false;
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();
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
            variables.M_User_cve = GridView1.Rows[e.NewEditIndex].Cells[0].Text;
            variables.M_Nombre = GridView1.Rows[e.NewEditIndex].Cells[1].Text;
            variables.M_Email = GridView1.Rows[e.NewEditIndex].Cells[2].Text;
            if (GridView1.Rows[e.NewEditIndex].Cells[3].Text.Equals("Activo") == true)
            {
                variables.M_Status = "1";
            }
            if (GridView1.Rows[e.NewEditIndex].Cells[3].Text.Equals("Desactivado") == true)
            {
                variables.M_Status = "0";
            }
            variables.M_Roles = GridView1.Rows[e.NewEditIndex].Cells[4].Text;
            Response.Redirect("AdmUsuarios.aspx");
        }
    }
}