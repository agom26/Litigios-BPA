using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Users
{
    public class PermisoModuloDataResponse
    {
        public int usuario_id { get; set; }
        public string usuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int modulo_id { get; set; }
        public string clave_slug { get; set; }
        public int rol_id { get; set; }
        public string nombre_rol { get; set; }
    }
}
