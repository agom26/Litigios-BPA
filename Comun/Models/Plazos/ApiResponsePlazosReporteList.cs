using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Plazos
{
    public class ApiResponsePlazosReporteList
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public int total { get; set; }
        public List<PlazoReporteListItem> data { get; set; } = new();
    }
}
