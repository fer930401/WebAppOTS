using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoDatos;
using System.Collections;

namespace LogicaNegocio
{
    public class LogicaNegocioCls
    {
        AccesoDatos.AccesoDatosCls datos;
        public LogicaNegocioCls()
        {
            datos = new AccesoDatosCls();
        }
        public string LoginOTS(String user, String pass)
        {
            return datos.LoginUserOTS(user, pass);
        }
        public List<Entidades.otscatlgos> ListadoRolesOTS()
        {
            return datos.ListaRolesOTS();
        }
        public List<Entidades.usuarios> ListadoUsuarios()
        {
            return datos.ListaUsuarios();
        }
        public List<Entidades.otscatlgos> ListadoPaises()
        {
            return datos.ListaPaises();
        }
        public string validarRol(string cve_user,string rol)
        {
            return datos.ValidarRol(cve_user, rol);
        }
        public Entidades.sp_WebAppOTSAdmUsers_Result admUserOTS(string user_cve, string user_nom, string user_pass, short? user_status, string user_email, string user_rol, string opcion)
        {
            return datos.admUserOTS(user_cve, user_nom, user_pass, user_status, user_email, user_rol, opcion);
        }
        public List<Entidades.sp_WebAppOTSConsultaUser_Result> ListadoProgramadores(int status, string rol)
        {
            return datos.ListaProgramadores(status, rol);
        }
        public Entidades.sp_WebAppOTSAdmOTS_Result admOTS(string ss, string tipoOTS, string asg, int? OTSP, string OTSTP, string oper, string resp, string status, string descr, string fechaIni, string fechaFin, string aplica, string tipoProceso, string nomOTS, string errorOTS, string solu_OTS, string obs_OTS, string clasi_OTS, string opcion, string nomImg, DateTime fec_prom)
        {
            return datos.admOTS(ss, tipoOTS, asg, OTSP, OTSTP, oper, resp, status, descr, fechaIni, fechaFin, aplica, tipoProceso, nomOTS, errorOTS, solu_OTS, obs_OTS, clasi_OTS, opcion, nomImg, fec_prom);
        }
        public List<Entidades.otscatlgos> ListadoSubSistemas()
        {
            return datos.ListaSubSistemas();
        }
        public List<Entidades.otscatlgos> ListadoOperaciones()
        {
            return datos.ListaOperaciones();
        }
        public List<Entidades.sp_WebAppOTSConsultaOTS_Result> ListadoOTS(string user_cve, string status, int opc, string tipoOTS, string user_filtro, string descr_filtro)
        {
            return datos.ListaOTS(user_cve, status,opc,tipoOTS, user_filtro, descr_filtro);
        }
        public Entidades.sp_WebAppOTSAdmParos_Result admParos(int? numOTS, string tipoOTS, string motivo, string opcion, int? numReng)
        {
            return datos.admParos(numOTS, tipoOTS, motivo, opcion, numReng);
        }
        public List<Entidades.otsemov> consulta_OTS(int numOTS, string tipoOTS, string user_cve)
        {
            return datos.consulta_OTS(numOTS, tipoOTS, user_cve);
        }
        public List<Entidades.otscatlgos> ListadoCLSOTS()
        {
            return datos.ListaCLSOTS();
        }
        public List<Entidades.otsdcatlgos> opcionesCatalogo()
        {
            return datos.opcionesCatalogo();
        }
        public Entidades.sp_WebAppOTSAdmOpc_Result opcOTS(string cve_catlgo, string elm_cve, string nombre, short? status, string opcion)
        {
            return datos.opcOTS(cve_catlgo,elm_cve,nombre,status,opcion);
        }
        public List<Entidades.otscatlgos> infoOpc(string elm_cve)
        {
            return datos.infoOpc(elm_cve);
        }
        public string consultaEmail(string user_cve)
        {
            return datos.consultaEmail(user_cve);
        }
    }
}
