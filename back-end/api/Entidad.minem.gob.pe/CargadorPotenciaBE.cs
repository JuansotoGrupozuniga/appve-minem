﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.minem.gob.pe
{
    public class CargadorPotenciaBE : BaseBE
    {
        public int ID_POTENCIA { get; set; }
        public decimal POTENCIA { get; set; }
        public string UNIDAD { get; set; }
        public int ID_CARGADOR { get; set; }
        public string FLAG_ESTADO { get; set; }
    }
}
