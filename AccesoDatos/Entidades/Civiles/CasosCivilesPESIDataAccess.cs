using Comun.Models;
using Comun.Models.Casos.Civiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Civiles
{
    public class CasosCivilesPESIDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/civiles/proceso_ejecucion/segunda_instancia.php";

        private static readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponseCasosCivilesList> ListarCasosSegundaInstancia(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos" },
                { "usuario_id", usuarioId.ToString() },
                { "pagina", pagina.ToString() },
                { "registros", registros.ToString() },
                { "filtro", filtro ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseCasosCivilesList>(jsonResult)
                    ?? new ApiResponseCasosCivilesList
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosCivilesList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<CasoCivilViaApremioDetalleData>> ObtenerCasoViaApremioPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_caso" },
                { "usuario_id", usuarioId.ToString() },
                { "caso_id", casoId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<CasoCivilViaApremioDetalleData>>(jsonResult)
                    ?? new ApiResponse<CasoCivilViaApremioDetalleData>
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoCivilViaApremioDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseEditarCasoCivil> EditarCasoViaApremio(EditarCasoViaApremioRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_caso" },
                { "usuario_id", req.UsuarioId.ToString() },
                { "caso_id", req.CasoId.ToString() },
                
                { "expediente", req.Expediente ?? "" },
                { "titulo", req.Titulo ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                { "guardar_historial", req.HuboCambioEstado ? "1" : "0" },
                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },
                { "origen", req.Origen ?? "" },
                { "demandantes", ToCsv(req.Demandantes) },
                { "demandados", ToCsv(req.Demandados) },
                { "terceros_interesados", ToCsv(req.TercerosInteresados) },
                { "contactos_empresa", ToCsv(req.ContactosEmpresa) },

                { "abogados_directores", ToCsv(req.AbogadosDirectores) },
                { "socios_responsables", ToCsv(req.SociosResponsables) },
                { "abogados_asistentes", ToCsv(req.AbogadosAsistentes) },

                { "caso_referencia_id", req.CasoReferenciaId.HasValue ? req.CasoReferenciaId.Value.ToString() : "" },
                { "observacion_referencia", req.ObservacionReferencia ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseEditarCasoCivil>(jsonResult)
                    ?? new ApiResponseEditarCasoCivil
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoCivil
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoViaApremio(int casoId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_caso_civil_via_apremio" },
                { "caso_id", casoId.ToString() },
                { "usuario_id", usuarioId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<object>>(jsonResult)
                    ?? new ApiResponse<object>
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }
    }
}
