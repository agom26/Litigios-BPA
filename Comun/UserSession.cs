using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class UserSession
    {
        public static int Id { get; set; }
        public static string Usuario { get; set; }
        public static string Nombres { get; set; }
        public static string Apellidos { get; set; }
        public static List<string> Modulos { get; set; } = new List<string>();

        public static void Logout()
        {
            Id = 0;
            Nombres = string.Empty;
            Modulos = new List<string>();
        }
    }
}
