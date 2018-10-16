using CNTS.Models;
using CNTS.Validaciones;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CNTS.Controllers
{
    [Authorize]
    [Access(Funcion = "Institucion")]
    [CustomErrorHandler]
    public class InstitucionController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: Institucion
        public ActionResult Index()
        {
            return View(db.c_institucion.ToList());
        }

        // GET: Institucion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_institucion c_institucion = db.c_institucion.Find(id);
            if (c_institucion == null)
            {
                return HttpNotFound();
            }
            return View(c_institucion);
        }

        // GET: Institucion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Institucion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_institucion,cl_institucion,nb_institucion")] c_institucion c_institucion)
        {
            if (ModelState.IsValid)
            {
                db.c_institucion.Add(c_institucion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_institucion);
        }

        // GET: Institucion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_institucion c_institucion = db.c_institucion.Find(id);
            if (c_institucion == null)
            {
                return HttpNotFound();
            }
            return View(c_institucion);
        }

        // POST: Institucion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_institucion,cl_institucion,nb_institucion")] c_institucion c_institucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_institucion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_institucion);
        }

        // GET: Institucion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_institucion c_institucion = db.c_institucion.Find(id);
            if (c_institucion == null)
            {
                return HttpNotFound();
            }
            return View(c_institucion);
        }

        // POST: Institucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_institucion c_institucion = db.c_institucion.Find(id);
            db.c_institucion.Remove(c_institucion);
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
