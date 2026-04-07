using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.DatosParaInterfaz
{
    public class EstadoConstitucionalHelper
    {
        public static List<string> ObtenerEstadosAmparo()
        {
            return new List<string>
            {
                "Interposición de Amparo",
                "Contestación de Amparo",
                "Amparo provisional",
                "Primera audiencia",
                "Apertura a prueba",
                "Segunda audiencia",
                "Vista pública",
                "Auto para mejor fallar",
                "Sentencia",
                "Recurso de Aclaración o ampliación",
                "Liquidación de costas/multas",
                "Recurso de Apelación",
                "Recurso de Apelación/Diligencias para mejor fallar",
                "Recurso de Apelación/Vista",
                "Recurso de Apelación/Recurso de Aclaración o ampliación",
                "Desistimiento",
                "Archivo de expediente"
            };
        }

        public static bool RequiereVencimiento(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Primera audiencia" ||
                estado == "Apertura a prueba" ||
                estado == "Segunda audiencia"  ||
                estado == "Recurso de Aclaración o ampliación" ||
                estado == "Recurso de Apelación" ||
                estado == "Recurso de Apelación/Vista" ||
                estado == "Recurso de Apelación/Recurso de Aclaración o ampliación";
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
