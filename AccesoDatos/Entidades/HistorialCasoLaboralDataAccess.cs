using Comun.Models;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AccesoDatos.Entidades
{
    public class HistorialCasoLaboralDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/historial_caso_laboral.php";
        // Recomendado: reutilizar HttpClient (no crearlo en cada método)
        private static readonly HttpClient _http = new HttpClient();
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

        public async Task<ApiResponse<object>> TerminarCasoLaboral(
        int casoId,
        int usuarioId,
        string fecha,
        string anotaciones,
        string origen)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "terminar_caso_laboral" },
                { "caso_id", casoId.ToString() },
                { "usuario_id", usuarioId.ToString() },
                { "fecha", fecha }, // formato: yyyy-MM-dd HH:mm:ss
                { "anotaciones", anotaciones ?? "" },
                { "origen", origen }
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
