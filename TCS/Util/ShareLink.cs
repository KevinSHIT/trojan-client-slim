using System.Net;
using System.Web;

namespace TCS.Util
{
    public static class ShareLink
    {
        public static string[] ConvertShareToTrojanConf(string trojanShareLink)
        {
            //Example: trojan://password@ip:port#node_name
            if (trojanShareLink.StartsWith("trojan://"))
            {

                /*
                 * 0 -> Server
                 * 1 -> Port
                 * 2 -> Passwd
                 * 3 -> Name
                 */
                string[] tmp = new string[5];

                string tsl = trojanShareLink.Substring(9);
                string[] tmp_1 = tsl.Split('#');
                if (tmp_1.Length == 2)
                    tmp[3] = tmp_1[1];
                else
                    tmp[3] = "Untitled";

                //tmp_1[0] -> pass@serv:port

                string[] tmp_2 = tmp_1[0].Split('@');
                tmp[2] = HttpUtility.UrlDecode(tmp_2[0]);

                //tmp_2[1] -> serv:port
                string[] tmp_3 = tmp_2[1].Split(':');

                if (int.TryParse(tmp_3[tmp_3.Length - 1], out int p))
                    tmp[1] = p.ToString();
                else
                    return null;

                tmp[0] = tmp_2[1].Substring(0, tmp_2[1].Length - tmp[1].Length - 1);

                SetRightIP(ref tmp[0]);

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
            if (string.IsNullOrEmpty(remotePort))
                remotePort = "443";

            SetRightIP(ref remoteAddress);

            try
            {
                int.Parse(remotePort);
            }
            catch
            {
                remotePort = "443";
            }

            return "trojan://" + HttpUtility.UrlEncode(password) + "@" + remoteAddress.Trim() + ":" + remotePort + "#" + nodeName;
        }

        private static void SetRightIP(ref string ip)
        {
            if (IPAddress.TryParse(ip, out IPAddress address))
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    ip = "[" + address.ToString() + "]";

        }
    }
}
