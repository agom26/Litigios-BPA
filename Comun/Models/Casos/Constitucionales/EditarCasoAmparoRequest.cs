using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class EditarCasoAmparoRequest
    {
        public int UsuarioId { get; set; }
        public int CasoId { get; set; }

        public string? Expediente { get; set; }
        public string? NombreParticular { get; set; }
        public string? Oficial { get; set; }
        public string? Causa { get; set; }
        // 🔹 Referencia
        public int CasoReferenciaId { get; set; }

        // CONTROL HISTORIAL
        public bool HuboCambioEstado { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public string? Fecha { get; set; }
        public string? FechaVencimiento { get; set; }

        // PERSONAS
        public List<int>? Solicitantes { get; set; }
        public List<int>? AutoridadesImpugnadas { get; set; }
        public List<int>? TercerosInteresados { get; set; }
        public List<int>? ContactosEmpresa { get; set; }

        // 🔹 USUARIOS
        public List<int>? AbogadosDirectores { get; set; }
        public List<int>? SociosResponsables { get; set; }
        public List<int>? AbogadosAsistentes { get; set; }
    }
}
