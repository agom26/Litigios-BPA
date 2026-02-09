using AccesoDatos.Entidades;
using Comun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class DemandanteModel
    {
        private DemandanteDataAccess demandanteData = new DemandanteDataAccess();

        public async Task<ApiGetUserListResponse<List<PersonaListDataResponse>>> ObtenerDemanante(int pagina, int cantidad)
        {
            try
            {
                return await demandanteData.GetDemandantes(pagina, cantidad);
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

        public async Task<ApiGetUserListResponse<List<PersonaListDataResponse>>> ObtenerDemandantesFiltrados(int pagina, int cantidad, string filtro)
        {
            try
            {
                return await demandanteData.GetDemandantesFiltrados(pagina, cantidad, filtro);
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

        public async Task<ApiResponse<object>> CrearDemandante(
            string nombre,
            string direccion,
            string correo,
            string telefono,
            string nombreAbogado,
            string telefonoAbogado,
            string correoAbogado
        )
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return new ApiResponse<object> { success = false, message = "El nombre es obligatorio" };

            if (string.IsNullOrWhiteSpace(direccion))
                return new ApiResponse<object> { success = false, message = "La dirección es obligatoria" };

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

            return await demandanteData.CrearDemandante(
                nombre,
                direccion,
                correo,
                telefono,
                nombreAbogado,
                telefonoAbogado,
                correoAbogado
            );
        }

        public async Task<ApiDemandadoDetalleResponse> ObtenerDetallesDemandantePorId(int idPersona)
        {
            try
            {
                return await demandanteData.ObtenerDetallesDemandantePorId(idPersona);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

        }

        public async Task<ApiResponse<object>> EditarDemandante(
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
            if (string.IsNullOrWhiteSpace(nombre))
                return new ApiResponse<object> { success = false, message = "El usuario es obligatorio" };

            if (string.IsNullOrWhiteSpace(direccion))
                return new ApiResponse<object> { success = false, message = "El nombre es obligatorio" };


            if (!string.IsNullOrWhiteSpace(correo))
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(correo))
                    return new ApiResponse<object> { success = false, message = "El correo no tiene un formato válido" };
            }

            if (!string.IsNullOrWhiteSpace(correoAbogado))
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(correoAbogado))
                    return new ApiResponse<object> { success = false, message = "El correo del abogado no tiene un formato válido" };
            }


            if (!string.IsNullOrWhiteSpace(telefono))
            {
                var phoneRegex = new Regex(@"^\+?\d{7,15}$");
                if (!phoneRegex.IsMatch(telefono))
                    return new ApiResponse<object> { success = false, message = "El teléfono no tiene un formato válido" };
            }

            if (!string.IsNullOrWhiteSpace(telefonoAbogado))
            {
                var phoneRegex = new Regex(@"^\+?\d{7,15}$");
                if (!phoneRegex.IsMatch(telefonoAbogado))
                    return new ApiResponse<object> { success = false, message = "El teléfono del abogado no tiene un formato válido" };
            }

            return await demandanteData.EditarDemandante(
                idPersona,
                nombre,
                direccion,
                correo,
                telefono,
                nombreAbogado,
                correoAbogado,
                telefonoAbogado
            );
        }

        public async Task<ApiResponse<object>> EliminarDemandante(int idPersona)
        {
            if (idPersona <= 0)
                return new ApiResponse<object>
                {
                    success = false,
                    message = "ID de demandante inválido"
                };

            return await demandanteData.EliminarDemandante(idPersona);
        }
    }
}
