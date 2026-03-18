using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Laborales
{
    public class HistorialDto
    {
        public int id { get; set; }
        public string fecha { get; set; }              // "YYYY-MM-DD HH:mm:ss"
        public string fecha_vencimiento { get; set; }  // puede venir null
        public string estado { get; set; }
        public string anotaciones { get; set; }
        public int usuario_creador { get; set; }
        public string origen {  get; set; }
    }
}
