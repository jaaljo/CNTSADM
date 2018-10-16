using CNTS.Models;
using CNTS.Validaciones;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CNTS.Controllers
{
    [Authorize]
    [Access(Funcion = "TipoEstablecimiento")]
    [CustomErrorHandler]
    public class TipoEstablecimientoController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: TipoEstablecimiento
        public ActionResult Index()
        {
            return View(db.c_tipo_establecimiento.ToList());
        }

        // GET: TipoEstablecimiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_tipo_establecimiento c_tipo_establecimiento = db.c_tipo_establecimiento.Find(id);
            if (c_tipo_establecimiento == null)
            {
                return HttpNotFound();
            }
            return View(c_tipo_establecimiento);
        }

        // GET: TipoEstablecimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEstablecimiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_establecimiento,cl_tipo_establecimiento,nb_tipo_establecimiento")] c_tipo_establecimiento c_tipo_establecimiento)
        {
            if (ModelState.IsValid)
            {
                db.c_tipo_establecimiento.Add(c_tipo_establecimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_tipo_establecimiento);
        }

        // GET: TipoEstablecimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_tipo_establecimiento c_tipo_establecimiento = db.c_tipo_establecimiento.Find(id);
            if (c_tipo_establecimiento == null)
            {
                return HttpNotFound();
            }
            return View(c_tipo_establecimiento);
        }

        // POST: TipoEstablecimiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_establecimiento,cl_tipo_establecimiento,nb_tipo_establecimiento")] c_tipo_establecimiento c_tipo_establecimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_tipo_establecimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_tipo_establecimiento);
        }

        // GET: TipoEstablecimiento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_tipo_establecimiento c_tipo_establecimiento = db.c_tipo_establecimiento.Find(id);
            if (c_tipo_establecimiento == null)
            {
                return HttpNotFound();
            }
            return View(c_tipo_establecimiento);
        }

        // POST: TipoEstablecimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_tipo_establecimiento c_tipo_establecimiento = db.c_tipo_establecimiento.Find(id);
            db.c_tipo_establecimiento.Remove(c_tipo_establecimiento);
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
