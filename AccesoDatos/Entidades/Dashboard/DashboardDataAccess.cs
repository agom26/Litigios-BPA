using Comun.Models;
using Comun.Models.Dasboard;
using Comun.Models.Plazos;
using Comun.Models.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades.Dashboard
{
    public class DashboardDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/dashboard/dashboard.php";

        private static readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponse<DashboardTotalesDataResponse>> ObtenerTotalesDashboard()
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "action", "dashboard" },
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApiResponse<DashboardTotalesDataResponse>>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiResponse<DashboardTotalesDataResponse>
                    {
                        success = false,
                        message = "Error: " + ex.Message
                    };
                }
            }
        }
    }
}
