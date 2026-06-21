using Comun.Models;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class CasosLaboralesTerminadosDataAccess
    {
        private readonly string _apiUrl = "https://bpa.com.es/peticiones-litigios/casos_laborales_terminados.php";
        private readonly string _apiUrlArchivos = "https://bpa.com.es/peticiones-litigios/archivos_casos_laborales.php";

        // Recomendado: reutilizar HttpClient (no crearlo en cada método)
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(60)
        };
        private static readonly HttpClient _httpArchivos = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(120)
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
        private async Task<HttpResponseMessage> PostFormAsyncArchivos(
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

            return await _httpArchivos.SendAsync(
                request,
                HttpCompletionOption.ResponseContentRead,
                token
            );
        }

        public async Task<ApiResponseCasosLaboralesTerminadosList> ListarCasosLaborales(
           int usuarioId,
           int pagina,
           int registros,
           string? filtro = null
       )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos_laborales" },
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
                    return new ApiResponseCasosLaboralesTerminadosList
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponseCasosLaboralesTerminadosList>(jsonResult)
                       ?? new ApiResponseCasosLaboralesTerminadosList { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosLaboralesTerminadosList
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        // Crear caso
        public async Task<ApiResponseCrearCasoLaboral> CrearCasoLaboral(CrearCasoLaboralRequest req)
        {
            // Helpers: convertir listas a "1,2,3"
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "crear_caso_laboral" },

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
                    return new ApiResponseCrearCasoLaboral
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponseCrearCasoLaboral>(jsonResult)
                       ?? new ApiResponseCrearCasoLaboral { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoLaboral
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        // Obtener caso por ID
        public async Task<ApiResponse<CasoLaboralDetalleData>> ObtenerCasoLaboralPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_caso_laboral_por_id" },
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
                    return new ApiResponse<CasoLaboralDetalleData>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse<CasoLaboralDetalleData>>(jsonResult)
                       ?? new ApiResponse<CasoLaboralDetalleData> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoLaboralDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponseEditarCasoLaboral> EditarCasoLaboral(EditarCasoLaboralRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_caso_laboral" },
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
                { "origen", req.Origen ?? "" },

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
                    return new ApiResponseEditarCasoLaboral
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {jsonResult}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponseEditarCasoLaboral>(jsonResult)
                    ?? new ApiResponseEditarCasoLaboral { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoLaboral
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoLaboral(int casoId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_caso_laboral" },
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

        // LISTAR
        public async Task<ListarArchivosCasoLaboralResponse> ListarArchivos(int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_archivos_caso_laboral" },
                { "caso_id", casoId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsyncArchivos(_apiUrlArchivos, content);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ListarArchivosCasoLaboralResponse
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {json}"
                    };
                }

                return JsonConvert.DeserializeObject<ListarArchivosCasoLaboralResponse>(json)
                       ?? new ListarArchivosCasoLaboralResponse { success = false, message = "Respuesta vacía o inválida.", data = new List<ArchivoCasoLaboralItem>() };
            }
            catch (Exception ex)
            {
                return new ListarArchivosCasoLaboralResponse
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : ""),
                    data = new List<ArchivoCasoLaboralItem>()
                };
            }
        }

        // SUBIR (multipart/form-data)
        public async Task<ApiResponse<SubirArchivoCasoLaboralData>> SubirArchivo(int casoId, string filePath)
        {
            if (!File.Exists(filePath))
                return new ApiResponse<SubirArchivoCasoLaboralData> { success = false, message = "El archivo no existe." };

            try
            {
                using var form = new MultipartFormDataContent();

                form.Add(new StringContent("subir_archivo_caso_laboral"), "action");
                form.Add(new StringContent(casoId.ToString()), "caso_id");

                await using var fs = File.OpenRead(filePath);
                using var fileContent = new StreamContent(fs);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                form.Add(fileContent, "archivo", Path.GetFileName(filePath));

                using var response = await PostFormAsyncArchivos(_apiUrlArchivos, form);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<SubirArchivoCasoLaboralData>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {json}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse<SubirArchivoCasoLaboralData>>(json)
                       ?? new ApiResponse<SubirArchivoCasoLaboralData> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SubirArchivoCasoLaboralData> { 
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }

        public async Task<ApiResponse<List<SubirArchivoCasoLaboralData>>> SubirArchivos(int casoId, List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
            {
                return new ApiResponse<List<SubirArchivoCasoLaboralData>>
                {
                    success = false,
                    message = "No se seleccionaron archivos.",
                    data = new List<SubirArchivoCasoLaboralData>()
                };
            }

            try
            {
                using var form = new MultipartFormDataContent();

                form.Add(new StringContent("subir_archivos_caso_laboral"), "action");
                form.Add(new StringContent(casoId.ToString()), "caso_id");

                int archivosAgregados = 0;
                foreach (var filePath in filePaths)
                {
                    if (!File.Exists(filePath))
                        continue;

                    var fileBytes = await File.ReadAllBytesAsync(filePath);
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    form.Add(fileContent, "archivo[]", Path.GetFileName(filePath));
                    archivosAgregados++;
                }

                if (archivosAgregados == 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoLaboralData>>
                    {
                        success = false,
                        message = "Ningún archivo seleccionado existe o pudo leerse.",
                        data = new List<SubirArchivoCasoLaboralData>()
                    };
                }

                using var response = await PostFormAsyncArchivos(_apiUrlArchivos, form);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<List<SubirArchivoCasoLaboralData>>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {json}",
                        data = new List<SubirArchivoCasoLaboralData>()
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse<List<SubirArchivoCasoLaboralData>>>(json)
                       ?? new ApiResponse<List<SubirArchivoCasoLaboralData>>
                       {
                           success = false,
                           message = "Respuesta vacía o inválida.",
                           data = new List<SubirArchivoCasoLaboralData>()
                       };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SubirArchivoCasoLaboralData>>
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : ""),
                    data = new List<SubirArchivoCasoLaboralData>()
                };
            }
        }

        // ELIMINAR
        public async Task<ApiResponse<object>> EliminarArchivo(int casoId, string archivoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_archivo_caso_laboral" },
                { "caso_id", casoId.ToString() },
                { "archivo_id", archivoId }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsyncArchivos(_apiUrlArchivos, content);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<object>
                    {
                        success = false,
                        message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}. Respuesta: {json}"
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse<object>>(json)
                       ?? new ApiResponse<object> { success = false, message = "Respuesta vacía o inválida." };
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

        // DESCARGAR (binario) -> guarda a disco
        // Nota: tu PHP devuelve binario, no JSON
        public async Task<ApiResponse<string>> DescargarArchivo(int casoId, string archivoId, string saveToPath)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "descargar_archivo_caso_laboral" },
                { "caso_id", casoId.ToString() },
                { "archivo_id", archivoId }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                using var response = await PostFormAsyncArchivos(_apiUrlArchivos, content);

                if (!response.IsSuccessStatusCode)
                    return new ApiResponse<string> { success = false, message = "HTTP Error: " + response.StatusCode };

                var contentType = response.Content.Headers.ContentType?.MediaType ?? "";

                if (contentType.Contains("application/json"))
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ApiResponse<object>>(jsonError);

                    return new ApiResponse<string>
                    {
                        success = false,
                        message = error?.message ?? "El servidor devolvió un error."
                    };
                }
                var bytes = await response.Content.ReadAsByteArrayAsync();

                if (bytes == null || bytes.Length == 0)
                {
                    return new ApiResponse<string>
                    {
                        success = false,
                        message = "El archivo descargado está vacío."
                    };
                }

                await File.WriteAllBytesAsync(saveToPath, bytes);

                return new ApiResponse<string> { success = true, message = "Archivo descargado.", data = saveToPath };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    success = false,
                    message = "Error: " + ex.Message +
                    (ex.InnerException != null ? " | Detalle: " + ex.InnerException.Message : "")
                };
            }
        }
    }
}
