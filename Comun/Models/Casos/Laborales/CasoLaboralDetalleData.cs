using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Comun.Models.Casos.Laborales
{

    public class CasoLaboralDetalleData
    {
        public CasoDto caso { get; set; }
        public HistorialDto ultimo_historial { get; set; }

        // rol => lista de personas
        [JsonConverter(typeof(DictionaryFlexibleConverter<List<PersonaMiniDto>>))]
        public Dictionary<string, List<PersonaMiniDto>> personas_por_rol { get; set; }

        // rol => lista de usuarios
        [JsonConverter(typeof(DictionaryFlexibleConverter<List<UsuarioMiniDto>>))]
        public Dictionary<string, List<UsuarioMiniDto>> usuarios_por_rol { get; set; }
    }
}
