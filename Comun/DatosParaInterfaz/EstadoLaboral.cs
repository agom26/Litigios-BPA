using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.DatosParaInterfaz
{
    public class EstadoLaboral
    {
        public static string? estado;
        public static DateTime? fechaEstado;
        public static string? observaciones;
        public static DateTime? fechaVencimiento;


        public static void LimpiarEstadoLaboral()
        {
            estado = null;
            fechaEstado = null;
            fechaVencimiento= null;
            observaciones = null;

        }
    }
}
