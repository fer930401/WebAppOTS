using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class variables
    {
        //string CadenaConecta = ConfigurationManager.ConnectionStrings["skytexEntities1"].ConnectionString;
        static string conexion = @"Data Source=192.168.100.133;Initial Catalog=DBOTS;User ID=soludin;Password=pluma";
        public static string Conexion
        {
            get { return variables.conexion; }
            set { variables.conexion = value; }
        }

        static string M_user_cve;
        public static string M_User_cve
        {
            get { return variables.M_user_cve; }
            set { variables.M_user_cve = value; }
        }
        static string M_nombre;
        public static string M_Nombre
        {
            get { return variables.M_nombre; }
            set { variables.M_nombre = value; }
        }
        static string M_email;
        public static string M_Email
        {
            get { return variables.M_email; }
            set { variables.M_email = value; }
        }
        static string M_status;
        public static string M_Status
        {
            get { return variables.M_status; }
            set { variables.M_status = value; }
        }
        static string M_roles;
        public static string M_Roles
        {
            get { return variables.M_roles; }
            set { variables.M_roles = value; }
        }
        static string M_statusOpc;
        public static string M_StatusOpc
        {
            get { return variables.M_statusOpc; }
            set { variables.M_statusOpc = value; }
        }

        static string user_filtro;
        public static string User_filtro
        {
            get { return variables.user_filtro; }
            set { variables.user_filtro = value; }
        }

        static string descr_filtro;
        public static string Descrip_filtro
        {
            get { return variables.descr_filtro; }
            set { variables.descr_filtro = value; }
        }

        static int num_OTS;
        public static int Num_OTS
        {
            get { return variables.num_OTS; }
            set { variables.num_OTS = value; }
        }

        static int num_rengOTS;
        public static int Num_rengOTS
        {
            get { return variables.num_rengOTS; }
            set { variables.num_rengOTS = value; }
        }

        static string tipo_OTS;
        public static string Tipo_OTS
        {
            get { return variables.tipo_OTS; }
            set { variables.tipo_OTS = value; }
        }

        static string user_OTS;
        public static string User_OTS
        {
            get { return variables.user_OTS; }
            set { variables.user_OTS = value; }
        }

        static string sts_OTS;
        public static string Sts_OTS
        {
            get { return variables.sts_OTS; }
            set { variables.sts_OTS = value; }
        }


        static string email;
        public static string Email
        {
            get { return variables.email; }
            set { variables.email = value; }
        }
        static string emailConfirm;
        public static string EmailConfirm
        {
            get { return variables.emailConfirm; }
            set { variables.emailConfirm = value; }
        }
        static string pass;
        public static string Pass
        {
            get { return variables.pass; }
            set { variables.pass = value; }
        }
        static string cve_catlgo;
        public static string Cve_catlgo
        {
            get { return variables.cve_catlgo; }
            set { variables.cve_catlgo = value; }
        }
        static string elm_cve;
        public static string Elm_cve
        {
            get { return variables.elm_cve; }
            set { variables.elm_cve = value; }
        }
        static string nombre_cve;
        public static string Nombre_cve
        {
            get { return variables.nombre_cve; }
            set { variables.nombre_cve = value; }
        }
        static string status_cve;
        public static string Status_cve
        {
            get { return variables.status_cve; }
            set { variables.status_cve = value; }
        }
    }
}
