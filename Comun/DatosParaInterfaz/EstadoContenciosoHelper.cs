using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.DatosParaInterfaz
{
    public class EstadoContenciosoHelper
    {
        public static List<string> ObtenerEstadosPrimeraInstancia()
        {
            return new List<string>
            {
                "Demanda presentada",
                "Admisión de la demanda y emplazamiento",
                "Excepciones previas",
                "Contestación de la demanda",
                "Excepciones perentorias",
                "Reconvención",
                "Apertura a prueba",
                "Vista",
                "Auto para mejor fallar",
                "Sentencia",
                "Sentencia/Recurso de casación"
            };
        }

        public static List<string> ObtenerEstadosSegundaInstancia()
        {
            return new List<string>
            {
                "Recurso de Casación",
                "Admisión",
                "Vista",
                "Desistimiento",
                "Sentencia",
                "Recurso de Aclaración y Ampliación",
                "Amparo"
            };
        }

     
        public static bool EsSegundaInstancia(string origen)
        {
            return !string.IsNullOrWhiteSpace(origen) &&
                   (origen.Trim().Equals("ADMINISTRATIVO GENERAL SEGUNDA INSTANCIA", StringComparison.OrdinalIgnoreCase) || origen.Trim().Equals("ADMINISTRATIVO TRIBUTARIO SEGUNDA INSTANCIA", StringComparison.OrdinalIgnoreCase) ||
                   origen.Trim().Equals("ADMINISTRATIVO SEGUNDA INSTANCIA", StringComparison.OrdinalIgnoreCase));
        }

        public static bool RequiereVencimientoPrimeraInstancia(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Apertura a prueba" ||
                estado == "Vista" ||
                estado == "Sentencia/Recurso de Casación" ;
        }

        public static bool RequiereVencimientoSegundaInstancia(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Recurso de Casación" ||
                estado == "Vista" ||
                estado == "Recurso de Aclaración y Ampliación";
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
