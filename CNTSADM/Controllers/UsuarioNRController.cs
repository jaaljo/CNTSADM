using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;
using CNTS.Seguridad;
using CNTS.ViewModels;
using CNTS.Utilidades;
using CNTS.Validaciones;
using System.Web.Security;

namespace CNTS.Controllers
{
    [Authorize]
    [Access(Funcion = "UsuarioNR")]
    [CustomErrorHandler]
    public class UsuarioNRController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        
        public ActionResult ChangePassword()
        {
            int segundos_restantes;
            string scc;

            try
            {
                segundos_restantes = Int32.Parse(HttpContext.Session["STCP"].ToString());
            }
            catch
            {
                segundos_restantes = 1;
            }
            try
            {
                scc = HttpContext.Session["SCC"].ToString();
            }
            catch
            {
                scc = "false";
            }
            


            if (segundos_restantes == -1)
            {
                ViewBag.Mensaje = "Su contraseña ha caducado, por favor ingrese una nueva.";
            }
            else
            {
                ViewBag.Mensaje = "false";
            }

            if (scc == "true")
            {
                ViewBag.Mensaje = "La contraseña actual fue establecida por el administrador, por favor ingrese una nueva.";
            }
            else
            {
                ViewBag.Mensaje = "false";
            }


            IdentityPersonalizado ident = (IdentityPersonalizado)ControllerContext.HttpContext.User.Identity;
            CambiarContrasenaViewModel model = new CambiarContrasenaViewModel();
            model.original_password = ident.Password;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "original_password,password,new_password,repeat_password")] CambiarContrasenaViewModel cambio)
        {
            IdentityPersonalizado ident = (IdentityPersonalizado)ControllerContext.HttpContext.User.Identity;

            if (ModelState.IsValid) {
                string newPass = SeguridadUtilidades.SHA256Encripta(cambio.new_password);
                c_usuario c_usuario = db.c_usuario.Find(ident.Id_usuario);
                h_password Pass = new h_password();

                Pass.id_usuario = c_usuario.id_usuario;
                Pass.password = newPass;
                Pass.fe_actualizacion = DateTime.Now;
                db.h_password.Add(Pass);

                c_usuario.password = newPass;
                c_usuario.id_estatus_usuario = 2;
                c_usuario.fe_cambio_password = DateTime.Now;
                db.SaveChanges();
                HttpContext.Session["SCC"] = "false";
                return RedirectToAction("Success");
            }
            string scc;
            try
            {
                 scc = HttpContext.Session["SCC"].ToString();
            }
            catch
            {
                scc = "false";
            }
            
            if (scc == "true")
            {
                ViewBag.Mensaje = "La contraseña actual fue establecida por el administrador, por favor ingrese una nueva.";
            }
            else
            {
                ViewBag.Mensaje = "false";
            }
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Editar()
        {
            IdentityPersonalizado ident = (IdentityPersonalizado)ControllerContext.HttpContext.User.Identity;
            EditarDatosUsuarioViewModel model = new EditarDatosUsuarioViewModel();
            model.nb_usuario = ident.Nb_usuario;
            model.email_principal = ident.Email_principal;
            model.no_telefono = ident.No_telefono;
            model.nb_puesto = ident.Nb_puesto;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(EditarDatosUsuarioViewModel cambio)
        {
            IdentityPersonalizado ident = (IdentityPersonalizado)ControllerContext.HttpContext.User.Identity;

            if (ModelState.IsValid)
            {
                c_usuario c_usuario = db.c_usuario.Find(ident.Id_usuario);
                c_usuario.nb_usuario = cambio.nb_usuario;
                c_usuario.email_principal = cambio.email_principal;
                c_usuario.no_telefono = cambio.no_telefono;
                c_usuario.nb_puesto = cambio.nb_puesto;
                db.SaveChanges();

                FormsAuthentication.SignOut();

                return RedirectToAction("Index","Home","");
            }
            return View(cambio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
