using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace TrojanClientSlim.Util
{
    class Http
    {
        static string Get(string url, int secondTimeout = 5)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = secondTimeout * 1000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

    }
}
