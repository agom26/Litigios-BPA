using Comun.Models;
using Comun.Models.Casos.Civiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Civiles
{
    public class JuicioOralSIDataAccess
    {
        private readonly string _apiUrl = "https://bpa.com.es/peticiones-litigios/civiles/juicio_oral/segunda_instancia.php";

        // Recomendado: reutilizar HttpClient (no crearlo en cada método)
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

        public async Task<ApiResponseCasosCivilesList> ListarCasosCiviles(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos_civiles" },
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
                       ?? new ApiResponseCasosCivilesList { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosCivilesList
                {
                    success = false,
                    message = "Error: " + ex.Message +
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }

        // Crear caso
        public async Task<ApiResponseCrearCasoCivil> CrearCasoCivil(CrearCasoCivilRequest req)
        {
            // Helpers: convertir listas a "1,2,3"
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "crear_caso_civil" },

                // Caso
                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                // Historial
                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "usuario_creador", req.UsuarioCreador.ToString() },

                // Fechas
                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },

                // Listas PERSONAS
                { "demandantes", ToCsv(req.Demandantes) },
                { "demandados", ToCsv(req.Demandados) },
                { "terceros_interesados", ToCsv(req.TercerosInteresados) },
                { "contactos_empresa", ToCsv(req.ContactosEmpresa) },

                // Listas USUARIOS
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
                    return new ApiResponseCrearCasoCivil
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponseCrearCasoCivil>(jsonResult)
                       ?? new ApiResponseCrearCasoCivil { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoCivil
                {
                    success = false,
                    message = "Error: " + ex.Message +
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }

        // Obtener caso por ID
        public async Task<ApiResponse<CasoCivilDetalleData>> ObtenerCasoCivilPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_caso_civil_por_id" },
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
                    return new ApiResponse<CasoCivilDetalleData>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse<CasoCivilDetalleData>>(jsonResult)
                       ?? new ApiResponse<CasoCivilDetalleData> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoCivilDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message +
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponseEditarCasoCivil> EditarCasoCivil(EditarCasoCivilRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_caso_civil" },
                { "usuario_id", req.UsuarioId.ToString() },
                { "caso_id", req.CasoId.ToString() },

                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                { "guardar_historial", req.huboCambioEstado ? "1" : "0" },
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
                    ?? new ApiResponseEditarCasoCivil { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoCivil { 
                    success = false, 
                    message = "Error: " + ex.Message + 
                        (ex.InnerException != null ? " | InnerException: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoCivil(int casoId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_caso_civil" },
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
