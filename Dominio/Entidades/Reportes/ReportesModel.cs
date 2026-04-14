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
        ReportesDataAccess reportesData = new ReportesDataAccess();
        public async Task<ApiResponseReporteCasos> ObtenerReporteCasos(ReporteCasosRequest req)
        {
            try
            {
                if (req == null)
                    return new ApiResponseReporteCasos { success = false, message = "Solicitud inválida" };

                // 🔹 Normalizar strings
                req.Expediente = string.IsNullOrWhiteSpace(req.Expediente) ? null : req.Expediente.Trim();
                req.Juzgado = string.IsNullOrWhiteSpace(req.Juzgado) ? null : req.Juzgado.Trim();
                req.Oficial = string.IsNullOrWhiteSpace(req.Oficial) ? null : req.Oficial.Trim();
                req.Notificador = string.IsNullOrWhiteSpace(req.Notificador) ? null : req.Notificador.Trim();
                req.Estado = string.IsNullOrWhiteSpace(req.Estado) ? null : req.Estado.Trim();
                req.Causa = string.IsNullOrWhiteSpace(req.Causa) ? null : req.Causa.Trim();
                req.TipoReferencia = string.IsNullOrWhiteSpace(req.TipoReferencia) ? null : req.TipoReferencia.Trim();

                // 🔹 Normalizar listas
                req.Demandantes = NormalizarIds(req.Demandantes);
                req.Demandados = NormalizarIds(req.Demandados);
                req.TercerosInteresados = NormalizarIds(req.TercerosInteresados);
                req.ContactosEmpresa = NormalizarIds(req.ContactosEmpresa);

                req.Solicitantes = NormalizarIds(req.Solicitantes);
                req.AutoridadesImpugnadas = NormalizarIds(req.AutoridadesImpugnadas);

                req.AbogadosDirectores = NormalizarIds(req.AbogadosDirectores);
                req.SociosResponsables = NormalizarIds(req.SociosResponsables);
                req.AbogadosAsistentes = NormalizarIds(req.AbogadosAsistentes);

                return await reportesData.ObtenerReporteCasos(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseReporteCasos
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
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
