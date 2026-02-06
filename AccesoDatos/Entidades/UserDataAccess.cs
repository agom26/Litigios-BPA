using Comun;
using Comun.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AccesoDatos.Entidades
{
    public class UserDataAccess
    {
        // Cambia esta URL por la dirección real de tu servidor
        private readonly string _apiUrl = "http://bpa.com.es/peticiones-litigios/users.php";

        public async Task<ApiResponse<UserDataResponse>> Login(string user, string pass)
        {
            using (var client = new HttpClient())
            {
                // Preparamos los parámetros según lo que espera el $_POST en PHP
                var parameters = new Dictionary<string, string>
                {
                    { "action", "login" },
                    { "usuario", user },
                    { "password", pass }
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    // Deserializamos la respuesta usando Newtonsoft.Json
                    return JsonConvert.DeserializeObject<ApiResponse<UserDataResponse>>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiResponse<UserDataResponse>
                    {
                        success = false,
                        message = "Error de conexión: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiGetUserListResponse<List<UserListDataResponse>>> GetUsuarios(int paginaActual, int cantidad)
        {
            using (var client = new HttpClient())
            {
                
                var parameters = new Dictionary<string, string>
                {
                    { "action", "get_usuarios" },
                    { "pagina",  paginaActual.ToString()},
                    { "registrosPorPagina", cantidad.ToString()}
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApiGetUserListResponse<List<UserListDataResponse>>>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiGetUserListResponse<List<UserListDataResponse>>
                    {
                        success = false,
                        message = "Error en servidor: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiGetModulosRoles> GetModulosRoles()
        {
            using (var client = new HttpClient())
            {
                // Preparamos los parámetros según lo que espera el $_POST en PHP
                var parameters = new Dictionary<string, string>
                {
                    { "action", "get_modulos_roles" }
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    // Deserializamos la respuesta usando Newtonsoft.Json
                    return JsonConvert.DeserializeObject<ApiGetModulosRoles>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiGetModulosRoles
                    {
                        success = false,
                        message = "Error en servidor: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiGetUsuarioDetalles> ObtenerDetallesUsuarioPorId(int id)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "action", "get_usuario_por_id" },
                    { "id_usuario", id.ToString()}
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    // Deserializamos la respuesta usando Newtonsoft.Json
                    return JsonConvert.DeserializeObject<ApiGetUsuarioDetalles>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiGetUsuarioDetalles
                    {
                        success = false,
                        message = "Error: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiPermisosResponse> ObtenerPermisosoPorUsuarioId(int id)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "action", "get_permisos_usuario" },
                    { "id_usuario", id.ToString()}
                };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    var response = await client.PostAsync(_apiUrl, content);
                    var jsonResult = await response.Content.ReadAsStringAsync();

                    // Deserializamos la respuesta usando Newtonsoft.Json
                    return JsonConvert.DeserializeObject<ApiPermisosResponse>(jsonResult);
                }
                catch (Exception ex)
                {
                    return new ApiPermisosResponse
                    {
                        success = false,
                        message = "Error: " + ex.Message
                    };
                }
            }
        }

        public async Task<ApiResponse<object>> EditarUsuario(
            int idUsuario,
            string usuario,
            string password, // null si no se cambia
            string nombres,
            string apellidos,
            string correo,
            string telefono,
            string permisosJson
        )
        {
            using (var client = new HttpClient())
            {
                // Preparamos los parámetros según lo que espera PHP
                var parameters = new Dictionary<string, string>
                {
                    { "action", "editar_usuario" },
                    { "id_usuario", idUsuario.ToString() },
                    { "usuario", usuario },
                    { "nombres", nombres },
                    { "apellidos", apellidos },
                    { "correo", correo ?? "" },
                    { "telefono", telefono ?? "" },
                    { "permisos", permisosJson }
                };

                // Solo enviamos password si se cambió
                if (!string.IsNullOrEmpty(password))
                    parameters.Add("contrasena", password);

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

        public async Task<ApiResponse<object>> CrearUsuario(
            
            string usuario,
            string password, // null si no se cambia
            string nombres,
            string apellidos,
            string correo,
            string telefono,
            string permisosJson
        )
        {
            using (var client = new HttpClient())
            {
                // Preparamos los parámetros según lo que espera PHP
                var parameters = new Dictionary<string, string>
                {
                    { "action", "crear_usuario" },
                    { "usuario", usuario },
                    { "nombres", nombres },
                    { "apellidos", apellidos },
                    { "correo", correo ?? "" },
                    { "telefono", telefono ?? "" },
                    { "permisos", permisosJson }
                };

                // Solo enviamos password si se cambió
                if (!string.IsNullOrEmpty(password))
                    parameters.Add("contrasena", password);

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

        public async Task<ApiResponse<object>> EliminarUsuario(int idUsuario)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "action", "eliminar_usuario" },
                    { "id", idUsuario.ToString() }
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



    }
}
