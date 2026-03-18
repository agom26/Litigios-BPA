using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Civiles
{
    public class ArchivoCasoCivilItem
    {
        public string nombre { get; set; }
        public long tamano_bytes { get; set; }
        public string fecha { get; set; }       // "YYYY-MM-DD HH:MM:SS"
        public string archivo_id { get; set; }  // base64 del nombre
    }
}
