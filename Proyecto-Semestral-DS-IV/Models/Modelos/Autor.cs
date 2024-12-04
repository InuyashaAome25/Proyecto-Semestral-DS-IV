using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Autor
    {
        [Key]

        public int IDAutor { get; set; }
        [Required(ErrorMessage = "El nombre del autor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del autor debe tener al menos 6 caracteres.", MinimumLength = 6)]
        public string NombreAutor { get; set; }
        [Required(ErrorMessage = "El apellido del autor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido del autor debe tener al menos 6 caracteres.", MinimumLength = 6)]
        public string ApellidoAutor { get; set; }
        [StringLength(1000, ErrorMessage = "La biografía del autor no puede tener más de 1000 caracteres.")]
        public string Biografia { get; set; }
    }
}