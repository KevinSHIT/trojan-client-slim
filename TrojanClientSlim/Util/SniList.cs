using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCS.Util
{
    public class SniList
    {
        private Dictionary<string, string> dic;
        public SniList()
        {
            dic = new Dictionary<string, string> { };
        }

        public SniList(string[] snis)
        {
            dic = new Dictionary<string, string> { };
            foreach (var sni in snis)
            {
                if (!string.IsNullOrWhiteSpace(sni) && sni.Contains(":"))
                {
                    dic.Add(sni.Split(':')[0], sni.Split(':')[1]);
                }
            }
        }

        public void Add(string ip, string sni) => dic.Add(ip, sni);


        public void Remove(string ip) => dic.Remove(ip);

        public string[] ToArray()
        {

            var al = new List<string>();
            foreach (var ip in dic)
            {
                al.Add($"{ip.Key}:{ip.Value}");
            }
            return al.ToArray();
        }

        public string this[string index]
        {
            get
            {
                if (dic.Keys.Contains<string>(index))
                    return dic[index];
                else
                    return string.Empty;
            }
            set => dic[index] = value;
        }

    }
}
