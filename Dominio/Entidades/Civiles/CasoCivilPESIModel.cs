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
    }
}
