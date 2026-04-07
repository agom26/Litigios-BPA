using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class ReferenciaAmparoDto
    {
        public int caso_referencia_id { get; set; }
        public string expediente_referenciado { get; set; }
        public string observacion { get; set; }
    }
}
