using System;
using System.Security.Cryptography;
using System.Text;

namespace TCS.Util
{
    public static class Encrypt
    {
        public static string Base64(string str)
        {
            try
            {
                return Convert.ToBase64String((byte[])Encoding.UTF8.GetBytes(str));
            }
            catch
            {
                throw new InvalidCastException();
            }
        }

        public static string DeBase64(string str, bool IsExceptionReturnSourceData = false)
        {
            try
            {
                return Encoding.UTF8.GetString((byte[])Convert.FromBase64String(str));
            }
            catch
            {
                if (IsExceptionReturnSourceData)
                    return str;
                throw new FormatException();
            }
        }

        private static readonly SHA1CryptoServiceProvider _sha1 = new SHA1CryptoServiceProvider();
        public static string SHA1(string str) => BitConverter.ToString((byte[])_sha1.ComputeHash((byte[])Encoding.UTF8.GetBytes(str))).Replace("-", string.Empty).ToLower();

    }
}
