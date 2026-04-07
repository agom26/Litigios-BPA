using AccesoDatos.Entidades.Constitucionales;
using Comun.Models;
using Comun.Models.Casos.Constitucionales;
using Comun.Models.Casos.Contenciosos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.Constitucionales
{
    public class ArchivosCasosConstitucionalesModel
    {
        ArchivosCasoConstitucionalDataAccess archivosConstitucionalData = new ArchivosCasoConstitucionalDataAccess();
        public async Task<ListarArchivosCasoConstitucionalResponse> ListarArchivosCasoConstitucional(int casoId)
        {
            if (casoId <= 0)
                return new ListarArchivosCasoConstitucionalResponse{ success = false, message = "caso es requerido", data = new List<ArchivoCasoConstitucionalItem>() };

            return await archivosConstitucionalData.ListarArchivos(casoId);
        }

        public async Task<ApiResponse<SubirArchivoCasoConstitucionalData>> SubirArchivoCasoConstitucional(int casoId, string filePath)
        {
            if (casoId <= 0)
                return new ApiResponse<SubirArchivoCasoConstitucionalData> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return new ApiResponse<SubirArchivoCasoConstitucionalData> { success = false, message = "Archivo inválido o no existe" };

            return await archivosConstitucionalData.SubirArchivo(casoId, filePath);
        }

        public async Task<ApiResponse<List<SubirArchivoCasoConstitucionalData>>> SubirArchivosCasoConstitucional(int casoId, List<string> filePaths)
        {
            try
            {
                if (casoId <= 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoConstitucionalData>>
                    {
                        success = false,
                        message = "caso_id es requerido",
                        data = new List<SubirArchivoCasoConstitucionalData>()
                    };
                }

                if (filePaths == null || filePaths.Count == 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoConstitucionalData>>
                    {
                        success = false,
                        message = "Debe seleccionar al menos un archivo.",
                        data = new List<SubirArchivoCasoConstitucionalData>()
                    };
                }

                return await archivosConstitucionalData.SubirArchivos(casoId, filePaths);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SubirArchivoCasoConstitucionalData>>
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    data = new List<SubirArchivoCasoConstitucionalData>()
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarArchivoCasoConstitucional(int casoId, string archivoId)
        {
            if (casoId <= 0)
                return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(archivoId))
                return new ApiResponse<object> { success = false, message = "archivo_id es requerido" };

            return await archivosConstitucionalData.EliminarArchivo(casoId, archivoId);
        }

        public async Task<ApiResponse<string>> DescargarArchivoCasoConstitucional(int casoId, string archivoId, string saveToPath)
        {
            if (casoId <= 0)
                return new ApiResponse<string> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(archivoId))
                return new ApiResponse<string> { success = false, message = "archivo_id es requerido" };

            if (string.IsNullOrWhiteSpace(saveToPath))
                return new ApiResponse<string> { success = false, message = "Ruta destino inválida" };

            return await archivosConstitucionalData.DescargarArchivo(casoId, archivoId, saveToPath);
        }
    }
}
