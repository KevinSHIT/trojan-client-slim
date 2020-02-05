using IniParser.Model;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace TrojanClientSlim.Util
{
    public static class Config
    {
        public static IniData iniData;

        public static TCS tcs;

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

        public const int DEFAULT_SOCKS_PORT = 1080;
        //public const int DEFAULT_CLASH_SOCKS_LISTEN = 67362;
        public const int DEFAULT_HTTP_PORT = 7263;
        public const bool DEFAULT_VERIFY_CERT = true;
        public const bool DEFAULT_HTTP_PROXY = true;
        public const bool DEFAULT_VERIFY_HOSTNAME = true;
        public const ProxyMode DEFAULT_PROXY = ProxyMode.GFWList;

        //public static int trojanSocksListen = DEFAULT_CLASH_HTTP_LISTEN;
        //public static int clashSocksListen = DEFAULT_CLASH_SOCKS_LISTEN;
        //public static int clashHttpListen = DEFAULT_CLASH_HTTP_LISTEN;

        public static ProxyMode proxyMode
        {
            set
            {
                //Debug.WriteLine(value.ToString());
                switch (value)
                {
                    case ProxyMode.GFWList:
                        tcs.GFWList.Checked = true;
                        tcs.Global.Checked = tcs.GeoIP.Checked = false;
                        break;
                    case ProxyMode.Full:
                        tcs.Global.Checked = true;
                        tcs.GFWList.Checked = tcs.GeoIP.Checked = false;
                        break;
                    case ProxyMode.Clash:
                        tcs.GeoIP.Checked = true;
                        tcs.GFWList.Checked = tcs.Global.Checked = false;
                        break;
                }
                iniData["TCS"]["ProxyMode"] = ProxyMode2String(value);
                TCS.iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);

            }
            get
            {
                if (tcs.Global.Checked)
                    return ProxyMode.Full;
                if (tcs.GFWList.Checked)
                    return ProxyMode.GFWList;
                if (tcs.GeoIP.Checked)
                    return ProxyMode.Clash;
                return ProxyMode.Clash;
            }

        }
        public static bool verifyHostname
        {
            set
            {
                tcs.isVerifyHostname.Checked = value;
                iniData["TCS"]["VerifyHostname"] = value.ToString();
                TCS.iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);
            }
            get
            {
                return tcs.isVerifyHostname.Checked;
            }
        }
        public static bool verifyCert
        {
            set
            {
                tcs.isVerifyCert.Checked = value;
                iniData["TCS"]["VerifyCert"] = value.ToString();
                TCS.iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);
            }
            get
            {
                return tcs.isVerifyCert.Checked;
            }

        }

        public static bool httpProxy
        {
            set
            {
                tcs.isHttp.Checked = value;
                iniData["TCS"]["HttpProxy"] = value.ToString();
                TCS.iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);

            }
            get
            {
                return tcs.isHttp.Checked;
            }

        }

        public static int localHttpPort
        {
            set
            {
                tcs.HttpPortBox.Text = iniData["TCS"]["LocalHttpPort"] = value.ToString();
                Debug.WriteLine("HttpPortBox ->" + tcs.HttpPortBox.Text);
                TCS.iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);

                //TCS.iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);
            }
            get
            {
                return int.Parse(tcs.HttpPortBox.Text);
            }
        }

        public static int localSocksPort
        {
            set
            {
                tcs.SocksPortBox.Text = iniData["TCS"]["LocalSocksPort"] = value.ToString();

                TCS.iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);

                //TCS.iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);
            }
            get
            {
                return int.Parse(tcs.SocksPortBox.Text);
            }
        }

        public static string remoteAddress
        {
            set
            {
                tcs.RemoteAddressBox.Text = value;
            }
            get
            {
                return tcs.RemoteAddressBox.Text;
            }
        }

        public static int remotePort
        {
            set
            {
                tcs.RemotePortBox.Text = value.ToString();
            }
            get
            {
                return int.Parse(tcs.RemotePortBox.Text);
            }
        }

        public static string password
        {
            set
            {
                tcs.PasswordBox.Text = value;
            }
            get
            {
                return tcs.PasswordBox.Text;
            }
        }

        public const string DEFAULT_CONFIG_SECTION = "TCS";
        public const string SECTION_HTTP_PROXY = "HttpProxy";

        public const string DEFAULT_CONFIG_PATH = "config.ini";
        public const string DEFAULT_CONFIG_INI =
                    "[TCS]\r\n" +
                    "HttpProxy = true\r\n" +
                    "LocalSocksPort = 1080\r\n" +
                    "LocalHttpPort = 56784\r\n" +
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
