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
    
    public partial class h_acceso_establecimiento
    {
        public int id_acceso_establecimiento { get; set; }
        public System.DateTime fe_acceso { get; set; }
        public string nb_funcion { get; set; }
        public int id_establecimiento { get; set; }
    
        public virtual c_establecimiento c_establecimiento { get; set; }
    }
}
