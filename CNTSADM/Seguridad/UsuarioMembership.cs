using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CNTS.Models;
using CNTS.Utilidades;

namespace CNTS.Seguridad
{
    public class UsuarioMembership:MembershipUser
    {
        private SeguridadUtilidades utilidades = new SeguridadUtilidades();

        public int Id_usuario { get; set; }
        public string Cl_usuario { get; set; }
        public string Nb_usuario { get; set; }
        public string Password { get; set; }
        public string Email_principal { get; set; }
        public string No_telefono { get; set; }
        public bool Esta_activo { get; set; }
        public bool Es_super_usuario { get; set; }
        public string Nb_puesto { get; set; }
        public int Id_estatus_usuario { get; set; }
        public bool Solo_lectura { get; set; }
        public DateTime Fe_cambio_password { get; set; }
        public DateTime Fe_ultimo_acceso { get; set; }
        public string[] Funciones { get; set; }

        public UsuarioMembership(c_usuario us)
        {
            Id_usuario = us.id_usuario;
            Cl_usuario = us.cl_usuario;
            Nb_usuario = us.nb_usuario;
            Password = us.password;
            Email_principal = us.email_principal;
            No_telefono = us.no_telefono;
            Esta_activo = us.esta_activo;
            Es_super_usuario = us.es_super_usuario;
            Nb_puesto = us.nb_puesto;
            Id_estatus_usuario = us.id_estatus_usuario; // ?? 2;
            Fe_cambio_password = us.fe_cambio_password ?? DateTime.Now;
            try
            {
                Fe_ultimo_acceso = (DateTime)HttpContext.Current.Session["UltimoAcceso"];
            }
            catch
            {
                Fe_ultimo_acceso = DateTime.Now;
            }
            Funciones = utilidades.ObtenerFunciones(us.id_usuario);
        }
    }
}