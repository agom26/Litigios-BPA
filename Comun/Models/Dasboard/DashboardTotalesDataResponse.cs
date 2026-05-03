using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Dasboard
{
    public class DashboardTotalesDataResponse
    {
        
        public int laborales { get; set; }
        public int civiles { get; set; }
        public int contenciosos_administrativos { get; set; }
        public int constitucionales { get; set; }
        public int total_casos_activos { get; set; }
    }
}
