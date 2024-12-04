using Proyecto_Semestral_DS_IV.Models.DAO;
using Proyecto_Semestral_DS_IV.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Semestral_DS_IV.Controllers
{
    public class LibroController : Controller
    {
        private clsLibros clsLibros = new clsLibros();
        private clsGenero clsGenero = new clsGenero();
        private clsAutor clsAutor = new clsAutor();

        // GET: Libro
        [Authorize]
        public ActionResult Index()
        {
            var libros = clsLibros.MostrarLibros();
            return View(libros);
        }

        // GET: Libro/Create
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Generos = new SelectList(clsGenero.MostrarGeneros(), "IDGenero", "NombreGenero");
            ViewBag.Autores = new SelectList(clsAutor.MostrarAutores(), "IDAutor", "NombreAutor");
            return View();
        }

        // POST: Libro/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Libro libro)
        {
            if (string.IsNullOrWhiteSpace(libro.Titulo) || libro.Titulo.Length < 6)
            {
                ModelState.AddModelError("Titulo", "El título del libro es obligatorio y debe tener al menos 6 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(libro.ISBN) || libro.ISBN.Length != 13)
            {
                ModelState.AddModelError("ISBN", "El ISBN es obligatorio y debe tener exactamente 13 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(libro.Editorial) || libro.Editorial.Length < 2)
            {
                ModelState.AddModelError("Editorial", "La editorial es obligatoria y debe tener al menos 2 caracteres.");
            }

            if (libro.FechaPublicacion == default(DateTime))
            {
                ModelState.AddModelError("FechaPublicacion", "La fecha de publicación es obligatoria.");
            }

            if (libro.DescripcionLibro != null && libro.DescripcionLibro.Length > 1000)
            {
                ModelState.AddModelError("DescripcionLibro", "La descripción del libro no puede tener más de 1000 caracteres.");
            }

            if (libro.Stock < 0)
            {
                ModelState.AddModelError("Stock", "El stock debe ser un número positivo.");
            }

            if (libro.Genero <= 0)
            {
                ModelState.AddModelError("Genero", "Debe seleccionar un género válido.");
            }

            if (libro.Autor <= 0)
            {
                ModelState.AddModelError("Autor", "Debe seleccionar un autor válido.");
            }

            if (ModelState.IsValid)
            {
                string mensaje;
                clsLibros.CrearLibro(libro.Titulo, libro.ISBN, libro.Editorial, libro.FechaPublicacion, libro.DescripcionLibro, libro.Stock, libro.Genero, libro.Autor, out mensaje);
                ViewBag.Message = mensaje;
                if (mensaje == "Libro creado con éxito")
                {
                    return RedirectToAction("Create");
                }
            }

            ViewBag.Generos = new SelectList(clsGenero.MostrarGeneros(), "IDGenero", "NombreGenero");
            ViewBag.Autores = new SelectList(clsAutor.MostrarAutores(), "IDAutor", "NombreAutor");
            return View(libro);
        }

        // GET: Libro/Edit/5
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var libro = clsLibros.ObtenerLibroPorId(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Generos = new SelectList(clsGenero.MostrarGeneros(), "IDGenero", "NombreGenero", libro.Genero);
            ViewBag.Autores = new SelectList(clsAutor.MostrarAutores(), "IDAutor", "NombreAutor", libro.Autor);
            return View(libro);
        }

        // POST: Libro/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Libro libro)
        {
            if (string.IsNullOrWhiteSpace(libro.Titulo) || libro.Titulo.Length < 6)
            {
                ModelState.AddModelError("Titulo", "El título del libro es obligatorio y debe tener al menos 6 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(libro.ISBN) || libro.ISBN.Length != 13)
            {
                ModelState.AddModelError("ISBN", "El ISBN es obligatorio y debe tener exactamente 13 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(libro.Editorial) || libro.Editorial.Length < 2)
            {
                ModelState.AddModelError("Editorial", "La editorial es obligatoria y debe tener al menos 2 caracteres.");
            }

            if (libro.FechaPublicacion == default(DateTime))
            {
                ModelState.AddModelError("FechaPublicacion", "La fecha de publicación es obligatoria.");
            }

            if (libro.DescripcionLibro != null && libro.DescripcionLibro.Length > 1000)
            {
                ModelState.AddModelError("DescripcionLibro", "La descripción del libro no puede tener más de 1000 caracteres.");
            }

            if (libro.Stock < 0)
            {
                ModelState.AddModelError("Stock", "El stock debe ser un número positivo.");
            }

            if (libro.Genero <= 0)
            {
                ModelState.AddModelError("Genero", "Debe seleccionar un género válido.");
            }

            if (libro.Autor <= 0)
            {
                ModelState.AddModelError("Autor", "Debe seleccionar un autor válido.");
            }

            if (ModelState.IsValid)
            {
                string mensaje;
                clsLibros.ActualizarLibro(libro.IDLibros, libro.Titulo, libro.ISBN, libro.Editorial, libro.FechaPublicacion, libro.DescripcionLibro, libro.Stock, libro.Genero, libro.Autor, out mensaje);
                ViewBag.Message = mensaje;
                if (mensaje == "Libro actualizado con éxito")
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Generos = new SelectList(clsGenero.MostrarGeneros(), "IDGenero", "NombreGenero", libro.Genero);
            ViewBag.Autores = new SelectList(clsAutor.MostrarAutores(), "IDAutor", "NombreAutor", libro.Autor);
            return View(libro);
        }

        // GET: Libro/Delete/5
        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var libro = clsLibros.ObtenerLibroPorId(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: Libro/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string mensaje;
            clsLibros.EliminarLibro(id, out mensaje);
            ViewBag.Message = mensaje;
            return RedirectToAction("Index");
        }
    }
}