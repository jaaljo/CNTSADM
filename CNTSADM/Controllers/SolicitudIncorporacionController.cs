using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;

namespace CNTS.Controllers
{
    [Authorize]
    public class SolicitudIncorporacionController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var solicitudes = db.k_solicitud_incorporacion.Where(b => b.fe_incorporacion == null && b.fe_rechazo == null).Include(k => k.c_entidad_federativa).Include(k => k.c_institucion).Include(k => k.c_tipo_establecimiento);
            //var k_solicitud_incorporacion = db.k_solicitud_incorporacion.Include(k => k.c_entidad_federativa).Include(k => k.c_institucion).Include(k => k.c_tipo_establecimiento);
            return View(solicitudes.ToList());
        }

        [HttpGet]
        public ActionResult Revisar(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                k_solicitud_incorporacion solicitud = db.k_solicitud_incorporacion.Find(id);
                if (solicitud == null)
                {
                    return HttpNotFound();
                }
                ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "nb_entidad_federativa", solicitud.id_entidad_federativa);
                ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "nb_institucion", solicitud.id_institucion);
                ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "nb_tipo_establecimiento", solicitud.id_tipo_establecimiento);

                // Guarda archivos
                string nombreArchivo1 = Server.MapPath("~/App_Data/Solicitudes/" + solicitud.id_solicitud_incorporacion.ToString() + ".L.pdf");
                string nombreArchivo2 = Server.MapPath("~/App_Data/Solicitudes/" + solicitud.id_solicitud_incorporacion.ToString() + ".A.pdf");

                if (!System.IO.File.Exists(nombreArchivo1)) System.IO.File.WriteAllBytes(nombreArchivo1, (byte[])solicitud.data_archivo_1);

                if (!System.IO.File.Exists(nombreArchivo2))
                {
                    byte[] dataArchivo2;
                    dataArchivo2 = (byte[])solicitud.data_archivo_2;
                    System.IO.File.WriteAllBytes(nombreArchivo2, dataArchivo2);
                }

                return View(solicitud);
            }
            catch
            {
                //ViewBag.Error = "";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Revisar(k_solicitud_incorporacion solicitud)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Mueve los archivos de la carpeta Solicitudes a Documentos


                    // Actualiza los datos de la solicitud
                    solicitud.fe_incorporacion = DateTime.Now;
                    db.Entry(solicitud).State = EntityState.Modified;
                    db.SaveChanges();

                    // Incorpora el establecimiento
                    var result = db.incorpora_establecimiento(solicitud.id_solicitud_incorporacion);
                    int id_establecimiento = result.SingleOrDefault().Value;

                    // Genera la contraseña temporal para el nuevo establecimiento
                    c_establecimiento establecimiento = db.c_establecimiento.Find(id_establecimiento);
                    string password = establecimiento.password;
                    establecimiento.password = Utilidades.SeguridadUtilidades.SHA256Encripta(password);
                    db.Entry(establecimiento).State = EntityState.Modified;
                    db.SaveChanges();

                    // Envia notificación de incorporación al establecimiento
                    string contenido =
                        "<strong>" + establecimiento.nb_establecimiento + "</strong>" +
                        "<br /> Bienvenido." +
                        "<br />Se ha autorizado tu solicitud de incorporación a la plataforma." +
                        "<br />Los datos que se han asignado son los siguientes:" +
                        "<br /><br /><ul>  Código de Establecimiento: " + establecimiento.codigo_establecimiento + "</ul>" +
                        "<br /><br />  Contraseña temporal: " + password + "</ul>" +
                        "<br /><br /> Atentamente." +
                        "<br /><br /><strong>Centro Nacional de la Transfusión Sanguínea.</strong>";

                    Utilidades.Utilidades.EnviaCorreo(solicitud.email_principal_responsable, "Confirmación de incorporación", contenido);
                }
                catch(Exception e)
                {

                }

                return RedirectToAction("Index");
            }
            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "cl_entidad_federativa", solicitud.id_entidad_federativa);
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "cl_institucion", solicitud.id_institucion);
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "cl_tipo_establecimiento", solicitud.id_tipo_establecimiento);
            return View(solicitud);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            k_solicitud_incorporacion k_solicitud_incorporacion = db.k_solicitud_incorporacion.Find(id);
            if (k_solicitud_incorporacion == null)
            {
                return HttpNotFound();
            }
            return View(k_solicitud_incorporacion);
        }

        // POST: SolicitudIncorporacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            k_solicitud_incorporacion k_solicitud_incorporacion = db.k_solicitud_incorporacion.Find(id);
            db.k_solicitud_incorporacion.Remove(k_solicitud_incorporacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private bool SaveFiles(k_solicitud_incorporacion solicitud, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, bool edit = false)
        {
            Type m_tipo = solicitud.GetType();
            PropertyInfo[] m_propiedades = m_tipo.GetProperties();
            HttpPostedFileBase[] files = { file1, file2, file3, file4 };

            if (!edit) //si estamos creando, creamos las nuevas carpetas
            {
                Directory.CreateDirectory(Server.MapPath("~/App_Data/Documentos/" + solicitud.id_solicitud_incorporacion)); //carpeta con el id del Cliente/prospecto

                string extension = "";

                //guardamos el 
                extension = (file1.FileName.Split(new char[] { '.' })).Last();
                file1.SaveAs(Server.MapPath("~/App_Data/Documentos/" + solicitud.id_solicitud_incorporacion + "/INE." + extension));
                solicitud.nb_archivo_1 = "INE." + extension;

                //guardamos el 
                extension = (file2.FileName.Split(new char[] { '.' })).Last();
                file2.SaveAs(Server.MapPath("~/App_Data/Documentos/" + solicitud.id_solicitud_incorporacion + "/CURP." + extension));
                solicitud.nb_archivo_1 = "CURP." + extension;
            }
            else
            {
                string extension = "";
                //si los archivos no son nulos, reemplazamos los existentes
                //guardamos el INE
                if (file1 != null)
                {
                    extension = (file1.FileName.Split(new char[] { '.' })).Last();
                    file1.SaveAs(Server.MapPath("~/App_Data/Documentos/" + solicitud.id_solicitud_incorporacion + "/INE." + extension));
                }

                //guardamos el CURP
                if (file2 != null)
                {
                    extension = (file2.FileName.Split(new char[] { '.' })).Last();
                    file2.SaveAs(Server.MapPath("~/App_Data/Documentos/" + solicitud.id_solicitud_incorporacion + "/CURP." + extension));
                }
            }
            db.SaveChanges();

            return true;
        }

        public FileResult DescargaPDF(int id, int tipo)
        {
            return File("~/App_Data/Solicitudes/" + id.ToString() + "." + (tipo == 1 ? "L" : "A") + ".pdf", System.Net.Mime.MediaTypeNames.Application.Octet, (tipo == 1 ? "Licencia.pdf" : "Aviso.pdf"));
        }

        public bool TrasladaPDFs(int id)
        {

            return true;
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
