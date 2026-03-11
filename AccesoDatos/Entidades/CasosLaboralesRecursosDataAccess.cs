using Comun.Models;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class CasosLaboralesRecursosDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/casos_laborales_recursos.php";
        private readonly string _apiUrlArchivos = "http://bpa.com.es/peticiones-litigios/archivos_casos_laborales.php";

        // Recomendado: reutilizar HttpClient (no crearlo en cada método)
        private static readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponseCasosLaboralesList> ListarCasosLaborales(
            int usuarioId,
            int pagina,
            int registros,
            string tipoRecurso,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos_laborales" },
                { "usuario_id", usuarioId.ToString() },
                { "pagina", pagina.ToString() },
                { "registros", registros.ToString() },
                { "filtro", filtro ?? "" },
                { "tipo_recurso", tipoRecurso.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseCasosLaboralesList>(jsonResult)
                       ?? new ApiResponseCasosLaboralesList { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosLaboralesList
                {
                    success = false,
                    message = "Error: " + ex.Message
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
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

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
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<CasoLaboralDetalleData>>(jsonResult)
                       ?? new ApiResponse<CasoLaboralDetalleData> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoLaboralDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
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
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseEditarCasoLaboral>(jsonResult)
                    ?? new ApiResponseEditarCasoLaboral { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoLaboral { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<ApiResponseHistorialCasoLaboral> ListarHistorialCasoLaboral(int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_historial_caso_laboral" },
                { "caso_id", casoId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseHistorialCasoLaboral>(jsonResult)
                       ?? new ApiResponseHistorialCasoLaboral { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseHistorialCasoLaboral
                {
                    success = false,
                    message = "Error: " + ex.Message
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
                var response = await _http.PostAsync(_apiUrlArchivos, content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ListarArchivosCasoLaboralResponse>(json)
                       ?? new ListarArchivosCasoLaboralResponse { success = false, message = "Respuesta vacía o inválida.", data = new List<ArchivoCasoLaboralItem>() };
            }
            catch (Exception ex)
            {
                return new ListarArchivosCasoLaboralResponse
                {
                    success = false,
                    message = "Error: " + ex.Message,
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

                var fileBytes = await File.ReadAllBytesAsync(filePath);
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                form.Add(fileContent, "archivo", Path.GetFileName(filePath));

                var response = await _http.PostAsync(_apiUrlArchivos, form);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<SubirArchivoCasoLaboralData>>(json)
                       ?? new ApiResponse<SubirArchivoCasoLaboralData> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SubirArchivoCasoLaboralData> { success = false, message = "Error: " + ex.Message };
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

                foreach (var filePath in filePaths)
                {
                    if (!File.Exists(filePath))
                        continue;

                    var fileBytes = await File.ReadAllBytesAsync(filePath);
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    form.Add(fileContent, "archivo[]", Path.GetFileName(filePath));
                }

                var response = await _http.PostAsync(_apiUrlArchivos, form);
                var json = await response.Content.ReadAsStringAsync();

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
                    message = "Error: " + ex.Message,
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
                var response = await _http.PostAsync(_apiUrlArchivos, content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<object>>(json)
                       ?? new ApiResponse<object> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { success = false, message = "Error: " + ex.Message };
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
                var response = await _http.PostAsync(_apiUrlArchivos, content);

                if (!response.IsSuccessStatusCode)
                    return new ApiResponse<string> { success = false, message = "HTTP Error: " + response.StatusCode };

                var bytes = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(saveToPath, bytes);

                return new ApiResponse<string> { success = true, message = "Archivo descargado.", data = saveToPath };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<ApiResponse<object>> EliminarHistorialCasoLaboral(int historialId, int casoId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
    {
        { "action", "eliminar_historial_caso_laboral" },
        { "historial_id", historialId.ToString() },
        { "caso_id", casoId.ToString() },
        { "usuario_id", usuarioId.ToString() }
    };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<object>>(jsonResult)
                       ?? new ApiResponse<object> { success = false, message = "Respuesta vacía o inválida." };
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


        public async Task<ApiResponse<object>> EditarHistorialCasoLaboral(EditarHistorialCasoRequest req)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_historial_caso_laboral" },
                { "historial_id", req.HistorialId.ToString() },
                { "caso_id", req.CasoId.ToString() },
                { "usuario_id", req.UsuarioId.ToString() },
                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },
                { "estado", req.Estado ?? "" },
                { "anotaciones", req.Anotaciones ?? "" }
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
