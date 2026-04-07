using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models.Casos.Constitucionales
{
    public class ApiResponseCasoRama<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string rama { get; set; }
        public T data { get; set; }
    }
}
