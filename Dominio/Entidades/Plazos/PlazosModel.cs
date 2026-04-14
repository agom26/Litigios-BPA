using AccesoDatos.Entidades.Plazos;
using Comun.Models;
using Comun.Models.Casos.Laborales;
using Comun.Models.Plazos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio.Entidades.Plazos
{
    public class PlazosModel
    {
        PlazosDataAccess plazosData = new PlazosDataAccess();

        public async Task<ApiResponsePlazosList> ObtenerPlazos(
            int usuarioId,
            int pagina,
            int registros,
            int? modulo = null,
            string? filtro = null
        )
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponsePlazosList { success = false, message = "Usuario requerido" };

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await plazosData.ListarPlazos(usuarioId, pagina, registros, modulo, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponsePlazosList { success = false, message = "Error: " + ex.Message };
            }
        }

        //obtener por id
        public async Task<ApiResponse<PlazoDetalleData>> ObtenerPlazoPorId(int usuarioId, int historialId)
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponse<PlazoDetalleData> { success = false, message = "Usuario requerido" };

                if (historialId <= 0)
                    return new ApiResponse<PlazoDetalleData> { success = false, message = "El historial es requerido" };

                // Llamar DataAccess
                return await plazosData.ObtenerPlazoPorId(usuarioId, historialId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PlazoDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<object>> EditarPlazo(
            int usuarioId,
            int historialId,
            DateTime fechaEstado,
            string estado,
            string anotaciones,
            DateTime? fechaVencimiento = null
        )
        {
            if (usuarioId <= 0)
                return new ApiResponse<object> { success = false, message = "El usuario es obligatorio" };

            if (historialId <= 0)
                return new ApiResponse<object> { success = false, message = "El historial es obligatorio" };

            return await plazosData.EditarPlazo(
                usuarioId,
                historialId,
                fechaEstado,
                estado,
                anotaciones,
                fechaVencimiento
            );
        }

        public async Task<ApiResponsePlazosReporteList> GenerarReportePlazos(
            int usuarioId,
            int? rama = null,
            string? expediente = null,
            string? nombre = null,
            string? oficial = null,
            string? notificador = null,
            string? tipoInstancia = null,
            string? organoJudicial = null,
            string? estado = null,
            string? origen = null,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null,
            DateTime? fechaVencInicio = null,
            DateTime? fechaVencFin = null
        )
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponsePlazosReporteList
                    {
                        success = false,
                        message = "Usuario requerido"
                    };

                return await plazosData.GenerarReportePlazos(
                    usuarioId,
                    rama,
                    expediente,
                    nombre,
                    oficial,
                    notificador,
                    organoJudicial,
                    tipoInstancia,
                    estado,
                    origen,
                    fechaInicio,
                    fechaFin,
                    fechaVencInicio,
                    fechaVencFin
                );
            }
            catch (Exception ex)
            {
                return new ApiResponsePlazosReporteList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }
    }
}
