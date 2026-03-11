using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Laborales
{
    public class ListarArchivosCasoLaboralResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int total { get; set; }
        public List<ArchivoCasoLaboralItem> data { get; set; }
    }
}
