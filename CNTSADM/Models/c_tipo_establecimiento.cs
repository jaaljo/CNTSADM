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
    
    public partial class c_tipo_establecimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_tipo_establecimiento()
        {
            this.c_establecimiento = new HashSet<c_establecimiento>();
            this.k_solicitud_incorporacion = new HashSet<k_solicitud_incorporacion>();
            this.c_informe = new HashSet<c_informe>();
        }
    
        public int id_tipo_establecimiento { get; set; }
        public string cl_tipo_establecimiento { get; set; }
        public string nb_tipo_establecimiento { get; set; }
        public string codigo_tipo_establecimiento { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<c_establecimiento> c_establecimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<k_solicitud_incorporacion> k_solicitud_incorporacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<c_informe> c_informe { get; set; }
    }
}
