using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Laborales
{
    public class CasoDto
    {
        public int id { get; set; }
        public int modulo_id { get; set; }
        public string expediente { get; set; }
        public string titulo { get; set; }
        public string? corte { get; set; }
        public string nombre_particular { get; set; }
        public string juzgado { get; set; }
        public string sala { get; set; }
        public string oficial { get; set; }
        public string notificador { get; set; }
        public string estado { get; set; }
        public string observaciones { get; set; }
        public string fecha_creacion { get; set; }
        public string? motivo { get; set; }
        public string? causa { get; set; }
    }
}
