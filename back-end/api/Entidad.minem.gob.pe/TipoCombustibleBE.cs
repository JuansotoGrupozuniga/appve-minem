﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.minem.gob.pe
{
    public class TipoCombustibleBE : BaseBE
    {
        public int ID_TIPO_COMBUSTIBLE { get; set; }
        public string NOMBRE { get; set; }
        public string FLAG_ESTADO { get; set; }
    }
}
