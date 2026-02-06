using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models
{
    public class UserDataResponse
    {
       
            public int id { get; set; }
            public string usuario { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public List<string> modulos { get; set; }
        
    }

}
