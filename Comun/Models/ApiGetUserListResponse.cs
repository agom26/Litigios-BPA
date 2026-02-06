using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Models
{
    public class ApiGetUserListResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int pagina { get; set; }
        public int registrosPorPagina { get; set; }
        public int totalRegistros {get;set;}
        public T data { get; set; }
    }
}
