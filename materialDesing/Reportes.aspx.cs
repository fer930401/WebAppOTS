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
    public partial class Reportes : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                String user = Session["nombre"].ToString();
                String clave = Session["user_cve"].ToString();
                Label1.Text = user;
                user_cve.Text = clave;
                OTS(Session["user_cve"].ToString().ToUpper(), "consultaOTS");

                GridView1.DataSource = BindGrid();
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        public void OTS(string user, string opcion)
        {
            int sopTerminados = 0;
            int penTerminados = 0;
            int minutosParosOTS = 0;
            Entidades.sp_WebAppOTSAdmOTS_Result contadorSopT = logicaNegocio.admOTS("", "SOP", "", 3, "", "", user, "3", "", "", "", "", "", "", "", "", "", "", opcion, "", DateTime.Now); 
            if (contadorSopT != null)
            {
                error = contadorSopT.error;
                mensaje = contadorSopT.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    sopTerminados = Int32.Parse(mensaje);
                    soportesT.Text = sopTerminados.ToString();
                }
            }

            Entidades.sp_WebAppOTSAdmOTS_Result contadorPenT = logicaNegocio.admOTS("", "PEN", "", 3, "", "", user, "3", "", "", "", "", "", "", "", "", "", "", opcion, "", DateTime.Now); 
            if (contadorPenT != null)
            {
                error = contadorPenT.error;
                mensaje = contadorPenT.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    penTerminados = Int32.Parse(mensaje);
                    pendientesT.Text = penTerminados.ToString();
                }
            }

            Entidades.sp_WebAppOTSAdmOTS_Result minutosParos = logicaNegocio.admOTS("", "", "", 0, "", "", user, "", "", "", "", "", "", "", "", "", "", "", "minParos", "", DateTime.Now);
            if (minutosParos != null)
            {
                error = minutosParos.error;
                mensaje = minutosParos.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    minutosParosOTS = Int32.Parse(mensaje);
                    minParos.Text = minutosParosOTS.ToString();
                }
            }
        }

        public DataTable BindGrid()
        {
            string user_cve = Session["user_cve"].ToString();
            DataTable dt = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(variables.Conexion);
                SqlCommand _cmd = new SqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.Text;
                _cmd.CommandText = String.Format("select otscatlgos.nombre as Dificultad, count('') as Contador,catlgos2.nombre as Tipo from otsdmov join otscatlgos on otsdmov.clasificacion = otscatlgos.elm_cve and otscatlgos.cve_catlgo = 'CLS' join otscatlgos catlgos2 on	otsdmov.tipo_OTS = catlgos2.elm_cve and	catlgos2.cve_catlgo = 'OTS' WHERE otsdmov.id_user = '{0}' group by otsdmov.clasificacion, otscatlgos.nombre,catlgos2.nombre order by contador desc", user_cve);
                SqlDataAdapter _da = new SqlDataAdapter(_cmd);
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _da.Fill(dt);
                _conn.Close();
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return dt;
        }
    }
}