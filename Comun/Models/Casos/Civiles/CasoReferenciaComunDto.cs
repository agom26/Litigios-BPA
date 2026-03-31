using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Civiles
{
    public class CasoReferenciaComunDto
    {
        public int id { get; set; }
        public int caso_referencia_id { get; set; }
        public string? tipo_referencia { get; set; }
        public string? observacion { get; set; }
        public string? expediente_referencia { get; set; }
        public string? titulo_referencia { get; set; }
        public string? juzgado_referencia { get; set; }
    }
}
