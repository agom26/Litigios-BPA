using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace AccesoDatos.Entidades.Civiles
{
    public class CasosCivilesViaApremioDataAccess
    {
        private readonly string _apiUrl = "https://bpa.com.es/peticiones-litigios/civiles/proceso_ejecucion/via_apremio.php";

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

        public async Task<ApiResponseCasosCivilesList> ListarCasosViaApremio(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos_via_apremio" },
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
                    return new ApiResponseCasosCivilesList
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

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
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponseCrearCasoCivil> CrearCasoViaApremio(CrearCasoViaApremioRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "crear_caso_via_apremio" },

                { "expediente", req.Expediente ?? "" },
                { "titulo", req.Titulo ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "usuario_creador", req.UsuarioId.ToString() },

                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },

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
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseCrearCasoCivil
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponseCrearCasoCivil>(jsonResult)
                    ?? new ApiResponseCrearCasoCivil
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoCivil
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponse<CasoCivilViaApremioDetalleData>> ObtenerCasoViaApremioPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_caso_via_apremio_por_id" },
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
                    return new ApiResponse<CasoCivilViaApremioDetalleData>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

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
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponseEditarCasoCivil> EditarCasoViaApremio(EditarCasoViaApremioRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_caso_via_apremio" },
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
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseEditarCasoCivil
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

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
                    message = "Error: " + ex.Message +
                              (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
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
                              (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

    }
}