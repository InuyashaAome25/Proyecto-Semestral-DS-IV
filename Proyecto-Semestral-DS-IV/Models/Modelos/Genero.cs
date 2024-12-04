using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Genero
    {
        public int IDGenero { get; set; }
        [Required(ErrorMessage = "El nombre del género es obligatorio.")]
        [MinLength(6, ErrorMessage = "El nombre del género debe tener al menos 6 caracteres.")]
        public string NombreGenero { get; set; }
    }
}