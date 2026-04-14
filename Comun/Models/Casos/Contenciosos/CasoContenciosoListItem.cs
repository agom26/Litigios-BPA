using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Contenciosos
{
    public class CasoContenciosoListItem
    {
        public int id { get; set; }
        public string? expediente { get; set; }
        public string? nombre_particular { get; set; }
        public string? sala { get; set; }
        public string? oficial { get; set; }
        public string? notificador { get; set; }
        public string? estado { get; set; }
    }
}
