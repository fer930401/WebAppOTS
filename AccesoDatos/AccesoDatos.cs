using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using System.Collections;

namespace AccesoDatos
{
    public class AccesoDatosCls
    {
        DBOTSEntities contextoOTS;
        public AccesoDatosCls()
        {
            contextoOTS = new DBOTSEntities();
        }
        public string LoginUserOTS(String user, String pass)
        {
            String resultado;
            var consultaLogin = (from u in contextoOTS.usuarios
                                 where u.status_usr.Equals(true)
                                         && u.user_cve.Equals(user)
                                         && u.password.Equals(pass)
                                 select u).SingleOrDefault();
            if (consultaLogin == null)
            {
                resultado = null;
            }
            else
            {
                resultado = consultaLogin.nombre;
            }
            return resultado;
        }
        public List<otscatlgos> ListaRolesOTS()
        {
            return (from r in contextoOTS.otscatlgos where r.cve_catlgo.Equals("ROL") select r).ToList();
        }
        public List<usuarios> ListaUsuarios()
        {
            return (from u in contextoOTS.usuarios where u.status_usr.Equals(true) orderby u.nombre select u).ToList();
        }
        public List<sp_WebAppOTSConsultaUser_Result> ListaProgramadores(int status, string rol)
        {
            return (contextoOTS.sp_WebAppOTSConsultaUser(status, rol)).ToList();
        }
        public List<otscatlgos> ListaPaises()
        {
            return (from p in contextoOTS.otscatlgos where p.cve_catlgo.Equals("APL") orderby p.nombre select p).ToList();
        }
        public string ValidarRol(string cve_user, string rol)
        {
            return (from r in contextoOTS.rolesxUsr where r.user_cve.Equals(cve_user) && r.cve_rol.Equals(rol) select r.cve_rol).SingleOrDefault();
        }
        public Entidades.sp_WebAppOTSAdmUsers_Result admUserOTS(string user_cve, string user_nom, string user_pass, short? user_status, string user_email, string user_rol, string opcion)
        {
            return (contextoOTS.sp_WebAppOTSAdmUsers(user_cve,user_nom,user_pass,user_status,user_email,user_rol,opcion)).FirstOrDefault();
        }
        public Entidades.sp_WebAppOTSAdmOTS_Result admOTS(string ss, string tipoOTS, string asg, int? OTSP, string OTSTP, string oper, string resp, string status, string descr, string fechaIni, string fechaFin, string aplica, string tipoProceso, string nomOTS, string errorOTS, string solu_OTS, string obs_OTS, string clasi_OTS, string opcion, string nomImg, DateTime fec_prom) 
        {
            return (contextoOTS.sp_WebAppOTSAdmOTS(ss, tipoOTS, asg, OTSP, OTSTP, oper, resp, status, descr, fechaIni, fechaFin, aplica, tipoProceso, nomOTS, errorOTS, solu_OTS, obs_OTS, clasi_OTS, opcion, nomImg, fec_prom)).FirstOrDefault();
        }
        public List<otscatlgos> ListaSubSistemas()
        {
            return (from s in contextoOTS.otscatlgos where s.cve_catlgo.Equals("SS") orderby s.nombre select s).ToList();
        }
        public List<otscatlgos> ListaOperaciones()
        {
            return (from s in contextoOTS.otscatlgos where s.cve_catlgo.Equals("OPR")  orderby s.nombre select s).ToList();
        }
        public List<sp_WebAppOTSConsultaOTS_Result> ListaOTS(string user_cve, string status, int opc, string tipoOTS, string user_filtro, string descr_filtro)
        {
            return (contextoOTS.sp_WebAppOTSConsultaOTS(user_cve, status,opc, tipoOTS, user_filtro, descr_filtro)).ToList();
        }
        public Entidades.sp_WebAppOTSAdmParos_Result admParos(int? numOTS, string tipoOTS, string motivo, string opcion, int? numReng)
        {
            return (contextoOTS.sp_WebAppOTSAdmParos(numOTS, tipoOTS, motivo, opcion, numReng)).FirstOrDefault();
        }
        public List<otsemov> consulta_OTS(int numOTS, string tipoOTS, string user_cve)
        {
            var consulta = from e in contextoOTS.otsemov where e.num_OTS == numOTS && e.userResp == user_cve && e.tipo_OTS == tipoOTS select e;
            return consulta.ToList();
        }
        public List<otscatlgos> ListaCLSOTS()
        {
            return (from r in contextoOTS.otscatlgos where r.cve_catlgo.Equals("CLS") select r).ToList();
        }
        public List<otsdcatlgos> opcionesCatalogo()
        {
            return (from r in contextoOTS.otsdcatlgos where r.status.Equals(1) select r).ToList();
        }
        public Entidades.sp_WebAppOTSAdmOpc_Result opcOTS(string cve_catlgo, string elm_cve, string nombre, short? status, string opcion)
        {
            return (contextoOTS.sp_WebAppOTSAdmOpc(cve_catlgo, elm_cve, nombre, status, opcion)).FirstOrDefault();
        }
        public List<otscatlgos> infoOpc(string elm_cve)
        {
            return (from r in contextoOTS.otscatlgos where r.elm_cve.Equals(elm_cve) select r).ToList();
        }
    }
}
