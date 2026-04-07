using Comun.Models;
using Comun.Models.Casos.Constitucionales;
using Comun.Models.Casos.Contenciosos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Constitucionales
{
    public class ArchivosCasoConstitucionalDataAccess
    {
        private readonly string _apiUrlArchivos = "http://bpa.com.es/peticiones-litigios/constitucionales/archivos_casos_constitucionales.php";

        private static readonly HttpClient _http = new HttpClient();
        // LISTAR
        public async Task<ListarArchivosCasoConstitucionalResponse> ListarArchivos(int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_archivos_caso_constitucional" },
                { "caso_id", casoId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrlArchivos, content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ListarArchivosCasoConstitucionalResponse>(json)
                       ?? new ListarArchivosCasoConstitucionalResponse { success = false, message = "Respuesta vacía o inválida.", data = new List<ArchivoCasoConstitucionalItem>() };
            }
            catch (Exception ex)
            {
                return new ListarArchivosCasoConstitucionalResponse
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    data = new List<ArchivoCasoConstitucionalItem>()
                };
            }
        }

        public async Task<ApiResponse<SubirArchivoCasoConstitucionalData>> SubirArchivo(int casoId, string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return new ApiResponse<SubirArchivoCasoConstitucionalData> { success = false, message = "El archivo no existe." };

            try
            {
                using var form = new MultipartFormDataContent();

                form.Add(new StringContent("subir_archivo_caso_constitucional"), "action");
                form.Add(new StringContent(casoId.ToString()), "caso_id");

                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                form.Add(fileContent, "archivo", Path.GetFileName(filePath));

                var response = await _http.PostAsync(_apiUrlArchivos, form);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<SubirArchivoCasoConstitucionalData>>(json)
                       ?? new ApiResponse<SubirArchivoCasoConstitucionalData> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SubirArchivoCasoConstitucionalData> { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<ApiResponse<List<SubirArchivoCasoConstitucionalData>>> SubirArchivos(int casoId, List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
            {
                return new ApiResponse<List<SubirArchivoCasoConstitucionalData>>
                {
                    success = false,
                    message = "No se seleccionaron archivos.",
                    data = new List<SubirArchivoCasoConstitucionalData>()
                };
            }

            try
            {
                using var form = new MultipartFormDataContent();

                form.Add(new StringContent("subir_archivos_caso_constitucional"), "action");
                form.Add(new StringContent(casoId.ToString()), "caso_id");

                foreach (var filePath in filePaths)
                {
                    if (!System.IO.File.Exists(filePath))
                        continue;

                    var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    form.Add(fileContent, "archivo[]", Path.GetFileName(filePath));
                }

                var response = await _http.PostAsync(_apiUrlArchivos, form);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<List<SubirArchivoCasoConstitucionalData>>>(json)
                       ?? new ApiResponse<List<SubirArchivoCasoConstitucionalData>>
                       {
                           success = false,
                           message = "Respuesta vacía o inválida.",
                           data = new List<SubirArchivoCasoConstitucionalData>()
                       };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SubirArchivoCasoConstitucionalData>>
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    data = new List<SubirArchivoCasoConstitucionalData>()
                };
            }
        }

        // ELIMINAR
        public async Task<ApiResponse<object>> EliminarArchivo(int casoId, string archivoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_archivo_caso_constitucional" },
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

        public async Task<ApiResponse<string>> DescargarArchivo(int casoId, string archivoId, string saveToPath)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "descargar_archivo_caso_constitucional" },
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
                await System.IO.File.WriteAllBytesAsync(saveToPath, bytes);

                return new ApiResponse<string> { success = true, message = "Archivo descargado.", data = saveToPath };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { success = false, message = "Error: " + ex.Message };
            }
        }
    }
}
