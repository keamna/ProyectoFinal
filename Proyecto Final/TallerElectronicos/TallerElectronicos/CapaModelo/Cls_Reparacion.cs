using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerElectronicos.CapaModelo
{
    public class Cls_Reparacion
    {
        public int reparacionId { get; set; }
        public int equipoId { get; set; }
        public string fechaSolicitud { get; set; }
        public string estado { get; set; }
    }
}