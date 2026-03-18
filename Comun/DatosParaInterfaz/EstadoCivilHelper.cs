using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.DatosParaInterfaz
{
    public class EstadoCivilHelper
    {
        public static List<string> ObtenerEstadosPrimeraInstancia()
        {
            return new List<string>
            {
                "Demanda presentada",
                "Excepciones previas",
                "Contestación de la demanda",
                "Apertura a prueba",
                "Vista",
                "Sentencia/Recurso de aclaración y ampliación",
                "Sentencia/Recurso de apelación"
            };
        }

        public static List<string> ObtenerEstadosSegundaInstancia()
        {
            return new List<string>
            {
                "Recurso de Apelación presentado",
                "Ocuso de hecho",
                "Apertura a prueba",
                "Vista",
                "Resolución"
            };
        }

        public static bool EsSegundaInstancia(string origen)
        {
            return !string.IsNullOrWhiteSpace(origen) &&
                   origen.Trim().Equals("JUICIO SUMARIO SEGUNDA INSTANCIA", StringComparison.OrdinalIgnoreCase);
        }

        public static bool RequiereVencimientoPrimeraInstancia(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Apertura a prueba" ||
                estado == "Vista" ||
                estado == "Tercera Audiencia para recepción de pruebas" ||
                estado == "Sentencia/Recurso de aclaración y ampliación" ||
                estado == "Sentencia/Recurso de Apelación" ;
        }

        public static bool RequiereVencimientoSegundaInstancia(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Recurso de Apelación presentado" ||
                estado == "Ocuso de hecho" ||
                estado == "Apertura a prueba" ||
                estado == "Vista" ||
                estado == "Resolución";
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
