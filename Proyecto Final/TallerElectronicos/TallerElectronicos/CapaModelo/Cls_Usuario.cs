﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerElectronicos.CapaModelo
{
    public class Cls_Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }

        public string estado { get; set; }

    }
}