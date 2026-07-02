using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Contenciosos;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Contenciosos
{
    public class CAGeneralDataAccess
    {
        private readonly string _apiUrl = "https://bpa.com.es/peticiones-litigios/contenciosos/general.php";

        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(60)
        };
        private async Task<HttpResponseMessage> PostFormAsync(
            string url,
            HttpContent content,
            CancellationToken token = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content,
                Version = HttpVersion.Version11,
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            request.Headers.ConnectionClose = true;

            return await _http.SendAsync(
                request,
                HttpCompletionOption.ResponseContentRead,
                token
            );
        }
        public async Task<ApiResponseCasosContenciososList> ListarCasos(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos_contenciosos" },
                { "usuario_id", usuarioId.ToString() },
                { "pagina", pagina.ToString() },
                { "registros", registros.ToString() },
                { "filtro", filtro ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseCasosContenciososList
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };

                }

                return JsonConvert.DeserializeObject<ApiResponseCasosContenciososList>(jsonResult)
                    ?? new ApiResponseCasosContenciososList
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosContenciososList
                {
                    success = false,
                    message = "Error: " + ex.Message +
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponseCrearCasoContencioso> CrearCasoContencioso(CrearCasoContenciosoRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "crear_caso_contencioso" },

                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "usuario_creador", req.UsuarioCreador.ToString() },

                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },

                { "demandantes", ToCsv(req.Demandantes) },
                { "demandados", ToCsv(req.Demandados) },
                { "terceros_interesados", ToCsv(req.TercerosInteresados) },
                { "contactos_empresa", ToCsv(req.ContactosEmpresa) },

                { "abogados_directores", ToCsv(req.AbogadosDirectores) },
                { "socios_responsables", ToCsv(req.SociosResponsables) },
                { "abogados_asistentes", ToCsv(req.AbogadosAsistentes) },

                { "recurso_revocatoria_id", req.MarcaReferenciaId.HasValue ? req.MarcaReferenciaId.Value.ToString() : "" },
                { "observacion_referencia", req.ObservacionReferencia ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseCrearCasoContencioso
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };

                }

                return JsonConvert.DeserializeObject<ApiResponseCrearCasoContencioso>(jsonResult)
                    ?? new ApiResponseCrearCasoContencioso
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoContencioso
                {
                    success = false,
                    message = "Error: " + ex.Message +
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponse<CasoContenciosoDetalleData>> ObtenerCasoContenciosoPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_caso_contencioso_por_id" },
                { "usuario_id", usuarioId.ToString() },
                { "caso_id", casoId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<CasoContenciosoDetalleData>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse<CasoContenciosoDetalleData>>(jsonResult)
                    ?? new ApiResponse<CasoContenciosoDetalleData>
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoContenciosoDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message +
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponseEditarCasoContencioso> EditarCasoContencioso(EditarCasoContenciosoRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_caso_contencioso" },
                { "usuario_id", req.UsuarioId.ToString() },
                { "caso_id", req.CasoId.ToString() },

                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                { "guardar_historial", req.HuboCambioEstado ? "1" : "0" },
                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },

                { "demandantes", ToCsv(req.Demandantes) },
                { "demandados", ToCsv(req.Demandados) },
                { "terceros_interesados", ToCsv(req.TercerosInteresados) },
                { "contactos_empresa", ToCsv(req.ContactosEmpresa) },

                { "abogados_directores", ToCsv(req.AbogadosDirectores) },
                { "socios_responsables", ToCsv(req.SociosResponsables) },
                { "abogados_asistentes", ToCsv(req.AbogadosAsistentes) },

                { "recurso_revocatoria_id", req.MarcaReferenciaId.HasValue ? req.MarcaReferenciaId.Value.ToString() : "" },
                { "observacion_referencia", req.ObservacionReferencia ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseEditarCasoContencioso
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponseEditarCasoContencioso>(jsonResult)
                    ?? new ApiResponseEditarCasoContencioso
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoContencioso
                {
                    success = false,
                    message = "Error: " + ex.Message +
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoContencioso(int casoId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_caso_contencioso" },
                { "caso_id", casoId.ToString() },
                { "usuario_id", usuarioId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<object>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

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
                    message = "Error: " + ex.Message +
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }
    }
}