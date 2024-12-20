using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerElectronicos.CapaModelo
{
    public class Cls_Asignacion
    {
        public int AsignacionId { get; set; }
        public int ReparacionId { get; set; }
        public int TecnicoId { get; set; }
        public string FechaAsignacion { get; set; }
        public string Estado { get; set; }
    }
}