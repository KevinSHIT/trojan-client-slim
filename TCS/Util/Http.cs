using System.IO;
using System.Net;

namespace TCS.Util
{
    public class Http
    {
        public static string GET(string url)
        {
            if (url.ToLower().StartsWith("https"))
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            WebRequest request = WebRequest.Create(url);
            request.ContentType = "text/html; charset=utf-8";
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            string v;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                v = reader.ReadToEnd();
            }

            response.Close();

            return v;
        }

    }
}
