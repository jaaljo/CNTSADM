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
    
    public partial class c_establecimiento_inicial
    {
        public int id_establecimiento_inicial { get; set; }
        public string codigo_establecimiento { get; set; }
        public string licencia_sanitaria { get; set; }
        public string nb_establecimiento { get; set; }
        public string calle { get; set; }
        public string colonia { get; set; }
        public string cp { get; set; }
        public string municipio { get; set; }
        public string ciudad { get; set; }
        public string entidad_federativa { get; set; }
        public Nullable<int> id_tipo_establecimiento { get; set; }
        public Nullable<int> id_institucion { get; set; }
        public Nullable<int> id_entidad_federativa { get; set; }
    }
}