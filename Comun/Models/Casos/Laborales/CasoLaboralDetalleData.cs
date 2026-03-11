using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Laborales
{

    public class CasoLaboralDetalleData
    {
        public CasoDto caso { get; set; }
        public HistorialDto ultimo_historial { get; set; }

        // rol => lista de personas
        public Dictionary<string, List<PersonaMiniDto>> personas_por_rol { get; set; }

        // rol => lista de usuarios
        public Dictionary<string, List<UsuarioMiniDto>> usuarios_por_rol { get; set; }
    }
}
