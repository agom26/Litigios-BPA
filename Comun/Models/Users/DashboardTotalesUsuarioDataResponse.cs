using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Users
{
    public class DashboardTotalesUsuarioDataResponse
    {
        public int usuario_id { get; set; }
        public string usuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int laborales { get; set; }
        public int civiles { get; set; }
        public int contenciosos_administrativos { get; set; }
        public int constitucionales { get; set; }
        public int total_casos { get; set; }
    }
}
