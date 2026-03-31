using Comun.Models.Casos.Laborales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Civiles
{
    public class CasoCivilViaApremioDetalleData
    {
        public CasoDto? caso {  get; set; }
        public HistorialDto ultimo_historial { get; set; }
        public Dictionary<string, List<PersonaMiniDto>> personas_por_rol { get; set; }
        public Dictionary<string, List<UsuarioMiniDto>> usuarios_por_rol { get; set; }
        public CasoReferenciaComunDto? referencia_comun { get; set; }

    }
}
