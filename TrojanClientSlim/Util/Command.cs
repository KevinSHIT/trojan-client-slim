using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrojanClientSlim.Util
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
                .Replace("{VERIFY_CERT}", Config.verifyCert.ToString())
                .Replace("{VERIFY_HOSTNAME}", Config.verifyHostname.ToString());

            JObject jo = new JObject();
            jo = JObject.Parse(trojanJson);
            
            jo["local_port"] = Config.localSocksPort;
            jo["remote_addr"] = Config.remoteAddress;
            jo["remote_port"] = Config.remotePort;
            JArray ja = new JArray();
            ja.Add(Config.password);
            jo["password"] = ja;

            File.WriteAllText(@"temp\trojan.json", jo.ToString());

            Process p = new Process();
            p.StartInfo.FileName = @"trojan\trojan.exe";
            p.StartInfo.Arguments = @"-c temp\trojan.json";
#if DEBUG
            p.StartInfo.UseShellExecute = true;
#else
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
#endif
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
                    File.Copy(Config.DEFAULT_TROJAN_CONFIG_PATH, @"temp\config.txt");
                    Command.tmp = File.ReadAllText(@"temp\config.txt")
                        .Replace("{TROJAN_SOCKS_LISTEN}", Config.localTrojanPort.ToString())
                        .Replace("{PRIVOXY_HTTP_LISTEN}", 54392.ToString());

                    File.WriteAllText(@"temp\config.txt", Command.tmp);

                    p.StartInfo.Arguments = @"/c START /MIN privoxy\privoxy.exe temp\config.txt";

                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    break;

                case Config.ProxyMode.GFWList:
                    File.Copy(@"privoxy\config_gfw.txt", @"temp\config.txt");
                    File.Copy(@"privpxy\gfwlist.action", @"temp\gfwlist.action");

                    Command.tmp = File.ReadAllText(@"temp\config.txt")
                        .Replace("{PRIVOXY_HTTP_LISTEN}", 54392.ToString());

                    File.WriteAllText(@"temp\config.txt", Command.tmp);

                    Command.tmp = File.ReadAllText(@"temp\gfwlist.action")
                        .Replace("{TROJAN_SOCKS_LISTEN}", Config.localTrojanPort.ToString());

                    File.WriteAllText(@"temp\gfwlist.action", Command.tmp);
                    p.StartInfo.Arguments = @"/c START /MIN privoxy\privoxy.exe temp\config.txt";


                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    break;
                case Config.ProxyMode.Clash:
                    File.Copy(@"clash\config.yaml", @"temp\config.yaml", true);
                    File.Copy(@"clash\Country.mmdb", @"temp\Country.mmdb", true);

                    Command.tmp = File.ReadAllText(@"temp\config.yaml")
                        .Replace("{TROJAN_SOCKS_LISTEN}", Config.localTrojanPort.ToString())
                        .Replace("{CLASH_HTTP_LISTEN}", 54392.ToString())
                        .Replace("{CLASH_SOCKS_LISTEN}", 0.ToString());

                    File.WriteAllText(@"temp\config.yaml", Command.tmp);

                    p.StartInfo.FileName = @"clash\clash.exe";
                    p.StartInfo.Arguments = @"-d temp";
#if DEBUG
                    p.StartInfo.UseShellExecute = true;
#else
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
#endif
                    break;
            }
            p.Start();
            //p.Dispose();
        }

    }
}
