using Comun.Models;
using Comun.Models.Casos.Contenciosos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Contenciosos
{
    public class CATerminadosDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/contenciosos/terminados.php";

        private static readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponseCasosContenciososList> ListarCasos(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_casos" },
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

                return JsonConvert.DeserializeObject<ApiResponseCasosContenciososList>(jsonResult)
                    ?? new ApiResponseCasosContenciososList
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosContenciososList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseCrearCasoContencioso> CrearCasoContencioso(CrearCasoContenciosoRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "crear_caso_contencioso" },

                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                { "estado", req.Estado ?? "" },
                { "observaciones", req.Observaciones ?? "" },
                { "usuario_creador", req.UsuarioCreador.ToString() },

                { "fecha", req.Fecha ?? "" },
                { "fecha_vencimiento", req.FechaVencimiento ?? "" },

                { "demandantes", ToCsv(req.Demandantes) },
                { "demandados", ToCsv(req.Demandados) },
                { "terceros_interesados", ToCsv(req.TercerosInteresados) },
                { "contactos_empresa", ToCsv(req.ContactosEmpresa) },

                { "abogados_directores", ToCsv(req.AbogadosDirectores) },
                { "socios_responsables", ToCsv(req.SociosResponsables) },
                { "abogados_asistentes", ToCsv(req.AbogadosAsistentes) },

                { "recurso_revocatoria_id", req.MarcaReferenciaId.HasValue ? req.MarcaReferenciaId.Value.ToString() : "" },
                { "observacion_referencia", req.ObservacionReferencia ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseCrearCasoContencioso>(jsonResult)
                    ?? new ApiResponseCrearCasoContencioso
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoContencioso
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<CasoContenciosoDetalleData>> ObtenerCasoContenciosoPorId(int usuarioId, int casoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_caso_contencioso_por_id" },
                { "usuario_id", usuarioId.ToString() },
                { "caso_id", casoId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<CasoContenciosoDetalleData>>(jsonResult)
                    ?? new ApiResponse<CasoContenciosoDetalleData>
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoContenciosoDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseEditarCasoContencioso> EditarCasoContencioso(EditarCasoContenciosoRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "editar_caso_contencioso" },
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

                { "demandantes", ToCsv(req.Demandantes) },
                { "demandados", ToCsv(req.Demandados) },
                { "terceros_interesados", ToCsv(req.TercerosInteresados) },
                { "contactos_empresa", ToCsv(req.ContactosEmpresa) },

                { "abogados_directores", ToCsv(req.AbogadosDirectores) },
                { "socios_responsables", ToCsv(req.SociosResponsables) },
                { "abogados_asistentes", ToCsv(req.AbogadosAsistentes) },

                { "recurso_revocatoria_id", req.MarcaReferenciaId.HasValue ? req.MarcaReferenciaId.Value.ToString() : "" },
                { "observacion_referencia", req.ObservacionReferencia ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseEditarCasoContencioso>(jsonResult)
                    ?? new ApiResponseEditarCasoContencioso
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoContencioso
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoContencioso(int casoId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_caso_contencioso" },
                { "caso_id", casoId.ToString() },
                { "usuario_id", usuarioId.ToString() }
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
