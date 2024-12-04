using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Semestral_DS_IV.Models.Modelos
{
    public class Sobre
    {
        public string Nombre { get; set; }
        public string FotoUrl { get; set; }
        public string Descripcion { get; set; }
        public List<string> Hobbies { get; set; }
        public List<string> Logros { get; set; }
        public string DescripcionProyecto { get; set; }
    }
}