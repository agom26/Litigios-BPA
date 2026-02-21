using Comun.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class ContactoEmpresaDataAccess
    {
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/contacto_de_empresa.php";

        public async Task<ApiGetUserListResponse<List<PersonaListDataResponse>>> GetContactosDeEmpresa(int paginaActual, int cantidad)
        {
            using (var client = new HttpClient())
            {

                var parameters = new Dictionary<string, string>
                {
                    { "action", "get_contactos_de_empresa" },
                    { "pagina",  paginaActual.ToString()},
                    { "registrosPorPagina", cantidad.ToString()}
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApiGetUserListResponse<List<PersonaListDataResponse>>>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiGetUserListResponse<List<PersonaListDataResponse>>
                    {
                        success = false,
                        message = "Error: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiGetUserListResponse<List<PersonaListDataResponse>>> GetContactosDeEmpresaFiltrados(int paginaActual, int cantidad, string filtro)
        {
            using (var client = new HttpClient())
            {

                var parameters = new Dictionary<string, string>
                {
                    { "action", "get_contactos_de_empresa_filtrados" },
                    { "pagina",  paginaActual.ToString()},
                    { "registrosPorPagina", cantidad.ToString()},
                    {"filtro",filtro}
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApiGetUserListResponse<List<PersonaListDataResponse>>>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiGetUserListResponse<List<PersonaListDataResponse>>
                    {
                        success = false,
                        message = "Error: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiDemandadoDetalleResponse> ObtenerDetallesContactoEmpresaPorId(int id)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "action", "get_contacto_de_empresa_por_id" },
                    { "id_persona", id.ToString()}
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    // Deserializamos la respuesta usando Newtonsoft.Json
                    return JsonConvert.DeserializeObject<ApiDemandadoDetalleResponse>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiDemandadoDetalleResponse
                    {
                        success = false,
                        message = "Error: " + ex.Message
                    };
                }
            }
        }
        public async Task<ApiResponse<object>> EditarContactoDeEmpresa(
            int idPersona,
            string nombre,
            string direccion,
            string correo,
            string telefono,
            string nombreAbogado,
            string correoAbogado,
            string telefonoAbogado
        )
        {
            using (var client = new HttpClient())
            {
                // Preparamos los parámetros según lo que espera PHP
                var parameters = new Dictionary<string, string>
                {
                    { "action", "editar_contacto_de_empresa" },
                    { "id_persona", idPersona.ToString() },
                    { "nombre_persona", nombre},
                    { "direccion_persona", direccion},
                    { "correo_persona", correo ?? "" },
                    { "telefono_persona", telefono ?? "" },
                    { "nombre_abogado", nombreAbogado ?? "" },
                    { "correo_abogado", correoAbogado ?? ""},
                    { "telefono_abogado",telefonoAbogado ?? ""}
                };


                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApiResponse<object>>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiResponse<object>
                    {
                        success = false,
                        message = "Error de conexión: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiResponse<object>> CrearContactoDeEmpresa(

            string nombre,
            string direccion,
            string correo,
            string telefono,
            string nombreAbogado,
            string telefonoAbogado,
            string correoAbogado
        )
        {
            using (var client = new HttpClient())
            {
                // Preparamos los parámetros según lo que espera PHP
                var parameters = new Dictionary<string, string>
                {
                    { "action", "crear_contacto_de_empresa" },
                    { "nombre_persona", nombre},
                    { "direccion_persona",direccion},
                    { "telefono_persona", telefono ?? ""},
                    { "correo_persona", correo ?? "" },
                    { "nombre_abogado", nombreAbogado ?? "" },
                    { "telefono_abogado", telefonoAbogado ?? ""},
                    { "correo_abogado", correoAbogado ?? ""}
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApiResponse<object>>(jsonResult);
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

        public async Task<ApiResponse<object>> EliminarContactoDeEmpresa(int idPersona)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "action", "eliminar_contacto_de_empresa" },
                    { "id", idPersona.ToString() }
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApiResponse<object>>(jsonResult);
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
}
