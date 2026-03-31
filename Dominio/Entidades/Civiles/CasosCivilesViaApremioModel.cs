using AccesoDatos.Entidades.Civiles;
using Comun.Models;
using Comun.Models.Casos.Civiles;

namespace Dominio.Entidades.Civiles
{
    public class CasosCivilesViaApremioModel
    {
        private readonly CasosCivilesViaApremioDataAccess dataAccess = new CasosCivilesViaApremioDataAccess();

        public async Task<ApiResponseCasosCivilesList> ObtenerCasosViaApremio(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return new ApiResponseCasosCivilesList
                    {
                        success = false,
                        message = "Usuario requerido"
                    };
                }

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await dataAccess.ListarCasosViaApremio(usuarioId, pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosCivilesList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseCrearCasoCivil> CrearCasoViaApremio(CrearCasoViaApremioRequest req)
        {
            try
            {
                if (req == null)
                {
                    return new ApiResponseCrearCasoCivil
                    {
                        success = false,
                        message = "Solicitud inválida"
                    };
                }

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Titulo))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Titulo es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Juzgado es requerido" };

                if (string.IsNullOrWhiteSpace(req.Notificador))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Notificador es requerido" };

                if (string.IsNullOrWhiteSpace(req.Oficial))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Oficial es requerido" };

                if (string.IsNullOrWhiteSpace(req.Estado))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Estado es requerido" };

                if (req.UsuarioId <= 0)
                    return new ApiResponseCrearCasoCivil { success = false, message = "Usuario creador es requerido" };

                req.Expediente = req.Expediente.Trim();
                req.Titulo = req.Titulo.Trim();
                req.Juzgado = req.Juzgado.Trim();
                req.Estado = req.Estado.Trim();
                req.Oficial = req.Oficial.Trim();
                req.Notificador = req.Notificador.Trim();

                req.NombreParticular = string.IsNullOrWhiteSpace(req.NombreParticular) ? null : req.NombreParticular.Trim();
                req.Observaciones = string.IsNullOrWhiteSpace(req.Observaciones) ? null : req.Observaciones.Trim();
                req.Fecha = string.IsNullOrWhiteSpace(req.Fecha) ? null : req.Fecha.Trim();
                req.FechaVencimiento = string.IsNullOrWhiteSpace(req.FechaVencimiento) ? null : req.FechaVencimiento.Trim();
                req.ObservacionReferencia = string.IsNullOrWhiteSpace(req.ObservacionReferencia) ? null : req.ObservacionReferencia.Trim();

                req.Demandantes = NormalizarIds(req.Demandantes);
                req.Demandados = NormalizarIds(req.Demandados);
                req.TercerosInteresados = NormalizarIds(req.TercerosInteresados);
                req.ContactosEmpresa = NormalizarIds(req.ContactosEmpresa);

                req.AbogadosDirectores = NormalizarIds(req.AbogadosDirectores);
                req.SociosResponsables = NormalizarIds(req.SociosResponsables);
                req.AbogadosAsistentes = NormalizarIds(req.AbogadosAsistentes);

                if (req.CasoReferenciaId.HasValue && req.CasoReferenciaId.Value <= 0)
                    req.CasoReferenciaId = null;

                return await dataAccess.CrearCasoViaApremio(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoCivil
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<CasoCivilViaApremioDetalleData>> ObtenerCasoViaApremioPorId(int usuarioId, int casoId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return new ApiResponse<CasoCivilViaApremioDetalleData>
                    {
                        success = false,
                        message = "Usuario requerido"
                    };
                }

                if (casoId <= 0)
                {
                    return new ApiResponse<CasoCivilViaApremioDetalleData>
                    {
                        success = false,
                        message = "caso_id es requerido"
                    };
                }

                return await dataAccess.ObtenerCasoViaApremioPorId(usuarioId, casoId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoCivilViaApremioDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseEditarCasoCivil> EditarCasoViaApremio(EditarCasoViaApremioRequest req)
        {
            try
            {
                if (req == null)
                    return new ApiResponseEditarCasoCivil { success = false, message = "Solicitud inválida" };

                if (req.UsuarioId <= 0)
                    return new ApiResponseEditarCasoCivil { success = false, message = "Usuario requerido" };

                if (req.CasoId <= 0)
                    return new ApiResponseEditarCasoCivil { success = false, message = "Caso requerido" };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseEditarCasoCivil { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Titulo))
                    return new ApiResponseEditarCasoCivil { success = false, message = "Titulo es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseEditarCasoCivil { success = false, message = "Juzgado es requerido" };

                if (req.HuboCambioEstado && string.IsNullOrWhiteSpace(req.Fecha))
                    return new ApiResponseEditarCasoCivil { success = false, message = "Fecha es requerida" };

                req.Demandantes = NormalizarIds(req.Demandantes);
                req.Demandados = NormalizarIds(req.Demandados);
                req.TercerosInteresados = NormalizarIds(req.TercerosInteresados);
                req.ContactosEmpresa = NormalizarIds(req.ContactosEmpresa);

                req.AbogadosDirectores = NormalizarIds(req.AbogadosDirectores);
                req.SociosResponsables = NormalizarIds(req.SociosResponsables);
                req.AbogadosAsistentes = NormalizarIds(req.AbogadosAsistentes);

                req.ObservacionReferencia = string.IsNullOrWhiteSpace(req.ObservacionReferencia)
                    ? null
                    : req.ObservacionReferencia.Trim();

                if (req.CasoReferenciaId.HasValue && req.CasoReferenciaId.Value <= 0)
                    req.CasoReferenciaId = null;

                return await dataAccess.EditarCasoViaApremio(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoCivil
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoViaApremio(int casoId, int usuarioId)
        {
            try
            {
                if (casoId <= 0)
                {
                    return new ApiResponse<object>
                    {
                        success = false,
                        message = "caso_id es requerido"
                    };
                }

                if (usuarioId <= 0)
                {
                    return new ApiResponse<object>
                    {
                        success = false,
                        message = "usuario_id es requerido"
                    };
                }

                return await dataAccess.EliminarCasoViaApremio(casoId, usuarioId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
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
