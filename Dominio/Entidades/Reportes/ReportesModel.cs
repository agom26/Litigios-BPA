using AccesoDatos.Entidades.Reportes;
using Comun.Models.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.Reportes
{
    public class ReportesModel
    {
        private readonly ReportesDataAccess reportesData = new ReportesDataAccess();

        public async Task<ApiResponseReporteMaestroCasos> ObtenerReporteMaestroCasos(ReporteMaestroCasosRequest req)
        {
            try
            {
                if (req == null)
                {
                    return new ApiResponseReporteMaestroCasos
                    {
                        success = false,
                        message = "Solicitud invalida"
                    };
                }

                if (req.UsuarioId <= 0)
                {
                    return new ApiResponseReporteMaestroCasos
                    {
                        success = false,
                        message = "El UsuarioId es obligatorio"
                    };
                }

                // paginación
                if (req.Pagina <= 0) req.Pagina = 1;
                if (req.RegistrosPorPagina <= 0) req.RegistrosPorPagina = 20;

                // normalizar strings
                req.Origen = NormalizarTexto(req.Origen);
                req.EstadoActual = NormalizarTexto(req.EstadoActual);
                req.Expediente = NormalizarTexto(req.Expediente);
                req.NombreParticular = NormalizarTexto(req.NombreParticular);
                req.TipoInstancia = NormalizarTexto(req.TipoInstancia);
                req.OrganoJudicial = NormalizarTexto(req.OrganoJudicial);
                req.Oficial = NormalizarTexto(req.Oficial);
                req.Notificador = NormalizarTexto(req.Notificador);
                req.MotivoCasacion= NormalizarTexto(req.MotivoCasacion);
                req.Titulo = NormalizarTexto(req.Titulo);
                req.TipoReferencia = NormalizarTexto(req.TipoReferencia);
                req.FechaDesde = NormalizarTexto(req.FechaDesde);
                req.FechaHasta = NormalizarTexto(req.FechaHasta);

                // normalizar listas
                req.AbogadoDirectorIds = NormalizarIds(req.AbogadoDirectorIds);
                req.SocioIds = NormalizarIds(req.SocioIds);
                req.AsistenteIds = NormalizarIds(req.AsistenteIds);

                req.DemandanteIds = NormalizarIds(req.DemandanteIds);
                req.DemandadoIds = NormalizarIds(req.DemandadoIds);
                req.TerceroIds = NormalizarIds(req.TerceroIds);
                req.ContactoIds = NormalizarIds(req.ContactoIds);
                req.SolicitanteIds = NormalizarIds(req.SolicitanteIds);
                req.AutoridadIds = NormalizarIds(req.AutoridadIds);

                return await reportesData.ObtenerReporteMaestroCasos(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseReporteMaestroCasos
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseReporteMaestroCasos> ObtenerReporteMaestroCasosExportacionRelacionados(ReporteMaestroCasosRequest req)
        {
            try
            {
                if (req == null)
                {
                    return new ApiResponseReporteMaestroCasos
                    {
                        success = false,
                        message = "Solicitud invalida"
                    };
                }

                if (req.UsuarioId <= 0)
                {
                    return new ApiResponseReporteMaestroCasos
                    {
                        success = false,
                        message = "El UsuarioId es obligatorio"
                    };
                }

                if (req.NivelRelacion <= 0)
                    req.NivelRelacion = 1;

                req.Origen = NormalizarTexto(req.Origen);
                req.EstadoActual = NormalizarTexto(req.EstadoActual);
                req.Expediente = NormalizarTexto(req.Expediente);
                req.NombreParticular = NormalizarTexto(req.NombreParticular);
                req.TipoInstancia = NormalizarTexto(req.TipoInstancia);
                req.OrganoJudicial = NormalizarTexto(req.OrganoJudicial);
                req.Oficial = NormalizarTexto(req.Oficial);
                req.Notificador = NormalizarTexto(req.Notificador);
                req.TipoReferencia = NormalizarTexto(req.TipoReferencia);
                req.FechaDesde = NormalizarTexto(req.FechaDesde);
                req.FechaHasta = NormalizarTexto(req.FechaHasta);

                req.AbogadoDirectorIds = NormalizarIds(req.AbogadoDirectorIds);
                req.SocioIds = NormalizarIds(req.SocioIds);
                req.AsistenteIds = NormalizarIds(req.AsistenteIds);

                req.DemandanteIds = NormalizarIds(req.DemandanteIds);
                req.DemandadoIds = NormalizarIds(req.DemandadoIds);
                req.TerceroIds = NormalizarIds(req.TerceroIds);
                req.ContactoIds = NormalizarIds(req.ContactoIds);
                req.SolicitanteIds = NormalizarIds(req.SolicitanteIds);
                req.AutoridadIds = NormalizarIds(req.AutoridadIds);

                return await reportesData.ObtenerReporteMaestroCasosExportacionRelacionados(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseReporteMaestroCasos
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        private static string? NormalizarTexto(string? valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
        }

        private static List<int>? NormalizarIds(List<int>? ids)
        {
            if (ids == null) return null;

            var clean = ids
                .Where(x => x > 0)
                .Distinct()
                .ToList();

            return clean.Count == 0 ? new List<int>() : clean;
        }
    }
}
