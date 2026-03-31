using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Contenciosos
{
    public class HistorialCasoContenciosoDetalle
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public DateTime? fecha_vencimiento { get; set; }
        public string estado { get; set; }
        public string anotaciones { get; set; }
        public string origen { get; set; }
        public int usuario_creador_id { get; set; }
        public string usuario_creador { get; set; }
        public int? usuario_editor_id { get; set; }
        public string usuario_editor { get; set; }
        public int caso_id { get; set; }
    }
}
