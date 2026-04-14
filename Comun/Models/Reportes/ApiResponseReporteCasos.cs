using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Reportes
{
    public class ApiResponseReporteCasos
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public List<dynamic>? data { get; set; }
        public int total { get; set; }
    }
}
