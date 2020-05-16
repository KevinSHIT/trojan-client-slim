using Newtonsoft.Json.Linq;

using System.Diagnostics;
using System.IO;

namespace TCS.Util
{
    class Command
    {

        /*
         * CLASH
         * {CLASH_SOCKS_LISTEN}
         * {CLASH_HTTP_LISTEN}
         *
         * TROJAN
         * {TROJAN_SOCKS_LISTEN}
         *
         * PRIVOXY
         * {PRIVOXY_HTTP_LISTEN}
         *
         */

        public static void RunTrojan()
        {
            File.Copy(@"trojan\config.json", @"temp\trojan.json", true);
            string trojanJson = File.ReadAllText(@"temp\trojan.json")
                .Replace("\"{VERIFY_CERT}\"", Config.verifyCert.ToString().ToLower())
                .Replace("\"{VERIFY_HOSTNAME}\"", Config.verifyHostname.ToString().ToLower())
                .Replace("{SNI}", Config.sniList[Config.remoteAddress]);

            JObject jo = JObject.Parse(trojanJson);

            jo["local_port"] = Config.localSocksPort;
            jo["remote_addr"] = Config.remoteAddress;
            jo["remote_port"] = Config.remotePort;

            JArray ja = new JArray
            {
                Config.password
            };
            jo["password"] = ja;

            File.WriteAllText(@"temp\trojan.json", jo.ToString());

            Process p = new Process();
            p.StartInfo.FileName = @"trojan\trojan.exe";
            p.StartInfo.Arguments = @"-c temp\trojan.json";
            if (Config.Debug)
                p.StartInfo.UseShellExecute = true;
            else
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
            }
            p.Start();
        }

        private static string tmp;
        public static void RunHttpProxy()
        {
            //string tmp = "";

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //pc.StartInfo.Arguments = $"start {path}\\privoxy\\privoxy.exe {path}\\privoxy\\config.txt";
            switch (Config.proxyMode)
            {
                case Config.ProxyMode.Full:
                    File.Copy(@"privoxy\config.txt", @"temp\config.txt");
                    Command.tmp = File.ReadAllText(@"temp\config.txt")
                        .Replace("{TROJAN_SOCKS_LISTEN}", Config.localSocksPort.ToString())
                        .Replace("{PRIVOXY_HTTP_LISTEN}", Config.localHttpPort.ToString());

                    File.WriteAllText(@"temp\config.txt", Command.tmp);

                    p.StartInfo.Arguments = @"/c START /MIN privoxy\privoxy.exe temp\config.txt";

                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    break;

                case Config.ProxyMode.GFWList:
                    File.Copy(@"privoxy\config_gfw.txt", @"temp\config.txt");
                    File.Copy(@"privoxy\gfwlist.action", @"temp\gfwlist.action");

                    Command.tmp = File.ReadAllText(@"temp\config.txt")
                        .Replace("{PRIVOXY_HTTP_LISTEN}", Config.localHttpPort.ToString());

                    File.WriteAllText(@"temp\config.txt", Command.tmp);

                    Command.tmp = File.ReadAllText(@"temp\gfwlist.action")
                        .Replace("{TROJAN_SOCKS_LISTEN}", Config.localSocksPort.ToString());

                    File.WriteAllText(@"temp\gfwlist.action", Command.tmp);
                    p.StartInfo.Arguments = @"/c START /MIN privoxy\privoxy.exe temp\config.txt";


                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    break;
                case Config.ProxyMode.Clash:
                    File.Copy(@"clash\config.yaml", @"temp\config.yaml", true);
                    File.Copy(@"clash\Country.mmdb", @"temp\Country.mmdb", true);

                    Command.tmp = File.ReadAllText(@"temp\config.yaml")
                        .Replace("{TROJAN_SOCKS_LISTEN}", Config.localSocksPort.ToString())
                        .Replace("{CLASH_HTTP_LISTEN}", Config.localHttpPort.ToString())
                        .Replace("{CLASH_SOCKS_LISTEN}", 0.ToString());

                    File.WriteAllText(@"temp\config.yaml", Command.tmp);

                    p.StartInfo.FileName = @"clash\clash.exe";
                    p.StartInfo.Arguments = @"-d temp";
                    if (Config.Debug)
                        p.StartInfo.UseShellExecute = true;
                    else
                    {
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.CreateNoWindow = true;
                    }
                    break;
            }
            p.Start();
            //p.Dispose();
        }

        public static void RunSocksProxy()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";

            File.Copy(@"clash\config.yaml", @"temp\config.yaml", true);
            File.Copy(@"clash\Country.mmdb", @"temp\Country.mmdb", true);

            Command.tmp = File.ReadAllText(@"temp\config.yaml")
                .Replace("{TROJAN_SOCKS_LISTEN}", Config.localSocksPort.ToString())
                .Replace("{CLASH_HTTP_LISTEN}", 0.ToString())
                .Replace("{CLASH_SOCKS_LISTEN}", Config.localHttpPort.ToString());

            File.WriteAllText(@"temp\config.yaml", Command.tmp);

            p.StartInfo.FileName = @"clash\clash.exe";
            p.StartInfo.Arguments = @"-d temp";
            if (Config.Debug)
                p.StartInfo.UseShellExecute = true;
            else
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
            }

            p.Start();

        }

        public static void StopProcess()
        {

            Process[] myproc = Process.GetProcesses();
            foreach (Process item in myproc)
            {
                if (item.ProcessName.ToLower() == "trojan" ||
                    item.ProcessName.ToLower() == "privoxy" ||
                    item.ProcessName.ToLower() == "clash")
                {
                    item.Kill();
                }
            }
        }

    }
}
