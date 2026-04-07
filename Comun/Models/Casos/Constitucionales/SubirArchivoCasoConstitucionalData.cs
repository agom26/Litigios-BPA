using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class SubirArchivoCasoConstitucionalData
    {
        public int caso_id { get; set; }
        public string nombre_original { get; set; }
        public string nombre_guardado { get; set; }
        public long tamano_bytes { get; set; }
    }
}
