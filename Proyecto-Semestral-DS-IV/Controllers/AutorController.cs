using Proyecto_Semestral_DS_IV.Models.DAO;
using Proyecto_Semestral_DS_IV.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Semestral_DS_IV.Controllers
{
    public class AutorController : Controller
    {
        private clsAutor clsAutor = new clsAutor(); 
        [Authorize]
        // GET: Autor/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        // POST: Autor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor autor)
        {
            if (ModelState.IsValid)
            {
                string mensaje;
                clsAutor.CrearAutor(autor.NombreAutor,autor.ApellidoAutor,autor.Biografia, out mensaje);
                ViewBag.Message = mensaje;
                if (mensaje == "Autor creado con éxito")
                {
                    return RedirectToAction("Create");
                }
            }

            return View(autor);
        }

        // POST: Autor/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                TempData["Message"] = "ID inválido para eliminar autor.";
                return RedirectToAction("Create");
            }

            string mensaje;
            clsAutor.EliminarAutor(id.Value, out mensaje);
            TempData["Message"] = mensaje;
            return RedirectToAction("Create");
        }

        // POST: Autor/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int idAutor, string nuevoNombre, string nuevoApellido, string nuevaDescripcion)
        {
            string mensaje;
            clsAutor.ActualizarAutor(idAutor, nuevoNombre, nuevoApellido, nuevaDescripcion, out mensaje);
            TempData["Message"] = mensaje;
            return RedirectToAction("Create");
        }

        // GET: Autor/Index
        [HttpGet]
        public ActionResult Index()
        {
            var autores = clsAutor.MostrarAutores();
            return View(autores);
        }
    }
}