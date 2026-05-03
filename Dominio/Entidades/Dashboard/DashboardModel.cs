using AccesoDatos.Entidades.Dashboard;
using Comun.Models;
using Comun.Models.Dasboard;
using Comun.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.Dashboard
{
    public class DashboardModel
    {
        private DashboardDataAccess dashboardData = new DashboardDataAccess();

        public async Task<ApiResponse<DashboardTotalesDataResponse>> ObtenerTotalesDashboard()
        {
            try
            {
                return await dashboardData.ObtenerTotalesDashboard();
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
