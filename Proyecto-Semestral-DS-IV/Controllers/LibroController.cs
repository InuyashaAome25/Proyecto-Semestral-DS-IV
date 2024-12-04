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
            ViewBag.Autores = new SelectList(clsAutor.MostrarAutoresParaCombobox(), "IDAutor", "NombreCompleto");
            return View();
        }

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
            ViewBag.Autores = new SelectList(clsAutor.MostrarAutoresParaCombobox(), "IDAutor", "NombreCompleto");
            return View(libro);
        }



        // Nueva acción para generar ISBN
        [Authorize]
        [HttpPost]
        public JsonResult GenerarISBN()
        {
            string isbn = "";
            Random rnd = new Random();
            for (int i = 0; i < 13; i++)
            {
                isbn += rnd.Next(0, 10).ToString();
            }
            return Json(isbn);
        }

        // Acción para buscar un libro por ID para actualizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FindBookForUpdate(int IDLibros)
        {
            var libro = clsLibros.ObtenerLibroPorId(IDLibros);
            if (libro == null)
            {
                TempData["Message"] = "No se encontró el libro con el ID proporcionado.";
                return RedirectToAction("Update");
            }

            ViewBag.Libro = libro;
            ViewBag.Generos = new SelectList(clsGenero.MostrarGeneros(), "IDGenero", "NombreGenero", libro.Genero);
            ViewBag.Autores = new SelectList(clsAutor.MostrarAutoresParaCombobox(), "IDAutor", "NombreCompleto", libro.Autor);
            return View("Update");
        }

        // Acción GET para mostrar la vista de actualización (Update)
        [HttpGet]
        [Authorize]
        public ActionResult Update()
        {
            return View();
        }

        // Acción POST para actualizar el libro
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
                TempData["Message"] = mensaje;
                if (mensaje == "Libro actualizado con éxito")
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Generos = new SelectList(clsGenero.MostrarGeneros(), "IDGenero", "NombreGenero", libro.Genero);
            ViewBag.Autores = new SelectList(clsAutor.MostrarAutoresParaCombobox(), "IDAutor", "NombreCompleto", libro.Autor);
            ViewBag.Libro = libro;  // Devolver el libro con sus datos para corregir en caso de error
            return View("Update",libro);
        }

        // Acción para buscar un libro por ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FindBook(int IDLibros)
        {
            var libro = clsLibros.ObtenerLibroPorId(IDLibros);
            if (libro == null)
            {
                TempData["Message"] = "No se encontró el libro con el ID proporcionado.";
                return RedirectToAction("Delete");
            }

            ViewBag.Libro = libro;
            return View("Delete");
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
        // POST: Libro/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? IDLibros)
        {
            if (IDLibros == null || IDLibros <= 0)
            {
                TempData["Message"] = "ID inválido para eliminar libro.";
                return RedirectToAction("Delete"); // Redirigir a la misma vista Delete
            }

            string mensaje;
            clsLibros.EliminarLibro(IDLibros.Value, out mensaje);
            TempData["Message"] = mensaje;
            return RedirectToAction("Delete");
        }
    }
}