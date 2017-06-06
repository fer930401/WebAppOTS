﻿using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class C_Soportes : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string usuarioC = "";
        string statusC = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                string clave = Session["user_cve"].ToString();
                user_cve.Text = clave;
                if (logicaNegocio.validarRol(clave.ToUpper(), "PRG") != null)
                {
                    usuarioC = clave.ToUpper();
                    statusC = "1";
                    rol_cve.Text = logicaNegocio.validarRol(clave, "PRG");
                    cmbProgramador.Visible = false;
                    lblRespon.Visible = false;
                    GridView1.Columns[2].Visible = false;
                    Session["visibleReasigna"] = "style = 'display:none'";
                }
                else
                {
                    usuarioC = "ASG";
                    statusC = "1";
                    rol_cve.Text = logicaNegocio.validarRol(clave.ToUpper(), "ASG");
                    cmbProgramador.Visible = true;
                    Session["visibleAgregar"] = "style = 'display:none'";
                }
                if (!IsPostBack)
                {
                    cmbProgramador.Items.Clear();
                    cmbProgramador.Items.Insert(0, new ListItem("Selecciona una opción", ""));
                    cmbProgramador.SelectedIndex = 0;
                    cmbProgramador.AppendDataBoundItems = true;
                    cmbProgramador.DataSource = logicaNegocio.ListadoProgramadores(1, "PRG");
                    cmbProgramador.DataValueField = "user_cve";
                    cmbProgramador.DataTextField = "nombre";
                    cmbProgramador.AutoPostBack = true;
                    cmbProgramador.DataBind();
                    
                    DataTable dt = new DataTable();
                    DataRow dr;
                    List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 0, "SOP", "","");
                    dt.Columns.Add("num_OTS", typeof(string));
                    dt.Columns.Add("tipo_OTS", typeof(string));
                    dt.Columns.Add("userResp", typeof(string));
                    dt.Columns.Add("fec_asig", typeof(string));
                    dt.Columns.Add("fec_fin", typeof(string));
                    dt.Columns.Add("sts_prog", typeof(string));
                    dt.Columns.Add("aplica", typeof(string));
                    dt.Columns.Add("fec_prom", typeof(string));
                    dt.Columns.Add("descripcion", typeof(string));

                    if (listaOTS != null)
                    {
                        int i = 0;
                        foreach (var elemento in listaOTS)
                        {
                            dr = dt.NewRow();
                            dr["num_OTS"] = elemento.num_OTS;
                            dr["tipo_OTS"] = elemento.tipo_OTS;
                            dr["userResp"] = elemento.userResp;
                            dr["fec_asig"] = elemento.fec_asig;
                            dr["fec_fin"] = elemento.fec_fin;
                            dr["sts_prog"] = elemento.sts_prog;
                            dr["aplica"] = entiFinancieras(elemento.aplica);
                            dr["fec_prom"] = elemento.fec_prom;
                            dr["descripcion"] = elemento.descripcion;
                            dt.Rows.Add(dr);
                            i++;
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('btnReasignar').style.visibility = 'hidden';", true);
                        }
                    }
                    
                    ViewState["dt"] = dt;
                    this.BindGrid();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();
        }

        protected void cmbProgramador_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user_filtro = cmbProgramador.SelectedValue;
            DataTable dt = new DataTable();
            DataRow dr;
            List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS;
            if (user_filtro.Equals("") == false)
            {
                variables.User_filtro = user_filtro;
                if (string.IsNullOrEmpty(variables.Descrip_filtro) == true)
                {
                    listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 1, "SOP", user_filtro, "");
                }
                else
                {
                    listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 3, "SOP", user_filtro, variables.Descrip_filtro + "%");
                }
                
            }else
            {
                if (string.IsNullOrEmpty(variables.Descrip_filtro) == true)
                {
                    listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 0, "SOP", "","");
                }
                else
                {
                    listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 2, "SOP", "", variables.Descrip_filtro + "%");
                }
                
            }
                dt.Columns.Add("num_OTS", typeof(string));
                dt.Columns.Add("tipo_OTS", typeof(string));
                dt.Columns.Add("userResp", typeof(string));
                dt.Columns.Add("fec_asig", typeof(string));
                dt.Columns.Add("fec_fin", typeof(string));
                dt.Columns.Add("sts_prog", typeof(string));
                dt.Columns.Add("aplica", typeof(string));
                dt.Columns.Add("fec_prom", typeof(string));
                dt.Columns.Add("descripcion", typeof(string));

                if (listaOTS != null)
                {
                    int i = 0;
                    foreach (var elemento in listaOTS)
                    {
                        dr = dt.NewRow();
                        dr["num_OTS"] = elemento.num_OTS;
                        dr["tipo_OTS"] = elemento.tipo_OTS;
                        dr["userResp"] = elemento.userResp;
                        dr["fec_asig"] = elemento.fec_asig;
                        dr["fec_fin"] = elemento.fec_fin;
                        dr["sts_prog"] = elemento.sts_prog;
                        dr["aplica"] = entiFinancieras(elemento.aplica);
                        dr["fec_prom"] = elemento.fec_prom;
                        dr["descripcion"] = elemento.descripcion;
                        dt.Rows.Add(dr);
                        i++;
                    }
                }
            ViewState["dt"] = dt;
            this.BindGrid();
        }

        protected void descripcion_TextChanged(object sender, EventArgs e)
        {
            string descr = descripcion.Text;
            DataTable dt = new DataTable();
            DataRow dr;
            List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS;
            if (string.IsNullOrEmpty(descr) == true)
            {
                variables.Descrip_filtro = "";
                if (string.IsNullOrEmpty(variables.User_filtro) == true)
                {
                    listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 0, "SOP", "", "");
                }
                else
                {
                    listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 1, "SOP", variables.User_filtro, "");
                }
                
            }else
            {
                if (descr.Length >= 50)
                {
                     descr =  descr.Substring(0,49);
                    variables.Descrip_filtro = descr;
                }
                else
                {
                    variables.Descrip_filtro = descr;
                }
                if (string.IsNullOrEmpty(variables.User_filtro) == true)
                {
                    listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 2, "SOP", "", descr + "%");
                }
                else
                {
                    listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 3, "SOP", variables.User_filtro, descr + "%");
                }
            }
            
            
            dt.Columns.Add("num_OTS", typeof(string));
            dt.Columns.Add("tipo_OTS", typeof(string));
            dt.Columns.Add("userResp", typeof(string));
            dt.Columns.Add("operacion", typeof(string));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("fec_asig", typeof(string));
            dt.Columns.Add("fec_fin", typeof(string));
            dt.Columns.Add("sts_prog", typeof(string));
            dt.Columns.Add("aplica", typeof(string));
            dt.Columns.Add("fec_prom", typeof(string));

            if (listaOTS != null)
            {
                int i = 0;
                foreach (var elemento in listaOTS)
                {
                    dr = dt.NewRow();
                    dr["num_OTS"] = elemento.num_OTS;
                    dr["tipo_OTS"] = elemento.tipo_OTS;
                    dr["userResp"] = elemento.userResp;
                    dr["operacion"] = elemento.operacion;
                    dr["descripcion"] = elemento.descripcion;
                    dr["fec_asig"] = elemento.fec_asig;
                    dr["fec_fin"] = elemento.fec_fin;
                    dr["sts_prog"] = elemento.sts_prog;
                    dr["aplica"] = elemento.aplica;
                    dr["fec_prom"] = elemento.fec_prom;
                    
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            ViewState["dt"] = dt;
            this.BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable();
            DataRow dr;
            int opc = 0;
            if (string.IsNullOrEmpty(variables.Descrip_filtro) == false && string.IsNullOrEmpty(variables.User_filtro) == true)
            {
                opc = 2;
            }
            else if (string.IsNullOrEmpty(variables.Descrip_filtro) == true && string.IsNullOrEmpty(variables.User_filtro) == false)
            {
                opc = 1;
            }
            else if (string.IsNullOrEmpty(variables.Descrip_filtro) == false && string.IsNullOrEmpty(variables.User_filtro) == false)
            {
                opc = 3;
            }
            List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, opc, "SOP", "", "");
            dt.Columns.Add("num_OTS", typeof(string));
            dt.Columns.Add("tipo_OTS", typeof(string));
            dt.Columns.Add("userResp", typeof(string));
            dt.Columns.Add("fec_asig", typeof(string));
            dt.Columns.Add("fec_fin", typeof(string));
            dt.Columns.Add("sts_prog", typeof(string));
            dt.Columns.Add("aplica", typeof(string));
            dt.Columns.Add("fec_prom", typeof(string));
            dt.Columns.Add("descripcion", typeof(string));

            if (listaOTS != null)
            {
                int i = 0;
                foreach (var elemento in listaOTS)
                {
                    dr = dt.NewRow();
                    dr["num_OTS"] = elemento.num_OTS;
                    dr["tipo_OTS"] = elemento.tipo_OTS;
                    dr["userResp"] = elemento.userResp;
                    dr["fec_asig"] = elemento.fec_asig;
                    dr["fec_fin"] = elemento.fec_fin;
                    dr["sts_prog"] = elemento.sts_prog;
                    dr["aplica"] = entiFinancieras(elemento.aplica);
                    dr["fec_prom"] = elemento.fec_prom;
                    dr["descripcion"] = elemento.descripcion;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            ViewState["dt"] = dt;
            this.BindGrid(); 
        }

        public string entiFinancieras(string idEntidades)
        {
            string nomEntidades = "";
            string[] entidades = idEntidades.Split(',');
            SqlConnection _conn = new SqlConnection(variables.Conexion);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            for (int i = 0; i < entidades.Length; i++)
            {
                _cmd.CommandText = String.Format("select nombre from otscatlgos where elm_cve = '{0}'", entidades[i].TrimStart(' ').TrimEnd(' '));
                _conn.Open();
                nomEntidades = nomEntidades + Convert.ToString(_cmd.ExecuteScalar()) + ", ";
                _cmd.ExecuteNonQuery();
                _conn.Close();
            }
            nomEntidades = nomEntidades.Substring(0, nomEntidades.Length - 2);
            return nomEntidades;
        }
        
    }
}