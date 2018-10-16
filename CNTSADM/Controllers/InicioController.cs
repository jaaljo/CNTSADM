using CNTS.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CNTS.Controllers
{
    [AllowAnonymous]
    public class InicioController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: Inicio
        public ActionResult Index()
        { 
            ViewBag.Mensaje = "false";
            c_usuario model = new c_usuario();
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(c_usuario model)
        {
            string pass = model.password;

            var listUsers = db.c_usuario.ToList();

            try
            {
                model = db.c_usuario.Where(u => u.email_principal == model.email_principal).First();
            }
            catch
            {
                ViewBag.Mensaje = "false";
                ViewBag.Error = "Nombre de usuario o contraseña incorrectos.";
                return View(model);
            }

            //obtenemos El numero de intentos máximo y el tiempo entre intentos
            int NoIntentos = Utilidades.Utilidades.GetIntSecurityProp("IntentosMaximos", "3");
            int intentosRestantes = 0;

            //Tomaremos los tiempos en segundos
                int TiempoEntreIntentos = Utilidades.Utilidades.GetIntSecurityProp("TiempoEntreIntentos", "30") * 60;
                int TiempoDesdeUtimoIntento = (int)DateTime.Now.Subtract(model.fe_ultimo_intento_acceso ?? DateTime.Now).TotalSeconds;
                int TiempoSiguienteIntento = (TiempoEntreIntentos - TiempoDesdeUtimoIntento) / 60;

                if (TiempoDesdeUtimoIntento > TiempoEntreIntentos || TiempoDesdeUtimoIntento < 1)
                {
                    model.no_intento_acceso = 0;
                }

                if (model.no_intento_acceso < NoIntentos)
                {
                    model.no_intento_acceso++;
                    model.fe_ultimo_intento_acceso = DateTime.Now;
                }
                intentosRestantes = NoIntentos - model.no_intento_acceso;



            if (Membership.ValidateUser(model.email_principal, pass) && ((model.no_intento_acceso < NoIntentos) || (model.es_super_usuario) ))
            {
                model.fe_ultimo_intento_acceso = DateTime.Now;
                model.no_intento_acceso = 0;

                db.SaveChanges();

                int aux;
                int TiempoSesion = Int32.TryParse(Utilidades.Utilidades.GetSecurityProp("TiempoSesion","20"), out aux) ? ((aux < 0) ? 30 : aux) : (30);

                //Variable de sesion para realizar 1 vez la validacion de la caducidad de la contraseña
                FormsAuthentication.SetAuthCookie(model.email_principal, false);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.email_principal, DateTime.Now, DateTime.Now.AddMinutes(TiempoSesion), false, model.id_usuario.ToString());
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { Expires = ticket.Expiration });
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Redirect(FormsAuthentication.GetRedirectUrl(model.email_principal, false));
                HttpContext.Session["CHECKEDPASS"] = false;
                //Seconds To Change Password
                //Si quedan menos de 3 días para cambiar la contraseña, se mostrara un aviso en la pantalla de inicio
                HttpContext.Session["STCP"] = "253800";
                HttpContext.Session["SCC"] = "false";

                HttpContext.Session.Timeout = 30;

                var cadena = HttpContext.Session["SCC"].ToString();
                return null;
            }
            //en caso de que ValidateUser retorne false, verificar si el usuario esta bloqueado

            if (model.id_estatus_usuario == 4)
            {
                ViewBag.Mensaje = "Su usuario se encuentra bloqueado, por favor acuda con el Administrador del sistema";
            }
            else
            {
                ViewBag.Mensaje = "false";
                if (intentosRestantes == 0)
                {
                    if (Utilidades.Utilidades.GetBoolSecurityProp("BSI","false"))
                    {
                        //Si está activada la opcion BSI el usuario será bloqueado
                        model.id_estatus_usuario = 4;
                        ViewBag.Mensaje = "Su usuario se encuentra bloqueado, por favor acuda con el Administrador del sistema";
                    }
                }

                
                if (intentosRestantes != 0)
                {
                    ViewBag.Error = "Nombre de usuario o contraseña incorrectos.\nQuedan " + intentosRestantes + " intentos para iniciar sesión";
                }
                else
                {
                    if (TiempoEntreIntentos != 0)
                    {
                        ViewBag.Error = "Ha superado el numero de intentos máximo. Espere " + (TiempoSiguienteIntento + 1) + " Minutos para volver a intentarlo";
                    }
                    else
                    {
                        ViewBag.Error = "Su usuario supero el número de intentos permitidos y fue bloqueado, por favor acuda con el Administrador del sistema";
                    }
                }
            }
            db.SaveChanges();
            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}