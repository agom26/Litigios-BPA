using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Laborales
{
    public class EditarCasoLaboralRequest
    {
        public int UsuarioId { get; set; }
        public int CasoId { get; set; }

        public string? Expediente { get; set; }
        public string? NombreParticular { get; set; }
        public string? Juzgado { get; set; }
        public string? Oficial { get; set; }
        public string? Notificador { get; set; }

        // historial
        public bool huboCambioEstado { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public string? Fecha { get; set; } // "yyyy-MM-dd HH:mm:ss"
        public string? FechaVencimiento { get; set; }
        public string? Origen {  get; set; }

        public List<int>? Demandantes { get; set; }
        public List<int>? Demandados { get; set; }
        public List<int>? TercerosInteresados { get; set; }
        public List<int>? ContactosEmpresa { get; set; }

        public List<int>? AbogadosDirectores { get; set; }
        public List<int>? SociosResponsables { get; set; }
        public List<int>? AbogadosAsistentes { get; set; }
    }
}
