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
                string[] tmp = new string[5];
                string tsl = trojanShareLink.Substring(9);
                string[] temp = tsl.Split(':');
                string[] temp_3 = temp[temp.Length - 1].Split('#');

                // Node Name
                if (temp_3.Length == 2)
                {
                    tmp[3] = temp_3[1];
                }
                else
                {
                    tmp[3] = "Untitled";
                }

                // Port
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

                // Server
                tmp[0] = temp_1[temp_1.Length - 1];
                temp_1[temp_1.Length - 1] = "";

                // Password
                try
                {
                    tmp[2] = HttpUtility.UrlDecode(CombineToString(temp_1));
                }
                catch
                {
                    return null;
                }
                /*
                 * 0 -> Server
                 * 1 -> Port
                 * 2 -> Passwd
                 * 3 -> Name
                 */
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

        public static string Generate(string remoteAddress, string remotePort, string password, string nodeName = "Untitled")
        {
            if (string.IsNullOrEmpty(nodeName))
                nodeName = "Untitled";
            return "trojan://" + HttpUtility.UrlEncode(password) + "@" + remoteAddress + ":" + remotePort + "#" + nodeName;
        }
    }
}
