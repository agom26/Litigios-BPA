using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models
{
    public class ApiGetModulosRoles
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<ModuloResponse> modulos { get; set; } = new List<ModuloResponse>();
        public List<RolResponse> roles { get; set; } = new List<RolResponse>();

    }
}
