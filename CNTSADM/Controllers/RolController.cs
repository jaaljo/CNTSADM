﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;
using CNTS.ViewModels;
using CNTS.Validaciones;

namespace CNTS.Controllers
{
    [Authorize]
    [Access(Funcion = "Rol")]
    [CustomErrorHandler]
    public class RolController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: Rol
        public ActionResult Index()
        {
            return View(db.c_rol.ToList());
        }

        // GET: Rol/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_rol c_rol = db.c_rol.Find(id);
            if (c_rol == null)
            {
                return HttpNotFound();
            }
            return View(c_rol);
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rol/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult Create([Bind(Include = "id_rol,cl_rol,nb_rol")] c_rol c_rol)
        {
            if (ModelState.IsValid)
            {
                db.c_rol.Add(c_rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_rol);
        }

        // GET: Rol/Create
        public ActionResult Agregar()
        {
            ViewBag.funciones = new MultiSelectList(db.c_funcion.OrderBy(x => x.c_menu_funcion.orden).OrderBy(x => x.orden), "id_funcion", "nb_funcion");
            return View();
        }

        // POST: Rol/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult Agregar([Bind(Include = "id_rol,cl_rol,nb_rol,id_funcion")] AgregarRolViewModel Rol)
        {
            c_rol c_rol = new c_rol()
            {
                cl_rol = Rol.cl_rol,
                nb_rol = Rol.nb_rol
            };
            if (ModelState.IsValid)
            {
                db.c_rol.Add(c_rol);
                if (Rol.id_funcion != null)
                {
                    foreach (int id_func in Rol.id_funcion)
                    {
                        c_funcion f = db.c_funcion.Find(id_func);
                        c_rol.c_funcion.Add(f);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.funciones = new MultiSelectList(db.c_funcion.OrderBy(x => x.c_menu_funcion.orden).OrderBy(x => x.orden), "id_funcion", "nb_funcion");
            return View(Rol);
        }

        private static c_rol GetC_rol(c_rol c_rol)
        {
            return c_rol;
        }

        // GET: Rol/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgregarRolViewModel rol = new AgregarRolViewModel();
            c_rol c_rol = db.c_rol.Find(id);
            if (c_rol == null)
            {
                return HttpNotFound();
            }
            rol.id_rol = c_rol.id_rol;
            rol.cl_rol = c_rol.cl_rol;
            rol.nb_rol = c_rol.nb_rol;

            string sql = "select id_funcion from c_funcion_rol where id_rol = " + rol.id_rol;
            var funciones = db.Database.SqlQuery<int>(sql).ToArray();
            ViewBag.funciones = new MultiSelectList(db.c_funcion.OrderBy(x => x.c_menu_funcion.orden).OrderBy(x => x.orden), "id_funcion", "nb_funcion",funciones );
            return View(rol);
        }

        // POST: Rol/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult Edit([Bind(Include = "id_rol,cl_rol,nb_rol,id_funcion")] AgregarRolViewModel Rol)
        {
            c_rol c_rol = db.c_rol.Find(Rol.id_rol);

            if (ModelState.IsValid)
            {
                c_rol.cl_rol = Rol.cl_rol;
                c_rol.nb_rol = Rol.nb_rol;
                c_rol.c_funcion.Clear();
                if (Rol.id_funcion != null)
                {
                    foreach (int id_func in Rol.id_funcion)
                    {
                        c_funcion f = db.c_funcion.Find(id_func);
                        c_rol.c_funcion.Add(f);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            string sql = "select id_funcion from c_funcion_rol where id_rol = " + Rol.id_rol;
            var funciones = db.Database.SqlQuery<int>(sql).ToArray();
            ViewBag.funciones = new MultiSelectList(db.c_funcion.OrderBy(x => x.c_menu_funcion.orden).OrderBy(x => x.orden), "id_funcion", "nb_funcion", funciones);
            return View(Rol);
        }

        // GET: Rol/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_rol c_rol = db.c_rol.Find(id);
            if (c_rol == null)
            {
                return HttpNotFound();
            }
            return View(c_rol);
        }

        // POST: Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult DeleteConfirmed(int id)
        {
            c_rol c_rol = db.c_rol.Find(id);
            c_rol.c_funcion.Clear();
            c_rol.c_usuario.Clear();
            db.c_rol.Remove(c_rol);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("CantErase", "Error", null);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AsignaUsuarios(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_rol c_rol = db.c_rol.Find(id);
            if (c_rol == null)
            {
                return HttpNotFound();
            }
            AsignaUsuarioRolViewModel Rol = new AsignaUsuarioRolViewModel();
            Rol.id_rol = c_rol.id_rol;
            ViewBag.nb_rol = c_rol.nb_rol;
            string sql = "select id_usuario from c_rol_usuario where id_rol = " + Rol.id_rol;
            var usuarios = db.Database.SqlQuery<int>(sql).ToArray();
            ViewBag.usuarios = new MultiSelectList(db.c_usuario.OrderBy(x => x.nb_usuario), "id_usuario", "nb_usuario", usuarios);
            return View(Rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult AsignaUsuarios([Bind(Include = "id_rol,id_usuario")] AsignaUsuarioRolViewModel Rol)
        {
            c_rol c_rol = db.c_rol.Find(Rol.id_rol);
            if (c_rol == null)
            {
                return HttpNotFound();
            }

            try
            {
                c_rol.c_usuario.Clear();
                if (Rol.id_usuario == null)
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                foreach (int id_usr in Rol.id_usuario)
                {
                    c_usuario u = db.c_usuario.Find(id_usr);
                    c_rol.c_usuario.Add(u);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.nb_rol = c_rol.nb_rol;
                string sql = "select id_usuario from c_rol_usuario where id_rol = " + Rol.id_rol;
                var usuarios = db.Database.SqlQuery<int>(sql).ToArray();
                ViewBag.usuarios = new MultiSelectList(db.c_usuario.OrderBy(x => x.nb_usuario), "id_usuario", "nb_usuario", usuarios);
                return View(Rol);
            }
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
