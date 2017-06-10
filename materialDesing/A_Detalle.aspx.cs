﻿using LogicaNegocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class A_Soportes : System.Web.UI.Page
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

                    List<AccesoDatos.otsemov> OTSEncabezado = logicaNegocio.consulta_OTS(variables.Num_OTS, variables.Tipo_OTS, variables.User_OTS);

                    num_OTS.Text = OTSEncabezado[0].num_OTS.ToString();
                    tipo_OTS.Text = OTSEncabezado[0].tipo_OTS.ToString();
                    descripcion.Text = OTSEncabezado[0].descripcion.ToString();
                    fechaIni.Text = Convert.ToDateTime(OTSEncabezado[0].fec_asig.ToString()).ToString("yyyy-MM-dd HH:MM:ss");
                    fechaFin.Text = Convert.ToDateTime(OTSEncabezado[0].fec_fin.ToString()).ToString("yyyy-MM-dd HH:MM:ss");

                    cls.DataSource = logicaNegocio.ListadoCLSOTS();
                    cls.DataTextField = "nombre";
                    cls.DataValueField = "elm_cve";
                    cls.DataBind();         
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnGuardarOTSD_Click(object sender, EventArgs e)
        {
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
            string aplica = "";
            string status = "2";
            AccesoDatos.sp_WebAppOTSAdmOTS_Result insertOTSD = logicaNegocio.admOTS("", variables.Tipo_OTS.ToUpper(), dificultad, variables.Num_OTS, "", "", variables.User_OTS.ToUpper(), status, desc, Convert.ToDateTime(fecha_Ini).ToString("yyyy-MM-dd HH:MM:ss"), Convert.ToDateTime(fecha_Fin).ToString("yyyy-MM-dd HH:MM:ss"), aplica, oper, nom_OTS, error_ots, solucion, observaciones, clasificacion, "altaDet", "", DateTime.Now);
            if (insertOTSD != null)
            {
                error = insertOTSD.error;
                mensaje = insertOTSD.mensaje;
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('El Detalle del OTS a sido creado.'); window.location.href = 'A_Detalle.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente De Nuevo.');  window.location.href = 'A_Detalle.aspx';</script>");
                }
            }
        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (variables.Tipo_OTS.Equals("SOP") == true)
            {
                Response.Redirect("C_Soportes.aspx");
            }
            else if (variables.Tipo_OTS.Equals("PEN") == true)
            {
                Response.Redirect("C_Pendientes.aspx");
            }
        }
    }
}