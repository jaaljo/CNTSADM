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
    
    public partial class c_estatus_usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_estatus_usuario()
        {
            this.c_usuario = new HashSet<c_usuario>();
        }
    
        public int id_estatus_usuario { get; set; }
        public string cl_estatus_usuario { get; set; }
        public string nb_estatus_usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<c_usuario> c_usuario { get; set; }
    }
}