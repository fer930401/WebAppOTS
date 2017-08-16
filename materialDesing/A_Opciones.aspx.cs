using LogicaNegocio;
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
                if (string.IsNullOrEmpty(variables.Cve_catlgo) == false)
                {
                    cmbOpciones.SelectedValue = variables.Cve_catlgo;
                }
            }
        }
        protected void btnGuardarOpc_Click(object sender, EventArgs e)
        {
            string newCve_catlgo = cmbOpciones.SelectedValue;
            string newElm_cve = elm_cve.Text.TrimEnd(' ');
            string newNombre = nom_catlgo.Text.TrimEnd(' ');
            short? newStatus = short.Parse(Request["status"].ToString());
            string opcion = "alta";
            bool valido = false;
            if (newCve_catlgo == "APL")
            {
                if (string.IsNullOrEmpty(newNombre) == false)
                {
                    valido = true;
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Llenar todos los campos');</script>");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(newElm_cve) == false && string.IsNullOrEmpty(newNombre) == false)
                {
                    valido = true;
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Llenar todos los campos');</script>");
                }
            }

            if (valido)
            {
                try
                {
                    Entidades.sp_WebAppOTSAdmOpc_Result insOpcOTS = logicaNegocio.opcOTS(newCve_catlgo, newElm_cve, newNombre, newStatus, opcion);
                    if (insOpcOTS != null)
                    {
                        error = insOpcOTS.error;
                        mensaje = insOpcOTS.mensaje;
                        //si no se regreso ningun error
                        if (Convert.ToInt32(error) == 0)
                        {
                            Response.Write("<script type=\"text/javascript\">alert('La nueva opcion a sido creado.'); window.location.href = 'AdmOpciones.aspx';</script>");
                        }
                        else
                        {
                            Response.Write("<script type=\"text/javascript\">alert('Se Encontro Un Error " + mensaje + " \\nIntente mas tarde.');  window.location.href = 'AdmOpciones.aspx';</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script type=\"text/javascript\">alert('Se ingrego una clave que ya existe, revise la informacion capturada');  window.location.href = 'AdmOpciones.aspx';</script>");
                }
            }
        }
        protected void cmbOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOpciones.SelectedValue.Equals("APL") == true)
            {
                elm_cve.Text = "";
                elm_cve.Enabled = false;
            }
            else
            {
                elm_cve.Enabled = true;
            }
        }
    }
}