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
    public class CARecursoCasacionModel
    {
        CARecursoCasacionDataAccess recursoCasacionData = new CARecursoCasacionDataAccess();
        public async Task<ApiResponseCasosContenciososRCList> ObtenerRecursosCasacion(
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
                    return new ApiResponseCasosContenciososRCList
                    {
                        success = false,
                        message = "Usuario requerido"
                    };
                }

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await recursoCasacionData.ListarCasos(usuarioId, pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosContenciososRCList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseCrearCasoContencioso> CrearRecursoCasacion(CrearCasoContenciosoRequest req)
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

                if (string.IsNullOrWhiteSpace(req.MotivoCasacion))
                    return new ApiResponseCrearCasoContencioso
                    {
                        success = false,
                        message = "Motivo de casación es requerido"
                    };

                if (req.CasoOrigenId <= 0)
                    return new ApiResponseCrearCasoContencioso
                    {
                        success = false,
                        message = "Caso origen es requerido"
                    };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseCrearCasoContencioso { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseCrearCasoContencioso { success = false, message = "Cámara es requerida" };

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
                req.MotivoCasacion = req.MotivoCasacion.Trim().ToUpper();
                req.NombreParticular = string.IsNullOrWhiteSpace(req.NombreParticular) ? null : req.NombreParticular.Trim();
                req.Observaciones = string.IsNullOrWhiteSpace(req.Observaciones) ? null : req.Observaciones.Trim();
                req.Fecha = string.IsNullOrWhiteSpace(req.Fecha) ? null : req.Fecha.Trim();
                req.FechaVencimiento = string.IsNullOrWhiteSpace(req.FechaVencimiento) ? null : req.FechaVencimiento.Trim();
                req.ObservacionReferencia = string.IsNullOrWhiteSpace(req.ObservacionReferencia) ? null : req.ObservacionReferencia.Trim();

                if (req.MarcaReferenciaId.HasValue && req.MarcaReferenciaId.Value <= 0)
                    req.MarcaReferenciaId = null;

                return await recursoCasacionData.CrearRecursoCasacion(req);
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

        public async Task<ApiResponse<CasoRecursoCasacionDetalleData>> ObtenerRecursoCasacionPorId(int usuarioId, int casoId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    return new ApiResponse<CasoRecursoCasacionDetalleData>
                    {
                        success = false,
                        message = "Usuario requerido"
                    };
                }

                if (casoId <= 0)
                {
                    return new ApiResponse<CasoRecursoCasacionDetalleData>
                    {
                        success = false,
                        message = "caso_id es requerido"
                    };
                }

                return await recursoCasacionData.ObtenerCasoContenciosoPorId(usuarioId, casoId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoRecursoCasacionDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseEditarCasoContencioso> EditarRecursoCasacion(EditarCasoContenciosoRequest req)
        {
            try
            {
                if (req == null)
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Solicitud inválida" };

                var motivosValidos = new[] { "FORMA", "FONDO", "FORMA Y FONDO" };

                if (!string.IsNullOrWhiteSpace(req.MotivoCasacion))
                {
                    var motivo = req.MotivoCasacion.Trim().ToUpper();

                    if (!motivosValidos.Contains(motivo))
                        return new ApiResponseEditarCasoContencioso
                        {
                            success = false,
                            message = "Motivo de casación inválido"
                        };

                    req.MotivoCasacion = motivo;
                }

                if (req.UsuarioId <= 0)
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Usuario requerido" };

                if (req.CasoId <= 0)
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Caso requerido" };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Cámara es requerida" };

                if (req.HuboCambioEstado && string.IsNullOrWhiteSpace(req.Fecha))
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Fecha es requerida" };

                if (req.Estado == "Amparo" && (req.expediente_amparo == null || string.IsNullOrWhiteSpace(req.expediente_amparo) ))
                {
                    return new ApiResponseEditarCasoContencioso { success = false, message = "Debe ingresar expediente de amparo" };
                }

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

                return await recursoCasacionData.EditarRecursoCasacion(req);
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

        public async Task<ApiResponse<object>> EliminarRecursoCasacion(int casoId, int usuarioId)
        {
            try
            {
                if (casoId <= 0)
                {
                    return new ApiResponse<object>
                    {
                        success = false,
                        message = "caso es requerido"
                    };
                }

                if (usuarioId <= 0)
                {
                    return new ApiResponse<object>
                    {
                        success = false,
                        message = "usuario es requerido"
                    };
                }

                return await recursoCasacionData.EliminarCasoContencioso(casoId, usuarioId);
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
