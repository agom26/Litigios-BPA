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
    public class CAObtenerMarcasContenciosasModel
    {
        CAObtenerMarcasContenciosoDataAccess marcasData = new CAObtenerMarcasContenciosoDataAccess();
        public async Task<ApiResponseMarcasContenciososasList> ObtenerCasosContenciosos(
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            try
            {
              

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await marcasData.ListarMarcasContenciosas(pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseMarcasContenciososasList
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse<MarcaContenciosaListItem>> ObtenerMarcaReferenciaPorId( int marcaId)
        {
            try
            {
                
                if (marcaId <= 0)
                {
                    return new ApiResponse<MarcaContenciosaListItem>
                    {
                        success = false,
                        message = "marca es requerida"
                    };
                }

                return await marcasData.ObtenerMarcaReferenciaPorId(marcaId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<MarcaContenciosaListItem>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }
    }


}
