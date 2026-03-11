using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.DatosParaInterfaz
{
    public class EstadoLaboralHelper
    {
        public static bool RequiereVencimiento(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Juicio Verbal y Período Conciliatorio" ||
                estado == "Segunda Audiencia" ||
                estado == "Tercera Audiencia para recepción de pruebas" ||
                estado == "Audiencia de Incidentes" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de revocatoria" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de Nulidad" ||
                estado == "Recursos contra resoluciones no definitivas/Contestación de Recurso de Nulidad" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de Apelación" ||
                estado == "Recursos contra resoluciones que ponen fin a juicio/Recurso de Aclaración y Ampliación" ||
                estado == "Recursos contra resoluciones que ponen fin a juicio/Recurso de Apelación";
        }

        public static bool RequiereVencimientoSI(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Audiencia" ||
                estado == "Vista" ||
                estado == "Tercera Audiencia para recepción de pruebas" ||
                estado == "Audiencia para recepción de pruebas" ||
                estado == "Recurso de aclaración y ampliación";
        }

        public static string GenerarObservacion(
            DateTime fechaEstado,
            string estado,
            bool tieneVencimiento,
            DateTime? fechaVencimiento,
            string textoExtra = null)
        {
            string observacion = $"{fechaEstado:dd/MM/yyyy} {estado}";

            if (tieneVencimiento && fechaVencimiento.HasValue)
            {
                observacion += $" | Fecha de vencimiento: {fechaVencimiento.Value:dd/MM/yyyy HH:mm}";
            }

            if (!string.IsNullOrWhiteSpace(textoExtra))
            {
                observacion += " " + textoExtra.Trim();
            }

            return observacion.Trim();
        }

        public static bool ValidarFechas(
            DateTime fechaEstado,
            bool tieneVencimiento,
            DateTime? fechaVencimiento,
            out string mensaje)
        {
            mensaje = null;

            if (tieneVencimiento)
            {
                if (!fechaVencimiento.HasValue)
                {
                    mensaje = "Debe ingresar fecha de vencimiento.";
                    return false;
                }

                if (fechaVencimiento.Value <= fechaEstado)
                {
                    mensaje = "La fecha de vencimiento debe ser mayor a la fecha del estado.";
                    return false;
                }
            }

            return true;
        }
    }
}
