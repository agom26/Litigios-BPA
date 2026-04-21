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
    public class CasosLaboralesSIModel
    {
        CasosLaboralesSegundaIDataAccess casoLaboralData = new CasosLaboralesSegundaIDataAccess();

        public async Task<ApiResponseCasosLaboralesSIList> ObtenerCasosLaborales(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponseCasosLaboralesSIList { success = false, message = "Usuario requerido" };

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await casoLaboralData.ListarCasosLaborales(usuarioId, pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosLaboralesSIList{ success = false, message = "Error: " + ex.Message };
            }
        }

        //crear caso
        public async Task<ApiResponseCrearCasoLaboral> CrearCasoLaboral(CrearCasoLaboralRequest req)
        {
            try
            {
                // ---- Validaciones mínimas (las mismas que PHP exige) ----
                if (req == null)
                    return new ApiResponseCrearCasoLaboral { success = false, message = "Solicitud inválida" };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseCrearCasoLaboral { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseCrearCasoLaboral { success = false, message = "Sala es requerida" };

                if (string.IsNullOrWhiteSpace(req.Notificador))
                    return new ApiResponseCrearCasoLaboral { success = false, message = "Notificador es requerido" };

                if (string.IsNullOrWhiteSpace(req.Oficial))
                    return new ApiResponseCrearCasoLaboral { success = false, message = "Oficial es requerido" };

                if (string.IsNullOrWhiteSpace(req.Estado))
                    return new ApiResponseCrearCasoLaboral { success = false, message = "Estado es requerido" };

                if (req.UsuarioCreador <= 0)
                    return new ApiResponseCrearCasoLaboral { success = false, message = "Usuario creador es requerido" };

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

                // Llamar DataAccess
                return await casoLaboralData.CrearCasoLaboral(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoLaboral { success = false, message = "Error: " + ex.Message };
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

        public async Task<ApiResponse<CasoLaboralDetalleData>> ObtenerCasoLaboralPorId(int usuarioId, int casoId)
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponse<CasoLaboralDetalleData> { success = false, message = "Usuario requerido" };

                if (casoId <= 0)
                    return new ApiResponse<CasoLaboralDetalleData> { success = false, message = "caso_id es requerido" };

                // Llamar DataAccess
                return await casoLaboralData.ObtenerCasoLaboralPorId(usuarioId, casoId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoLaboralDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponseEditarCasoLaboral> EditarCasoLaboral(EditarCasoLaboralRequest req)
        {
            try
            {
                if (req == null)
                    return new ApiResponseEditarCasoLaboral { success = false, message = "Solicitud inválida" };

                if (req.UsuarioId <= 0)
                    return new ApiResponseEditarCasoLaboral { success = false, message = "Usuario requerido" };

                if (req.CasoId <= 0)
                    return new ApiResponseEditarCasoLaboral { success = false, message = "Caso requerido" };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseEditarCasoLaboral { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Juzgado))
                    return new ApiResponseEditarCasoLaboral { success = false, message = "Juzgado es requerido" };

                // fecha obligatoria según PHP (para historial)
                if (string.IsNullOrWhiteSpace(req.Fecha))
                    return new ApiResponseEditarCasoLaboral { success = false, message = "Fecha es requerida" };

                // normalizar listas
                req.Demandantes = NormalizarIds(req.Demandantes);
                req.Demandados = NormalizarIds(req.Demandados);
                req.TercerosInteresados = NormalizarIds(req.TercerosInteresados);
                req.ContactosEmpresa = NormalizarIds(req.ContactosEmpresa);

                req.AbogadosDirectores = NormalizarIds(req.AbogadosDirectores);
                req.SociosResponsables = NormalizarIds(req.SociosResponsables);
                req.AbogadosAsistentes = NormalizarIds(req.AbogadosAsistentes);

                return await casoLaboralData.EditarCasoLaboral(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoLaboral { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<ApiResponse<object>> EliminarCasoLaboral(int casoId, int usuarioId)
        {
            try
            {
                if (casoId <= 0)
                    return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

                if (usuarioId <= 0)
                    return new ApiResponse<object> { success = false, message = "usuario_id es requerido" };

                return await casoLaboralData.EliminarCasoLaboral(casoId, usuarioId);
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
        public async Task<ListarArchivosCasoLaboralResponse> ListarArchivosCasoLaboral(int casoId)
        {
            if (casoId <= 0)
                return new ListarArchivosCasoLaboralResponse { success = false, message = "caso_id es requerido", data = new List<ArchivoCasoLaboralItem>() };

            return await casoLaboralData.ListarArchivos(casoId);
        }

        public async Task<ApiResponse<SubirArchivoCasoLaboralData>> SubirArchivoCasoLaboral(int casoId, string filePath)
        {
            if (casoId <= 0)
                return new ApiResponse<SubirArchivoCasoLaboralData> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return new ApiResponse<SubirArchivoCasoLaboralData> { success = false, message = "Archivo inválido o no existe" };

            return await casoLaboralData.SubirArchivo(casoId, filePath);
        }

        public async Task<ApiResponse<List<SubirArchivoCasoLaboralData>>> SubirArchivosCasoLaboral(int casoId, List<string> filePaths)
        {
            try
            {
                if (casoId <= 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoLaboralData>>
                    {
                        success = false,
                        message = "caso_id es requerido",
                        data = new List<SubirArchivoCasoLaboralData>()
                    };
                }

                if (filePaths == null || filePaths.Count == 0)
                {
                    return new ApiResponse<List<SubirArchivoCasoLaboralData>>
                    {
                        success = false,
                        message = "Debe seleccionar al menos un archivo.",
                        data = new List<SubirArchivoCasoLaboralData>()
                    };
                }

                return await casoLaboralData.SubirArchivos(casoId, filePaths);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SubirArchivoCasoLaboralData>>
                {
                    success = false,
                    message = "Error: " + ex.Message,
                    data = new List<SubirArchivoCasoLaboralData>()
                };
            }
        }

        public async Task<ApiResponse<object>> EliminarArchivoCasoLaboral(int casoId, string archivoId)
        {
            if (casoId <= 0)
                return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(archivoId))
                return new ApiResponse<object> { success = false, message = "archivo_id es requerido" };

            return await casoLaboralData.EliminarArchivo(casoId, archivoId);
        }

        public async Task<ApiResponse<string>> DescargarArchivoCasoLaboral(int casoId, string archivoId, string saveToPath)
        {
            if (casoId <= 0)
                return new ApiResponse<string> { success = false, message = "caso_id es requerido" };

            if (string.IsNullOrWhiteSpace(archivoId))
                return new ApiResponse<string> { success = false, message = "archivo_id es requerido" };

            if (string.IsNullOrWhiteSpace(saveToPath))
                return new ApiResponse<string> { success = false, message = "Ruta destino inválida" };

            return await casoLaboralData.DescargarArchivo(casoId, archivoId, saveToPath);
        }
    }
}
