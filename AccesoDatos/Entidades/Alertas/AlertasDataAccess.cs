using Comun.Models.Alertas;
using Comun.Models.Plazos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Alertas
{
    public class AlertasDataAccess
    {
        private readonly string _apiUrl = "https://bpa.com.es/peticiones-litigios/alertas/alertas.php";
        private static readonly HttpClient _http = new HttpClient();

        private DataTable ConvertirRowsADataTable(JToken? rowsToken)
        {
            var tabla = new DataTable();

            if (rowsToken == null || rowsToken.Type != JTokenType.Array || !rowsToken.HasValues)
                return tabla;

            foreach (var item in rowsToken)
            {
                if (item is not JObject obj) continue;

                if (tabla.Columns.Count == 0)
                {
                    foreach (var prop in obj.Properties())
                    {
                        if (!tabla.Columns.Contains(prop.Name))
                            tabla.Columns.Add(prop.Name);
                    }
                }

                var row = tabla.NewRow();
                foreach (var prop in obj.Properties())
                {
                    row[prop.Name] = prop.Value.Type == JTokenType.Null
                        ? DBNull.Value
                        : prop.Value.ToString();
                }

                tabla.Rows.Add(row);
            }

            return tabla;
        }


        public async Task<ApiResponseAlertas> ListarAlertas(
            int usuarioId,
            int moduloId = 0,
            int pagina = 1,
            int registrosPorPagina = 20,
            bool soloNoLeidas = false,
            string filtro = ""
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "obtener_alertas_usuario_paginadas" },
                { "usuarioId", usuarioId.ToString() },
                { "moduloId", moduloId.ToString() },
                { "pagina", pagina.ToString() },
                { "registrosPorPagina", registrosPorPagina.ToString() },
                { "soloNoLeidas", soloNoLeidas ? "1" : "0" },
                { "filtro", filtro ?? "" }
            };

            if (moduloId > 0)
            {
                parameters.Add("modulo_id", moduloId.ToString());
            }

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseAlertas>(jsonResult)
                       ?? new ApiResponseAlertas { ok = false, message = "Respuesta vacía o inválida." };
            }
            catch (Exception ex)
            {
                return new ApiResponseAlertas
                {
                    ok = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseAlertaSimple> ContarAlertasNoLeidas(int usuarioId, int moduloId = 0)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "contar_alertas_usuario" },
                { "usuarioId", usuarioId.ToString() },
                { "moduloId", moduloId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResult))
                {
                    return new ApiResponseAlertaSimple
                    {
                        success = false,
                        message = "Respuesta vacía del servidor."
                    };
                }

                var json = JObject.Parse(jsonResult);
                bool ok = json["ok"]?.Value<bool>() ?? false;

                if (!response.IsSuccessStatusCode || !ok)
                {
                    return new ApiResponseAlertaSimple
                    {
                        success = false,
                        message = json["error"]?.ToString() ?? $"Error HTTP {response.StatusCode}"
                    };
                }

                return new ApiResponseAlertaSimple
                {
                    success = true,
                    message = "Conteo obtenido correctamente.",
                    totalNoLeidas = json["totalNoLeidas"]?.Value<int>() ?? 0
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseAlertaSimple> MarcarAlertaLeida(int alertaId, int usuarioId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "marcar_alerta_leida" },
                { "alertaId", alertaId.ToString() },
                { "usuarioId", usuarioId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResult))
                {
                    return new ApiResponseAlertaSimple
                    {
                        success = false,
                        message = "Respuesta vacía del servidor."
                    };
                }

                var json = JObject.Parse(jsonResult);
                bool ok = json["ok"]?.Value<bool>() ?? false;

                if (!response.IsSuccessStatusCode || !ok)
                {
                    return new ApiResponseAlertaSimple
                    {
                        success = false,
                        message = json["error"]?.ToString() ?? $"Error HTTP {response.StatusCode}"
                    };
                }

                return new ApiResponseAlertaSimple
                {
                    success = true,
                    message = "Alerta marcada como leída.",
                    actualizadas = json["actualizadas"]?.Value<int>() ?? 0
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseAlertaSimple> EliminarAlertasAntiguas(int usuarioId, int moduloId = 0, bool soloLeidas = true)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "eliminar_alertas_antiguas" },
                { "usuarioId", usuarioId.ToString() },
                { "moduloId", moduloId.ToString() },
                { "soloLeidas", soloLeidas ? "1" : "0" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResult))
                {
                    return new ApiResponseAlertaSimple
                    {
                        success = false,
                        message = "Respuesta vacía del servidor."
                    };
                }

                var json = JObject.Parse(jsonResult);
                bool ok = json["ok"]?.Value<bool>() ?? false;

                if (!response.IsSuccessStatusCode || !ok)
                {
                    return new ApiResponseAlertaSimple
                    {
                        success = false,
                        message = json["error"]?.ToString() ?? $"Error HTTP {response.StatusCode}"
                    };
                }

                return new ApiResponseAlertaSimple
                {
                    success = true,
                    message = "Alertas antiguas eliminadas correctamente.",
                    eliminadas = json["eliminadas"]?.Value<int>() ?? 0
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }
    }
}