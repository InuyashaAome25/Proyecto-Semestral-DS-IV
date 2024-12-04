using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Libro
    {
        public int IDLibros { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Editorial { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string DescripcionLibro { get; set; }
        public string Stock {  get; set; }
        public string Genero { get; set; }
        public string Autor { get; set; }
    }
}