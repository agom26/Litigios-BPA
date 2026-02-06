using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models
{
    public class ApiDemandadoDetalleResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public DemandadoDetalle data { get; set; }
    }
}
