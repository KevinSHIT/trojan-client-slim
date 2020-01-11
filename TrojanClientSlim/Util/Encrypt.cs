using System;
using System.Text;

namespace TrojanClientSlim.Util
{
    public class Encrypt
    {
        public static string Base64(string str)
        {
            try
            {
                return Convert.ToBase64String((byte[])Encoding.Default.GetBytes(str));
            }
            catch
            {
                throw new InvalidCastException();
            }
        }

        public static string DeBase64(string str)
        {
            try
            {
                return Encoding.Default.GetString((byte[])Convert.FromBase64String(str));
            }
            catch
            {
                throw new InvalidCastException();
            }
        }

    }
}
