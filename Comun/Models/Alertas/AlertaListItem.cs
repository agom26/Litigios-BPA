using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Alertas
{
    public class AlertaListItem
    {
        public int id { get; set; }  
        public string? expediente { get; set; }
        public string? nombre_particular { get; set; }
        public string? organo_judicial { get; set; }
        public string? rama { get; set; }
        public string? titulo { get; set; }
        public string? tipo_alerta { get; set; }
        public string? estado { get; set; }
        public string? origen { get; set; }
        public DateTime? fecha_programada { get; set; }
        public DateTime? fecha_generada { get; set; }
        public int? leida { get; set; }
        public DateTime? fecha_lectura { get; set; }
    }
}
