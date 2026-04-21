using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.DatosParaInterfaz
{

    public class EstadoCivilHelper
    {
        public static List<string> ObtenerEstadosOralPrimerInstancia()
        {
            return new List<string>
            {
                "Demanda presentada",
                "Juicio Verbal y Período Conciliatorio",
                "Excepción dilatoria",
                "Excepción perentoria",
                "Reconvención",
                "Audiencia para recepción de pruebas",
                "Audiencia de Incidentes",
                "Auto para mejor fallar",
                "Sentencia",
                "Recursos contra resoluciones no definitivas/Recurso de Nulidad",
                "Recursos contra resoluciones no definitivas/Contestación de Recurso de Nulidad",
                "Recursos contra resoluciones que ponen fin a juicio/Recurso de Aclaración y Ampliación",
                "Recursos contra resoluciones que ponen fin a juicio/Recurso de Apelación"
            };
        }

        public static List<string> ObtenerEstadosOralSegundaInstancia()
        {
            return new List<string>
            {
                "Audiencia",
                "Vista",
                "Audiencia para recepción de pruebas",
                "Sentencia",
                "Recurso de aclaración y ampliación",
                "Recurso de Revocatoria y/o Reposición"
            };
        }

        public static List<string> ObtenerEstadosJSPrimeraInstancia()
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

        public static List<string> ObtenerEstadosJSSegundaInstancia()
        {
            return new List<string>
            {
                "Recurso de Apelación presentado",
                "Ocurso de hecho",
                "Apertura a prueba",
                "Vista",
                "Resolución"
            };
        }

        public static List<string> ObtenerEstadosPEViaApremio()
        {
            return new List<string>
            {
                "Demanda presentada",
                "Admisión de demanda y mandamiento de ejecución",
                "Excepciones",
                "Excepciones/Apertura a prueba de incidente",
                "Orden de Remate",
                "Publicación de orden de remate",
                "Remate",
                "Incidente de liquidación/Recurso de Apelación",
                "Incidente de liquidación/Recurso de Ampliación y Aclaración",
                "Incidente de liquidación/Recurso de Ampliación y Aclaración/Apertura a prueba",
                "Traslativa de dominio",
                "Entrega de bienes"
            };
        }

        public static List<string> ObtenerEstadosPEComun()
        {
            return new List<string>
            {
                "Demanda presentada",
                "Admisión de demanda y mandamiento de ejecución",
                "Oposición del demandado",
                "Oposición del demandado/Audiencia por Oposición",
                "Oposición del demandado/Excepciones",
                "Período de prueba",
                "Sentencia",
                "Recurso de aclaración y ampliación",
                "Incidente de Liquidación",
                "Incidente de Liquidación/Período de prueba",
                "Auto que aprueba liquidación",
                "Recurso de Apelación"
            };
        }

        public static List<string> ObtenerEstadosPESegundaInstancia()
        {
            return new List<string>
            {
                "Recurso de Apelación presentado",
                "Ocurso de hecho",
                "Vista",
                "Resolución"
            };
        }

        public static bool EsSegundaInstancia(string origen)
        {
            return !string.IsNullOrWhiteSpace(origen) &&
                   origen.Trim().Equals("JUICIO SUMARIO SEGUNDA INSTANCIA", StringComparison.OrdinalIgnoreCase);
        }

        public static bool RequiereVencimientoJOPI(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
               estado == "Juicio Verbal y Período Conciliatorio" ||
                estado == "Audiencia para recepción de pruebas" ||
                estado == "Audiencia de Incidentes" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de Nulidad" ||
                estado == "Recursos contra resoluciones no definitivas/Contestación de Recurso de Nulidad" ||
                estado == "Recursos contra resoluciones que ponen fin a juicio/Recurso de Aclaración y Ampliación" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de Apelación";
        }
        
        public static bool RequiereVencimientoJOSI(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
               estado == "Vista" ||
                estado == "Recurso de aclaración y ampliación" ||
                estado == "Recurso de Aclaración y ampliación" ||
                estado == "Recursos contra resoluciones no definitivas/Contestación de Recurso de Nulidad" ||
                estado == "Recursos contra resoluciones que ponen fin a juicio/Recurso de Aclaración y Ampliación" ||
                estado == "Recursos contra resoluciones no definitivas/Recurso de Apelación";
        }
        public static bool RequiereVencimientoJSPrimeraInstancia(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Apertura a prueba" ||
                estado == "Vista" ||
                estado == "Sentencia/Recurso de aclaración y ampliación" ||
                estado == "Sentencia/Recurso de Apelación";
        }

        public static bool RequiereVencimientoJSSegundaInstancia(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Recurso de Apelación presentado" ||
                estado == "Ocurso de hecho" ||
                estado == "Apertura a prueba" ||
                estado == "Vista";
        }

        //proceso de ejeucion
        public static bool RequiereVencimientoPEViaApremio(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Excepciones" ||
                estado == "Excepciones/Apertura a prueba de incidente" ||
                estado == "Publicación de orden de remate" ||
                estado == "Remate" ||
                estado == "Incidente de liquidación/Recurso de Apelación" ||
                estado == "Incidente de liquidación/Recurso de Ampliación y Aclaración" ||
                estado == "Traslativa de dominio" ||
                estado == "Entrega de bienes";
        }

        public static bool RequiereVencimientoPEComun(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Oposición del demandado" ||
                estado == "Oposición del demandado/Audiencia por Oposición" ||
                estado == "Oposición del demandado/Excepciones" ||
                estado == "Período de prueba" ||
                estado == "Recurso de Ampliación y Aclaración" ||
                estado == "Incidente de Liquidación/Período de prueba" ||
                estado == "Recurso de Apelación";
        }
        public static bool RequiereVencimientoPESegundaInstancia(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            return
                estado == "Recurso de Apelación" ||
                estado == "Ocurso de hecho" ||
                estado == "Vista" ;
        }

        public static bool RequiereVencimiento(string estado, string origen)
        {
            return EsSegundaInstancia(origen)
                ? RequiereVencimientoJSSegundaInstancia(estado)
                : RequiereVencimientoJSPrimeraInstancia(estado);
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

        public static string GenerarObservacionConFechas(
            DateTime fechaEstado,
            string estado,
            bool tieneVencimiento,
            DateTime? fechaVencimiento,
            DateTimePicker dateTimePickerFecha1,
            DateTimePicker dateTimePickerFecha2,
            DateTimePicker dateTimePickerFecha3,
            Label fecha1, 
            Label fecha2, 
            Label fecha3,
            string textoExtra = null)
        {
            string observacion = $"{fechaEstado:dd/MM/yyyy} {estado}";

            if (estado == "Publicación de orden de remate")
            {
                dateTimePickerFecha1.Enabled = true;
                dateTimePickerFecha2.Enabled = true;
                dateTimePickerFecha3.Enabled = true;
                dateTimePickerFecha1.Visible = true;
                dateTimePickerFecha2.Visible = true;
                dateTimePickerFecha3.Visible = true;
                fecha1.Visible = true;
                fecha2.Visible = true;
                fecha3.Visible = true;

                string f1 = dateTimePickerFecha1.Value.ToString("dd/MM/yyyy");
                string f2 = dateTimePickerFecha2.Value.ToString("dd/MM/yyyy");
                string f3 = dateTimePickerFecha3.Value.ToString("dd/MM/yyyy");

                observacion += $" | Publicaciones: {f1}, {f2}, {f3}";
            }
            else
            {
                dateTimePickerFecha1.Enabled = false;
                dateTimePickerFecha2.Enabled = false;
                dateTimePickerFecha3.Enabled = false;
                dateTimePickerFecha1.Visible = false;
                dateTimePickerFecha2.Visible = false;
                dateTimePickerFecha3.Visible = false;
                fecha1.Visible = false;
                fecha2.Visible = false;
                fecha3.Visible = false;
            }


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
