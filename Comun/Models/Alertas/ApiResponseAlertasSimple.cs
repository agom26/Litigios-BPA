using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Alertas
{
    public class ApiResponseAlertaSimple
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public int totalNoLeidas { get; set; }
        public int actualizadas { get; set; }
        public int eliminadas { get; set; }
    }
}
