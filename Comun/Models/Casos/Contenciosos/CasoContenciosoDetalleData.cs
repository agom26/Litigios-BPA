using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Laborales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Comun.Models.Casos.Contenciosos
{
    public class CasoContenciosoDetalleData
    {
        public CasoDto? caso { get; set; }
        public HistorialDto ultimo_historial { get; set; }
        [JsonConverter(typeof(DictionaryFlexibleConverter<List<PersonaMiniDto>>))]
        public Dictionary<string, List<PersonaMiniDto>> personas_por_rol { get; set; }
        [JsonConverter(typeof(DictionaryFlexibleConverter<List<UsuarioMiniDto>>))]
        public Dictionary<string, List<UsuarioMiniDto>> usuarios_por_rol { get; set; }
        public CasoMarcaReferenciaDto? referencia_recurso { get; set; }
        public string motivo_casacion { get; set; }
        public CasoDto? recurso_casacion { get; set; }
    }
}
