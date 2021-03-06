﻿using CNTS.Models;
using CNTS.Validaciones;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNTS.Controllers
{
    [Authorize]
    [Access(Funcion = null)]
    [CustomErrorHandler]
    public class HomeController : Controller
    {
        //private SICIEntities db = new SICIEntities();

        public ActionResult Index()
        {
            //Obtenemos el valor de la variable de sesion
            int segundos_restantes;
            try
            {
                segundos_restantes = Int32.Parse(HttpContext.Session["STCP"].ToString());
            }
            catch
            {
                segundos_restantes = 253800;
            }
            //si quedan menos de 253800 segundos (3 días) construir el mensaje que se mostrará
            if(segundos_restantes < 253800)
            {
                int dias = segundos_restantes/86400;
                segundos_restantes = segundos_restantes % 86400;
                int horas = segundos_restantes/3600;
                segundos_restantes = segundos_restantes % 3600;
                int minutos = segundos_restantes/60;
                string mensaje = "Quedan: " + dias + " días " + horas + " horas y " + minutos + " minutos para que la contraseña actual expire";
                ViewBag.Mensaje = mensaje;
            }
            else
            {
                ViewBag.Mensaje = "false";
            }

            HttpContext.Session["STCP"] = "253800";

            return View();
        }
    }
}