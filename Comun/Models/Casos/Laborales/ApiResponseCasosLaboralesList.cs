using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Laborales
{
    public class ApiResponseCasosLaboralesList
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public int total { get; set; }
        public int pagina { get; set; }
        public int registros { get; set; }
        public List<CasoLaboralListItem> data { get; set; } = new();
    }
}
