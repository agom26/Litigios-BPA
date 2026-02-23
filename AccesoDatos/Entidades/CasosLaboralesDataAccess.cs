
/*using Comun.Models;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AccesoDatos.Entidades
{
    public class CasosLaboralesDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/casos_laborales.php";

        public async Task<ApiResponseCasosLaboralesList> ListarCasosLaborales(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "action", "listar_casos_laborales" },
                    { "usuario_id", usuarioId.ToString() },
                    { "pagina", pagina.ToString() },
                    { "registros", registros.ToString() },
                    // Mandamos vacío si no hay filtro/estado, y en PHP lo conviertes a null si quieres
                    { "filtro", filtro ?? "" }
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
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
        }

        //crear
        public async Task<ApiResponseCrearCasoLaboral> CrearCasoLaboral(CrearCasoLaboralRequest req)
        {
            using (var client = new HttpClient())
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

                    // Opcional fechas
                    { "fecha", req.Fecha ?? "" },                 // "YYYY-MM-DD" o ""
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

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
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
        }

        public async Task<ApiResponse<CasoLaboralDetalleData>> ObtenerCasoLaboralPorId(int usuarioId, int casoId)
        {
            var form = new Dictionary<string, string>
            {
                ["action"] = "get_caso_laboral_por_id",
                ["usuario_id"] = usuarioId.ToString(),
                ["caso_id"] = casoId.ToString()
            };

            using var content = new FormUrlEncodedContent(form);

            var resp = await _http.PostAsync(_apiUrl, content);
            var json = await resp.Content.ReadAsStringAsync();

            var opts = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ApiResponse<CasoLaboralDetalleData>>(json, opts)
                   ?? new ApiResponse<CasoLaboralDetalleData> { success = false, message = "Respuesta inválida del servidor" };
        }
    }
}
*/

using Comun.Models;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class CasosLaboralesDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/casos_laborales.php";

        // Recomendado: reutilizar HttpClient (no crearlo en cada método)
        private static readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponseCasosLaboralesList> ListarCasosLaborales(
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
    }
}