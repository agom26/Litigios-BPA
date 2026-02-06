using System;
using System.Security.Cryptography;
using System.Text;

namespace Comun
{
    public static class SecurityHelper
    {
        
        public static string HashSha256(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = SHA256.HashData(bytes);

            
            return Convert.ToBase64String(hash);
        }
    }
}