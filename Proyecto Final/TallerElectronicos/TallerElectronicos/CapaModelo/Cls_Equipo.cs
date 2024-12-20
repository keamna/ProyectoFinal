using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerElectronicos.CapaModelo
{
    public class Cls_Equipo
    {
        public int equipoID { get; set; }
        public string tipoEquipo { get; set; }
        public string modelo { get; set; }
        public int usuarioID { get; set; }
        public string estado { get; set; }
    }
}