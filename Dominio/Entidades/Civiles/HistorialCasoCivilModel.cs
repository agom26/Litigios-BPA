using AccesoDatos.Entidades;
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
    public class HistorialCasoCivilModel
    {
        private HistorialCasoCivilDataAccess historialCivilData = new HistorialCasoCivilDataAccess();

        public async Task<ApiResponseHistorialCasoCivil> ObtenerHistorialCasoCivil(int casoId)
        {
            try
            {
                if (casoId <= 0)
                    return new ApiResponseHistorialCasoCivil { success = false, message = "Id de caso es requerido" };

                return await historialCivilData.ListarHistorialCasoCivil(casoId);
            }
            catch (Exception ex)
            {
                return new ApiResponseHistorialCasoCivil
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<object>> EditarHistorialCaso(EditarHistorialCasoCivilRequest req)
        {
            try
            {
                if (req == null)
                    return new ApiResponse<object> { success = false, message = "Solicitud inválida" };

                if (req.HistorialId <= 0)
                    return new ApiResponse<object> { success = false, message = "Historial requerido" };

                if (req.CasoId <= 0)
                    return new ApiResponse<object> { success = false, message = "Caso requerido" };

                if (req.UsuarioId <= 0)
                    return new ApiResponse<object> { success = false, message = "Usuario requerido" };

                if (string.IsNullOrWhiteSpace(req.Fecha))
                    return new ApiResponse<object> { success = false, message = "Fecha requerida" };

                if (string.IsNullOrWhiteSpace(req.Estado))
                    return new ApiResponse<object> { success = false, message = "Estado requerido" };

                req.Estado = req.Estado.Trim();
                req.Anotaciones = string.IsNullOrWhiteSpace(req.Anotaciones) ? null : req.Anotaciones.Trim();
                req.FechaVencimiento = string.IsNullOrWhiteSpace(req.FechaVencimiento) ? "" : req.FechaVencimiento.Trim();

                return await historialCivilData.EditarHistorialCasoCivil(req);
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

        public async Task<ApiResponse<object>> EliminarHistorialCasoCivil(int historialId, int casoId, int usuarioId)
        {
            try
            {
                if (historialId <= 0)
                    return new ApiResponse<object> { success = false, message = "historial_id es requerido" };

                if (casoId <= 0)
                    return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

                if (usuarioId <= 0)
                    return new ApiResponse<object> { success = false, message = "usuario_id es requerido" };

                return await historialCivilData.EliminarHistorialCasoCivil(historialId, casoId, usuarioId);
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

        public async Task<ApiResponse<object>> TerminarCasoCivil(
        int casoId,
        int usuarioId,
        string fecha,
        string anotaciones,
        string origen)
        {
            try
            {
                if (casoId <= 0)
                    return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

                if (usuarioId <= 0)
                    return new ApiResponse<object> { success = false, message = "usuario_id es requerido" };

                if (string.IsNullOrWhiteSpace(fecha))
                    return new ApiResponse<object> { success = false, message = "fecha es requerida" };

                if (string.IsNullOrWhiteSpace(origen))
                    return new ApiResponse<object> { success = false, message = "origen es requerido" };

                anotaciones = string.IsNullOrWhiteSpace(anotaciones) ? null : anotaciones.Trim();

                return await historialCivilData.TerminarCasoCivil(
                    casoId,
                    usuarioId,
                    fecha,
                    anotaciones,
                    origen
                );
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
