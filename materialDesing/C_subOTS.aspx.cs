using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class C_subOTS : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        string usuarioC = "";
        string statusC = "";
        int num_OTSU = 0;
        string tip_OTSU = "";
        string tip_OTS = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            num_OTSU = Int32.Parse(Request["num_OTS"].ToString());
            tip_OTSU = Request["tip_OTS"].ToString().ToUpper();
            tip_OTS = Request["tip_OTS"].ToString().ToUpper();
            tip_OTSU = tip_OTSU.Substring(0, 3);
            consultaEncabezado(num_OTSU,tip_OTSU);
            if (Session["user_cve"] != null)
            {
                string clave = Session["user_cve"].ToString();
                

                if (logicaNegocio.validarRol(clave, "PRG") != null)
                {
                    usuarioC = clave.ToUpper();
                    statusC = "1";
                    rol_cve.Text = logicaNegocio.validarRol(clave.ToUpper(), "PRG");
                    GridView1.Columns[2].Visible = false;
                }
                else
                {
                    usuarioC = "ASG";
                    statusC = "1";
                    rol_cve.Text = logicaNegocio.validarRol(clave.ToUpper(), "ASG");
                    GridView1.Columns[2].Visible = true;
                    GridView1.Columns[8].Visible = false;
                    GridView1.Columns[9].Visible = false;
                    GridView1.Columns[10].Visible = false;
                    GridView1.Columns[11].Visible = false;
                }
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataRow dr;
                    List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 4, tip_OTSU, num_OTSU.ToString(), "");
                    dt.Columns.Add("num_OTS", typeof(string));
                    dt.Columns.Add("tipo_OTS", typeof(string));
                    dt.Columns.Add("operacion", typeof(string));
                    dt.Columns.Add("userResp", typeof(string));
                    dt.Columns.Add("descripcion", typeof(string));
                    dt.Columns.Add("fec_asig", typeof(string));
                    dt.Columns.Add("fec_fin", typeof(string));
                    dt.Columns.Add("aplica", typeof(string));
                    dt.Columns.Add("sts_prog", typeof(string));

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
                            dr["aplica"] = "";
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
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["dt"] as DataTable;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            
            DataTable dt = new DataTable();
            DataRow dr;
            List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(usuarioC, statusC, 4, tip_OTSU, num_OTSU.ToString(), "");
            dt.Columns.Add("num_OTS", typeof(string));
            dt.Columns.Add("tipo_OTS", typeof(string));
            dt.Columns.Add("operacion", typeof(string));
            dt.Columns.Add("userResp", typeof(string));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("fec_asig", typeof(string));
            dt.Columns.Add("fec_fin", typeof(string));
            dt.Columns.Add("aplica", typeof(string));
            dt.Columns.Add("sts_prog", typeof(string));

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

        protected void iniciar_Click(object sender, EventArgs e)
        {
            int row = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            int folio = Convert.ToInt32(GridView1.Rows[row].Cells[0].Text);
            string status = "2";
            AccesoDatos.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", "", "", num_OTSU, folio.ToString(), "", "", status, "", "", "", "", "", "", "", "", "", "", "iniDet", "", DateTime.Now);
            if (insertOTSD != null)
            {
                error = insertOTSD.error;
                mensaje = insertOTSD.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('SubOTS Iniciado'); window.location.href = window.location.href;</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Detalle.aspx';</script>");
                }
            }
        }

        protected void terminar_Click(object sender, EventArgs e)
        {
            int row = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            //Get the value of column from the DataKeys using the RowIndex.
            int folio = Convert.ToInt32(GridView1.Rows[row].Cells[0].Text);
            string status = "3";
            AccesoDatos.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", "", "", num_OTSU, folio.ToString(), "", "", status, "", "", "", "", "", "", "", "", "", "", "iniDet", "", DateTime.Now);
            if (insertOTSD != null)
            {
                error = insertOTSD.error;
                mensaje = insertOTSD.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('SubOTS Finalizado'); window.location.href = window.location.href;</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Detalle.aspx';</script>");
                }
            }
        }

        protected void paro_Click(object sender, EventArgs e)
        {
            int row = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            //Get the value of column from the DataKeys using the RowIndex.
            int folio = Convert.ToInt32(GridView1.Rows[row].Cells[0].Text);
            string status = GridView1.Rows[row].Cells[5].Text;
            string tipoOTS = GridView1.Rows[row].Cells[1].Text;
            Response.Redirect("A_Paros.aspx?num_OTS=" + num_OTSU + "&num_reng=" + folio + "&status=" + status + "&tipoOTS=" + tip_OTSU);
        }

        protected void continuar_Click(object sender, EventArgs e)
        {
            int row = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            //Get the value of column from the DataKeys using the RowIndex.
            int folio = Convert.ToInt32(GridView1.Rows[row].Cells[0].Text);
            string status = GridView1.Rows[row].Cells[5].Text;
            string tipoOTS = GridView1.Rows[row].Cells[1].Text;
            Response.Redirect("A_Paros.aspx?num_OTS=" + num_OTSU + "&num_reng=" + folio + "&status=" + status + "&tipoOTS=" + tip_OTSU);
        }

        protected void soportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("C_Soportes.aspx");
        }
        public void consultaEncabezado(int num_OTSc, string tipo_OTSc)
        {
            DataTable encabezado = new DataTable(); ;
            SqlConnection _conn = new SqlConnection(variables.Conexion);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select num_OTS,tipo_OTS,descripcion,aplica,sts_prog,isnull(nomImg,'') as nomImg from otsemov where num_OTS = '{0}' and tipo_OTS = '{1}'", num_OTSc, tipo_OTSc);
            SqlDataAdapter _da = new SqlDataAdapter(_cmd);
            _conn.Open();
            _cmd.ExecuteNonQuery();
            _da.Fill(encabezado);
            _conn.Close();
            lblNumOts.Text = encabezado.Rows[0][0].ToString().TrimEnd(' ');
            lblTipoOts.Text = encabezado.Rows[0][1].ToString().TrimEnd(' ');
            lblDescripcion.Text = encabezado.Rows[0][2].ToString().TrimEnd(' ');
            lblAplica.Text = encabezado.Rows[0][3].ToString().TrimEnd(' ');
            lblStatus.Text = encabezado.Rows[0][4].ToString().TrimEnd(' ');
            //lblImg.Text = encabezado.Rows[0][4].ToString().TrimEnd(' ');
        }
    }
}