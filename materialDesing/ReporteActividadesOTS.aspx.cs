using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace materialDesing
{
    public partial class ReporteActividaesOTS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                ReportDocument rep = new ReportDocument();
                rep.Load(Server.MapPath("EstadisticasOTS.rpt"));
                
                rep.SetDatabaseLogon("soludin", "pluma","192.168.100.133","DBOTS");
                /*rep.SetParameterValue("@user", Session["user_cve"].ToString());
                rep.SetParameterValue("@status", "C/U");
                rep.SetParameterValue("@opc",1);
                rep.SetParameterValue("@filtro", Session["user_cve"].ToString());*/
                Response.ContentType = "application/pdf";
                rep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Reporte Actividades OTS");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}