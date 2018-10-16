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
using System.Data.Entity.Core;

namespace CNTS.Controllers
{
    [Authorize]
    [Access(Funcion = "Usuario")]
    [CustomErrorHandler]
    public class UsuarioController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        //Estatus de usuario
        // 1.-  Nuevo
        // 2.-  Activo
        // 3.-  Inactivo
        // 4.-  Bloqueado

        // GET: Usuario
        public ActionResult Index()
        {
            var c_usuario = db.c_usuario;
            return View(c_usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_usuario c_usuario = db.c_usuario.Find(id);
            if (c_usuario == null)
            {
                return HttpNotFound();
            }
            return View(c_usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.id_estatus_usuario = new SelectList(db.c_estatus_usuario, "id_estatus_usuario", "nb_estatus_usuario");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult Create(c_usuario c_usuario)
        {
            if (ModelState.IsValid)
            {
                c_usuario.fe_cambio_password = DateTime.Now;
                c_usuario.id_estatus_usuario = 1;
                db.c_usuario.Add(c_usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_estatus_usuario = new SelectList(db.c_estatus_usuario, "id_estatus_usuario", "nb_estatus_usuario",c_usuario.id_estatus_usuario);
            return View(c_usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_usuario c_usuario = db.c_usuario.Find(id);
            if (c_usuario == null)
            {
                return HttpNotFound();
            }
            return View(c_usuario);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult Edit(c_usuario c_usuario)
        {
            if (ModelState.IsValid)
            {
                c_usuario user = db.c_usuario.Find(c_usuario.id_usuario);

                if (c_usuario.esta_activo)
                {
                    c_usuario.id_estatus_usuario = 2;
                }
                if (user.password != c_usuario.password)
                {
                    c_usuario.password = SeguridadUtilidades.SHA256Encripta(c_usuario.password);
                    c_usuario.fe_cambio_password = DateTime.Now;
                    c_usuario.fe_ultimo_acceso = DateTime.Now;

                    //Agregamos la contraseña al historico del usuario
                    h_password Pass = new h_password();
                    Pass.id_usuario = c_usuario.id_usuario;
                    Pass.password = c_usuario.password;
                    Pass.fe_actualizacion = DateTime.Now;
                    db.h_password.Add(Pass);

                    //fijamos el id en 1 para que su estatus sea nuevo, y se requiera un cambio de contraseña
                    if (c_usuario.id_estatus_usuario != 4)
                    {
                        c_usuario.id_estatus_usuario = 1;
                    }
                }
                user.cl_usuario = c_usuario.cl_usuario;
                user.nb_usuario = c_usuario.nb_usuario;
                user.password = c_usuario.password;
                user.email_principal = c_usuario.email_principal;
                user.no_telefono = c_usuario.no_telefono;
                user.esta_activo = c_usuario.esta_activo;
                user.nb_puesto = c_usuario.nb_puesto;
                user.es_super_usuario = c_usuario.es_super_usuario;
                user.id_estatus_usuario = c_usuario.id_estatus_usuario;
                /*db.Entry(c_usuario).State = EntityState.Modified;*/
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    int ErrorCode = Int32.Parse(ex.InnerException.InnerException.HResult.ToString());

                    if (ErrorCode == -2146232060)
                    {
                        ModelState.AddModelError("es_super_usuario", "Asegurese de que la clave y el Correo electrónico principal no esten en uso.");
                    }
                    System.Console.WriteLine("");
                }
            }
            return View(c_usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id, string redirect = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_usuario c_usuario = db.c_usuario.Find(id);
            if (c_usuario == null)
            {
                return HttpNotFound();
            }

            if (redirect != null)
            {
                if (redirect != "bfo")
                {
                    //obtenemos el valor del numero de salto
                    int ns;
                    try
                    {
                        ns = (int)HttpContext.Session["JumpCounter"];
                    }
                    catch
                    {
                        ns = 0;
                    }
                    //Si ns es 0, creamos un nuevo array, agregamos la direccion actual y lo asignamos a la variable "Directions" y establecemos "JumpCounter" = 1
                    if (ns == 0)
                    {
                        List<string> directions = new List<string>();
                        directions.Add(redirect);
                        HttpContext.Session["JumpCounter"] = 1;
                        HttpContext.Session["Directions"] = directions;

                    }//En caso de que ns sea distinto a 0, obtenemos el Array "Directions", agregamos la direccion actual, aumentamos el contador y salvamos ambas variables globales
                    else
                    {
                        ns++;
                        List<string> directions = (List<string>)HttpContext.Session["Directions"];
                        directions.Add(redirect);
                        HttpContext.Session["JumpCounter"] = ns;
                        HttpContext.Session["Directions"] = directions;
                    }
                }
            }
            else
            {
                HttpContext.Session["JumpCounter"] = null;
                HttpContext.Session["Directions"] = null;
            }

            return View(c_usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult DeleteConfirmed(int id)
        {
            c_usuario c_usuario = db.c_usuario.Find(id);
            c_usuario.c_rol.Clear();



            //fijar su estado en 0
            c_usuario.esta_activo = false;
            c_usuario.id_estatus_usuario = 3;
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("CantErase", "Error", null);
            }

            //En caso de que el registro se haya eliminado correctamente, redireccionar dependiendo desde donde se haya accesado al menú de eliminar
            int ns;
            try
            {
                ns = (int)HttpContext.Session["JumpCounter"];
            }
            catch
            {
                ns = 0;
            }
            //Si ns es 0 redireccionamos al index de este controlador
            if (ns == 0)
            {
                return RedirectToAction("Index");

            }//En caso de que ns sea distinto a 0, obtenemos el Array "Directions", agregamos la direccion actual, aumentamos el contador y salvamos ambas variables globales
            else
            {
                List<string> directions = new List<string>();
                try
                {
                    directions = (List<string>)HttpContext.Session["Directions"];
                }
                catch
                {
                    directions = null;
                }

                if (directions == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string direction = directions.Last();
                    DirectionViewModel dir = Utilidades.Utilidades.getDirection(direction);
                    //disminuimos ns y eliminamos el ultimo elemento de directions
                    ns--;
                    directions.RemoveAt(ns);

                    //Guardamos ambas variables de sesion para seguir trabajando
                    HttpContext.Session["JumpCounter"] = ns;
                    HttpContext.Session["Directions"] = directions;

                    return RedirectToAction(dir.Action, dir.Controller, new { id = dir.Id, redirect = "bfo" });
                }
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

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult Agregar(c_usuario c_usuario)
        {

            if (ModelState.IsValid)
            {
                c_usuario.esta_activo = true;
                c_usuario.id_estatus_usuario = 1;
                c_usuario.password = SeguridadUtilidades.SHA256Encripta(c_usuario.password);
                c_usuario.fe_cambio_password = DateTime.Now;
                db.c_usuario.Add(c_usuario);

                try
                {
                    db.SaveChanges();

                    //Agregamos la contraseña al historico del usuario
                    h_password Pass = new h_password();
                    Pass.id_usuario = c_usuario.id_usuario;
                    Pass.password = c_usuario.password;
                    Pass.fe_actualizacion = DateTime.Now;
                    db.h_password.Add(Pass);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    int ErrorCode = Int32.Parse(ex.InnerException.InnerException.HResult.ToString());

                    if (ErrorCode == -2146232060)
                    {
                        ModelState.AddModelError("es_super_usuario", "Asegurese de que la clave y el Correo electrónico principal no esten en uso.");
                    }
                    System.Console.WriteLine("");
                }
                
            }
            return View(c_usuario);
        }

        public ActionResult AsignaRol(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_usuario c_usuario = db.c_usuario.Find(id);
            if (c_usuario == null)
            {
                return HttpNotFound();
            }
            AsignaRolUsuarioViewModel Usuario = new AsignaRolUsuarioViewModel();
            Usuario.id_usuario = c_usuario.id_usuario;
            ViewBag.nb_usuario = c_usuario.nb_usuario;
            string sql = "select id_rol from c_rol_usuario where id_usuario = " + Usuario.id_usuario;
            var roles = db.Database.SqlQuery<int>(sql).ToArray();
            ViewBag.roles = new MultiSelectList(db.c_rol.OrderBy(x => x.nb_rol), "id_rol", "nb_rol", roles);
            return View(Usuario);
        }


        public ActionResult unlockUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_usuario c_usuario = db.c_usuario.Find(id);
            if (c_usuario == null)
            {
                return HttpNotFound();
            }


            c_usuario.id_estatus_usuario = 2;
            c_usuario.esta_activo = true;
            c_usuario.fe_cambio_password = DateTime.Now;
            c_usuario.fe_ultimo_intento_acceso = DateTime.Now;
            c_usuario.no_intento_acceso = 0;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NotOnlyRead]
        public ActionResult AsignaRol([Bind(Include = "id_usuario,id_rol")] AsignaRolUsuarioViewModel Usuario)
        {
            c_usuario c_usuario = db.c_usuario.Find(Usuario.id_usuario);
            if (c_usuario == null)
            {
                return HttpNotFound();
            }

            try
            {
                c_usuario.c_rol.Clear();
                if (Usuario.id_rol == null)
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                foreach (int id_rl in Usuario.id_rol)
                {
                    c_rol r = db.c_rol.Find(id_rl);
                    c_usuario.c_rol.Add(r);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.nb_usuario = c_usuario.nb_usuario;
                string sql = "select id_rol from c_rol_usuario where id_usuario = " + Usuario.id_usuario;
                var roles = db.Database.SqlQuery<int>(sql).ToArray();
                ViewBag.roles = new MultiSelectList(db.c_rol.OrderBy(x => x.nb_rol), "id_rol", "nb_rol", roles);
                return View(Usuario);
            }
        }
    }
}
