using Comun.Models;
using Comun.Models.Casos.Contenciosos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Contenciosos
{
    public class CARecursoCasacionDataAccess
    {
        private readonly string _apiUrl = "https://bpa.com.es/peticiones-litigios/contenciosos/recurso_casacion.php";
        private static readonly HttpClient _http = new HttpClient();

        /* ==== LISTAR === */
        public async Task<ApiResponseCasosContenciososRCList> ListarCasos(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_recursos" },
                { "usuario_id", usuarioId.ToString() },
                { "pagina", pagina.ToString() },
                { "registros", registros.ToString() },
                { "filtro", filtro ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseCasosContenciososRCList>(json)
                    ?? new ApiResponseCasosContenciososRCList { success = false, message = "Respuesta inválida" };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosContenciososRCList { success = false, message = ex.Message };
            }
        }

        /* === CREAR === */
        public async Task<ApiResponseCrearCasoContencioso> CrearRecursoCasacion(CrearCasoContenciosoRequest req)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "crear_recurso" },

                { "caso_origen_id", req.CasoOrigenId.ToString() },
                { "expediente", req.Expediente ?? "" },
                { "usuario_creador", req.UsuarioCreador.ToString() },

                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },

                // 🔥 CLAVE NUEVA
                { "motivo_casacion", req.MotivoCasacion ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseCrearCasoContencioso>(json)
                    ?? new ApiResponseCrearCasoContencioso { success = false, message = "Respuesta inválida" };
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoContencioso { success = false, message = ex.Message };
            }
        }

        /* =========================================================
           OBTENER POR ID
        ========================================================= */
        public async Task<ApiResponse<CasoRecursoCasacionDetalleData>> ObtenerCasoContenciosoPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_recurso_por_id" },
                { "usuario_id", usuarioId.ToString() },
                { "caso_id", casoId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<CasoRecursoCasacionDetalleData>>(json)
                    ?? new ApiResponse<CasoRecursoCasacionDetalleData> { success = false, message = "Respuesta inválida" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoRecursoCasacionDetalleData> { success = false, message = ex.Message };
            }
        }

        /* =========================================================
           EDITAR
        ========================================================= */
        public async Task<ApiResponseEditarCasoContencioso> EditarRecursoCasacion(EditarCasoContenciosoRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_recurso" },
                { "usuario_id", req.UsuarioId.ToString() },
                { "caso_id", req.CasoId.ToString() },

                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                { "guardar_historial", req.HuboCambioEstado ? "1" : "0" },
                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },

                // 🔥 CLAVE NUEVA
                { "motivo_casacion", req.MotivoCasacion ?? "" },
                {"expediente_amparo", req.expediente_amparo ?? ""},

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
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseEditarCasoContencioso>(json)
                    ?? new ApiResponseEditarCasoContencioso { success = false, message = "Respuesta inválida" };
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoContencioso { success = false, message = ex.Message };
            }
        }

        /* =========================================================
           ELIMINAR
        ========================================================= */
        public async Task<ApiResponse<object>> EliminarCasoContencioso(int casoId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_recurso" },
                { "caso_id", casoId.ToString() },
                { "usuario_id", usuarioId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<object>>(json)
                    ?? new ApiResponse<object> { success = false, message = "Respuesta inválida" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { success = false, message = ex.Message };
            }
        }
    }
}