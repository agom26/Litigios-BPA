using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Reportes
{
    public class ReporteCasosRequest
    {
        // 🔹 básicos
        public int? ModuloId { get; set; }
        public string? Expediente { get; set; }
        public string? Juzgado { get; set; }
        public string? Oficial { get; set; }
        public string? Notificador { get; set; }
        public string? Estado { get; set; }
        public string? Causa { get; set; }

        // 🔹 PERSONAS
        public List<int>? Demandantes { get; set; }
        public List<int>? Demandados { get; set; }
        public List<int>? TercerosInteresados { get; set; }
        public List<int>? ContactosEmpresa { get; set; }

        // 🔹 CONSTITUCIONAL
        public List<int>? Solicitantes { get; set; }
        public List<int>? AutoridadesImpugnadas { get; set; }

        // 🔹 USUARIOS
        public List<int>? AbogadosDirectores { get; set; }
        public List<int>? SociosResponsables { get; set; }
        public List<int>? AbogadosAsistentes { get; set; }

        // 🔹 REFERENCIA
        public int? CasoReferenciaId { get; set; }
        public string? TipoReferencia { get; set; }
    }
}
