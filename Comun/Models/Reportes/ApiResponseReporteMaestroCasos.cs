using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Reportes
{
    public class ApiResponseReporteMaestroCasos
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public int total { get; set; }
        public List<ReporteMaestroCasoItem> data { get; set; } = new List<ReporteMaestroCasoItem>();
    }
}
