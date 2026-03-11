using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Laborales
{
    public class ApiResponseHistorialCasoLaboral
    {
        public bool success { get; set; }
        public string message { get; set; }

        public int caso_id { get; set; }
        public int total { get; set; }

        public List<HistorialCasoLaboralDetalle> data { get; set; }
    }

}
