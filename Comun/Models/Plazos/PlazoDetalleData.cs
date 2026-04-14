using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Plazos
{
    public class PlazoDetalleData
    {
        public int caso_id { get; set; }
        public string? expediente { get; set; }
        public string? nombre { get; set; }
        public string? tipo_instancia { get; set; }
        public string? organo_judicial { get; set; }
        public string? oficial { get; set; }
        public string? notificador { get; set; }
        public int historial_id { get; set; }
        public string? estado { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_vencimiento { get; set; }
        public string? anotaciones { get; set; }
        public string? origen { get; set; }
        public string? rama { get; set; }
    }
}
