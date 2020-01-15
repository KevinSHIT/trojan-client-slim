using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrojanClientSlim.Util
{
    class Data
    {
        static Dictionary<string, string> nodeDic = new Dictionary<string, string> { };

        static void TcsdbToDictionary(string Tcsdb)
        {
            string[] nodes = Encrypt.DeBase64(Tcsdb).Split(new string[] { "\\r\\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string node in nodes)
            {
                nodeDic.Add(ShareLink.ConvertShareToTrojanConf(node)[4], node);
            }
        }

        static string DictionaryToTcsdb()
        {
            string tmp = string.Empty;
            foreach (KeyValuePair<string, string> kvp in nodeDic)
            {
                tmp += kvp.Value + "\r\n";
            }
            return Encrypt.Base64(tmp);
        }
    }
}
