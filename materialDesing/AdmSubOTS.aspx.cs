using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class U_Pendientes : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                string clave = Session["user_cve"].ToString();
                user_cve.Text = clave;
                if (!IsPostBack)
                {
                    opr.DataSource = logicaNegocio.ListadoOperaciones();
                    opr.DataTextField = "nombre";
                    opr.DataValueField = "elm_cve";
                    opr.DataBind();
                    int valor = Int32.Parse(Request["num_OTS"].ToString());
                    List<AccesoDatos.otsemov> consultaOTSE = logicaNegocio.consulta_OTS(valor, clave);
                    num_OTS.Text = consultaOTSE[0].num_OTS.ToString();
                    tipo_OTS.Text = consultaOTSE[0].tipo_OTS.ToString();
                    descripcion.Text = consultaOTSE[0].descripcion.ToString();
                    fechaIni.Text = Convert.ToDateTime(consultaOTSE[0].fec_asig.ToString()).ToString("yyyy-MM-dd HH:MM:ss");
                    fechaFin.Text = Convert.ToDateTime(consultaOTSE[0].fec_fin.ToString()).ToString("yyyy-MM-dd HH:MM:ss");
                    string idEntidad = "";
                    string nomEntidad = "";
                    idEntidad = consultaOTSE[0].aplica.ToString();
                    nomEntidad = entiFinancieras(idEntidad);
                    aplica_para.Text = nomEntidad;
                    string status = consultaOTSE[0].sts_prog.ToString().Trim();

                    cls.DataSource = logicaNegocio.ListadoCLSOTS();
                    cls.DataTextField = "nombre";
                    cls.DataValueField = "elm_cve";
                    cls.DataBind();
                    /* agregar la validacion de status del OTS hijo */
                    btnTerminarOTS.Visible = false;
                    Session["visibleSD"] = "style = 'display:none'";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnGuardarOTSD_Click(object sender, EventArgs e)
        {
            int numOTS = Int32.Parse(Request["num_OTS"].ToString());
            string user = user_cve.Text.ToUpper();
            string tip_OTS = tipo_OTS.Text;
            string oper = opr.SelectedValue;
            string nom_OTS = nom_proceso.Text;
            string desc = descripcion.Text;
            string fecha_Ini = fechaIni.Text;
            string fecha_Fin = fechaFin.Text;
            string dificultad = "0";
            string error_ots = Request["error"];
            string solucion = Request["solucion"];
            string observaciones = Request["obs"];
            string clasificacion = cls.SelectedValue.TrimStart(' ').TrimEnd(' ');
            string aplica = aplica_para.Text;
            string status = "2";
            Response.Write("<script type=\"text/javascript\">alert('El Detalle del OTS a sido creado.'); window.location.href = 'C_Soportes.aspx';</script>");
            AccesoDatos.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", tip_OTS.ToUpper(), dificultad, numOTS, "", "", user, status, desc, Convert.ToDateTime(fecha_Ini).ToString("yyyy-MM-dd HH:MM:ss"), Convert.ToDateTime(fecha_Fin).ToString("yyyy-MM-dd HH:MM:ss"), aplica, oper, nom_OTS, error_ots, solucion, observaciones, clasificacion, "altaDet", "",DateTime.Now);
            if (insertOTSD != null)
            {
                error = insertOTSD.error;
                mensaje = insertOTSD.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('El Detalle del OTS a sido creado.'); window.location.href = 'C_HijosOTS.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Detalle.aspx';</script>");
                }
            }
        }

        protected void btnTerminarOTS_Click(object sender, EventArgs e)
        {
            int valor = Int32.Parse(Request.QueryString["num_OTS"]);
            string dificultad = Request["dificultad"];
            int numOTS = valor;
            string nom_OTS = "";
            string descripcion2 = descripcion.Text;
            string user = user_cve.Text.ToUpper();
            //el ots papa se reinicia con status 1
            string status = "1";
            Response.Write("<script type=\"text/javascript\">alert('El Detalle del OTS a sido terminado.'); window.location.href = 'C_Soportes.aspx';</script>");
            AccesoDatos.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", "", dificultad, numOTS, "", "", user, status, descripcion2, "", "", "", "", nom_OTS, "", "", "", "", "terminaDet", "", DateTime.Now);
            if (insertOTSD != null)
            {
                error = insertOTSD.error;
                mensaje = insertOTSD.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('El Detalle del OTS a sido terminado.'); window.location.href = 'C_Soportes.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Detalle.aspx';</script>");
                }
            }
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