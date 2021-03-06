﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CNTS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CNTSEntities : DbContext
    {
        public CNTSEntities()
            : base("name=CNTSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<c_cp> c_cp { get; set; }
        public virtual DbSet<c_entidad_federativa> c_entidad_federativa { get; set; }
        public virtual DbSet<c_establecimiento> c_establecimiento { get; set; }
        public virtual DbSet<c_establecimiento_inicial> c_establecimiento_inicial { get; set; }
        public virtual DbSet<c_estatus_establecimiento> c_estatus_establecimiento { get; set; }
        public virtual DbSet<c_estatus_usuario> c_estatus_usuario { get; set; }
        public virtual DbSet<c_funcion> c_funcion { get; set; }
        public virtual DbSet<c_informe> c_informe { get; set; }
        public virtual DbSet<c_institucion> c_institucion { get; set; }
        public virtual DbSet<c_menu_funcion> c_menu_funcion { get; set; }
        public virtual DbSet<c_parametro> c_parametro { get; set; }
        public virtual DbSet<c_periodo> c_periodo { get; set; }
        public virtual DbSet<c_rol> c_rol { get; set; }
        public virtual DbSet<c_tipo_establecimiento> c_tipo_establecimiento { get; set; }
        public virtual DbSet<c_usuario> c_usuario { get; set; }
        public virtual DbSet<h_acceso_establecimiento> h_acceso_establecimiento { get; set; }
        public virtual DbSet<h_acceso_usuario> h_acceso_usuario { get; set; }
        public virtual DbSet<h_excepcion> h_excepcion { get; set; }
        public virtual DbSet<h_password> h_password { get; set; }
        public virtual DbSet<k_cnts_01_002> k_cnts_01_002 { get; set; }
        public virtual DbSet<k_cnts_01_003_a> k_cnts_01_003_a { get; set; }
        public virtual DbSet<k_cnts_01_003_b> k_cnts_01_003_b { get; set; }
        public virtual DbSet<k_cnts_01_003_c> k_cnts_01_003_c { get; set; }
        public virtual DbSet<k_secc_a_tabla_01> k_secc_a_tabla_01 { get; set; }
        public virtual DbSet<k_secc_a_tabla_02> k_secc_a_tabla_02 { get; set; }
        public virtual DbSet<k_secc_a_tabla_03> k_secc_a_tabla_03 { get; set; }
        public virtual DbSet<k_secc_a_tabla_04> k_secc_a_tabla_04 { get; set; }
        public virtual DbSet<k_secc_a_tabla_05> k_secc_a_tabla_05 { get; set; }
        public virtual DbSet<k_secc_a_tabla_06> k_secc_a_tabla_06 { get; set; }
        public virtual DbSet<k_secc_a_tabla_07> k_secc_a_tabla_07 { get; set; }
        public virtual DbSet<k_secc_a_tabla_08> k_secc_a_tabla_08 { get; set; }
        public virtual DbSet<k_solicitud_incorporacion> k_solicitud_incorporacion { get; set; }
        public virtual DbSet<v_establecimiento> v_establecimiento { get; set; }
    
        public virtual ObjectResult<Nullable<int>> incorpora_establecimiento(Nullable<int> id_solicitud_incorporacion)
        {
            var id_solicitud_incorporacionParameter = id_solicitud_incorporacion.HasValue ?
                new ObjectParameter("id_solicitud_incorporacion", id_solicitud_incorporacion) :
                new ObjectParameter("id_solicitud_incorporacion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("incorpora_establecimiento", id_solicitud_incorporacionParameter);
        }
    }
}
