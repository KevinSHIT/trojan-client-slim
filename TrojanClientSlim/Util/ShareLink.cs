namespace TrojanClientSlim.Util
{
    public static class ShareLink
    {
        public static string[] ConverteToTrojanConf(string tcsShareLink)
        {
            if (tcsShareLink.Length <= 6)
                return null;
            if (tcsShareLink.Substring(0, 6).ToLower() == "tcs://")
            {
                try
                {
                    string[] tmp = Encrypt.DeBase64(tcsShareLink.Substring(6)).Split(':');
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        tmp[i] = Encrypt.DeBase64(tmp[i]);
                    }
                    if (int.Parse(tmp[1]) > 65535 || int.Parse(tmp[1]) < 0)
                    {
                        return null;
                    }
                    else
                    {
                        return tmp;
                    }
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static string Generate(string remoteAddress, string remotePort, string password)
        {
            return "tcs://" + Encrypt.Base64(Encrypt.Base64(remoteAddress) + ":" + Encrypt.Base64(remotePort) + ":" + Encrypt.Base64(password));
        }
    }
}
