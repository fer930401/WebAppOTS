using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace materialDesing
{
    public class sessionOTS
    {
        const string usoOTS = @"C:\Users\fernando.garcia\Documents\Proyectos Skytex\WebApp\materialDesing\sessionOTS\Activo.txt";
        //const string usoApp = @"C:\Desarrollo\Desarrollo_web\Agenda\Activo\Activo.txt";
        const string sessionvar = "checkCounter";

        public sessionOTS()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static bool OTSIniciada()
        {
            //Si existe variable de estado asociada al visitante: TERMINAR return false
            if (HttpContext.Current.Session[sessionvar] != null) { return false; }
            //Crear la variable de sesion....
            HttpContext.Current.Session[sessionvar] = (byte)0;
            //Leyendo el fichero.....
            string fcounter = usoOTS;
            string line = "1";
            //Si el fichero existe incrementar el valor....
            if (File.Exists(fcounter) == true)
            {//Incrementar el contador........
                using (StreamReader sr = new StreamReader(fcounter))
                {
                    line = sr.ReadLine();
                    try { line = Convert.ToString(1); }
                    catch { line = "0"; } //Proteccion contra corrupción de fichero
                    sr.Close();
                }
            }
            //Si no existe guarda 1, si existe guarda el valor incrementado
            using (StreamWriter wr = File.CreateText(fcounter))
            {
                wr.Write(line);
                wr.Flush();
                wr.Close();
            }
            return true;
        }
        public static bool OTSterminada()
        {
            //Si existe variable de estado asociada al visitante: TERMINAR return false
            if (HttpContext.Current.Session[sessionvar] == null) { return false; }
            //Crear la variable de sesion....
            HttpContext.Current.Session[sessionvar] = (byte)0;
            //Leyendo el fichero.....
            string fcounter = usoOTS;
            string line = "1";
            //Si el fichero existe incrementar el valor....
            if (File.Exists(fcounter) == true)
            {//Incrementar el contador........
                using (StreamReader sr = new StreamReader(fcounter))
                {
                    line = sr.ReadLine();
                    try { line = Convert.ToString(0); }
                    catch { line = "0"; } //Proteccion contra corrupción de fichero
                    sr.Close();
                }
            }
            //Si no existe guarda 1, si existe guarda el valor incrementado
            using (StreamWriter wr = File.CreateText(fcounter))
            {
                wr.Write(line);
                wr.Flush();
                wr.Close();
            }
            return true;
        }
        public static int estaEnUso()
        {
            try
            {
                //Si no existe variable de estado asociada al visitante: return -1 TERMINAR
                if (HttpContext.Current.Session[sessionvar] != null) { return -1; }
                //Si no existe fichero de conteo: return -2 TERMINAR
                string fcounter = usoOTS;
                if (File.Exists(fcounter) != true) { return -2; }
                //Abrir fichero de conteo, leer variable, cerrar fichero de conteo
                using (StreamReader sr = new StreamReader(fcounter))
                {
                    int num = 0;
                    try { num = Convert.ToInt32(sr.ReadLine()); }
                    catch { num = -4; } //Proteccion contra corrupción de fichero
                    sr.Close();
                    return num;
                }
                //return valor de conteo

            }
            catch
            { return -3; }
        }
        public static int OTSenProceso()
        {
            try
            {
                //Si no existe variable de estado asociada al visitante: return -1 TERMINAR
                if (HttpContext.Current.Session[sessionvar] == null) { return -1; }
                //Si no existe fichero de conteo: return -2 TERMINAR
                string fcounter = usoOTS;
                if (File.Exists(fcounter) != true) { return -2; }
                //Abrir fichero de conteo, leer variable, cerrar fichero de conteo
                using (StreamReader sr = new StreamReader(fcounter))
                {
                    int num = 0;
                    try { num = Convert.ToInt32(sr.ReadLine()); }
                    catch { num = -4; } //Proteccion contra corrupción de fichero
                    sr.Close();
                    return num;
                }
                //return valor de conteo

            }
            catch
            { return -3; }
        } 
    }
}