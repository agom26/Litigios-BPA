using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Constitucionales;
using Comun.Models.Casos.Contenciosos;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Constitucionales
{
    public class CasosConstitucionalesAmparoDataAccess
    {
        private readonly string _apiUrl = "https://bpa.com.es/peticiones-litigios/constitucionales/amparo.php";
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

        public async Task<ApiResponseCasosLaboralesList> ListarCasosPorRama(
            int usuarioId,
            int pagina,
            int registros,
            string? rama = null,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos_rama" },
                { "usuario_id", usuarioId.ToString() },
                { "pagina", pagina.ToString() },
                { "registros", registros.ToString() },
                { "rama", rama ?? ""},
                { "filtro", filtro ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseCasosLaboralesList
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };

                }

                return JsonConvert.DeserializeObject<ApiResponseCasosLaboralesList>(jsonResult)
                       ?? new ApiResponseCasosLaboralesList { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosLaboralesList
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponseCasoRama<dynamic>> ObtenerCasoPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_caso_por_id" },
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
                    return new ApiResponseCasoRama<dynamic>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };

                }

                return JsonConvert.DeserializeObject<ApiResponseCasoRama<dynamic>>(jsonResult)
                    ?? new ApiResponseCasoRama<dynamic>
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasoRama<dynamic>
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        //crear
        public async Task<ApiResponseCrearCasoAmparo> CrearCasoAmparo(CrearCasoAmparoRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "crear_amparo" },

                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "causa", req.Causa ?? "" },

                { "caso_referencia_id", req.CasoReferenciaId.ToString() },

                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "usuario_creador", req.UsuarioCreador.ToString() },

                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },

                { "solicitantes", ToCsv(req.Solicitantes) },
                { "autoridades_impugnadas", ToCsv(req.AutoridadesImpugnadas) },
                { "terceros_interesados", ToCsv(req.TercerosInteresados) },
                { "contactos_empresa", ToCsv(req.ContactosEmpresa) },

                { "abogados_directores", ToCsv(req.AbogadosDirectores) },
                { "socios_responsables", ToCsv(req.SociosResponsables) },
                { "abogados_asistentes", ToCsv(req.AbogadosAsistentes) },
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseCrearCasoAmparo
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };

                }

                return JsonConvert.DeserializeObject<ApiResponseCrearCasoAmparo>(jsonResult)
                       ?? new ApiResponseCrearCasoAmparo
                       {
                           success = false,
                           message = "Respuesta vacía o inválida."
                       };
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoAmparo
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        //listar
        public async Task<ApiResponseCasosConstitucionalesList> ListarCasosConstitucionales(
           int usuarioId,
           int pagina,
           int registros,
           string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos_constitucionales" },
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
                    return new ApiResponseCasosConstitucionalesList
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };

                }

                return JsonConvert.DeserializeObject<ApiResponseCasosConstitucionalesList>(jsonResult)
                       ?? new ApiResponseCasosConstitucionalesList { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosConstitucionalesList
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        //editar amparo
        public async Task<ApiResponseEditarCasoAmparo> EditarCasoAmparo(EditarCasoAmparoRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_amparo" },

                { "usuario_id", req.UsuarioId.ToString() },
                { "caso_id", req.CasoId.ToString() },
                { "caso_referencia_id", req.CasoReferenciaId.ToString() },
                // 🔹 DATOS DEL CASO
                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "causa", req.Causa ?? "" },

                // 🔹 HISTORIAL (🔥 INSERT NUEVO, no update)
                { "guardar_historial", req.HuboCambioEstado ? "1" : "0" },
                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },
                { "usuario_creador", req.UsuarioId.ToString() },
                // 🔹 PERSONAS
                { "solicitantes", ToCsv(req.Solicitantes) },
                { "autoridades_impugnadas", ToCsv(req.AutoridadesImpugnadas) },
                { "terceros_interesados", ToCsv(req.TercerosInteresados) },
                { "contactos_empresa", ToCsv(req.ContactosEmpresa) },

                // 🔹 USUARIOS
                { "abogados_directores", ToCsv(req.AbogadosDirectores) },
                { "socios_responsables", ToCsv(req.SociosResponsables) },
                { "abogados_asistentes", ToCsv(req.AbogadosAsistentes) },
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponseEditarCasoAmparo
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };

                }

                return JsonConvert.DeserializeObject<ApiResponseEditarCasoAmparo>(jsonResult)
                       ?? new ApiResponseEditarCasoAmparo
                       {
                           success = false,
                           message = "Respuesta vacía o inválida."
                       };
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoAmparo
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        //obtener amparo por id
        public async Task<ApiResponse<CasoConstitucionalDetalleData>> ObtenerCasoConstitucionalPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "obtener_amparo_por_id" },
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
                    return new ApiResponse<CasoConstitucionalDetalleData>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };

                }

                return JsonConvert.DeserializeObject<ApiResponse<CasoConstitucionalDetalleData>>(jsonResult)
                    ?? new ApiResponse<CasoConstitucionalDetalleData>
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoConstitucionalDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        //eliminar
        public async Task<ApiResponse<object>> EliminarCasoConstitucional(int casoId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_caso_constitucional" },
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
                    message = "Error: " + ex.Message + (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }
    }
}
