using AccesoDatos.Entidades.Civiles;
using Comun.Models;
using Comun.Models.Casos.Civiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.Civiles
{
    public class CasoCivilJuicioOralPI
    {
        JuicioOralPIDataAccess casoCivilData = new JuicioOralPIDataAccess();
        public async Task<ApiResponseCasosCivilesList> ObtenerCasosCiviles(
           int usuarioId,
           int pagina,
           int registros,
           string? filtro = null
        )
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponseCasosCivilesList { success = false, message = "Usuario requerido" };

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await casoCivilData.ListarCasosCiviles(usuarioId, pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosCivilesList { success = false, message = "Error: " + ex.Message };
            }
        }

        //crear caso
        public async Task<ApiResponseCrearCasoCivil> CrearCasoCivil(CrearCasoCivilRequest req)
        {
            try
            {
                // ---- Validaciones mínimas (las mismas que PHP exige) ----
                if (req == null)
                    return new ApiResponseCrearCasoCivil { success = false, message = "Solicitud inválida" };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Juzgado es requerido" };

                if (string.IsNullOrWhiteSpace(req.Notificador))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Notificador es requerido" };

                if (string.IsNullOrWhiteSpace(req.Oficial))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Oficial es requerido" };

                if (string.IsNullOrWhiteSpace(req.Estado))
                    return new ApiResponseCrearCasoCivil { success = false, message = "Estado es requerido" };

                if (req.UsuarioCreador <= 0)
                    return new ApiResponseCrearCasoCivil { success = false, message = "Usuario creador es requerido" };

                // ---- Normalizar strings ----
                req.Expediente = req.Expediente.Trim();
                req.Juzgado = req.Juzgado.Trim();
                req.Estado = req.Estado.Trim();
                req.Oficial = req.Oficial.Trim();
                req.Notificador = req.Notificador.Trim();

                req.NombreParticular = string.IsNullOrWhiteSpace(req.NombreParticular) ? null : req.NombreParticular.Trim();
                req.Observaciones = string.IsNullOrWhiteSpace(req.Observaciones) ? null : req.Observaciones.Trim();

                // Fechas opcionales
                req.Fecha = string.IsNullOrWhiteSpace(req.Fecha) ? null : req.Fecha.Trim();
                req.FechaVencimiento = string.IsNullOrWhiteSpace(req.FechaVencimiento) ? null : req.FechaVencimiento.Trim();

                // ---- Normalizar listas (evitar ids 0, duplicados) ----
                req.Demandantes = NormalizarIds(req.Demandantes);
                req.Demandados = NormalizarIds(req.Demandados);
                req.TercerosInteresados = NormalizarIds(req.TercerosInteresados);
                req.ContactosEmpresa = NormalizarIds(req.ContactosEmpresa);

                req.AbogadosDirectores = NormalizarIds(req.AbogadosDirectores);
                req.SociosResponsables = NormalizarIds(req.SociosResponsables);
                req.AbogadosAsistentes = NormalizarIds(req.AbogadosAsistentes);
                // Validar que no estén vacías
                if (!req.Demandantes.Any())
                    return new ApiResponseCrearCasoCivil { success = false, message = "Debe ingresar al menos un demandante" };

                if (!req.Demandados.Any())
                    return new ApiResponseCrearCasoCivil { success = false, message = "Debe ingresar al menos un demandado" };

                if (!req.AbogadosDirectores.Any())
                    return new ApiResponseCrearCasoCivil { success = false, message = "Debe ingresar al menos un abogado director" };

                if (!req.SociosResponsables.Any())
                    return new ApiResponseCrearCasoCivil { success = false, message = "Debe ingresar al menos un socio responsable" };

                if (!req.AbogadosAsistentes.Any())
                    return new ApiResponseCrearCasoCivil { success = false, message = "Debe ingresar al menos un abogado asistente" };

                // Llamar DataAccess
                return await casoCivilData.CrearCasoCivil(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoCivil { success = false, message = "Error: " + ex.Message };
            }
        }

        // Helper interno del Dominio
        private static List<int>? NormalizarIds(List<int>? ids)
        {
            if (ids == null) return null;

            var clean = ids
                .Where(x => x > 0)
                .Distinct()
                .ToList();

            return clean.Count == 0 ? new List<int>() : clean;
        }

        public async Task<ApiResponse<CasoCivilDetalleData>> ObtenerCasoCivilPorId(int usuarioId, int casoId)
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponse<CasoCivilDetalleData> { success = false, message = "Usuario requerido" };

                if (casoId <= 0)
                    return new ApiResponse<CasoCivilDetalleData> { success = false, message = "Id de caso es requerido" };

                // Llamar DataAccess
                return await casoCivilData.ObtenerCasoCivilPorId(usuarioId, casoId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoCivilDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseEditarCasoCivil> EditarCasoCivil(EditarCasoCivilRequest req)
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

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseEditarCasoCivil { success = false, message = "Juzgado es requerido" };

                // fecha obligatoria según PHP (para historial)
                if (string.IsNullOrWhiteSpace(req.Fecha))
                    return new ApiResponseEditarCasoCivil { success = false, message = "Fecha es requerida" };

                // normalizar listas
                req.Demandantes = NormalizarIds(req.Demandantes);
                req.Demandados = NormalizarIds(req.Demandados);
                req.TercerosInteresados = NormalizarIds(req.TercerosInteresados);
                req.ContactosEmpresa = NormalizarIds(req.ContactosEmpresa);

                req.AbogadosDirectores = NormalizarIds(req.AbogadosDirectores);
                req.SociosResponsables = NormalizarIds(req.SociosResponsables);
                req.AbogadosAsistentes = NormalizarIds(req.AbogadosAsistentes);

                // Validar que no estén vacías
                if (!req.Demandantes.Any())
                    return new ApiResponseEditarCasoCivil { success = false, message = "Debe ingresar al menos un demandante" };

                if (!req.Demandados.Any())
                    return new ApiResponseEditarCasoCivil { success = false, message = "Debe ingresar al menos un demandado" };

                if (!req.AbogadosDirectores.Any())
                    return new ApiResponseEditarCasoCivil { success = false, message = "Debe ingresar al menos un abogado director" };

                if (!req.SociosResponsables.Any())
                    return new ApiResponseEditarCasoCivil { success = false, message = "Debe ingresar al menos un socio responsable" };

                if (!req.AbogadosAsistentes.Any())
                    return new ApiResponseEditarCasoCivil { success = false, message = "Debe ingresar al menos un abogado asistente" };

                return await casoCivilData.EditarCasoCivil(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoCivil { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoCivil(int casoId, int usuarioId)
        {
            try
            {
                if (casoId <= 0)
                    return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

                if (usuarioId <= 0)
                    return new ApiResponse<object> { success = false, message = "usuario_id es requerido" };

                return await casoCivilData.EliminarCasoCivil(casoId, usuarioId);
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
    }
}
