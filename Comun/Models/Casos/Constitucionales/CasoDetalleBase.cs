using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class CasoDetalleBase
    {
        public int id { get; set; }
        public string expediente { get; set; }
        public string nombre_particular { get; set; }
        public string sede { get; set; }
        public string oficial { get; set; }
        public string notificador { get; set; }
        public string estado { get; set; }

        // 🔥 Solo contencioso
        public int? casacion_id { get; set; }
        public string expediente_casacion { get; set; }
        public int? tiene_casacion { get; set; }
    }
}
