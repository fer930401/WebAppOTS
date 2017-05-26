﻿using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace materialDesing
{
    public partial class A_Opciones : System.Web.UI.Page
    {
        LogicaNegocioCls logicaNegocio = new LogicaNegocioCls();
        string mensaje = "";
        short? error = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_cve"] != null)
            {
                if (!IsPostBack)
                {
                    string clave = Session["user_cve"].ToString();
                    string nombre = Session["nombre"].ToString();
                    user_cve.Text = clave;
                    usuario.Text = nombre;
                    cmbOpciones.DataSource = logicaNegocio.opcionesCatalogo();
                    cmbOpciones.DataTextField = "nom_catlgo";
                    cmbOpciones.DataValueField = "cve_catlgo";
                    cmbOpciones.DataBind();
                    cmbOpciones.Items.Insert(0, new ListItem("----Selecciona opcion----", null));
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnGuardarOpc_Click(object sender, EventArgs e)
        {
            string newCve_catlgo = cmbOpciones.SelectedValue;
            string newElm_cve = elm_cve.Text;
            string newNombre = nom_catlgo.Text;
            short? newStatus = short.Parse(Request["status"].ToString());
            string opcion = "alta";
            AccesoDatos.sp_WebAppOTSAdmOpc_Result insOpcOTS = logicaNegocio.opcOTS(newCve_catlgo,newElm_cve,newNombre,newStatus,opcion);
            if (insOpcOTS != null)
            {
                error = insOpcOTS.error;
                mensaje = insOpcOTS.mensaje;
                //si no se regreso ningun error
                if (Convert.ToInt32(error) == 0)
                {
                    Response.Write("<script type=\"text/javascript\">alert('La nueva opcion a sido creado.'); window.location.href = 'Inicio.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente mas tarde.');  window.location.href = 'Inicio.aspx';</script>");
                }
            }
        }

        protected void cmbOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOpciones.SelectedValue.Equals("APL") == true)
            {
                elm_cve.Enabled = false;
            }
            else
            {
                elm_cve.Enabled = true;
            }
        }
    }
}