using AccesoDatos.Entidades.Contenciosos;
using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Contenciosos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.Contenciosos
{
    public class ArchivosContenciososModel
    {
        ArchivosCasosContenciososDataAccess archivosContenciososData = new ArchivosCasosContenciososDataAccess();
        public async Task<ListarArchivosCasoContenciosoResponse> ListarArchivosCasoContencioso(int casoId)
        {
            if (casoId <= 0)
                return new ListarArchivosCasoContenciosoResponse { success = false, message = "caso es requerido", data = new List<ArchivoCasoContenciosoItem>() };

            return await archivosContenciososData.ListarArchivos(casoId);
        }

        public async Task<ApiResponse<SubirArchivoCasoContenciosoData>> SubirArchivoCasoContencioso(int casoId, string filePath)
        {
            if (casoId <= 0)
                return new ApiResponse<SubirArchivoCasoContenciosoData> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return new ApiResponse<SubirArchivoCasoContenciosoData> { success = false, message = "Archivo inválido o no existe" };

            return await archivosContenciososData.SubirArchivo(casoId, filePath);
        }

        public async Task<ApiResponse<List<SubirArchivoCasoContenciosoData>>> SubirArchivosCasoContencioso(int casoId, List<string> filePaths)
        {
            try
            {
                if (casoId <= 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoContenciosoData>>
                    {
                        success = false,
                        message = "caso_id es requerido",
                        data = new List<SubirArchivoCasoContenciosoData>()
                    };
                }

                if (filePaths == null || filePaths.Count == 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoContenciosoData>>
                    {
                        success = false,
                        message = "Debe seleccionar al menos un archivo.",
                        data = new List<SubirArchivoCasoContenciosoData>()
                    };
                }

                return await archivosContenciososData.SubirArchivos(casoId, filePaths);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SubirArchivoCasoContenciosoData>>
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    data = new List<SubirArchivoCasoContenciosoData>()
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarArchivoCasoContencioso(int casoId, string archivoId)
        {
            if (casoId <= 0)
                return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(archivoId))
                return new ApiResponse<object> { success = false, message = "archivo_id es requerido" };

            return await archivosContenciososData.EliminarArchivo(casoId, archivoId);
        }

        public async Task<ApiResponse<string>> DescargarArchivoCasoContencioso(int casoId, string archivoId, string saveToPath)
        {
            if (casoId <= 0)
                return new ApiResponse<string> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(archivoId))
                return new ApiResponse<string> { success = false, message = "archivo_id es requerido" };

            if (string.IsNullOrWhiteSpace(saveToPath))
                return new ApiResponse<string> { success = false, message = "Ruta destino inválida" };

            return await archivosContenciososData.DescargarArchivo(casoId, archivoId, saveToPath);
        }
    }
}
