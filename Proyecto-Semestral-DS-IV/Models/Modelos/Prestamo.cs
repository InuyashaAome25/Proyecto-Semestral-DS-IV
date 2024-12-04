using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Prestamo
    {
        public int IDPrestamos { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevoluvion { get; set; }
        public string LibroTitulo { get; set; }
        public string UsuarioNombre { get; set; }
        public string AdministradorNombre { get; set; }
    }
}