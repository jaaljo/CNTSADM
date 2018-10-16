using CNTS.Models;
using CNTS.Validaciones;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CNTS.Controllers
{
    [Authorize]
    [Access(Funcion = "Establecimiento")]
    [CustomErrorHandler]
    public class EstablecimientoController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: Establecimiento
        public ActionResult Index()
        {
            return View(db.v_establecimiento.ToList());
        }

        // GET: Establecimiento/Details/5
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

        // GET: Establecimiento/Create
        public ActionResult Create()
        {
            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "cl_entidad_federativa");
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion");
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento");
            return View();
        }

        // POST: Establecimiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(c_establecimiento c_establecimiento)
        {
            if (ModelState.IsValid)
            {
                db.c_establecimiento.Add(c_establecimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "cl_entidad_federativa", c_establecimiento.id_entidad_federativa);
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion", c_establecimiento.id_institucion);
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento", c_establecimiento.id_tipo_establecimiento);
            return View(c_establecimiento);
        }

        // GET: Establecimiento/Edit/5
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
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion", c_establecimiento.id_institucion);
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento", c_establecimiento.id_tipo_establecimiento);
            return View(c_establecimiento);
        }

        // POST: Establecimiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_establecimiento c_establecimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_establecimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "cl_entidad_federativa", c_establecimiento.id_entidad_federativa);
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion", c_establecimiento.id_institucion);
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento", c_establecimiento.id_tipo_establecimiento);
            return View(c_establecimiento);
        }

        // GET: Establecimiento/Delete/5
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

        // POST: Establecimiento/Delete/5
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
