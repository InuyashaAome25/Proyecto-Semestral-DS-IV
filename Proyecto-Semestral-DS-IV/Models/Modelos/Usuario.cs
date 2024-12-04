using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "No se permiten caracteres especiales.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "No se permiten caracteres especiales.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "No se permiten caracteres especiales.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "No se permiten caracteres especiales.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "No se permiten caracteres especiales.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[*-_])[A-Za-z\d*-_]{8,15}$", ErrorMessage = "La contraseña debe tener al menos una letra mayúscula, un dígito y un carácter especial (*-_).")]
        public string PasswordUser { get; set; }
        public int RolID { get; set; }

        public string confirmarPassword { get; set; }
    }
}