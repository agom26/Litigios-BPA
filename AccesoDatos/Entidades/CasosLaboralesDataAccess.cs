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
        public async Task<ApiResponseCrearCasoLaboral> CrearCasoLaboral(
           string expediente,
           string juzgado,
           string estado,
           string? nombreParticular = null,
           string? oficial = null,
           string? notificador = null,
           List<int>? personasId = null,
           List<string>? tiposPersona = null,
           List<int>? usuariosId = null,
           List<string>? rolesUsuario = null
       )
        {
            using (var client = new HttpClient())
            {
                var kv = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("action", "crear_caso_laboral"),
                    new KeyValuePair<string, string>("expediente", expediente ?? ""),
                    new KeyValuePair<string, string>("juzgado", juzgado ?? ""),
                    new KeyValuePair<string, string>("estado", estado ?? ""),
                    new KeyValuePair<string, string>("nombre_particular", nombreParticular ?? ""),
                    new KeyValuePair<string, string>("oficial", oficial ?? ""),
                    new KeyValuePair<string, string>("notificador", notificador ?? ""),
                };

                // PERSONAS (Demandante/Demandado/etc.)
                if (personasId != null && personasId.Count > 0)
                {
                    if (tiposPersona == null) tiposPersona = new List<string>();
                    while (tiposPersona.Count < personasId.Count) tiposPersona.Add("");

                    for (int i = 0; i < personasId.Count; i++)
                    {
                        kv.Add(new KeyValuePair<string, string>("persona_id", personasId[i].ToString()));
                        kv.Add(new KeyValuePair<string, string>("tipo_persona", tiposPersona[i] ?? ""));
                    }
                }

                // USUARIOS (Abogados) + rol
                if (usuariosId != null && usuariosId.Count > 0)
                {
                    if (rolesUsuario == null) rolesUsuario = new List<string>();
                    while (rolesUsuario.Count < usuariosId.Count) rolesUsuario.Add("");

                    for (int i = 0; i < usuariosId.Count; i++)
                    {
                        kv.Add(new KeyValuePair<string, string>("usuario_id", usuariosId[i].ToString()));
                        kv.Add(new KeyValuePair<string, string>("rol_usuario", rolesUsuario[i] ?? ""));
                    }
                }

                var content = new FormUrlEncodedContent(kv);

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
