using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace TrojanClientSlim
{
    public partial class TCS : Form
    {
        public TCS()
        {
            InitializeComponent();
        }

        private void TCS_Load(object sender, EventArgs e)
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
#if DEBUG
            this.Text = "[D]" + this.Text;
#endif
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
            KillProcess();
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


            try
            {
                File.WriteAllText("conf", Encrypt.Base64($"{RemoteAddressBox.Text}:{RemotePortBox.Text}:{PasswordBox.Text}:{isHttp.Checked}:{ch}"));
            }
            catch
            {
                MessageBox.Show("FATAL ERROR: Conf file written failed!", "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                KillProcess();
            }
            catch
            {
                MessageBox.Show("FATAL ERROR: Kill process failed!", "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
#if DEBUG
            p.StartInfo.UseShellExecute = true;
#else
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
#endif
            p.Start();
        }

        private void RunPivoxy()
        {

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //pc.StartInfo.Arguments = $"start {path}\\privoxy\\privoxy.exe {path}\\privoxy\\config.txt";
            if (Global.Checked)
            {
                p.StartInfo.Arguments = "/c START /MIN privoxy\\privoxy.exe privoxy\\config.txt";
            }
            if (GFWList.Checked)
            {
                p.StartInfo.Arguments = "/c cd privoxy && START /MIN privoxy.exe config_gfw.txt";
            }
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.Dispose();
        }

        private void GeneralTrojanConf()
        {
            try
            {
                File.WriteAllText("trojan.conf", "{\"run_type\": \"client\", \"local_addr\": \"127.0.0.1\", \"local_port\": 1080, \"remote_addr\":\"" +
                    RemoteAddressBox.Text + "\", \"remote_port\": " + RemotePortBox.Text + ", \"password\": [\"" + PasswordBox.Text + "\"], \"log_level\": 1, \"ssl\": { \"verify\": " +
                    isVerifyCert.Checked.ToString().ToLower() + ",\"verify_hostname\": " + isVerifyHostname.Checked.ToString().ToLower() + ", \"cert\": \"\", \"cipher\": \"ECDHE - ECDSA - AES128 - GCM - SHA256:" +
                    "ECDHE - RSA - AES128 - GCM - SHA256:ECDHE - ECDSA - AES256 - GCM - SHA384:ECDHE - RSA - AES256 - GCM - SHA384:ECDHE - ECDSA - CHACHA20 - POLY1305:" +
                    "ECDHE - RSA - CHACHA20 - POLY1305:ECDHE - RSA - AES128 - SHA:ECDHE - RSA - AES256 - SHA:RSA - AES128 - GCM - SHA256:RSA - AES256 - GCM - SHA384:" +
                    "RSA - AES128 - SHA:RSA - AES256 - SHA:RSA - 3DES - EDE - SHA\", \"sni\": \"\", \"alpn\": [ \"http / 1.1\" ], \"reuse_session\": true, \"session_ticket\": false," +
                    " \"curves\": \"\" }, \"tcp\": { \"no_delay\": true, \"keep_alive\": true, \"fast_open\": false, \"fast_open_qlen\": 20 } }");
            }
            catch
            {
                MessageBox.Show("FATAL ERROR: Conf file written failed!", "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void TCS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Proxy.UnsetProxy();
            KillProcess();
        }

        private void GFWList_CheckedChanged(object sender, EventArgs e)
        {
            if (GFWList.Checked == true)
                Global.Checked = false;
            else
                Global.Checked = true;
        }

        private void Global_CheckedChanged(object sender, EventArgs e)
        {
            if (Global.Checked == true)
                GFWList.Checked = false;
            else
                GFWList.Checked = true;
        }

        private void ShowPassword_MouseHover(object sender, EventArgs e)
        {
            PasswordBox.PasswordChar = new char();
        }

        private void ShowPassword_MouseLeave(object sender, EventArgs e)
        {
            PasswordBox.PasswordChar = '*';
        }
    }
}
