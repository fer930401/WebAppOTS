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
    public partial class C_HijosOTS : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string usuarioC = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int num_OTSU = Int32.Parse(Request["num_OTS"].ToString());
            string tip_OTSU = Request["tip_OTS"].ToString().ToUpper();
            tip_OTSU = tip_OTSU.Substring(0, 3);
            if (Session["user_cve"] != null)
            {
                String clave = Session["user_cve"].ToString();
                if (!IsPostBack)
                {
                    if (logicaNegocio.validarRol(Session["user_cve"].ToString().ToUpper(), "PRG") != null)
                    {
                        usuarioC = Session["user_cve"].ToString().ToUpper();
                    }
                    else
                    {
                        usuarioC = "S/U";
                    }
                    DataTable dt = new DataTable();
                    DataRow dr;
                    List<AccesoDatos.sp_WebAppOTSConsultaOTS_Result> listaOTS = logicaNegocio.ListadoOTS(usuarioC, tip_OTSU, 3, num_OTSU.ToString());
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
    }
}