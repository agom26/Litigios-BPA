using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class CrearCasoAmparoRequest
    {
        // 🔹 Caso
        public string? Expediente { get; set; }
        public string? NombreParticular { get; set; }
        public string? Oficial { get; set; }
        public string? Causa { get; set; }

        // 🔹 Referencia
        public int CasoReferenciaId { get; set; }

        // 🔹 Historial
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public int UsuarioCreador { get; set; }

        // 🔹 Fechas
        public string? Fecha { get; set; }
        public string? FechaVencimiento { get; set; }

        // 🔹 Personas
        public List<int>? Solicitantes { get; set; }
        public List<int>? AutoridadesImpugnadas { get; set; }
        public List<int>? TercerosInteresados { get; set; }
        public List<int>? ContactosEmpresa { get; set; }

        // 🔹 Usuarios
        public List<int>? AbogadosDirectores { get; set; }
        public List<int>? SociosResponsables { get; set; }
        public List<int>? AbogadosAsistentes { get; set; }
    }
}
