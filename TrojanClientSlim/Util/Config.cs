using IniParser.Model;
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
                //Debug.WriteLine
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

        //public const string DEFAULT_TROJAN_CONFIG_PATH = "trojan\\conf";
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
