using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class img : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int num_OTSU = Int32.Parse(Request["num_OTS"].ToString());
            string tip_OTSU = Request["tip_OTS"].ToString().ToUpper();
            tip_OTSU = tip_OTSU.Substring(0, 3);
            string nomImg = nomImagenes(num_OTSU, tip_OTSU);
            string[] nomImgInd;
            nomImgInd = nomImg.Split(';');

            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("ImageName"),
                        new DataColumn("ImageUrl")/*,
                        new DataColumn("ZoomImageUrl")*/
                    });
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Media/upload/"));

                for (int i = 0; i < nomImgInd.Length; i++)
                {
                    if (nomImgInd[i].ToString().Equals("") == false)
                    {
                        string fileName = Path.GetFileName(nomImgInd[i]);
                        dt.Rows.Add(fileName, "~/Media/upload/" + fileName /*, "~/Media/upload/" + fileName*/);
                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        public string nomImagenes(int num_OTS, string tip_OTSU)
        {
            string nomImagen;
            SqlConnection _conn = new SqlConnection(variables.Conexion);
            SqlCommand _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = String.Format("select nomImg from otsemov where num_OTS = '{0}' and tipo_OTS = '{1}'", num_OTS, tip_OTSU);
            _conn.Open();
            nomImagen = Convert.ToString(_cmd.ExecuteScalar());
            _cmd.ExecuteNonQuery();
            _conn.Close();
            return nomImagen;
        }
    }
}