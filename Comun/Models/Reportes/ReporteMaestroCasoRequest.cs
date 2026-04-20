using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Reportes
{
    public class ReporteMaestroCasosRequest
    {
        // básicos
        public int UsuarioId { get; set; }
        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 20;

        // filtros generales
        public int? ModuloId { get; set; }
        public string? Origen { get; set; }
        public string? EstadoActual { get; set; }
        public string? Expediente { get; set; }
        public string? NombreParticular { get; set; }
        public string? TipoInstancia { get; set; }
        public string? OrganoJudicial { get; set; }
        public string? Oficial { get; set; }
        public string? Notificador { get; set; }

        // equipo legal
        public List<int>? AbogadoDirectorIds { get; set; }
        public List<int>? SocioIds { get; set; }
        public List<int>? AsistenteIds { get; set; }

        // partes involucradas
        public List<int>? DemandanteIds { get; set; }
        public List<int>? DemandadoIds { get; set; }
        public List<int>? TerceroIds { get; set; }
        public List<int>? ContactoIds { get; set; }
        public List<int>? SolicitanteIds { get; set; }
        public List<int>? AutoridadIds { get; set; }

        // referencias
        public int? TieneReferencia { get; set; }
        public int? CasoReferenciaId { get; set; }
        public string? TipoReferencia { get; set; }

        // otros
        public int SoloTerminados { get; set; } = 0;
        public string? FechaDesde { get; set; }
        public string? FechaHasta { get; set; }

        public int? IncluirRelacionados { get; set; } = 0;
        public int? NivelRelacion { get; set; } = 1;

        public string? MotivoCasacion { get; set; }
        public string? Titulo { get; set; }
    }
}
