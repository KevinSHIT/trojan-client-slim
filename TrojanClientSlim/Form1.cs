using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TrojanClientSlim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (isPortUsed(1080))
            {
                MessageBox.Show("Port 1080 is in use!\r\nTrojan may failure to work.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (isPortUsed(54392))
            {
                MessageBox.Show("Port 54392 is in use!\r\nPrivoxy may failure to work.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            if (File.Exists("conf"))
            {
                string[] conf = Encrypt.DeBase64(File.ReadAllText("conf")).Split(':');
                if (conf.Length == 5)
                {
                    this.RemoteAddressBox.Text = conf[0];
                    this.RemotePortBox.Text = conf[1];
                    this.PasswordBox.Text = conf[2];
                    if (conf[3].ToLower() == "true")
                    {
                        isHttp.Checked = true;
                    }
                    if (conf[4].ToLower().Contains("c"))
                    {
                        isVerifyCert.Checked = true;
                    }
                    if (conf[4].ToLower().Contains("h"))
                    {
                        isVerifyHostname.Checked = true;
                    }
                }
            }
            else
            {
                File.Create("conf");
            }

        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Proxy.UnsetProxy();
            try
            {
                KillProcess();
                MessageBox.Show("Kill trojan process successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Kill trojan process failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class Encrypt
        {
            public static string Base64(string str)
            {
                try
                {
                    return Convert.ToBase64String((byte[])Encoding.Default.GetBytes(str));
                }
                catch
                {
                    throw new InvalidCastException();
                }
            }

            public static string DeBase64(string str)
            {
                try
                {
                    return Encoding.Default.GetString((byte[])Convert.FromBase64String(str));
                }
                catch
                {
                    throw new InvalidCastException();
                }
            }
        }

        private void Cancle_Click(object sender, EventArgs e)
        {
            Proxy.UnsetProxy();
            System.Environment.Exit(0);
        }

        private void Run_Click(object sender, EventArgs e)
        {
            string ch = string.Empty;
            if (isVerifyCert.Checked == true)
            {
                ch += "c";
            }
            if (isVerifyHostname.Checked == true)
            {
                ch += "h";
            }
            File.WriteAllText("conf", Encrypt.Base64($"{RemoteAddressBox.Text}:{RemotePortBox.Text}:{PasswordBox.Text}:{isHttp.Checked}:{ch}"));
            try
            {
                KillProcess();
            }
            catch
            {
                MessageBox.Show("Kill trojan process failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GeneralTrojanConf();
            RunTrojan();
            if (isHttp.Checked == true)
            {
                
                RunPivoxy();
                Proxy.SetProxy("127.0.0.1:54392");

            }
            MessageBox.Show("Trojan run successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void KillProcess()
        {

            Process[] myproc = Process.GetProcesses();
            foreach (Process item in myproc)
            {
                if (item.ProcessName == "trojan" || item.ProcessName == "privoxy")
                {
                    item.Kill();
                }
            }
        }

        private void RunTrojan()
        {
            Process p = new Process();
            p.StartInfo.FileName = @"trojan\trojan.exe";
            p.StartInfo.Arguments = @"-c trojan.conf";
            p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.Dispose();

        }

        private void RunPivoxy()
        {
            
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //pc.StartInfo.Arguments = $"start {path}\\privoxy\\privoxy.exe {path}\\privoxy\\config.txt";
            p.StartInfo.Arguments = "/c START /MIN privoxy\\privoxy.exe privoxy\\config.txt";
            p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.Dispose();
        }

        private void GeneralTrojanConf()
        {
            //if (File.Exists("trojan.conf"))
            //{
                File.WriteAllText("trojan.conf", "{\"run_type\": \"client\", \"local_addr\": \"127.0.0.1\", \"local_port\": 1080, \"remote_addr\":\"" +
                    RemoteAddressBox.Text + "\", \"remote_port\": " + RemotePortBox.Text + ", \"password\": [\"" + PasswordBox.Text + "\"], \"log_level\": 1, \"ssl\": { \"verify\": " +
                    isVerifyCert.Checked.ToString().ToLower() + ",\"verify_hostname\": " + isVerifyHostname.Checked.ToString().ToLower() + ", \"cert\": \"\", \"cipher\": \"ECDHE - ECDSA - AES128 - GCM - SHA256:" +
                    "ECDHE - RSA - AES128 - GCM - SHA256:ECDHE - ECDSA - AES256 - GCM - SHA384:ECDHE - RSA - AES256 - GCM - SHA384:ECDHE - ECDSA - CHACHA20 - POLY1305:" +
                    "ECDHE - RSA - CHACHA20 - POLY1305:ECDHE - RSA - AES128 - SHA:ECDHE - RSA - AES256 - SHA:RSA - AES128 - GCM - SHA256:RSA - AES256 - GCM - SHA384:" +
                    "RSA - AES128 - SHA:RSA - AES256 - SHA:RSA - 3DES - EDE - SHA\", \"sni\": \"\", \"alpn\": [ \"http / 1.1\" ], \"reuse_session\": true, \"session_ticket\": false," +
                    " \"curves\": \"\" }, \"tcp\": { \"no_delay\": true, \"keep_alive\": true, \"fast_open\": false, \"fast_open_qlen\": 20 } }");
            //}
            //else
            //{
                //File.Create("")
            //}
        }



        private static bool isPortUsed(int port)
        {
            bool isPortUsed = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    isPortUsed = true;
                    break;
                }
            }
            return isPortUsed;
        }

    }
}
