using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.DatosParaInterfaz
{
    public class EstadoCivil
    {
        public static string? estado;
        public static DateTime? fechaEstado;
        public static string? observaciones;
        public static DateTime? fechaVencimiento;
        public static string? origen;

        public static void LimpiarEstado()
        {
            estado = null;
            fechaEstado = null;
            fechaVencimiento = null;
            observaciones = null;
            origen = null;
        }
    }
}
