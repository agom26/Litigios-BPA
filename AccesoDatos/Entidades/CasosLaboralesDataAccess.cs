using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comun.Models.Casos.Laborales;

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
    }
}

