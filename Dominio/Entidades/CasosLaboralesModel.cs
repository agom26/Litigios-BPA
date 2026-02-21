using AccesoDatos.Entidades;
using Comun.Models.Casos.Laborales;
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
        public async Task<ApiResponseCrearCasoLaboral> CrearCasoLaboral(
            string expediente,
            string juzgado,
            string estado,
            string? nombreParticular,
            string? oficial,
            string? notificador,
            List<int> personasId,
            List<string> tiposPersona,
            List<int> usuariosId,
            List<string> rolesUsuario
        )
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expediente) ||
                    string.IsNullOrWhiteSpace(juzgado) ||
                    string.IsNullOrWhiteSpace(estado))
                {
                    return new ApiResponseCrearCasoLaboral
                    {
                        success = false,
                        message = "Campos obligatorios faltantes (expediente, juzgado, estado)."
                    };
                }

                // Validación básica de arrays
                if (personasId != null && tiposPersona != null && personasId.Count != tiposPersona.Count)
                {
                    return new ApiResponseCrearCasoLaboral
                    {
                        success = false,
                        message = "La cantidad de persona_id no coincide con tipo_persona."
                    };
                }

                if (usuariosId != null && rolesUsuario != null && usuariosId.Count != rolesUsuario.Count)
                {
                    return new ApiResponseCrearCasoLaboral
                    {
                        success = false,
                        message = "La cantidad de usuario_id no coincide con rol_usuario."
                    };
                }

                return await casoLaboralData.CrearCasoLaboral(
                    expediente,
                    juzgado,
                    estado,
                    nombreParticular,
                    oficial,
                    notificador,
                    personasId,
                    tiposPersona,
                    usuariosId,
                    rolesUsuario
                );
            }
            catch (Exception ex)
            {
                return new ApiResponseCrearCasoLaboral
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }
    }
}

