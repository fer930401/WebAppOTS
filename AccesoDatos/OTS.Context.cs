﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBOTSEntities : DbContext
    {
        public DBOTSEntities()
            : base("name=DBOTSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<otsdcatlgos> otsdcatlgos { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<otscatlgos> otscatlgos { get; set; }
        public virtual DbSet<otsdmov> otsdmov { get; set; }
        public virtual DbSet<otsemov> otsemov { get; set; }
        public virtual DbSet<paros> paros { get; set; }
        public virtual DbSet<rolesxUsr> rolesxUsr { get; set; }
        public virtual DbSet<ChatsGrupales> ChatsGrupales { get; set; }
        public virtual DbSet<ChatsPrivados> ChatsPrivados { get; set; }
        public virtual DbSet<ChatsPrivadosDetalles> ChatsPrivadosDetalles { get; set; }
        public virtual DbSet<ChatUserDetalle> ChatUserDetalle { get; set; }
    
        public virtual ObjectResult<sp_WebAppOTSAdmOpc_Result> sp_WebAppOTSAdmOpc(string cve_catlgo, string elm_cve, string nombre, Nullable<short> status, string opcion)
        {
            var cve_catlgoParameter = cve_catlgo != null ?
                new ObjectParameter("cve_catlgo", cve_catlgo) :
                new ObjectParameter("cve_catlgo", typeof(string));
    
            var elm_cveParameter = elm_cve != null ?
                new ObjectParameter("elm_cve", elm_cve) :
                new ObjectParameter("elm_cve", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(short));
    
            var opcionParameter = opcion != null ?
                new ObjectParameter("opcion", opcion) :
                new ObjectParameter("opcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_WebAppOTSAdmOpc_Result>("sp_WebAppOTSAdmOpc", cve_catlgoParameter, elm_cveParameter, nombreParameter, statusParameter, opcionParameter);
        }
    
        public virtual ObjectResult<sp_WebAppOTSAdmOTS_Result> sp_WebAppOTSAdmOTS(string sub_sistema, string tip_OTS, string asg, Nullable<int> otsP, string otsTP, string oper, string resp, string status, string descr, string fechaIni, string fechaFin, string aplica, string tip_Proceso, string nombre_OTS, string error_OTS, string solucion_OTS, string obs_OTS, string clasi_OTS, string opcion, string nomImg, Nullable<System.DateTime> fec_prom)
        {
            var sub_sistemaParameter = sub_sistema != null ?
                new ObjectParameter("sub_sistema", sub_sistema) :
                new ObjectParameter("sub_sistema", typeof(string));
    
            var tip_OTSParameter = tip_OTS != null ?
                new ObjectParameter("tip_OTS", tip_OTS) :
                new ObjectParameter("tip_OTS", typeof(string));
    
            var asgParameter = asg != null ?
                new ObjectParameter("asg", asg) :
                new ObjectParameter("asg", typeof(string));
    
            var otsPParameter = otsP.HasValue ?
                new ObjectParameter("otsP", otsP) :
                new ObjectParameter("otsP", typeof(int));
    
            var otsTPParameter = otsTP != null ?
                new ObjectParameter("otsTP", otsTP) :
                new ObjectParameter("otsTP", typeof(string));
    
            var operParameter = oper != null ?
                new ObjectParameter("oper", oper) :
                new ObjectParameter("oper", typeof(string));
    
            var respParameter = resp != null ?
                new ObjectParameter("resp", resp) :
                new ObjectParameter("resp", typeof(string));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            var descrParameter = descr != null ?
                new ObjectParameter("descr", descr) :
                new ObjectParameter("descr", typeof(string));
    
            var fechaIniParameter = fechaIni != null ?
                new ObjectParameter("fechaIni", fechaIni) :
                new ObjectParameter("fechaIni", typeof(string));
    
            var fechaFinParameter = fechaFin != null ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(string));
    
            var aplicaParameter = aplica != null ?
                new ObjectParameter("aplica", aplica) :
                new ObjectParameter("aplica", typeof(string));
    
            var tip_ProcesoParameter = tip_Proceso != null ?
                new ObjectParameter("tip_Proceso", tip_Proceso) :
                new ObjectParameter("tip_Proceso", typeof(string));
    
            var nombre_OTSParameter = nombre_OTS != null ?
                new ObjectParameter("nombre_OTS", nombre_OTS) :
                new ObjectParameter("nombre_OTS", typeof(string));
    
            var error_OTSParameter = error_OTS != null ?
                new ObjectParameter("error_OTS", error_OTS) :
                new ObjectParameter("error_OTS", typeof(string));
    
            var solucion_OTSParameter = solucion_OTS != null ?
                new ObjectParameter("solucion_OTS", solucion_OTS) :
                new ObjectParameter("solucion_OTS", typeof(string));
    
            var obs_OTSParameter = obs_OTS != null ?
                new ObjectParameter("obs_OTS", obs_OTS) :
                new ObjectParameter("obs_OTS", typeof(string));
    
            var clasi_OTSParameter = clasi_OTS != null ?
                new ObjectParameter("clasi_OTS", clasi_OTS) :
                new ObjectParameter("clasi_OTS", typeof(string));
    
            var opcionParameter = opcion != null ?
                new ObjectParameter("opcion", opcion) :
                new ObjectParameter("opcion", typeof(string));
    
            var nomImgParameter = nomImg != null ?
                new ObjectParameter("nomImg", nomImg) :
                new ObjectParameter("nomImg", typeof(string));
    
            var fec_promParameter = fec_prom.HasValue ?
                new ObjectParameter("fec_prom", fec_prom) :
                new ObjectParameter("fec_prom", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_WebAppOTSAdmOTS_Result>("sp_WebAppOTSAdmOTS", sub_sistemaParameter, tip_OTSParameter, asgParameter, otsPParameter, otsTPParameter, operParameter, respParameter, statusParameter, descrParameter, fechaIniParameter, fechaFinParameter, aplicaParameter, tip_ProcesoParameter, nombre_OTSParameter, error_OTSParameter, solucion_OTSParameter, obs_OTSParameter, clasi_OTSParameter, opcionParameter, nomImgParameter, fec_promParameter);
        }
    
        public virtual ObjectResult<sp_WebAppOTSAdmUsers_Result> sp_WebAppOTSAdmUsers(string user_cve, string user_nom, string user_pass, Nullable<short> user_status, string user_email, string user_rol, string opcion)
        {
            var user_cveParameter = user_cve != null ?
                new ObjectParameter("user_cve", user_cve) :
                new ObjectParameter("user_cve", typeof(string));
    
            var user_nomParameter = user_nom != null ?
                new ObjectParameter("user_nom", user_nom) :
                new ObjectParameter("user_nom", typeof(string));
    
            var user_passParameter = user_pass != null ?
                new ObjectParameter("user_pass", user_pass) :
                new ObjectParameter("user_pass", typeof(string));
    
            var user_statusParameter = user_status.HasValue ?
                new ObjectParameter("user_status", user_status) :
                new ObjectParameter("user_status", typeof(short));
    
            var user_emailParameter = user_email != null ?
                new ObjectParameter("user_email", user_email) :
                new ObjectParameter("user_email", typeof(string));
    
            var user_rolParameter = user_rol != null ?
                new ObjectParameter("user_rol", user_rol) :
                new ObjectParameter("user_rol", typeof(string));
    
            var opcionParameter = opcion != null ?
                new ObjectParameter("opcion", opcion) :
                new ObjectParameter("opcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_WebAppOTSAdmUsers_Result>("sp_WebAppOTSAdmUsers", user_cveParameter, user_nomParameter, user_passParameter, user_statusParameter, user_emailParameter, user_rolParameter, opcionParameter);
        }
    
        public virtual ObjectResult<sp_WebAppOTSConsultaOTS_Result> sp_WebAppOTSConsultaOTS(string user_cve, string status, Nullable<int> opc, string tipoOTS, string user_filtro, string descr_filtro)
        {
            var user_cveParameter = user_cve != null ?
                new ObjectParameter("user_cve", user_cve) :
                new ObjectParameter("user_cve", typeof(string));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            var opcParameter = opc.HasValue ?
                new ObjectParameter("opc", opc) :
                new ObjectParameter("opc", typeof(int));
    
            var tipoOTSParameter = tipoOTS != null ?
                new ObjectParameter("tipoOTS", tipoOTS) :
                new ObjectParameter("tipoOTS", typeof(string));
    
            var user_filtroParameter = user_filtro != null ?
                new ObjectParameter("user_filtro", user_filtro) :
                new ObjectParameter("user_filtro", typeof(string));
    
            var descr_filtroParameter = descr_filtro != null ?
                new ObjectParameter("descr_filtro", descr_filtro) :
                new ObjectParameter("descr_filtro", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_WebAppOTSConsultaOTS_Result>("sp_WebAppOTSConsultaOTS", user_cveParameter, statusParameter, opcParameter, tipoOTSParameter, user_filtroParameter, descr_filtroParameter);
        }
    
        public virtual ObjectResult<sp_WebAppOTSConsultaUser_Result> sp_WebAppOTSConsultaUser(Nullable<int> status, string rol)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));
    
            var rolParameter = rol != null ?
                new ObjectParameter("rol", rol) :
                new ObjectParameter("rol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_WebAppOTSConsultaUser_Result>("sp_WebAppOTSConsultaUser", statusParameter, rolParameter);
        }
    
        public virtual ObjectResult<sp_WebAppOTSAdmParos_Result> sp_WebAppOTSAdmParos(Nullable<int> numOTS, string tipoOTS, string motivo, string opcion, Nullable<int> numReng, string user)
        {
            var numOTSParameter = numOTS.HasValue ?
                new ObjectParameter("numOTS", numOTS) :
                new ObjectParameter("numOTS", typeof(int));
    
            var tipoOTSParameter = tipoOTS != null ?
                new ObjectParameter("tipoOTS", tipoOTS) :
                new ObjectParameter("tipoOTS", typeof(string));
    
            var motivoParameter = motivo != null ?
                new ObjectParameter("motivo", motivo) :
                new ObjectParameter("motivo", typeof(string));
    
            var opcionParameter = opcion != null ?
                new ObjectParameter("opcion", opcion) :
                new ObjectParameter("opcion", typeof(string));
    
            var numRengParameter = numReng.HasValue ?
                new ObjectParameter("numReng", numReng) :
                new ObjectParameter("numReng", typeof(int));
    
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_WebAppOTSAdmParos_Result>("sp_WebAppOTSAdmParos", numOTSParameter, tipoOTSParameter, motivoParameter, opcionParameter, numRengParameter, userParameter);
        }
    }
}
