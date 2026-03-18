using Comun.Models.Casos.Laborales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Civiles
{
    public class ApiResponseHistorialCasoCivil
    {
        public bool success { get; set; }
        public string message { get; set; }

        public int caso_id { get; set; }
        public int total { get; set; }

        public List<HistorialCasoCivilDetalle> data { get; set; }
    }
}
