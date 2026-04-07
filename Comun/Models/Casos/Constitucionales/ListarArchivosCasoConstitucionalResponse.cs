using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class ListarArchivosCasoConstitucionalResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int total { get; set; }
        public List<ArchivoCasoConstitucionalItem> data { get; set; }
    }
}
