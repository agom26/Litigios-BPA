using Comun.Models;
using Comun.Models.Casos.Civiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AccesoDatos.Entidades.Civiles
{
    public class ArchivosCivilesDataAccess
    {
        private readonly string _apiUrlArchivos = "http://bpa.com.es/peticiones-litigios/civiles/archivos_casos_civiles.php";
        // Recomendado: reutilizar HttpClient (no crearlo en cada método)
        private static readonly HttpClient _http = new HttpClient();
        // LISTAR
        public async Task<ListarArchivosCasoCivilResponse> ListarArchivos(int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_archivos_caso_civil" },
                { "caso_id", casoId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrlArchivos, content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ListarArchivosCasoCivilResponse>(json)
                       ?? new ListarArchivosCasoCivilResponse { success = false, message = "Respuesta vacía o inválida.", data = new List<ArchivoCasoCivilItem>() };
            }
            catch (Exception ex)
            {
                return new ListarArchivosCasoCivilResponse
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    data = new List<ArchivoCasoCivilItem>()
                };
            }
        }

        // SUBIR (multipart/form-data)
        public async Task<ApiResponse<SubirArchivoCasoCivilData>> SubirArchivo(int casoId, string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return new ApiResponse<SubirArchivoCasoCivilData> { success = false, message = "El archivo no existe." };

            try
            {
                using var form = new MultipartFormDataContent();

                form.Add(new StringContent("subir_archivo_caso_civil"), "action");
                form.Add(new StringContent(casoId.ToString()), "caso_id");

                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                form.Add(fileContent, "archivo", Path.GetFileName(filePath));

                var response = await _http.PostAsync(_apiUrlArchivos, form);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<SubirArchivoCasoCivilData>>(json)
                       ?? new ApiResponse<SubirArchivoCasoCivilData> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SubirArchivoCasoCivilData> { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<ApiResponse<List<SubirArchivoCasoCivilData>>> SubirArchivos(int casoId, List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
            {
                return new ApiResponse<List<SubirArchivoCasoCivilData>>
                {
                    success = false,
                    message = "No se seleccionaron archivos.",
                    data = new List<SubirArchivoCasoCivilData>()
                };
            }

            try
            {
                using var form = new MultipartFormDataContent();

                form.Add(new StringContent("subir_archivos_caso_civil"), "action");
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

                return JsonConvert.DeserializeObject<ApiResponse<List<SubirArchivoCasoCivilData>>>(json)
                       ?? new ApiResponse<List<SubirArchivoCasoCivilData>>
                       {
                           success = false,
                           message = "Respuesta vacía o inválida.",
                           data = new List<SubirArchivoCasoCivilData>()
                       };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SubirArchivoCasoCivilData>>
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    data = new List<SubirArchivoCasoCivilData>()
                };
            }
        }

        // ELIMINAR
        public async Task<ApiResponse<object>> EliminarArchivo(int casoId, string archivoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_archivo_caso_civil" },
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
                { "action", "descargar_archivo_caso_civil" },
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