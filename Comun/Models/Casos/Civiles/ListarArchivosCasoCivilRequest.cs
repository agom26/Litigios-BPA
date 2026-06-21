using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Laborales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Comun.Models.Casos.Civiles
{
    public class ListarArchivosCasoCivilRequest
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int total { get; set; }
        public List<ArchivoCasoCivilItem> data { get; set; }
    }
}
