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
    public class CasoCivilPESIModel
    {
        CasosCivilesPESIDataAccess casoCivilData= new CasosCivilesPESIDataAccess();
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

                return await casoCivilData.ListarCasosSegundaInstancia(usuarioId, pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosCivilesList { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<ApiResponse<CasoCivilViaApremioDetalleData>> ObtenerCasoCivilPorId(int usuarioId, int casoId)
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponse<CasoCivilViaApremioDetalleData> { success = false, message = "Usuario requerido" };

                if (casoId <= 0)
                    return new ApiResponse<CasoCivilViaApremioDetalleData> { success = false, message = "Id de caso es requerido" };

                // Llamar DataAccess
                return await casoCivilData.ObtenerCasoViaApremioPorId(usuarioId, casoId);
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

        public async Task<ApiResponseEditarCasoCivil> EditarCasoCivil(EditarCasoViaApremioRequest req)
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

                return await casoCivilData.EditarCasoViaApremio(req);
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

                return await casoCivilData.EliminarCaso(casoId, usuarioId);
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
    }
}
