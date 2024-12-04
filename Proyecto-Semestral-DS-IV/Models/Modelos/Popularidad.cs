using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Popularidad
    {
        public int IDPopularidad { get; set; }
        public int Popularidades { get; set; }
        public int VecesPrestado { get; set; }
        public int VecesBuscado { get; set; }
        public string LibroTitulo { get; set; }
        public string ISBN { get; set; }
    }
}