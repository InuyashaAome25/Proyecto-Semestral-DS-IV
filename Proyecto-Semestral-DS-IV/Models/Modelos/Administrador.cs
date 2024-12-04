using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Administrador
    {
        public int IDAdim { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string UserName { get; set; }
        public string PasswordAdm { get; set; }
        public int RolID { get; set; }
    }
}