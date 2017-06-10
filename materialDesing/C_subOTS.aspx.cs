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

        string user_consulta = "";
        string sts_consulta = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            consultaEncabezado(variables.Num_OTS, variables.Tipo_OTS);
            if (Session["user_cve"] != null)
            {
                string clave = Session["user_cve"].ToString();
                if (logicaNegocio.validarRol(clave, "PRG") != null)
                {
                    user_consulta = clave.ToUpper();
                    sts_consulta = "1";
                    rol_cve.Text = logicaNegocio.validarRol(clave.ToUpper(), "PRG");
                    GridView1.Columns[2].Visible = false;
                    GridView1.Columns[12].Visible = false;
                }
                else
                {
                    user_consulta = "ASG";
                    sts_consulta = "1";
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
                    List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(user_consulta, sts_consulta, 4, variables.Tipo_OTS, variables.Num_OTS.ToString(), "");
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
            List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(user_consulta, sts_consulta, 4, variables.Tipo_OTS, variables.Num_OTS.ToString(), "");
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

        protected void iniciar_Click(object sender, EventArgs e)
        {
            int row = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            int num_SubOTS = Int32.Parse(GridView1.Rows[row].Cells[0].Text);
            string sts_SubOTS = "2";

            AccesoDatos.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", "", "", variables.Num_OTS, num_SubOTS.ToString(), "", "", sts_SubOTS, "", "", "", "", "", "", "", "", "", "", "iniDet", "", DateTime.Now);
            if (insertOTSD != null)
            {
                error = insertOTSD.error;
                mensaje = insertOTSD.mensaje;
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
            int num_SubOTS = Int32.Parse(GridView1.Rows[row].Cells[0].Text);
            string sts_SubOTS = "3";

            AccesoDatos.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", "", "", variables.Num_OTS, num_SubOTS.ToString(), "", "", sts_SubOTS, "", "", "", "", "", "", "", "", "", "", "iniDet", "", DateTime.Now);
            if (insertOTSD != null)
            {
                error = insertOTSD.error;
                mensaje = insertOTSD.mensaje;
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
            //int num_SubOTS = Int32.Parse(GridView1.Rows[row].Cells[0].Text);
            variables.Num_rengOTS = Int32.Parse(GridView1.Rows[row].Cells[0].Text);
            //string sts_SubOTS = GridView1.Rows[row].Cells[5].Text;
            variables.Sts_OTS = GridView1.Rows[row].Cells[5].Text;
            
            //Response.Redirect("A_Paros.aspx?num_OTS=" + variables.Num_OTS + "&num_reng=" + num_SubOTS + "&status=" + sts_SubOTS + "&tipoOTS=" + variables.Tipo_OTS);
            Response.Redirect("A_Paros.aspx");
        }

        protected void continuar_Click(object sender, EventArgs e)
        {
            int row = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            //int num_SubOTS = Int32.Parse(GridView1.Rows[row].Cells[0].Text);
            variables.Num_rengOTS = Int32.Parse(GridView1.Rows[row].Cells[0].Text);
            //string sts_SubOTS = GridView1.Rows[row].Cells[5].Text;
            variables.Sts_OTS = GridView1.Rows[row].Cells[5].Text;

            //Response.Redirect("A_Paros.aspx?num_OTS=" + variables.Num_OTS + "&num_reng=" + num_SubOTS + "&status=" + sts_SubOTS + "&tipoOTS=" + variables.Tipo_OTS);
            Response.Redirect("A_Paros.aspx");
        }

        public void consultaEncabezado(int num_OTSc, string tipo_OTSc)
        {
            DataTable encabezado = new DataTable();
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
            if (string.IsNullOrEmpty(encabezado.Rows[0][5].ToString()))
            {
                Session["visibleSOTS"] = "style = 'display:none'"; 
            }
            else
            {
                Session["visibleSOTS"] = "";
            }
        }
    }
}