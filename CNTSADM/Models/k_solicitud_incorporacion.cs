//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class k_solicitud_incorporacion
    {
        public int id_solicitud_incorporacion { get; set; }
        public string uuid { get; set; }
        public string codigo_establecimiento { get; set; }
        public System.DateTime fe_solicitud { get; set; }
        public Nullable<System.DateTime> fe_incorporacion { get; set; }
        public Nullable<System.DateTime> fe_rechazo { get; set; }
        public string ds_motivo_rechazo { get; set; }
        public string folio_licencia_sanitaria { get; set; }
        public Nullable<System.DateTime> fe_emision_licencia_sanitaria { get; set; }
        public Nullable<System.DateTime> fe_vigencia_licencia_sanitaria { get; set; }
        public string folio_aviso_responsable { get; set; }
        public Nullable<System.DateTime> fe_emision_aviso_responsable { get; set; }
        public Nullable<System.DateTime> fe_vigencia_aviso_responsable { get; set; }
        public string nb_establecimiento { get; set; }
        public string razon_social { get; set; }
        public string rfc { get; set; }
        public string calle { get; set; }
        public string no_ext { get; set; }
        public string no_int { get; set; }
        public string entre_calle1 { get; set; }
        public string entre_calle2 { get; set; }
        public string cp { get; set; }
        public string colonia { get; set; }
        public string municipio { get; set; }
        public string ciudad { get; set; }
        public int id_entidad_federativa { get; set; }
        public string nb_responsable { get; set; }
        public string ap_responsable { get; set; }
        public string am_responsable { get; set; }
        public string especialidad_responsable { get; set; }
        public string cedula_responsable { get; set; }
        public string email_principal_responsable { get; set; }
        public string email_alterno_responsable { get; set; }
        public string telefono_fijo_responsable { get; set; }
        public string telefono_ext_responsable { get; set; }
        public string telefono_movil_responsable { get; set; }
        public string nb_director_unidad { get; set; }
        public string ap_director_unidad { get; set; }
        public string am_director_unidad { get; set; }
        public string especialidad_director_unidad { get; set; }
        public string cedula_director_unidad { get; set; }
        public string email_principal_director_unidad { get; set; }
        public string email_alterno_director_unidad { get; set; }
        public string telefono_fijo_director_unidad { get; set; }
        public string telefono_ext_director_unidad { get; set; }
        public string telefono_movil_director_unidad { get; set; }
        public string observaciones { get; set; }
        public string nb_archivo_1 { get; set; }
        public string nb_archivo_2 { get; set; }
        public byte[] data_archivo_1 { get; set; }
        public byte[] data_archivo_2 { get; set; }
        public bool pregunta_1 { get; set; }
        public bool pregunta_2 { get; set; }
        public int id_tipo_establecimiento { get; set; }
        public int id_institucion { get; set; }
    
        public virtual c_entidad_federativa c_entidad_federativa { get; set; }
        public virtual c_institucion c_institucion { get; set; }
        public virtual c_tipo_establecimiento c_tipo_establecimiento { get; set; }
    }
}
