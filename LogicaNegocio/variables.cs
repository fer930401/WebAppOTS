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
    }
}
