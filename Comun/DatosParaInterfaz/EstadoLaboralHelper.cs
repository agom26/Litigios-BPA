using System;
using System.Collections.Generic;

namespace Comun.DatosParaInterfaz
{
    public class EstadoLaboralHelper
    {
        public static List<string> ObtenerEstadosPrimeraInstancia()
        {
            return new List<string>
            {
                "Demanda presentada",
                "Juicio Verbal y Período Conciliatorio",
                "Ampliación o Modificación de demanda",
                "Excepción dilatoria",
                "Excepción perentoria",
                "Reconvención",
                "Segunda Audiencia",
                "Tercera Audiencia para recepción de pruebas",
                "Audiencia de Incidentes",
                "Auto para mejor fallar",
                "Sentencia",
                "Recursos contra resoluciones no definitivas/Recurso de revocatoria",
                "Recursos contra resoluciones no definitivas/Recurso de Nulidad",
                "Recursos contra resoluciones no definitivas/Contestación de Recurso de Nulidad",
                "Recursos contra resoluciones no definitivas/Recurso de Apelación",
                "Recursos contra resoluciones que ponen fin a juicio/Recurso de Aclaración y Ampliación",
                "Recursos contra resoluciones que ponen fin a juicio/Recurso de Apelación"
            };
        }

        public static List<string> ObtenerEstadosSegundaInstancia()
        {
            return new List<string>
            {
                "Recurso de apelación presentado",
                "Audiencia",
                "Vista",
                "Audiencia para recepción de pruebas",
                "Sentencia",
                "Recursos de aclaración y ampliación",
                "Liquidación y ejecución de sentencia"
            };
        }

        public static bool EsSegundaInstancia(string origen)
        {
            return !string.IsNullOrWhiteSpace(origen) &&
                   origen.Trim().Equals("LABORAL SEGUNDA INSTANCIA", StringComparison.OrdinalIgnoreCase);
        }

        public static bool RequiereVencimientoPrimeraInstancia(string estado)
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

        public static bool RequiereVencimientoSegundaInstancia(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Audiencia" ||
                estado == "Vista" ||
                estado == "Audiencia para recepción de pruebas" ||
                estado == "Recursos de aclaración y ampliación";
        }

        public static bool RequiereVencimiento(string estado, string origen)
        {
            return EsSegundaInstancia(origen)
                ? RequiereVencimientoSegundaInstancia(estado)
                : RequiereVencimientoPrimeraInstancia(estado);
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