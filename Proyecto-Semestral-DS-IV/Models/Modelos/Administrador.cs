using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Administrador
    {
        [Key]
        public int IDAdmin { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(25, ErrorMessage = "El nombre debe tener como máximo 25 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(25, ErrorMessage = "El apellido debe tener como máximo 25 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [StringLength(50, ErrorMessage = "El correo debe tener como máximo 50 caracteres.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(50, ErrorMessage = "La dirección debe tener como máximo 50 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono válido.")]
        [StringLength(20, ErrorMessage = "El teléfono debe tener como máximo 20 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre de usuario debe tener como máximo 20 caracteres.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(15, ErrorMessage = "La contraseña debe tener como máximo 15 caracteres.")]
        public string PasswordAdm { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public int RolID { get; set; }
    }
}