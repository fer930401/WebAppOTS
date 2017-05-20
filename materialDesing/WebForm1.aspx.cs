using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace materialDesing
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<AccesoDatos.sp_WebAppOTSConsultaUser_Result> usuario = logicaNegocio.ListadoProgramadores(1, "");
            DataList1.DataSource = usuario;
            DataList1.DataBind();

            Repeater1.DataSource = usuario;
            Repeater1.DataBind();
        }
    }
}