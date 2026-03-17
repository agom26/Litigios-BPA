using AccesoDatos.Entidades;
using Comun.Models;
using Comun.Models.Casos.Laborales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class HistorialCasoLaboralModel
    {
        private HistorialCasoLaboralDataAccess historialLaboralData = new HistorialCasoLaboralDataAccess();

        public async Task<ApiResponseHistorialCasoLaboral> ObtenerHistorialCasoLaboral(int casoId)
        {
            try
            {
                if (casoId <= 0)
                    return new ApiResponseHistorialCasoLaboral { success = false, message = "caso_id es requerido" };

                return await historialLaboralData.ListarHistorialCasoLaboral(casoId);
            }
            catch (Exception ex)
            {
                return new ApiResponseHistorialCasoLaboral
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<object>> EditarHistorialCaso(EditarHistorialCasoRequest req)
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

                return await historialLaboralData.EditarHistorialCasoLaboral(req);
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

        public async Task<ApiResponse<object>> EliminarHistorialCasoLaboral(int historialId, int casoId, int usuarioId)
        {
            try
            {
                if (historialId <= 0)
                    return new ApiResponse<object> { success = false, message = "historial_id es requerido" };

                if (casoId <= 0)
                    return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

                if (usuarioId <= 0)
                    return new ApiResponse<object> { success = false, message = "usuario_id es requerido" };

                return await historialLaboralData.EliminarHistorialCasoLaboral(historialId, casoId, usuarioId);
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
