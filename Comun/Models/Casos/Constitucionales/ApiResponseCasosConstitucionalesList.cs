using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class ApiResponseCasosConstitucionalesList
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public int total { get; set; }
        public int pagina { get; set; }
        public int registros { get; set; }
        public List<CasoConstitucionalListItem> data { get; set; } = new();
    }
}
