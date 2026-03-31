using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Contenciosos
{
    public class CasoMarcaReferenciaDto
    {
        public int id { get; set; }
        public int caso_id { get; set; }

        public int recurso_revocatoria_id { get; set; }
        public string? observacion { get; set; }
    }
}
