using AccesoDatos.Entidades.Contenciosos;
using Comun.Models;
using Comun.Models.Casos.Contenciosos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.Contenciosos
{
    public class CATributarioModel
    {
        CATributarioDataAccess casoContenciosoData = new CATributarioDataAccess();
        public async Task<ApiResponseCasosContenciososList> ObtenerCasosContenciosos(
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
                    return new ApiResponseCasosContenciososList
                    {
                        success = false,
                        message = "Usuario requerido"
                    };
                }

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await casoContenciosoData.ListarCasos(usuarioId, pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosContenciososList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseCrearCasoContencioso> CrearCasoContencioso(CrearCasoContenciosoRequest req)
        {
            try
            {
                if (req == null)
                {
                    return new ApiResponseCrearCasoContencioso
                    {
                        success = false,
                        message = "Solicitud inválida"
                    };
                }

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseCrearCasoContencioso { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseCrearCasoContencioso { success = false, message = "Sala es requerida" };

                if (string.IsNullOrWhiteSpace(req.Notificador))
                    return new ApiResponseCrearCasoContencioso { success = false, message = "Notificador es requerido" };

                if (string.IsNullOrWhiteSpace(req.Oficial))
                    return new ApiResponseCrearCasoContencioso { success = false, message = "Oficial es requerido" };

                if (string.IsNullOrWhiteSpace(req.Estado))
                    return new ApiResponseCrearCasoContencioso { success = false, message = "Estado es requerido" };

                if (req.UsuarioCreador <= 0)
                    return new ApiResponseCrearCasoContencioso { success = false, message = "Usuario creador es requerido" };

                req.Expediente = req.Expediente.Trim();
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

                if (req.MarcaReferenciaId.HasValue && req.MarcaReferenciaId.Value <= 0)
                    req.MarcaReferenciaId = null;

                return await casoContenciosoData.CrearCasoContencioso(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoContencioso
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<CasoContenciosoDetalleData>> ObtenerCasoContenciosoPorId(int usuarioId, int casoId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return new ApiResponse<CasoContenciosoDetalleData>
                    {
                        success = false,
                        message = "Usuario requerido"
                    };
                }

                if (casoId <= 0)
                {
                    return new ApiResponse<CasoContenciosoDetalleData>
                    {
                        success = false,
                        message = "caso_id es requerido"
                    };
                }

                return await casoContenciosoData.ObtenerCasoContenciosoPorId(usuarioId, casoId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoContenciosoDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseEditarCasoContencioso> EditarCasoContencioso(EditarCasoContenciosoRequest req)
        {
            try
            {
                if (req == null)
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Solicitud inválida" };

                if (req.UsuarioId <= 0)
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Usuario requerido" };

                if (req.CasoId <= 0)
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Caso requerido" };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Sala es requerida" };

                if (req.HuboCambioEstado && string.IsNullOrWhiteSpace(req.Fecha))
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Fecha es requerida" };

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

                if (req.MarcaReferenciaId.HasValue && req.MarcaReferenciaId.Value <= 0)
                    req.MarcaReferenciaId = null;

                return await casoContenciosoData.EditarCasoContencioso(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoContencioso
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoContencioso(int casoId, int usuarioId)
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

                return await casoContenciosoData.EliminarCasoContencioso(casoId, usuarioId);
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
