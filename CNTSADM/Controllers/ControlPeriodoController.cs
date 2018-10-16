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
    public class ControlPeriodoController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: ControlPeriodo
        public ActionResult Index()
        {
            return View(db.c_periodo.ToList());
        }

        // GET: ControlPeriodo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_periodo c_periodo = db.c_periodo.Find(id);
            if (c_periodo == null)
            {
                return HttpNotFound();
            }
            return View(c_periodo);
        }

        // GET: ControlPeriodo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ControlPeriodo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_periodo,cl_periodo,nb_periodo,fe_apertura,fe_cierre,esta_activo")] c_periodo c_periodo)
        {
            if (ModelState.IsValid)
            {
                db.c_periodo.Add(c_periodo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_periodo);
        }

        // GET: ControlPeriodo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_periodo c_periodo = db.c_periodo.Find(id);
            if (c_periodo == null)
            {
                return HttpNotFound();
            }
            return View(c_periodo);
        }

        // POST: ControlPeriodo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_periodo,cl_periodo,nb_periodo,fe_apertura,fe_cierre,esta_activo")] c_periodo c_periodo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_periodo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_periodo);
        }

        // GET: ControlPeriodo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_periodo c_periodo = db.c_periodo.Find(id);
            if (c_periodo == null)
            {
                return HttpNotFound();
            }
            return View(c_periodo);
        }

        // POST: ControlPeriodo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_periodo c_periodo = db.c_periodo.Find(id);
            db.c_periodo.Remove(c_periodo);
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
