using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace materialDesing
{
    public partial class A_Paros : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        string status;
        int numOTS;
        string tipOTS;
        int numReng;

        protected void Page_Load(object sender, EventArgs e)
        {
            status = variables.Sts_OTS;
            numOTS = variables.Num_OTS;
            tipOTS = variables.Tipo_OTS;
            numReng = variables.Num_rengOTS;
            if (status.Equals("Pausa") == true)
            {
                btnAltaParo.Visible = false;
                btnReanudarParo.Visible = true;
                Session["visibleSD" + numOTS] = "style = 'display:none'";
            }
            else if(status.Equals("Iniciada") == true)
            {
                btnAltaParo.Visible = true;
                btnReanudarParo.Visible = false;
                Session["visibleSD" + numOTS] = "";
            }
            num_otsTB.Text = numOTS.ToString();
            tipOTSTB.Text = tipOTS;
            statusOTS.Text = status;
            num_reng.Text = numReng.ToString();
        }
        protected void btnAltaParo_Click(object sender, EventArgs e)
        {
            string numOTS = num_otsTB.Text;
            string motivoParo = motivo.Text;
            string tipOTS = tipOTSTB.Text;
            string numReng = num_reng.Text;
            AccesoDatos.sp_WebAppOTSAdmParos_Result insertOTSP = logicaNegocio.admParos(Int32.Parse(numOTS), tipOTS.ToUpper().Substring(0, 3), motivoParo, "altaParo", Int32.Parse(numReng));
            if (insertOTSP != null)
            {
                error = insertOTSP.error;
                mensaje = insertOTSP.mensaje;
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('La actividad se a detenido.'); window.location.href = 'C_subOTS.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Paros.aspx';</script>");
                }
            }
        }

        protected void btnReanudarParo_Click(object sender, EventArgs e)
        {
            string numOTS = num_otsTB.Text;
            string motivoParo = motivo.Text;
            string tipOTS = tipOTSTB.Text;
            string numReng = num_reng.Text;
            AccesoDatos.sp_WebAppOTSAdmParos_Result insertOTSP = logicaNegocio.admParos(Int32.Parse(numOTS), tipOTS.ToUpper().Substring(0, 3), motivoParo, "termParo", Int32.Parse(numReng));
            if (insertOTSP != null)
            {
                error = insertOTSP.error;
                mensaje = insertOTSP.mensaje;
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('La actividad se a reanudado.'); window.location.href = 'C_subOTS.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Paros.aspx';</script>");
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("C_subOTS.aspx");
        }
    }
}