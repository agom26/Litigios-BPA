using Comun.Models.Reportes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;


namespace AccesoDatos.Entidades.Reportes
{
    public class ReportesDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/reportes/reportes.php";
        private static readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponseReporteMaestroCasos> ObtenerReporteMaestroCasosExportacionRelacionados(ReporteMaestroCasosRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "reporte_maestro_casos_exportacion_relacionados" },

                { "usuario_id", req.UsuarioId.ToString() },
                { "modulo_id", req.ModuloId?.ToString() ?? "" },
                { "origen", req.Origen ?? "" },
                { "estado_actual", req.EstadoActual ?? "" },
                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "tipo_instancia", req.TipoInstancia ?? "" },
                { "organo_judicial", req.OrganoJudicial ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                { "abogado_director_ids", ToCsv(req.AbogadoDirectorIds) },
                { "socio_ids", ToCsv(req.SocioIds) },
                { "asistente_ids", ToCsv(req.AsistenteIds) },

                { "demandante_ids", ToCsv(req.DemandanteIds) },
                { "demandado_ids", ToCsv(req.DemandadoIds) },
                { "tercero_ids", ToCsv(req.TerceroIds) },
                { "contacto_ids", ToCsv(req.ContactoIds) },
                { "solicitante_ids", ToCsv(req.SolicitanteIds) },
                { "autoridad_ids", ToCsv(req.AutoridadIds) },

                { "tiene_referencia", req.TieneReferencia?.ToString() ?? "" },
                { "caso_referencia_id", req.CasoReferenciaId?.ToString() ?? "" },
                { "tipo_referencia", req.TipoReferencia ?? "" },

                { "solo_terminados", req.SoloTerminados.ToString() },
                { "fecha_desde", req.FechaDesde ?? "" },
                { "fecha_hasta", req.FechaHasta ?? "" },

                { "incluir_relacionados", req.IncluirRelacionados.ToString() },
                { "nivel_relacion", req.NivelRelacion.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseReporteMaestroCasos>(jsonResult)
                       ?? new ApiResponseReporteMaestroCasos
                       {
                           success = false,
                           message = "Respuesta invalida del servidor"
                       };
            }
            catch (Exception ex)
            {
                return new ApiResponseReporteMaestroCasos
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseReporteMaestroCasos> ObtenerReporteMaestroCasos(ReporteMaestroCasosRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "reporte_maestro_casos" },

                // básicos
                { "usuario_id", req.UsuarioId.ToString() },
                { "pagina", req.Pagina.ToString() },
                { "registrosPorPagina", req.RegistrosPorPagina.ToString() },

                // filtros generales
                { "modulo_id", req.ModuloId?.ToString() ?? "" },
                { "origen", req.Origen ?? "" },
                { "estado_actual", req.EstadoActual ?? "" },
                { "expediente", req.Expediente ?? "" },
                { "nombre_particular", req.NombreParticular ?? "" },
                { "tipo_instancia", req.TipoInstancia ?? "" },
                { "organo_judicial", req.OrganoJudicial ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },

                // equipo legal
                { "abogado_director_ids", ToCsv(req.AbogadoDirectorIds) },
                { "socio_ids", ToCsv(req.SocioIds) },
                { "asistente_ids", ToCsv(req.AsistenteIds) },

                // partes involucradas
                { "demandante_ids", ToCsv(req.DemandanteIds) },
                { "demandado_ids", ToCsv(req.DemandadoIds) },
                { "tercero_ids", ToCsv(req.TerceroIds) },
                { "contacto_ids", ToCsv(req.ContactoIds) },
                { "solicitante_ids", ToCsv(req.SolicitanteIds) },
                { "autoridad_ids", ToCsv(req.AutoridadIds) },

                // referencias
                { "tiene_referencia", req.TieneReferencia?.ToString() ?? "" },
                { "caso_referencia_id", req.CasoReferenciaId?.ToString() ?? "" },
                { "tipo_referencia", req.TipoReferencia ?? "" },

                // otros
                { "solo_terminados", req.SoloTerminados.ToString() },
                { "fecha_desde", req.FechaDesde ?? "" },
                { "fecha_hasta", req.FechaHasta ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseReporteMaestroCasos>(jsonResult)
                       ?? new ApiResponseReporteMaestroCasos
                       {
                           success = false,
                           message = "Respuesta invalida del servidor"
                       };
            }
            catch (Exception ex)
            {
                return new ApiResponseReporteMaestroCasos
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }
    }
}
