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
    public class ArchivosCivilesModel
    {
        ArchivosCivilesDataAccess archivosCivilesData = new ArchivosCivilesDataAccess();


        public async Task<ListarArchivosCasoCivilResponse> ListarArchivosCasoCivil(int casoId)
        {
            if (casoId <= 0)
                return new ListarArchivosCasoCivilResponse { success = false, message = "caso_id es requerido", data = new List<ArchivoCasoCivilItem>() };

            return await archivosCivilesData.ListarArchivos(casoId);
        }

        public async Task<ApiResponse<SubirArchivoCasoCivilData>> SubirArchivoCasoCivil(int casoId, string filePath)
        {
            if (casoId <= 0)
                return new ApiResponse<SubirArchivoCasoCivilData> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return new ApiResponse<SubirArchivoCasoCivilData> { success = false, message = "Archivo inválido o no existe" };

            return await archivosCivilesData.SubirArchivo(casoId, filePath);
        }

        public async Task<ApiResponse<List<SubirArchivoCasoCivilData>>> SubirArchivosCasoCivil(int casoId, List<string> filePaths)
        {
            try
            {
                if (casoId <= 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoCivilData>>
                    {
                        success = false,
                        message = "caso_id es requerido",
                        data = new List<SubirArchivoCasoCivilData>()
                    };
                }

                if (filePaths == null || filePaths.Count == 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoCivilData>>
                    {
                        success = false,
                        message = "Debe seleccionar al menos un archivo.",
                        data = new List<SubirArchivoCasoCivilData>()
                    };
                }

                return await archivosCivilesData.SubirArchivos(casoId, filePaths);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SubirArchivoCasoCivilData>>
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    data = new List<SubirArchivoCasoCivilData>()
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarArchivoCasoCivil(int casoId, string archivoId)
        {
            if (casoId <= 0)
                return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(archivoId))
                return new ApiResponse<object> { success = false, message = "archivo_id es requerido" };

            return await archivosCivilesData.EliminarArchivo(casoId, archivoId);
        }

        public async Task<ApiResponse<string>> DescargarArchivoCasoCivil(int casoId, string archivoId, string saveToPath)
        {
            if (casoId <= 0)
                return new ApiResponse<string> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(archivoId))
                return new ApiResponse<string> { success = false, message = "archivo_id es requerido" };

            if (string.IsNullOrWhiteSpace(saveToPath))
                return new ApiResponse<string> { success = false, message = "Ruta destino inválida" };

            return await archivosCivilesData.DescargarArchivo(casoId, archivoId, saveToPath);
        }
    }
}
