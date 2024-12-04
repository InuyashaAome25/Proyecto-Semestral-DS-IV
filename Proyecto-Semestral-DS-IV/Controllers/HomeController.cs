using Proyecto_Semestral_DS_IV.Models.Modelos;
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
            var model = new Sobre
            {
                Nombre = "Tu Nombre",
                FotoUrl = "/Content/imagenes/foto_perfil.jpg",
                Descripcion = "Soy desarrollador apasionado por crear soluciones innovadoras. Me encanta aprender nuevas tecnologías y colaborar en proyectos desafiantes.",
                Hobbies = new List<string> { "Programación", "Leer libros", "Viajar", "Jugar videojuegos" },
                Logros = new List<string> { "Certificación en Azure Fundamentals", "Desarrollo de una app con 1000+ descargas", "Hackathon ganador (2023)" },
                DescripcionProyecto = "Este proyecto web es una plataforma MVC que permite gestionar información de clientes, productos y órdenes, con un diseño limpio y funcional."
            };

            return View(model);
        }
        // Método GET para mostrar el formulario de contacto
        public ActionResult Contacto()
        {
            return View(new ContactoViewModel
            {
                Nombre = string.Empty,
                Email = string.Empty,
                Telefono = string.Empty,
                Mensaje = string.Empty
            });
        }
        [HttpPost]
        public ActionResult Contacto(ContactoViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                // Aquí puedes agregar lógica para enviar un correo o almacenar la consulta en una base de datos.
                TempData["Mensaje"] = "¡Gracias por contactarnos! Responderemos a la brevedad.";
                return RedirectToAction("Contacto");
            }

            return View(modelo);
        }
    }
}