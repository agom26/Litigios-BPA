using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Alertas
{
    public class ApiResponseAlertas
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public DataTable? Tabla { get; set; }
        public int total { get; set; }
        public int pagina { get; set; }
        public int registrosPorPagina { get; set; }
        public int totalPaginas { get; set; }
    }
}
