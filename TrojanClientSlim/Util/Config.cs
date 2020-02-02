using Newtonsoft.Json.Linq;

namespace TrojanClientSlim.Util
{
    public static class Config
    {
        public static bool IsUseAdvance = false;
        public static string GenerateTrojanJson(int localPort, string remoteAddress, int remotePort, string password, bool IsVerifyCert, bool IsVerifyHostname)
        {
            return "{" +
                "\"run_type\": \"client\", " +
                "\"local_addr\": \"127.0.0.1\", " +
                "\"local_port\": " + localPort.ToString() + ", " +
                "\"remote_addr\":\"" + remoteAddress + "\", " +
                "\"remote_port\": " + remotePort.ToString() + ", " +
                "\"password\": [\"" + password + "\"], " +
                "\"log_level\": 1, " +
                "\"ssl\": { " +
                    "\"verify\": " + IsVerifyCert.ToString().ToLower() + "," +
                    "\"verify_hostname\": " + IsVerifyHostname.ToString().ToLower() + ", " +
                    "\"cert\": \"\", " +
                    "\"cipher\": \"ECDHE-ECDSA-AES128-GCM-SHA256:ECDHE-RSA-AES128-GCM-SHA256:ECDHE-ECDSA-AES256-GCM-SHA384:" +
                                "ECDHE-RSA-AES256-GCM-SHA384:ECDHE-ECDSA-CHACHA20-POLY1305:ECDHE-RSA-CHACHA20-POLY1305:ECDHE-RSA-AES128-SHA:" +
                                "ECDHE-RSA-AES256-SHA:RSA-AES128-GCM-SHA256:RSA-AES256-GCM-SHA384:RSA-AES128-SHA:RSA-AES256-SHA:RSA-3DES-EDE-SHA\", " +
                    "\"cipher_tls13\":\"TLS_AES_128_GCM_SHA256:TLS_CHACHA20_POLY1305_SHA256:TLS_AES_256_GCM_SHA384\", " +
                    "\"sni\": \"\", " +
                    "\"alpn\": [ " +
                            "\"h2\", " +
                            "\"http/1.1\" " +
                    "], " +
                    "\"reuse_session\": true, " +
                    "\"session_ticket\": false, " +
                    "\"curves\": \"\" " +
                "}, " +
                "\"tcp\": { " +
                    "\"no_delay\": true, " +
                    "\"keep_alive\": true, " +
                    "\"reuse_port\": false, " +
                    "\"fast_open\": false, " +
                    "\"fast_open_qlen\": 20 " +
                "} " +
            "}";
        }

        //public static JObject DEFAULT_TROJAN_CONFIG()
        //{
        //    var _jo = new JObject();
        //    _jo[""]
        //}



    }
}
