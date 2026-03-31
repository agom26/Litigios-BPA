using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Contenciosos
{
    public class EditarCasoContenciosoRequest
    {
        public int UsuarioId { get; set; }
        public int CasoId { get; set; }

        public string? Expediente { get; set; }
        public string? Titulo { get; set; }
        public string? NombreParticular { get; set; }
        public string? Juzgado { get; set; }
        public string? Oficial { get; set; }
        public string? Notificador { get; set; }

        public bool HuboCambioEstado { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public string? Fecha { get; set; }
        public string? FechaVencimiento { get; set; }

        public List<int>? Demandantes { get; set; }
        public List<int>? Demandados { get; set; }
        public List<int>? TercerosInteresados { get; set; }
        public List<int>? ContactosEmpresa { get; set; }

        public List<int>? AbogadosDirectores { get; set; }
        public List<int>? SociosResponsables { get; set; }
        public List<int>? AbogadosAsistentes { get; set; }

        public int? MarcaReferenciaId { get; set; }
        public string? ObservacionReferencia { get; set; }
        public int CasoOrigenId { get; set; }
        public string? MotivoCasacion { get; set; }
        public string? expediente_amparo { get; set; }
    }
}
