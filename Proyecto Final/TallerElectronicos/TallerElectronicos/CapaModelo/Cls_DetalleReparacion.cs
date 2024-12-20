using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerElectronicos.CapaModelo
{
    public class Cls_DetalleReparacion
    {
        public int DetalleId { get; set; }
        public int ReparacionId { get; set; }
        public string Descripcion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Estado { get; set; }
    }
}