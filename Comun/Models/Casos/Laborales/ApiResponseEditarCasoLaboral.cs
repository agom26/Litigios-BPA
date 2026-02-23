using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Laborales
{
    public class ApiResponseEditarCasoLaboral
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public int caso_id { get; set; }
        public int? historial_id { get; set; }
    }
}
