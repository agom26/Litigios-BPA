using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class ApiResponseEditarCasoAmparo
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public int caso_id { get; set; }
        public int? historial_id { get; set; }
    }
}
