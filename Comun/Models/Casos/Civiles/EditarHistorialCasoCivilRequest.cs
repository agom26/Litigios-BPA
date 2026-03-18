using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Civiles
{
    public class EditarHistorialCasoCivilRequest
    {
        public int HistorialId { get; set; }
        public int CasoId { get; set; }
        public int UsuarioId { get; set; }
        public string Fecha { get; set; }
        public string FechaVencimiento { get; set; }
        public string Estado { get; set; }
        public string Anotaciones { get; set; }
    }
}
