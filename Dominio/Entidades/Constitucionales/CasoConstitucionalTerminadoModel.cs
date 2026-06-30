using AccesoDatos.Entidades.Constitucionales;
using Comun.Models;
using Comun.Models.Casos.Civiles;
using Comun.Models.Casos.Constitucionales;
using Comun.Models.Casos.Contenciosos;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.Constitucionales
{
    public class CasoConstitucionalTerminadoModel
    {
        CasosConstitucionalesTerminadosDataAccess casoConstitucionalData= new CasosConstitucionalesTerminadosDataAccess();
        public async Task<ApiResponseCasosLaboralesList> ObtenerCasosPorRama(
            int usuarioId,
            int pagina,
            int registros,
            string? rama = null,
            string? filtro = null
        )
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponseCasosLaboralesList { success = false, message = "Usuario requerido" };

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await casoConstitucionalData.ListarCasosPorRama(usuarioId, pagina, registros, rama, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosLaboralesList { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<object> ObtenerCaso(int usuarioId, int casoId)
        {
            var resp = await casoConstitucionalData.ObtenerCasoPorId(usuarioId, casoId);

            if (!resp.success)
                throw new Exception(resp.message);

            string rama = resp.rama?.ToString() ?? "";

            var data = (JObject)resp.data;

            switch (rama)
            {
                case "CIVIL":
                    return data.ToObject<CasoCivilDetalleData>();

                case "CIVIL VIA APREMIO":
                    return data.ToObject<CasoCivilViaApremioDetalleData>();

                case "LABORAL":
                    return data.ToObject<CasoLaboralDetalleData>();

                case "CONTENCIOSO":
                    return data.ToObject<CasoContenciosoDetalleData>();

                case "CONTENCIOSO RECURSO DE CASACION":
                    return data.ToObject<CasoRecursoCasacionDetalleData>();

                default:
                    throw new Exception("Rama no soportada");
            }
        }

        public async Task<ApiResponseCrearCasoAmparo> CrearCasoAmparo(CrearCasoAmparoRequest req)
        {
            try
            {
                if (req == null)
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Solicitud inválida" };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Estado))
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Estado es requerido" };

                if (string.IsNullOrWhiteSpace(req.Oficial))
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Oficial es requerido" };

                if (string.IsNullOrWhiteSpace(req.Causa))
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Causa del Agravio/Hecho es requerida" };

                if (string.IsNullOrWhiteSpace(req.NombreParticular))
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Nombre del amparo es requerido" };

                if (req.UsuarioCreador <= 0)
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Usuario creador es requerido" };

                if (req.CasoReferenciaId <= 0)
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Debe seleccionar caso de referencia" };

                if (string.IsNullOrWhiteSpace(req.Causa))
                    return new ApiResponseCrearCasoAmparo { success = false, message = "La causa del amparo es requerida" };

                req.Expediente = req.Expediente.Trim();
                req.Estado = req.Estado.Trim();

                req.NombreParticular = string.IsNullOrWhiteSpace(req.NombreParticular) ? null : req.NombreParticular.Trim();
                req.Oficial = string.IsNullOrWhiteSpace(req.Oficial) ? null : req.Oficial.Trim();
                req.Observaciones = string.IsNullOrWhiteSpace(req.Observaciones) ? null : req.Observaciones.Trim();
                req.Causa = string.IsNullOrWhiteSpace(req.Causa) ? null : req.Causa.Trim();

                // Fechas
                req.Fecha = string.IsNullOrWhiteSpace(req.Fecha) ? null : req.Fecha.Trim();
                req.FechaVencimiento = string.IsNullOrWhiteSpace(req.FechaVencimiento) ? null : req.FechaVencimiento.Trim();

                req.Solicitantes = NormalizarIds(req.Solicitantes);
                req.AutoridadesImpugnadas = NormalizarIds(req.AutoridadesImpugnadas);
                req.TercerosInteresados = NormalizarIds(req.TercerosInteresados);
                req.ContactosEmpresa = NormalizarIds(req.ContactosEmpresa);

                req.AbogadosDirectores = NormalizarIds(req.AbogadosDirectores);
                req.SociosResponsables = NormalizarIds(req.SociosResponsables);
                req.AbogadosAsistentes = NormalizarIds(req.AbogadosAsistentes);
                // Validar que no estén vacías
                if (!req.Solicitantes.Any())
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Debe ingresar al menos un solicitante" };

                if (!req.AutoridadesImpugnadas.Any())
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Debe ingresar al menos una autoridad impugnada" };

                if (!req.TercerosInteresados.Any())
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Debe ingresar al menos un tercero interesado" };

                if (!req.AbogadosDirectores.Any())
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Debe ingresar al menos un abogado director" };

                if (!req.SociosResponsables.Any())
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Debe ingresar al menos un socio responsable" };

                if (!req.AbogadosAsistentes.Any())
                    return new ApiResponseCrearCasoAmparo { success = false, message = "Debe ingresar al menos un abogado asistente" };
                // 🔥 Llamar DataAccess
                return await casoConstitucionalData.CrearCasoAmparo(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoAmparo
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        private static List<int>? NormalizarIds(List<int>? ids)
        {
            if (ids == null) return null;

            var clean = ids
                .Where(x => x > 0)
                .Distinct()
                .ToList();

            return clean.Count == 0 ? new List<int>() : clean;
        }

        public async Task<ApiResponseCasosConstitucionalesList> ObtenerCasosConstitucionales(
           int usuarioId,
           int pagina,
           int registros,
           string? filtro = null
        )
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponseCasosConstitucionalesList { success = false, message = "Usuario requerido" };

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await casoConstitucionalData.ListarCasosConstitucionales(usuarioId, pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosConstitucionalesList { success = false, message = "Error: " + ex.Message };
            }
        }

        public async Task<ApiResponseEditarCasoAmparo> EditarCasoAmparo(EditarCasoAmparoRequest req)
        {
            try
            {
                if (req == null)
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Solicitud inválida" };

                if (req.CasoId <= 0)
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Caso es requerido" };

                if (req.UsuarioId <= 0)
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Usuario es requerido" };

                if (string.IsNullOrWhiteSpace(req.Expediente))
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Expediente es requerido" };

                if (string.IsNullOrWhiteSpace(req.Estado))
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Estado es requerido" };

                if (req.CasoReferenciaId <= 0)
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Debe seleccionar caso de referencia" };

                if (string.IsNullOrWhiteSpace(req.Causa))
                    return new ApiResponseEditarCasoAmparo { success = false, message = "La causa es requerida" };

                // LIMPIEZA
                req.Expediente = req.Expediente.Trim();
                req.Estado = req.Estado.Trim();

                req.NombreParticular = string.IsNullOrWhiteSpace(req.NombreParticular) ? null : req.NombreParticular.Trim();
                req.Oficial = string.IsNullOrWhiteSpace(req.Oficial) ? null : req.Oficial.Trim();
                req.Observaciones = string.IsNullOrWhiteSpace(req.Observaciones) ? null : req.Observaciones.Trim();
                req.Causa = string.IsNullOrWhiteSpace(req.Causa) ? null : req.Causa.Trim();

                req.Fecha = string.IsNullOrWhiteSpace(req.Fecha) ? null : req.Fecha.Trim();
                req.FechaVencimiento = string.IsNullOrWhiteSpace(req.FechaVencimiento) ? null : req.FechaVencimiento.Trim();

                // NORMALIZAR LISTAS
                req.Solicitantes = NormalizarIds(req.Solicitantes);
                req.AutoridadesImpugnadas = NormalizarIds(req.AutoridadesImpugnadas);
                req.TercerosInteresados = NormalizarIds(req.TercerosInteresados);
                req.ContactosEmpresa = NormalizarIds(req.ContactosEmpresa);

                req.AbogadosDirectores = NormalizarIds(req.AbogadosDirectores);
                req.SociosResponsables = NormalizarIds(req.SociosResponsables);
                req.AbogadosAsistentes = NormalizarIds(req.AbogadosAsistentes);

                // Validar que no estén vacías
                if (!req.Solicitantes.Any())
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Debe ingresar al menos un solicitante" };

                if (!req.AutoridadesImpugnadas.Any())
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Debe ingresar al menos una autoridad impugnada" };

                if (!req.TercerosInteresados.Any())
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Debe ingresar al menos un tercero interesado" };

                if (!req.AbogadosDirectores.Any())
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Debe ingresar al menos un abogado director" };

                if (!req.SociosResponsables.Any())
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Debe ingresar al menos un socio responsable" };

                if (!req.AbogadosAsistentes.Any())
                    return new ApiResponseEditarCasoAmparo { success = false, message = "Debe ingresar al menos un abogado asistente" };

                // LLAMADA AL DATA ACCESS
                return await casoConstitucionalData.EditarCasoAmparo(req);
            }
            catch (Exception ex)
            {
                return new ApiResponseEditarCasoAmparo
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        //obtener amparo por id
        public async Task<ApiResponse<CasoConstitucionalDetalleData>> ObtenerCasoConstitucionalPorId(int usuarioId, int casoId)
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponse<CasoConstitucionalDetalleData> { success = false, message = "Usuario requerido" };

                if (casoId <= 0)
                    return new ApiResponse<CasoConstitucionalDetalleData> { success = false, message = "Caso es requerido" };

                return await casoConstitucionalData.ObtenerCasoConstitucionalPorId(usuarioId, casoId);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CasoConstitucionalDetalleData>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        //eliminar caso constitucional
        public async Task<ApiResponse<object>> EliminarCasoConstitucional(int casoId, int usuarioId)
        {
            try
            {
                if (casoId <= 0)
                    return new ApiResponse<object> { success = false, message = "caso_id es requerido" };

                if (usuarioId <= 0)
                    return new ApiResponse<object> { success = false, message = "usuario_id es requerido" };

                return await casoConstitucionalData.EliminarCasoConstitucional(casoId, usuarioId);
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
