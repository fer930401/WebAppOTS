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
                /*int num_OTSU = Int32.Parse(Request["num_OTS"].ToString());
                string tip_OTSU = Request["tip_OTS"].ToString().ToUpper();
                int sts = Int32.Parse(Request["sts"].ToString());
                int ots_padre = Int32.Parse(Request["ots_padre"].ToString());
                string nom_resp = Request["respo"].ToString();
                tip_OTSU = tip_OTSU.Substring(0, 3);*/

                //Response.Write(num_OTSU + "," + tip_OTSU + "," + sts + "," + ots_padre + "," + nom_resp);
                
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
        /*protected void btnReasignar_Click(object sender, EventArgs e)
        {
            int numOTS = Int32.Parse(txtNumOTS.Text);
            string tipo_OTSU = Request["tip_OTS"].ToString();
            string tip_OTSU = Request["tip_OTS"].ToString().ToUpper();
            int opc = Int32.Parse(Request["opc"].ToString());
            int ots_padre = Int32.Parse(Request["ots_padre"].ToString());
            tip_OTSU = tip_OTSU.Substring(0, 3);
            DateTime fechaReasignado = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy"));
            DataTable dt_OTS = reasignacion(numOTS, ots_padre, tip_OTSU, opc);
            string nuevoRespo = cmbResponsable.SelectedValue.ToString();
            Entidades.sp_WebAppOTSAdmOTS_Result updateOTSE = null;
            if (opc == 1)
            {
                updateOTSE = logicaNegocio.admOTS("", tip_OTSU, nuevoRespo, numOTS, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "reasignarOTS", "", DateTime.Now);
            }
            else if (opc == 2)
            {
                updateOTSE = logicaNegocio.admOTS(ots_padre.ToString(), tip_OTSU, nuevoRespo, numOTS, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "reasignarSubOTS", "", DateTime.Now);
            }
            if (updateOTSE != null)
            {
                error = updateOTSE.error;
                mensaje = updateOTSE.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    sendEmail(nuevoRespo, tipo_OTSU, dt_OTS.Rows[0][1].ToString(), dt_OTS.Rows[0][3].ToString(), fechaReasignado);
                    if (tip_OTSU.Equals("SOP") == true)
                    {
                        Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "');  window.location.href = 'C_Soportes.aspx';</script>");
                    }
                    else if (tip_OTSU.Equals("PEN") == true)
                    {
                        Response.Write("<script type=\"text/javascript\">alert('" + mensaje + "');  window.location.href = 'C_Pendientes.aspx';</script>");
                    }
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'Inicio.aspx';</script>");
                }
            }
        }*/
    }
}