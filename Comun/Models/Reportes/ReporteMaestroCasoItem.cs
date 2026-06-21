using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Reportes
{
    public class ReporteMaestroCasoItem
    {
        //public int? id { get; set; }
        public string? expediente { get; set; }
        public string? nombre_particular { get; set; }
        public string? origen_actual { get; set; }
        //public string? tipo_instancia { get; set; }
        public string? organo_judicial { get; set; }
        public string? oficial { get; set; }
        public string? notificador { get; set; }
        //public string? estado_caso { get; set; }
        public string? causa { get; set; }
        public string? titulo { get; set; }
        public string? motivo_casacion { get; set; }
        //public int? modulo_id { get; set; }
        public string? rama { get; set; }
        //public string? rama_slug { get; set; }
        public string? estado_actual { get; set; }
        
        //public string? fecha_ultimo_movimiento { get; set; }
        //public string? fecha_vencimiento { get; set; }
        //public int tiene_referencia { get; set; }
        public string? abogados_directores { get; set; }
        public string? socios_responsables { get; set; }
        public string? abogados_asistentes { get; set; }
        public string? demandantes { get; set; }
        public string? demandados { get; set; }
        public string? solicitantes { get; set; }
        public string? autoridades_impugnadas { get; set; }
        public string? terceros_interesados { get; set; }
        public string? contactos_empresa { get; set; }
        public string? referencias { get; set; }
        public string? ultima_anotacion { get; set; }
        //public int nivel_relacion { get; set; }
    }
}
