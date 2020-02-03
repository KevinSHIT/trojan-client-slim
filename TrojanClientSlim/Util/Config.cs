using Newtonsoft.Json.Linq;

namespace TrojanClientSlim.Util
{
    public static class Config
    {
        public enum ProxyMode
        {
            Full = 0,
            GFWList = 1,
            Clash = 2
        }

        public static ProxyMode ProxyModeParser(string proxyMode)
        {
            switch (proxyMode.ToLower())
            {
                case "full":
                    return ProxyMode.Full;
                case "gfwlist":
                    return ProxyMode.GFWList;
                case "clash":
                    return ProxyMode.Clash;
                default:
                    throw new System.ArgumentException(proxyMode + " is not a valid proxymode");
            }

        }

        public static string ProxyMode2String(ProxyMode pm)
        {
            switch (pm)
            {
                case ProxyMode.Clash:
                    return "clash";
                case ProxyMode.Full:
                    return "full";
                case ProxyMode.GFWList:
                    return "gfwlist";
                default:
                    throw new System.ArgumentException("Not a valid ProxyMode");
            }
        }

        public static string GenerateTrojanJson(int localPort, string remoteAddress, int remotePort, string password, bool IsVerifyCert, bool IsVerifyHostname)
        {
            return "{" +
                "\"run_type\": \"client\", " +
                "\"local_addr\": \"127.0.0.1\", " +
                "\"local_port\": " + localPort.ToString() + ", " +
                "\"remote_addr\": \"" + remoteAddress + "\", " +
                "\"remote_port\": " + remotePort.ToString() + ", " +
                "\"password\": [\"" + password + "\"], " +
                "\"log_level\": 1, " +
                "\"ssl\": { " +
                    "\"verify\": " + IsVerifyCert.ToString().ToLower() + "," +
                    "\"verify_hostname\": " + IsVerifyHostname.ToString().ToLower() + ", " +
                    "\"cert\": \"\", " +
                    "\"cipher\": \"ECDHE-ECDSA-AES128-GCM-SHA256:" +
                                  "ECDHE-RSA-AES128-GCM-SHA256:" +
                                  "ECDHE-ECDSA-CHACHA20-POLY1305:" +
                                  "ECDHE-RSA-CHACHA20-POLY1305:" +
                                  "ECDHE-ECDSA-AES256-GCM-SHA384:" +
                                  "ECDHE-RSA-AES256-GCM-SHA384:" +
                                  "ECDHE-ECDSA-AES256-SHA:" +
                                  "ECDHE-ECDSA-AES128-SHA:" +
                                  "ECDHE-RSA-AES128-SHA:" +
                                  "ECDHE-RSA-AES256-SHA:" +
                                  "DHE-RSA-AES128-SHA:" +
                                  "DHE-RSA-AES256-SHA:" +
                                  "AES128-SHA:" +
                                  "AES256-SHA:" +
                                  "DES-CBC3-SHA" +
                                  "\", " +
                    "\"cipher_tls13\":\"TLS_AES_128_GCM_SHA256:" +
                                       "TLS_CHACHA20_POLY1305_SHA256:" +
                                       "TLS_AES_256_GCM_SHA384" +
                                       "\", " +
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

        public const int DEFAULT_TROJAN_SOCKS_LISTEN = 1080;
        public const int DEFAULT_CLASH_SOCKS_LISTEN = 67362;
        public const int DEFAULT_CLASH_HTTP_LISTEN = 0;
        public const bool DEFAULT_VERIFY_CERT = true;
        public const bool DEFAULT_HTTP_PROXY = true;
        public const bool DEFAULT_VERIFY_HOSTNAME = true;
        public const ProxyMode DEFAULT_PROXY = ProxyMode.GFWList;

        public static int trojanSocksListen = DEFAULT_CLASH_HTTP_LISTEN;
        public static int clashSocksListen = DEFAULT_CLASH_SOCKS_LISTEN;
        public static int clashHttpListen = DEFAULT_CLASH_HTTP_LISTEN;
        public static int localTrojanPort = 1080;

        public static ProxyMode proxyMode
        {
            set
            {
                switch (value)
                {
                    case ProxyMode.GFWList:
                        TCS.GFWList.Checked = true;
                        break;
                    case ProxyMode.Full:
                        TCS.Global.Checked = true;
                        break;
                    case ProxyMode.Clash:
                        //TODO
                        break;
                }
                TCS.iniData["TCS"]["ProxyMode"] = ProxyMode2String(value);

            }
            get
            {
                if (TCS.Global.Checked)
                    return ProxyMode.Full;
                if (TCS.GFWList.Checked)
                    return ProxyMode.GFWList;
                //TODO: CLASH
                return ProxyMode.Clash;
            }

        }
        public static bool verifyHostname
        {
            set
            {
                TCS.isVerifyHostname.Checked = value;
                TCS.iniData["TCS"]["VerifyHostname"] = value.ToString();
            }
            get
            {
                return TCS.isVerifyHostname.Checked;
            }
        }
        public static bool verifyCert
        {
            set
            {
                TCS.isVerifyCert.Checked = value;
                TCS.iniData["TCS"]["VerifyCert"] = value.ToString();
            }
            get
            {
                return TCS.isVerifyCert.Checked;
            }

        }

        public static bool httpProxy
        {
            set
            {
                TCS.isHttp.Checked = value;
                TCS.iniData["TCS"]["HttpProxy"] = value.ToString();
            }
            get
            {
                return TCS.isHttp.Checked;
            }

        }


        public const string DEFAULT_CONFIG_SECTION = "TCS";
        public const string SECTION_HTTP_PROXY = "HttpProxy";

        public const string DEFAULT_CONFIG_PATH = "config.ini";
        public const string DEFAULT_CONFIG_INI =
                    "[TCS]\r\n" +
                    "HttpProxy = true\r\n" +
                    "LocalPort = 1080\r\n" +
                    "VerifyCert = true\r\n" +
                    "VerifyHostname = true\r\n" +
                    "Proxy = GFWList";

        public const string DEFAULT_TROJAN_CONFIG_PATH = "trojan\\conf";
        public const string DEFAULT_CLASH_CONFIG_PATH = "clash\\config.yaml";
        public const string DEFAULT_PRIVOXY_FILE_PATH = "privoxy";

        //public const string Replacer()

        //public static JObject DEFAULT_TROJAN_CONFIG()
        //{
        //    var _jo = new JObject();
        //    _jo[""]
        //}



    }
}
