using AccesoDatos.Entidades;
using Comun;
using Comun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    message = "Error en dominio: " + ex.Message
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
                
                throw new Exception("Error en dominio: " + ex.Message);
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
    }
}
