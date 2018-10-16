using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;

namespace CNTS.Controllers
{
    public class ControlEstablecimientoController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: ControlEstablecimiento
        public ActionResult Index()
        {
            var c_establecimiento = db.c_establecimiento.Include(c => c.c_entidad_federativa).Include(c => c.c_estatus_establecimiento).Include(c => c.c_institucion).Include(c => c.c_tipo_establecimiento);
            return View(c_establecimiento.ToList());
        }

        // GET: ControlEstablecimiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_establecimiento c_establecimiento = db.c_establecimiento.Find(id);
            if (c_establecimiento == null)
            {
                return HttpNotFound();
            }
            return View(c_establecimiento);
        }

        // GET: ControlEstablecimiento/Create
        public ActionResult Create()
        {
            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "cl_entidad_federativa");
            ViewBag.id_estatus_establecimiento = new SelectList(db.c_estatus_establecimiento, "id_estatus_establecimiento", "cl_estatus_establecimiento");
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion");
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento");
            return View();
        }

        // POST: ControlEstablecimiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_establecimiento,uuid,folio_secuencial,codigo_establecimiento,fe_registro,folio_licencia_sanitaria,fe_emision_licencia_sanitaria,fe_vigencia_licencia_sanitaria,folio_aviso_responsable,fe_emision_aviso_responsable,fe_vigencia_aviso_responsable,nb_establecimiento,razon_social,rfc,calle,no_ext,no_int,entre_calle1,entre_calle2,colonia,cp,municipio,ciudad,telefono,email_principal,nb_responsable,especialidad_responsable,cedula_responsable,nb_director_unidad,especialidad_director_unidad,cedula_director_unidad,nb_contacto,observaciones,password,password_anterior,nb_archivo_1,nb_archivo_2,esta_activo,fe_ultimo_acceso,fe_cambio_password,id_tipo_establecimiento,id_institucion,id_entidad_federativa,id_estatus_establecimiento")] c_establecimiento c_establecimiento)
        {
            if (ModelState.IsValid)
            {
                db.c_establecimiento.Add(c_establecimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "cl_entidad_federativa", c_establecimiento.id_entidad_federativa);
            ViewBag.id_estatus_establecimiento = new SelectList(db.c_estatus_establecimiento, "id_estatus_establecimiento", "cl_estatus_establecimiento", c_establecimiento.id_estatus_establecimiento);
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion", c_establecimiento.id_institucion);
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento", c_establecimiento.id_tipo_establecimiento);
            return View(c_establecimiento);
        }

        // GET: ControlEstablecimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_establecimiento c_establecimiento = db.c_establecimiento.Find(id);
            if (c_establecimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "cl_entidad_federativa", c_establecimiento.id_entidad_federativa);
            ViewBag.id_estatus_establecimiento = new SelectList(db.c_estatus_establecimiento, "id_estatus_establecimiento", "cl_estatus_establecimiento", c_establecimiento.id_estatus_establecimiento);
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion", c_establecimiento.id_institucion);
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento", c_establecimiento.id_tipo_establecimiento);
            return View(c_establecimiento);
        }

        // POST: ControlEstablecimiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_establecimiento,uuid,folio_secuencial,codigo_establecimiento,fe_registro,folio_licencia_sanitaria,fe_emision_licencia_sanitaria,fe_vigencia_licencia_sanitaria,folio_aviso_responsable,fe_emision_aviso_responsable,fe_vigencia_aviso_responsable,nb_establecimiento,razon_social,rfc,calle,no_ext,no_int,entre_calle1,entre_calle2,colonia,cp,municipio,ciudad,telefono,email_principal,nb_responsable,especialidad_responsable,cedula_responsable,nb_director_unidad,especialidad_director_unidad,cedula_director_unidad,nb_contacto,observaciones,password,password_anterior,nb_archivo_1,nb_archivo_2,esta_activo,fe_ultimo_acceso,fe_cambio_password,id_tipo_establecimiento,id_institucion,id_entidad_federativa,id_estatus_establecimiento")] c_establecimiento c_establecimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_establecimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "cl_entidad_federativa", c_establecimiento.id_entidad_federativa);
            ViewBag.id_estatus_establecimiento = new SelectList(db.c_estatus_establecimiento, "id_estatus_establecimiento", "cl_estatus_establecimiento", c_establecimiento.id_estatus_establecimiento);
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion", c_establecimiento.id_institucion);
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento", c_establecimiento.id_tipo_establecimiento);
            return View(c_establecimiento);
        }

        // GET: ControlEstablecimiento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_establecimiento c_establecimiento = db.c_establecimiento.Find(id);
            if (c_establecimiento == null)
            {
                return HttpNotFound();
            }
            return View(c_establecimiento);
        }

        // POST: ControlEstablecimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_establecimiento c_establecimiento = db.c_establecimiento.Find(id);
            db.c_establecimiento.Remove(c_establecimiento);
            db.SaveChanges();
            return RedirectToAction("Index");
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
