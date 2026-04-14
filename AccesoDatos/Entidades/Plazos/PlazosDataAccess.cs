using Comun.Models;
using Comun.Models.Casos.Laborales;
using Comun.Models.Plazos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Plazos
{
    public class PlazosDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/plazos/plazos.php";

        private static readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponsePlazosList> ListarPlazos(
            int usuarioId,
            int pagina,
            int registros,
            int? modulo= null,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_plazos" },
                { "usuario_id", usuarioId.ToString() },
                { "pagina", pagina.ToString() },
                { "registros", registros.ToString() },
                { "filtro", filtro ?? "" }
            };

            if (modulo != null)
            {
                parameters.Add("modulo_id", modulo.ToString());
            }

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponsePlazosList>(jsonResult)
                       ?? new ApiResponsePlazosList { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponsePlazosList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        // Obtener plazo por ID
        public async Task<ApiResponse<PlazoDetalleData>> ObtenerPlazoPorId(int usuarioId, int historialId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_plazo_por_id" },
                { "usuario_id", usuarioId.ToString() },
                { "historial_id", historialId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<PlazoDetalleData>>(jsonResult)
                       ?? new ApiResponse<PlazoDetalleData> { success = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PlazoDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<object>> EditarPlazo(
            int usuarioId,
            int historialId,
            DateTime fechaEstado,
            string estado,
            string anotaciones,
            DateTime? fechaVencimiento = null
        )
        {
            using (var client = new HttpClient())
            {
                // Preparamos los parámetros según lo que espera PHP
                var parameters = new Dictionary<string, string>
                {
                    { "action", "editar_plazo" },
                    { "usuario_id", usuarioId.ToString() },
                    { "historial_id", historialId.ToString()},
                    { "estado", estado},
                    { "fecha", fechaEstado.Date.ToString("yyyy-MM-dd") },
                    { "anotaciones", anotaciones}
                };

                if (fechaVencimiento != null)
                {
                    parameters.Add("fecha_vencimiento", fechaVencimiento.Value.ToString("yyyy-MM-dd HH:mm"));
                }

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApiResponse<object>>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiResponse<object>
                    {
                        success = false,
                        message = "Error de conexión: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiResponsePlazosReporteList> GenerarReportePlazos(
            int usuarioId,
            int? rama = null,
            string? expediente = null,
            string? nombre = null,
            string? oficial = null,
            string? notificador = null,
            string? organoJudicial = null,
            string? tipoInstancia = null,
            string? estado = null,
            string? origen = null,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null,
            DateTime? fechaVencInicio = null,
            DateTime? fechaVencFin = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "reporte_plazos" },
                { "usuario_id", usuarioId.ToString() }
            };

            // 🔹 Filtros dinámicos (solo si tienen valor)
            if (!string.IsNullOrWhiteSpace(expediente))
                parameters.Add("expediente", expediente);

            if (!string.IsNullOrWhiteSpace(nombre))
                parameters.Add("nombre", nombre);

            if (!string.IsNullOrWhiteSpace(oficial))
                parameters.Add("oficial", oficial);

            if (!string.IsNullOrWhiteSpace(notificador))
                parameters.Add("notificador", notificador);

            if (!string.IsNullOrWhiteSpace(organoJudicial))
                parameters.Add("organo_judicial", organoJudicial);

            if (!string.IsNullOrWhiteSpace(tipoInstancia))
                parameters.Add("tipo_instancia", tipoInstancia);

            if (rama.HasValue && rama.Value != 0)
                parameters.Add("modulo_id", rama.Value.ToString());

            if (!string.IsNullOrWhiteSpace(estado))
                parameters.Add("estado", estado);

            if (!string.IsNullOrWhiteSpace(origen))
                parameters.Add("origen", origen);

            if (fechaInicio != null)
                parameters.Add("fecha_inicio", fechaInicio.Value.ToString("yyyy-MM-dd"));

            if (fechaFin != null)
                parameters.Add("fecha_fin", fechaFin.Value.ToString("yyyy-MM-dd"));

            if (fechaVencInicio != null)
                parameters.Add("fecha_venc_inicio", fechaVencInicio.Value.ToString("yyyy-MM-dd"));

            if (fechaVencFin != null)
                parameters.Add("fecha_venc_fin", fechaVencFin.Value.ToString("yyyy-MM-dd"));

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponsePlazosReporteList>(jsonResult)
                       ?? new ApiResponsePlazosReporteList
                       {
                           success = false,
                           message = "Respuesta vacía o inválida."
                       };
            }
            catch (Exception ex)
            {
                return new ApiResponsePlazosReporteList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

    }
}
