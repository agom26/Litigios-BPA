using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models
{
    public class ApiGetUsuarioDetalles
    {
        public bool success { get; set; }
        public string message { get; set; } // Opcional si tu API no la devuelve siempre
        public UsuarioData data { get; set; }
    }
}
