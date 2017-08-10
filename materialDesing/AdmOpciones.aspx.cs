using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class AdmOpciones : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio1 = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
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
                    cmbOpciones.DataSource = logicaNegocio1.opcionesCatalogo();
                    cmbOpciones.DataTextField = "nom_catlgo";
                    cmbOpciones.DataValueField = "cve_catlgo";
                    cmbOpciones.DataBind();
                    cmbOpciones.Items.Insert(0, new ListItem("----Selecciona opcion----", null));
                    if (string.IsNullOrEmpty(variables.Cve_catlgo) == false)
                    {
                        cmbOpciones.SelectedValue = variables.Cve_catlgo;
                        llenaGrid();
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        

        protected void cmbOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            variables.Cve_catlgo = cmbOpciones.SelectedValue;
            llenaGrid();
        }

        private void llenaGrid()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            List<Entidades.otscatlgos> listaOpciones = logicaNegocio1.ListaClaves(variables.Cve_catlgo);
            dt.Columns.Add("elm_cve", typeof(string));
            dt.Columns.Add("nombre", typeof(string));
            dt.Columns.Add("status", typeof(string));
            if (listaOpciones != null)
            {
                int i = 0;
                foreach (var elemento in listaOpciones)
                {
                    dr = dt.NewRow();
                    dr["elm_cve"] = elemento.elm_cve.TrimEnd(' ');
                    dr["nombre"] = elemento.nombre.TrimEnd(' ');
                    if (elemento.status.Value.ToString().Equals("1") == true)
                    {
                        dr["status"] = "Activo";
                    }
                    else
                    {
                        dr["status"] = "Desactivado";
                    }
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            dt.PrimaryKey = new DataColumn[] { dt.Columns["Clave"] };
            ViewState["dt"] = dt;
            BindGrid();
        }

        protected void BindGrid()
        {
            //GridView1.Columns[5].Visible = false;
            gvOpciones.DataSource = ViewState["dt"] as DataTable;
            gvOpciones.DataBind();
        }

        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvOpciones, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            variables.Cve_catlgo = cmbOpciones.SelectedValue;
            variables.Elm_cve = gvOpciones.Rows[e.NewEditIndex].Cells[0].Text;
            variables.Nombre_cve = gvOpciones.Rows[e.NewEditIndex].Cells[1].Text;
            if (gvOpciones.Rows[e.NewEditIndex].Cells[2].Text.Equals("Activo") == true)
            {
                variables.Status_cve = "1";
            }
            if (gvOpciones.Rows[e.NewEditIndex].Cells[2].Text.Equals("Desactivado") == true)
            {
                variables.Status_cve = "0";
            }
            Response.Redirect("E_Opciones.aspx");
        }
        
    }
}