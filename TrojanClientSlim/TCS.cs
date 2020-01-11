using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using TrojanClientSlim.Util;
using IniParser;
using IniParser.Model;
using Message = TrojanClientSlim.Util.Message;

namespace TrojanClientSlim
{
    public partial class TCS : Form
    {
        static int localPort;
        readonly FileIniDataParser iniParser = new FileIniDataParser();

        void ConfigIniToCheckBox(string iniSection, string iniKey, CheckBox chkbox, string defaultValue)
        {
            IniData iniData = iniParser.ReadFile("config.ini");
            try
            {
                string x = iniData[iniSection][iniKey].ToLower();
                if (x == "true")
                {
                    chkbox.Checked = true;
                }
                else if (x == "false")
                {
                    chkbox.Checked = false;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                iniData[iniSection][iniKey] = defaultValue;
                iniParser.WriteFile("config.ini", iniData);
                chkbox.Checked = bool.Parse(defaultValue);
            }
        }

        public TCS() => InitializeComponent();

        private void TCS_Load(object sender, EventArgs e)
        {
            if (File.Exists("config.ini"))
            {
                IniData iniData = iniParser.ReadFile("config.ini");

                try
                {
                    localPort = int.Parse(iniData["TCS"]["LocalPort"]);
                }
                catch
                {
                    iniData["TCS"]["LocalPort"] = "1080";
                    iniParser.WriteFile("config.ini", iniData);
                    localPort = 1080;
                }

                ConfigIniToCheckBox("TCS", "VerifyCert", isVerifyCert, "True");
                ConfigIniToCheckBox("TCS", "VerifyHostname", isVerifyHostname, "True");
                ConfigIniToCheckBox("TCS", "HttpProxy", isHttp, "True");

            }
            else
            {
                File.WriteAllText("config.ini", "" +
                    "[TCS]\r\n" +
                    "LocalPort = 1080\r\n" +
                    "VerifyCert = True\r\n" +
                    "VerifyHostname = True\r\n" +
                    "HttpProxy = True");

                isVerifyCert.Checked = true;
                isVerifyHostname.Checked = true;
            }
            if (IsPortUsed(localPort))
                Message.Show($"Port {localPort} is in use!\r\nTrojan may fail to work.", Message.Mode.Warning);
            if (IsPortUsed(54392))
                Message.Show("Port 54392 is in use!\r\nTrojan may fail to work.", Message.Mode.Warning);
            if (File.Exists("node.tcsdb"))
            {
                string[] tmp = ShareLink.ConverteToTrojanConf(File.ReadAllText("node.tcsdb"));
                if (!SetTrojanConf(File.ReadAllText("node.tcsdb")))
                {
                    File.Create("node.tcsdb").Dispose();
                }
            }
            else
                File.Create("node.tcsdb").Dispose();
#if DEBUG
            this.Text = "[D]" + this.Text;
#endif
        }

        private void Run_Click(object sender, EventArgs e) => RunTrojan();
        private void Stop_Click(object sender, EventArgs e)
        {
            StopTrojan();
            Message.Show("Stop Trojan succeeded!", Message.Mode.Info);
        }

        private void Cancle_Click(object sender, EventArgs e) => ExitTCS();

        private void ExitTCS()
        {
            StopTrojan();
            System.Environment.Exit(0);
        }

        private void StopTrojan()
        {
            Proxy.UnsetProxy();
            KillProcess();
        }
        private void RunTrojan()
        {
            if (IsConfigValid())
            {
                try
                {
                    File.WriteAllText("node.tcsdb", GenerateCurrentTrojanConf());
                }
                catch
                {
                    Message.Show("Conf file written failed!", Message.Mode.Error);
                    goto final;
                }
                KillProcess();
                SaveTrojanConf();
                RunTrojanCommand();
                if (isHttp.Checked == true)
                {
                    RunPivoxyCommand();
                    Proxy.SetProxy("127.0.0.1:54392");
                }
                Message.Show("Stop Trojan succeeded!", Message.Mode.Info);
                final:;
            }
            else
            {
                Message.Show("Config invalid! Please enter current trojan information.", Message.Mode.Error);
            }
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
        private bool IsConfigValid() => (!string.IsNullOrEmpty(RemoteAddressBox.Text.Trim()) && !string.IsNullOrEmpty(RemotePortBox.Text.Trim()) && !string.IsNullOrEmpty(PasswordBox.Text.Trim()));

        private void RunTrojanCommand()
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

        private void RunPivoxyCommand()
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

        private void SaveTrojanConf()
        {
            try
            {
                File.WriteAllText("trojan.conf", GenerateCurrentTrojanConf());
            }
            catch
            {
                Message.Show("Conf file written failed!", Message.Mode.Error);
            }
        }

        private string GenerateCurrentTrojanConf()
        {
            return Config.GenerateTrojanJson(1080, RemoteAddressBox.Text,
                    int.Parse(RemotePortBox.Text), PasswordBox.Text, isVerifyCert.Checked, isVerifyHostname.Checked);
        }

        private bool SetTrojanConf(string TcsShareLink) => SetTrojanConf((string[])ShareLink.ConverteToTrojanConf(TcsShareLink));

        private bool SetTrojanConf(string[] trojanConf)
        {
            if (trojanConf != null)
            {
                RemotePortBox.Text = trojanConf[1];
                RemoteAddressBox.Text = trojanConf[0];
                PasswordBox.Text = trojanConf[3];
                return true;
            }
            return false;

        }

        private static bool IsPortUsed(int port)
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

        #region Some Forms Widget
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

        private void ShowPassword_MouseHover(object sender, EventArgs e) => PasswordBox.PasswordChar = new char();

        private void ShowPassword_MouseLeave(object sender, EventArgs e) => PasswordBox.PasswordChar = '*';

        private void EnableShareLink_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableShareLink.Checked)
            {
                ShareLinkBox.ReadOnly = false;
            }
            else
            {
                ShareLinkBox.ReadOnly = true;
            }
        }
        private void ShareLinkBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void RemotePortBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
            if (e.KeyChar == (char)8)
                e.Handled = false;
        }
        #endregion

        #region Size&NotifyIcon

        private void TCS_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                this.Activate();
                this.ShowInTaskbar = true;
                notifyIcon.Visible = false;
            }
        }

        private void RunToolStripMenuItem_Click(object sender, EventArgs e) => RunTrojan();

        private void StopToolStripMenuItem_Click(object sender, EventArgs e) => StopTrojan();

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => ExitTCS();
        private void ShareStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ShareLinkBox.Text);
            Message.Show("TCS share link has copied to clipboard!", Message.Mode.Info);
        }

        private void ImportStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                string[] clipboardLines = ((string)iData.GetData(DataFormats.Text)).Split(new string[] { "\r\n" }, StringSplitOptions.None);
                foreach (string clipboardLine in clipboardLines)
                {
                    if (SetTrojanConf(clipboardLine))
                    {
                        if (WindowState == FormWindowState.Minimized)
                        {
                            WindowState = FormWindowState.Normal;
                            this.Activate();
                            this.ShowInTaskbar = true;
                            notifyIcon.Visible = false;
                        }
                    }
                }
            }
        }

        #endregion

        private void Conf2ShareLink() => ShareLinkBox.Text = ShareLink.Generate(RemoteAddressBox.Text, RemotePortBox.Text, PasswordBox.Text);

        #region TextChanged
        private void RemoteAddressBox_TextChanged(object sender, EventArgs e) => Conf2ShareLink();

        private void RemotePortBox_TextChanged(object sender, EventArgs e) => Conf2ShareLink();

        private void PasswordBox_TextChanged(object sender, EventArgs e) => Conf2ShareLink();

        private void ShareLinkBox_TextChanged(object sender, EventArgs e) => SetTrojanConf(ShareLinkBox.Text);
        #endregion

        private void IsVerifyCert_CheckedChanged(object sender, EventArgs e)
        {
            IniData i = iniParser.ReadFile("config.ini");
            i["TCS"]["VerifyCert"] = isVerifyCert.Checked.ToString();
            iniParser.WriteFile("config.ini", i);
        }

        private void IsVerifyHostname_CheckedChanged(object sender, EventArgs e)
        {
            IniData i = iniParser.ReadFile("config.ini");
            i["TCS"]["VerifyHostname"] = isVerifyCert.Checked.ToString();
            iniParser.WriteFile("config.ini", i);
        }

        private void IsHttp_CheckedChanged(object sender, EventArgs e)
        {
            IniData i = iniParser.ReadFile("config.ini");
            i["TCS"]["HttpProxy"] = isVerifyCert.Checked.ToString();
            iniParser.WriteFile("config.ini", i);
        }
    }
}
