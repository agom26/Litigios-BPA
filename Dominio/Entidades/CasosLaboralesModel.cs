using AccesoDatos.Entidades;
using Comun.Models.Casos.Laborales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class CasosLaboralesModel
    {
        private CasosLaboralesDataAccess casoLaboralData = new CasosLaboralesDataAccess();

        public async Task<ApiResponseCasosLaboralesList> ObtenerCasosLaborales(
            int usuarioId,
            int pagina,
            int registros,
            string? filtro = null
        )
        {
            try
            {
                if (usuarioId <= 0)
                    return new ApiResponseCasosLaboralesList { success = false, message = "Usuario requerido" };

                if (pagina <= 0) pagina = 1;
                if (registros <= 0) registros = 10;

                return await casoLaboralData.ListarCasosLaborales(usuarioId, pagina, registros, filtro);
            }
            catch (Exception ex)
            {
                return new ApiResponseCasosLaboralesList { success = false, message = "Error: " + ex.Message };
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
                    return new ApiResponseCrearCasoLaboral { success = false, message = "Juzgado es requerido" };

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
    }
}


