using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class TerminaOTS : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cls.DataSource = logicaNegocio.ListadoCLSOTS();
                cls.DataTextField = "nombre";
                cls.DataValueField = "elm_cve";
                cls.DataBind();
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            int num_OTSU = Int32.Parse(Request["num_OTS"].ToString());
            string tip_OTSU = Request["tip_OTS"].ToString().ToUpper();
            int sts = Int32.Parse(Request["sts"].ToString());
            int ots_padre = Int32.Parse(Request["ots_padre"].ToString());
            tip_OTSU = tip_OTSU.Substring(0, 3);
            string nom_resp = Request["respo"].ToString();
            string solucion = Request["solucion"].ToString().TrimEnd(' ');
            string clasificacion = cls.SelectedValue.ToString().TrimEnd(' ');

            Entidades.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", tip_OTSU, "", ots_padre, num_OTSU.ToString(), "", nom_resp, sts.ToString(), "", "", "", "", "", "", "", solucion, "", clasificacion, "terminaDet", "", DateTime.Now);
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
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'C_subOTS.aspx';</script>");
                }
            }
        }
    }
}