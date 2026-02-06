using AccesoDatos.Entidades;
using Comun;
using Comun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominio.Entidades
{
    public class UserModel
    {
        private UserDataAccess userData = new UserDataAccess();

        public async Task<ApiResponse<UserDataResponse>> LoginUser(string user, string pass)
        {
            
            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentException("El usuario no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(pass))
                throw new ArgumentException("La contraseña no puede estar vacía.");

           
            try
            {
                return await userData.Login(user, pass);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la autenticación: " + ex.Message);
            }
            
        }

        public async Task<ApiGetUserListResponse<List<UserListDataResponse>>> ObtenerUsuarios(int pagina, int cantidad)
        {
            try
            {
                // Aquí podrías validar que página sea > 0, etc.
                return await userData.GetUsuarios(pagina, cantidad);
            }
            catch (Exception ex)
            {
                return new ApiGetUserListResponse<List<UserListDataResponse>>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiGetUserListResponse<List<UserListDataResponse>>> ObtenerUsuariosFiltrados(int pagina, int cantidad, string filtro)
        {
            try
            {
                // Aquí podrías validar que página sea > 0, etc.
                return await userData.GetUsuariosFiltrados(pagina, cantidad, filtro);
            }
            catch (Exception ex)
            {
                return new ApiGetUserListResponse<List<UserListDataResponse>>
                {
                    success = false,
                    message = "Error: " + ex.Message
                };
            }
        }

        public async Task<ApiGetModulosRoles> ObtenerModulosRoles()
        {

            
            try
            {
                return await userData.GetModulosRoles();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error: " + ex.Message);
            }

        }

        public async Task<ApiGetUsuarioDetalles> ObtenerUsuarioPorId(int idUsuario)
        {
            try
            {
                return await userData.ObtenerDetallesUsuarioPorId(idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

        }

        public async Task<ApiPermisosResponse> ObtenerPermisosPorUsuarioId(int idUsuario)
        {
            try
            {
                return await userData.ObtenerPermisosoPorUsuarioId(idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

        }

        public async Task<ApiResponse<object>> EditarUsuario(
            int idUsuario,
            string usuario,
            string password,
            string nombres,
            string apellidos,
            string correo,
            string telefono,
            string permisosJson
        )
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return new ApiResponse<object> { success = false, message = "El usuario es obligatorio" };

            if (string.IsNullOrWhiteSpace(nombres))
                return new ApiResponse<object> { success = false, message = "El nombre es obligatorio" };

            if (string.IsNullOrWhiteSpace(telefono))
                return new ApiResponse<object> { success = false, message = "El teléfono es obligatorio" };

            if (string.IsNullOrWhiteSpace(apellidos))
                return new ApiResponse<object> { success = false, message = "El apellido es obligatorio" };

            
            if (!string.IsNullOrWhiteSpace(correo))
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(correo))
                    return new ApiResponse<object> { success = false, message = "El correo no tiene un formato válido" };
            }

           
            if (!string.IsNullOrWhiteSpace(telefono))
            {
                var phoneRegex = new Regex(@"^\+?\d{7,15}$"); 
                if (!phoneRegex.IsMatch(telefono))
                    return new ApiResponse<object> { success = false, message = "El teléfono no tiene un formato válido" };
            }
            
            return await userData.EditarUsuario(
                idUsuario,
                usuario,
                password,
                nombres,
                apellidos,
                correo,
                telefono,
                permisosJson
            );
        }

        public async Task<ApiResponse<object>> CrearUsuario(
            string usuario,
            string password,
            string nombres,
            string apellidos,
            string correo,
            string telefono,
            string permisosJson
        )
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return new ApiResponse<object> { success = false, message = "El usuario es obligatorio" };

            if (string.IsNullOrWhiteSpace(nombres))
                return new ApiResponse<object> { success = false, message = "El nombre es obligatorio" };

            if (string.IsNullOrWhiteSpace(apellidos))
                return new ApiResponse<object> { success = false, message = "El apellido es obligatorio" };
            
            if (string.IsNullOrWhiteSpace(telefono))
                return new ApiResponse<object> { success = false, message = "El teléfono es obligatorio" };

            if (!string.IsNullOrWhiteSpace(correo))
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(correo))
                    return new ApiResponse<object> { success = false, message = "El correo no tiene un formato válido" };
            }

            if (!string.IsNullOrWhiteSpace(telefono))
            {
                var phoneRegex = new Regex(@"^\+?\d{7,15}$");
                if (!phoneRegex.IsMatch(telefono))
                    return new ApiResponse<object> { success = false, message = "El teléfono no tiene un formato válido" };
            }

            return await userData.CrearUsuario(
                usuario,
                password,
                nombres,
                apellidos,
                correo,
                telefono,
                permisosJson
            );
        }

        public async Task<ApiResponse<object>> EliminarUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                return new ApiResponse<object>
                {
                    success = false,
                    message = "ID de usuario inválido"
                };

            return await userData.EliminarUsuario(idUsuario);
        }
    }
}
