using System.Web;

namespace TrojanClientSlim.Util
{
    public static class ShareLink
    {
        public static string[] ConvertShareToTrojanConf(string trojanShareLink)
        {
            //Example: trojan://password@ip:port#node_name
            if (trojanShareLink.StartsWith("trojan://"))
            {
                string[] tmp = new string[4];
                string tsl = trojanShareLink.Substring(9);
                string[] temp = tsl.Split(':');
                string[] temp_3 = temp[temp.Length - 1].Split('#');
                tmp[1] = temp_3[0];
                try
                {
                    int.Parse(tmp[1]);
                    temp[temp.Length - 1] = "";
                }
                catch
                {
                    return null;
                }
                temp_3[0] = "";
                tmp[4] = temp_3.CombineToString();
                tsl = CombineToString(temp);
                //Current: password@ip
                string[] temp_1 = tsl.Split('@');
                if (temp_1.Length == 1) 
                    return null;
                tmp[0] = temp_1[temp_1.Length - 1];
                temp_1[temp_1.Length - 1] = "";
                try
                {
                    tmp[2] = HttpUtility.UrlDecode(CombineToString(temp_1));
                }
                catch
                {
                    return null;
                }
                return tmp;
            }
            else
                return null;
        }

        public static string CombineToString(this string[] str)
        {
            string tmp = "";
            foreach (string s in str)
            {
                tmp += s;
            }
            return tmp;
        }

        public static string Generate(string remoteAddress, string remotePort, string password)
        {
            return "trojan://" + HttpUtility.UrlEncode(password) + "@" + remoteAddress + ":" + remotePort;
        }
    }
}
