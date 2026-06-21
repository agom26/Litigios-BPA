using Comun.Models.Casos.Laborales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Comun.Models.Casos.Constitucionales
{
    public class CasoConstitucionalDetalleData
    {
        public CasoDto? caso { get; set; }
        public HistorialDto ultimo_historial { get; set; }
        [JsonConverter(typeof(DictionaryFlexibleConverter<List<PersonaMiniDto>>))]
        public Dictionary<string, List<PersonaMiniDto>> personas_por_rol { get; set; }
        [JsonConverter(typeof(DictionaryFlexibleConverter<List<UsuarioMiniDto>>))]
        public Dictionary<string, List<UsuarioMiniDto>> usuarios_por_rol { get; set; }
        public ReferenciaAmparoDto? referencia_amparo { get; set; }
    }
}
