using LogicaNegocio;
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
                String clave = Session["user_cve"].ToString();
                user_cve.Text = clave;
                if (!IsPostBack)
                {
                    if (logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG") != null)
                    {
                        usuarioC = Session["user_cve"].ToString().ToUpper();
                        statusC ="1";
                        rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG");
                        cmbProgramador.Visible = false;
                        lblRespon.Visible = false;
                    }
                    else
                    {
                        usuarioC = "S/U";
                        statusC = "1";
                        rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "ASG");
                        cmbProgramador.Visible = true;
                        DataTable datos = new DataTable();
                        cmbProgramador.Items.Clear();
                        cmbProgramador.Items.Insert(0, new ListItem("Selecciona una opción", ""));
                        cmbProgramador.SelectedIndex = 0;
                        cmbProgramador.AppendDataBoundItems = true;
                        cmbProgramador.DataSource = logicaNegocio.ListadoProgramadores(1, "PRG");
                        cmbProgramador.DataValueField = "user_cve";
                        cmbProgramador.DataTextField = "nombre";
                        cmbProgramador.AutoPostBack = true;
                        cmbProgramador.DataBind();
                    }
                    DataTable dt = new DataTable();
                    DataRow dr;
                    List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 0, "SOP");
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
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            if (rol_cve.Text=="PRG")
            {
                GridView1.Columns[2].Visible = false;
            }
        }
        private void llenaGrid()
        {
            if (logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG") != null)
            {
                this.GridView1.DataSource = logicaNegocio.ListadoOTS(Session["user_cve"].ToString().ToUpper(),"1",0,"SOP");
                GridView1.Columns[7].Visible = true;
            }
            else
            {
                this.GridView1.DataSource = logicaNegocio.ListadoOTS("S/U","1",0,"");
                GridView1.Columns[7].Visible = false;
            }
            
            this.GridView1.DataBind();
        }
        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();
        }

        protected void cmbProgramador_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user_cveF = cmbProgramador.SelectedValue;
            descripcion.Text = "";
            DataTable dt = new DataTable();
            DataRow dr;
            if (logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG") != null)
            {
                usuarioC = Session["user_cve"].ToString().ToUpper();
                statusC = "1";
                rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG");
            }
            else
            {
                usuarioC = "S/U";
                statusC = "1";
                rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "ASG");
            }
            List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS;
            if (user_cveF.Equals("") == false)
            {
                listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 1, user_cveF);
            }else
            {
                listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 0, "SOP");
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
            string user_cveFi = cmbProgramador.SelectedValue;
            string descr = descripcion.Text;
            DataTable dt = new DataTable();
            DataRow dr;
            if (logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG") != null)
            {
                usuarioC = Session["user_cve"].ToString().ToUpper();
                user_cveFi = Session["user_cve"].ToString().ToUpper();
                statusC = "1";
                rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG");
            }
            else
            {
                usuarioC = "S/U";
                statusC = "1";
                rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "ASG");
            }
            if (descr.Length >= 50)
            {
                 descr =  descr.Substring(0,49);
            }
            List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(user_cveFi, statusC, 2, descr + "%");
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

        [WebMethod]
        public static string[] GetDescripcion(string prefix, int parentId, string user_cveF)
        {
            SqlConnection _conn = new SqlConnection(variables.Conexion);
            SqlCommand cmd = new SqlCommand();
            if (user_cveF.Equals("") == true)
            {
                cmd.CommandText = "select num_OTS, descripcion from otsemov where descripcion like @SearchText + '%'";
            }
            else
            {
                if (user_cveF.Equals("undefined") == true)
                {

                }
                cmd.CommandText = "select num_OTS, descripcion from otsemov where descripcion like '%' + @SearchText + '%' and userResp = '" + user_cveF.TrimStart(' ').TrimEnd(' ') + "'";
            }
            cmd.Parameters.AddWithValue("@SearchText", prefix.TrimStart(' ').TrimEnd(' '));
            cmd.Parameters.AddWithValue("@userCve", user_cveF.TrimStart(' ').TrimEnd(' '));
            return PopulateAutoComplete(cmd);
        }
        private static string[] PopulateAutoComplete(SqlCommand cmd)
        {
            List<string> autocompleteItems = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = string.Format(variables.Conexion);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        autocompleteItems.Add(string.Format("{0}-{1}", sdr[1], sdr[0]));
                    }
                }
                conn.Close();
            }
            return autocompleteItems.ToArray();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            if (logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG") != null)
            {
                usuarioC = Session["user_cve"].ToString().ToUpper();
                statusC = "1";
                rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG");
                cmbProgramador.Visible = false;
            }
            else
            {
                usuarioC = "S/U";
                statusC = "1";
                rol_cve.Text = logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "ASG");
                cmbProgramador.Visible = true;
                DataTable datos = new DataTable();
                cmbProgramador.Items.Clear();
                cmbProgramador.Items.Insert(0, new ListItem("Selecciona una opción", String.Empty));
                cmbProgramador.SelectedIndex = 0;
                cmbProgramador.AppendDataBoundItems = true;
                cmbProgramador.DataSource = logicaNegocio.ListadoProgramadores(1, "PRG");
                cmbProgramador.DataValueField = "user_cve";
                cmbProgramador.DataTextField = "nombre";
                cmbProgramador.AutoPostBack = true;
                cmbProgramador.DataBind();
            }
            DataTable dt = new DataTable();
            DataRow dr;
            List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 0, "SOP");
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