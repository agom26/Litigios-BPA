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
        public async Task<ApiResponseReporteCasos> ObtenerReporteCasos(ReporteCasosRequest req)
        {
            string ToCsv(IEnumerable<int>? ids) =>
                ids == null ? "" : string.Join(",", ids.Where(x => x > 0).Distinct());

            var parameters = new Dictionary<string, string>
            {
                { "action", "reporte_casos" },

                // 🔹 básicos
                { "modulo_id", req.ModuloId?.ToString() ?? "" },
                { "expediente", req.Expediente ?? "" },
                { "juzgado", req.Juzgado ?? "" },
                { "oficial", req.Oficial ?? "" },
                { "notificador", req.Notificador ?? "" },
                { "estado", req.Estado ?? "" },
                { "causa", req.Causa ?? "" },

                // 🔹 PERSONAS
                { "demandantes", ToCsv(req.Demandantes) },
                { "demandados", ToCsv(req.Demandados) },
                { "terceros", ToCsv(req.TercerosInteresados) },
                { "contactos", ToCsv(req.ContactosEmpresa) },

                // 🔹 CONSTITUCIONAL
                { "solicitantes", ToCsv(req.Solicitantes) },
                { "autoridades", ToCsv(req.AutoridadesImpugnadas) },

                // 🔹 USUARIOS
                { "abogados_directores", ToCsv(req.AbogadosDirectores) },
                { "socios", ToCsv(req.SociosResponsables) },
                { "asistentes", ToCsv(req.AbogadosAsistentes) },

                // 🔹 REFERENCIA
                { "caso_referencia_id", req.CasoReferenciaId?.ToString() ?? "" },
                { "tipo_referencia", req.TipoReferencia ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl.Replace("amparo.php", "reportes.php"), content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseReporteCasos>(jsonResult)
                       ?? new ApiResponseReporteCasos { success = false, message = "Respuesta inválida" };
            }
            catch (Exception ex)
            {
                return new ApiResponseReporteCasos
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }
    }
}
