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
    public class CAObtenerMarcasContenciosoDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/contenciosos/obtenerMarcas.php";

        private static readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponseMarcasContenciososasList> ListarMarcasContenciosas(
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "listar_marcas_contenciosas" },
                { "pagina", pagina.ToString() },
                { "registros", registros.ToString() },
                { "filtro", filtro ?? "" }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponseMarcasContenciososasList>(jsonResult)
                    ?? new ApiResponseMarcasContenciososasList
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponseMarcasContenciososasList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<MarcaContenciosaListItem>> ObtenerMarcaReferenciaPorId( int marcaId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "action", "get_marca_contenciosa_por_id" },
                { "recurso_id", marcaId.ToString() }
            };

            using var content = new FormUrlEncodedContent(parameters);

            try
            {
                var response = await _http.PostAsync(_apiUrl, content);
                var jsonResult = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<MarcaContenciosaListItem>>(jsonResult)
                    ?? new ApiResponse<MarcaContenciosaListItem>
                    {
                        success = false,
                        message = "Respuesta vacía o inválida."
                    };
            }
            catch (Exception ex)
            {
                return new ApiResponse<MarcaContenciosaListItem>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }
    }
}
