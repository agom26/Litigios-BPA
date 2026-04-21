using AccesoDatos.Entidades.Alertas;
using Comun.Models.Alertas;
using System;

namespace Dominio.Entidades.Alertas
{
    public class AlertasModel
    {
        private readonly AlertasDataAccess alertasDao = new AlertasDataAccess();

        public async Task<ApiResponseAlertas> ObtenerAlertasUsuarioPaginadas(
            int usuarioId,
            int moduloId = 0,
            int pagina = 1,
            int registrosPorPagina = 20,
            bool soloNoLeidas = false,
            string filtro = "")
        {
            if (usuarioId <= 0)
            {
                return new ApiResponseAlertas
                {
                    success = false,
                    message = "El id del usuario debe ser mayor que cero."
                };
            }

            if (moduloId < 0)
            {
                return new ApiResponseAlertas
                {
                    success = false,
                    message = "El id del módulo no puede ser negativo."
                };
            }

            if (pagina <= 0)
            {
                return new ApiResponseAlertas
                {
                    success = false,
                    message = "La página debe ser mayor que cero."
                };
            }

            if (registrosPorPagina <= 0)
            {
                return new ApiResponseAlertas
                {
                    success = false,
                    message = "Los registros por página deben ser mayores que cero."
                };
            }

            if (registrosPorPagina > 200)
            {
                return new ApiResponseAlertas
                {
                    success = false,
                    message = "No puedes solicitar más de 200 registros por página."
                };
            }

            filtro ??= string.Empty;
            filtro = filtro.Trim();

            if (filtro.Length > 150)
            {
                return new ApiResponseAlertas
                {
                    success = false,
                    message = "El filtro no puede tener más de 150 caracteres."
                };
            }

            return await alertasDao.ObtenerAlertasUsuarioPaginadas(
                usuarioId,
                moduloId,
                pagina,
                registrosPorPagina,
                soloNoLeidas,
                filtro
            );
        }

        public async Task<ApiResponseAlertaSimple> ContarAlertasNoLeidas(int usuarioId, int moduloId = 0)
        {
            if (usuarioId <= 0)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "El id del usuario debe ser mayor que cero."
                };
            }

            if (moduloId < 0)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "El id del módulo no puede ser negativo."
                };
            }

            return await alertasDao.ContarAlertasNoLeidas(usuarioId, moduloId);
        }

        public async Task<ApiResponseAlertaSimple> MarcarAlertaLeida(int alertaId, int usuarioId)
        {
            if (alertaId <= 0)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "El id de la alerta debe ser mayor que cero."
                };
            }

            if (usuarioId <= 0)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "El id del usuario debe ser mayor que cero."
                };
            }

            return await alertasDao.MarcarAlertaLeida(alertaId, usuarioId);
        }

        public async Task<ApiResponseAlertaSimple> EliminarAlertasAntiguas(int usuarioId, int moduloId = 0, bool soloLeidas = true)
        {
            if (usuarioId <= 0)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "El id del usuario debe ser mayor que cero."
                };
            }

            if (moduloId < 0)
            {
                return new ApiResponseAlertaSimple
                {
                    success = false,
                    message = "El id del módulo no puede ser negativo."
                };
            }

            return await alertasDao.EliminarAlertasAntiguas(usuarioId, moduloId, soloLeidas);
        }
    }
}