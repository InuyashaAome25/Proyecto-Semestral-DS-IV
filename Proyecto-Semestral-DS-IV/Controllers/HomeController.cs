using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Semestral_DS_IV.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Catalogo()
        {
            return View();
        }
        public ActionResult Reserva()
        {
            return View();
        }
        public ActionResult Devolucion()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contacto()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}