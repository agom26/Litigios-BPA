using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models
{
    public class ApiPermisosResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<PermisoResponse> permisos { get; set; } = new List<PermisoResponse>();
    }
}
