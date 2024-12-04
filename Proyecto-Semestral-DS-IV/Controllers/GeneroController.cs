using Proyecto_Semestral_DS_IV.Models.DAO;
using Proyecto_Semestral_DS_IV.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Semestral_DS_IV.Controllers
{
    public class GeneroController : Controller
    {
        private clsGenero clsGenero = new clsGenero();

        [Authorize]
        // GET: Genero
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        // POST: Genero/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genero genero)
        {
            if (ModelState.IsValid)
            {
                string mensaje;
                clsGenero.CrearGenero(genero.NombreGenero, out mensaje);
                ViewBag.Message = mensaje;
                if (mensaje == "Género creado con éxito")
                {
                    return RedirectToAction("Create");
                }
            }

            return View(genero);
        }

        // POST: Genero/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                TempData["Message"] = "ID inválido para eliminar género.";
                return RedirectToAction("Create");
            }

            string mensaje;
            clsGenero.EliminarGenero(id.Value, out mensaje);
            TempData["Message"] = mensaje;
            return RedirectToAction("Create");
        }

        // POST: Genero/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int idGenero, string nuevoNombre)
        {
            string mensaje;
            clsGenero.ActualizarGenero(idGenero, nuevoNombre, out mensaje);
            TempData["Message"] = mensaje;
            return RedirectToAction("Create");
        }

        // GET: Genero/Index
        [HttpGet]
        public ActionResult Index()
        {
            var generos = clsGenero.MostrarGeneros();
            return View(generos);
        }
    }
}
