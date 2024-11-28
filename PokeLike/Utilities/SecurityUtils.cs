using System;
using System.Text;

namespace PokeLike.Utilities
{
    public static class SecurityUtils
    {
        public static string EncodePasswordBase64(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
    }
}