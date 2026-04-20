using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class CasoConstitucionalListItem
    {
        public int id { get; set; }
        public string? expediente { get; set; }
        public string? nombre_amparo { get; set; }
        public string? corte { get; set; }
        public string? oficial { get; set; }
        public string? estado { get; set; }
    }
}
