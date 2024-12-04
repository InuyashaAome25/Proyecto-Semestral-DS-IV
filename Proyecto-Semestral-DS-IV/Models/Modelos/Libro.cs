using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Libro
    {
        [Key]
        public int IDLibros { get; set; }

        [Required(ErrorMessage = "El título del libro es obligatorio.")]
        [StringLength(200, ErrorMessage = "El título del libro debe tener al menos 6 caracteres.", MinimumLength = 6)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El ISBN es obligatorio.")]
        [StringLength(13, ErrorMessage = "El ISBN debe tener exactamente 13 caracteres.", MinimumLength = 13)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "La editorial es obligatoria.")]
        [StringLength(100, ErrorMessage = "La editorial debe tener al menos 2 caracteres.", MinimumLength = 2)]
        public string Editorial { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Publicación")]
        public DateTime FechaPublicacion { get; set; }

        [StringLength(1000, ErrorMessage = "La descripción del libro no puede tener más de 1000 caracteres.")]
        public string DescripcionLibro { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número positivo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public int Genero { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        public int Autor { get; set; }

        // Propiedades para mostrar los nombres de género y autor en las vistas
        public string NombreGenero { get; set; }
        public string NombreAutor { get; set; }
    }
}
