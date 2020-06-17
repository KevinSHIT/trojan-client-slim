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

        readonly public static FileIniDataParser iniParser = new FileIniDataParser();

        void InitialTemp()
        {
            try
            {
                Directory.Delete("temp", true);
            }
            catch { }
            finally
            {
                Directory.CreateDirectory("temp");
            }
        }

        public TCS(string[] args)
        {
            //TODO: Silece Mode
            InitializeComponent();
            Config.tcs = this;
            InitialTemp();

            ReadConfig();

            //if (IsPortUsed(Config.localSocksPort))
            //    Message.Show($"Port {Config.localSocksPort} is in use!\r\nTrojan may fail to work.", Message.Mode.Warning);
            //if (IsPortUsed(Config.localSocksPort))
            //    Message.Show($"Port {Config.localHttpPort} is in use!\r\nHTTP proxy may fail to work.", Message.Mode.Warning);
            if (File.Exists("node.tcsdb"))
            {
                string[] tmp = ShareLink.ConvertShareToTrojanConf(File.ReadAllText("node.tcsdb"));
                if (!SetTrojanConf(File.ReadAllText("node.tcsdb")))
                {
                    File.Create("node.tcsdb").Dispose();

                }
            }
            else
            {
                File.Create("node.tcsdb").Dispose();
            }
            if (Config.Debug)
            {
                this.Text = "[Debug] " + this.Text;
            }

            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "TCS.lnk")))
            {
                StartupToolStripMenuItem.Checked = true;
            }
            else
            {
                StartupToolStripMenuItem.Checked = false;
            }

            if (args.Length == 1 && args[0].Trim().ToLower() == "silence")
            {
                //this.WindowState = FormWindowState.Minimized;

                RunTrojan(RunMode.Silence);
                notifyIcon.Visible = true;
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void TCS_Load(object sender, EventArgs e)
        {

        }

        private void ResetConfig()
        {
            if (File.Exists(Config.DEFAULT_CONFIG_PATH))
            {
                File.Delete(Config.DEFAULT_CONFIG_PATH);
                ResetConfig();
            }
            else
            {
                File.WriteAllText(Config.DEFAULT_CONFIG_PATH, Config.DEFAULT_CONFIG_INI);
            }
        }

        private void ReadConfig()
        {
            if (File.Exists(Config.DEFAULT_CONFIG_PATH))
            {
                //IniData Config.iniData;
                bool needWrite = false;

                //Exist
                try
                {
                    iniParser.ReadFile(Config.DEFAULT_CONFIG_PATH);
                }
                catch
                {
                    ResetConfig();
                }
                finally
                {
                    Config.iniData = iniParser.ReadFile(Config.DEFAULT_CONFIG_PATH);
                }


                //proxtMode
                try
                {
                    Config.proxyMode = Config.ProxyModeParser(Config.iniData["TCS"]["ProxyMode"]);
                }
                catch
                {
                    needWrite = true;
                    Config.iniData["TCS"]["ProxyMode"] = "GFWList";
                }

                //verifyCert
                try
                {
                    Config.verifyCert = bool.Parse(Config.iniData["TCS"]["VerifyCert"]);
                }
                catch
                {
                    needWrite = true;
                    Config.iniData["TCS"]["VerifyCert"] = "true";
                }

                //verifyHostname
                try
                {
                    Config.verifyHostname = bool.Parse(Config.iniData["TCS"]["VerifyHostname"]);
                }
                catch
                {
                    needWrite = true;
                    Config.iniData["TCS"]["VerifyHostname"] = "true";
                }

                //httpProxy
                try
                {
                    Config.httpProxy = bool.Parse(Config.iniData["TCS"]["HttpProxy"]);
                }
                catch
                {
                    needWrite = true;
                    Config.iniData["TCS"]["HttpProxy"] = "true";
                }

                //localSocksPort
                try
                {
                    Config.localSocksPort = int.Parse(Config.iniData["TCS"]["LocalSocksPort"]);
                }
                catch
                {
                    Config.iniData["TCS"]["LocalSocksPort"] = Config.DEFAULT_SOCKS_PORT.ToString();
                    needWrite = true;
                }

                //localHttpPort
                try
                {
                    Config.localHttpPort = int.Parse(Config.iniData["TCS"]["LocalHttpPort"]);
                }
                catch
                {
                    Config.iniData["TCS"]["LocalHttpPort"] = Config.DEFAULT_HTTP_PORT.ToString();
                    needWrite = true;
                }
                try
                {
                    Config.Debug = bool.Parse(Config.iniData["TCS"]["Debug"]);
                }
                catch
                {
                    Config.iniData["TCS"]["Debug"] = "False";
                    needWrite = true;
                }


                if (needWrite)
                {
                    iniParser.WriteFile(Config.DEFAULT_CONFIG_PATH, Config.iniData);
                    ReadConfig();
                }

            }
            else
            {
                ResetConfig();
                ReadConfig();
                return;
            }

        }

        private void Run_Click(object sender, EventArgs e) => RunTrojan();
        private void Stop_Click(object sender, EventArgs e)
        {
            StopTrojan();
            InitialTemp();
            Message.Show("Stop Trojan succeeded!", Message.Mode.Info);
        }

        private void Cancle_Click(object sender, EventArgs e) => ExitTCS();

        private void ExitTCS()
        {
            StopTrojan();
            try
            {
                Directory.Delete("temp", true);
            }
            catch
            {

            }
            System.Environment.Exit(0);
        }

        private void StopTrojan()
        {
            KillProcess();
            try
            {
                Proxy.UnsetProxy();
            }
            catch
            {
                //FIXME: UNSET FAILED
            }
        }

        private enum RunMode
        {
            Silence, Normal
        }
        private void RunTrojan(RunMode mode = RunMode.Normal)
        {
            if (IsConfigValid())
            {
                int status = 0;

                /* Status Code
                 * 0 -> Normal
                 * 1 -> LocalSocksPortUsed
                 * 2 -> LocalHttpPortUsed
                 * 3 -> LocalPortUsed
                 * 4 -> ClashSocksUsed
                 * 5 -> LocalClashSocksPortUsed
                 */

                if (IsPortUsed(Config.localSocksPort))
                {
                    status = 1;
                }
                if (IsPortUsed(Config.localHttpPort))
                {
                    if (status == 0)
                    {
                        if (Config.httpProxy)
                            status = 3;
                        else
                            status = 4;
                    }
                    else
                    {
                        if (Config.httpProxy)
                            status = 2;
                        else
                            status = 5;
                    }
                }

                string message = string.Empty;
                if (status != 0)
                {
                    switch (status)
                    {
                        case 1:
                            message = $"{Config.localSocksPort} is in use. Trojan may not work well.\r\n";
                            break;
                        case 2:
                            message = $"{Config.localHttpPort} is in use. HTTP proxy may not work well.\r\n";
                            break;
                        case 3:
                            message = $"{Config.localSocksPort} is in use.Trojan may not work well.\r\n" +
                                $"{Config.localHttpPort} is in use. HTTP proxy may not work well.\r\n";
                            break;
                        case 4:
                            message = $"{Config.localHttpPort} is in use. Clash may not work well.\r\n";
                            break;
                        case 5:
                            message = $"{Config.localSocksPort} is in use.Trojan may not work well.\r\n" +
                                $"{Config.localHttpPort} is in use. Clash may not work well.\r\n";
                            break;
                    }
                    if (MessageBox.Show(message + "Do you still want to run?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        goto final;
                    }

                }

                //TODO: Logs
                //if(!Directory.Exists("logs"))
                //{
                //    Directory.CreateDirectory("logs");
                //}
                //File.CreateText("logs" + DateTime.Now.ToString(""))


                InitialTemp();
                try
                {
                    File.WriteAllText("node.tcsdb", ShareLink.Generate(RemoteAddressBox.Text, RemotePortBox.Text, PasswordBox.Text));
                }
                catch
                {
                    Message.Show("Conf file written failed!", Message.Mode.Error);
                    goto final;
                }
                KillProcess();
                Command.RunTrojan();
                if (isHttp.Checked == true)
                {
                    Command.RunHttpProxy();
                    Proxy.SetProxy("127.0.0.1:" + Config.localHttpPort.ToString());
                }
                else
                {
                    if (Config.proxyMode == Config.ProxyMode.Clash)
                    {
                        Command.RunSocksProxy();
                    }
                }
                if (mode == RunMode.Normal)
                    Message.Show("Start Trojan succeeded!", Message.Mode.Info);
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
                if (item.ProcessName.ToLower() == "trojan" ||
                    item.ProcessName.ToLower() == "privoxy" ||
                    item.ProcessName.ToLower() == "clash")
                {
                    item.Kill();
                }
            }
        }

        private bool IsConfigValid() => (!string.IsNullOrEmpty(RemoteAddressBox.Text.Trim()) && !string.IsNullOrEmpty(RemotePortBox.Text.Trim()) && !string.IsNullOrEmpty(PasswordBox.Text.Trim()));

        private bool SetTrojanConf(string TcsShareLink) => SetTrojanConf((string[])ShareLink.ConvertShareToTrojanConf(TcsShareLink));

        private bool SetTrojanConf(string[] trojanConf)
        {
            if (trojanConf != null)
            {
                RemotePortBox.Text = trojanConf[1];
                RemoteAddressBox.Text = trojanConf[0];
                PasswordBox.Text = trojanConf[2];
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
            try
            {
                Proxy.UnsetProxy();
            }
            catch
            {


            }
            KillProcess();
        }

        #region Some Forms Widget
        private void GFWList_CheckedChanged(object sender, EventArgs e)
        {
            if (GFWList.Checked == true)
                Config.proxyMode = Config.ProxyMode.GFWList;
        }

        private void Global_CheckedChanged(object sender, EventArgs e)
        {
            if (Global.Checked == true)
            {
                Config.proxyMode = Config.ProxyMode.Full;
                if (!Config.httpProxy)
                {
                    HttpPortBox.Enabled = false;
                }
            }
        }

        private void GeoIP_CheckedChanged(object sender, EventArgs e)
        {
            if (GeoIP.Checked)
            {
                Config.proxyMode = Config.ProxyMode.Clash;
                if (!Config.httpProxy)
                {
                    HttpPortBox.Enabled = true;
                }
            }
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

        char r = ' ';
        private new void KeyPress(object sender, KeyPressEventArgs e)
        {
            r = e.KeyChar;
            if (char.IsDigit(r) || r == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
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
            Config.verifyCert = isVerifyCert.Checked;
        }

        private void IsVerifyHostname_CheckedChanged(object sender, EventArgs e)
        {
            Config.verifyHostname = isVerifyHostname.Checked;
        }

        private void IsHttp_CheckedChanged(object sender, EventArgs e)
        {
            Config.httpProxy = isHttp.Checked;
            if (Config.httpProxy)
            {
                httpPortLabel.Text = "HTTP Port:";
                HttpPortBox.Enabled = true;
                GFWList.Enabled = true;
            }
            else
            {
                httpPortLabel.Text = "Socks Port:";
                //HttpPortBox.Enabled = false;
                GFWList.Enabled = false;
                if (GFWList.Checked)
                {
                    GeoIP.Checked = true;
                }
            }
            //IniData i = iniParser.ReadFile("config.ini");
            //i["TCS"]["HttpProxy"] = isVerifyCert.Checked.ToString();
            //iniParser.WriteFile("config.ini", i);
        }

        private void SocksPortBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Config.localSocksPort = int.Parse(SocksPortBox.Text);
            }
            catch
            {
                Message.Show("Port can only be an integer", Message.Mode.Error);
            }
        }

        private void HttpPortBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Config.localHttpPort = int.Parse(HttpPortBox.Text);
            }
            catch
            {
                Message.Show("Port can only be an integer", Message.Mode.Error);
            }
        }

        private void StartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "TCS.lnk")))
            {
                LnkHelper.RemoveLnk();
                StartupToolStripMenuItem.Checked = false;
            }
            else
            {
                LnkHelper.SetLnk();
                StartupToolStripMenuItem.Checked = true;
            }
        }

        private void aboutStripMenuItem_Click(object sender, EventArgs e)
        {
            Message.Show(
                "[Trojan]\r\n" +
                "Ahthor: trojan-gfw contributors\r\n" +
                "[Clash]\r\n" +
                "Author: Dreamacro and other contributors\r\n" +
                "[TCS]\r\n" +
                "Author: KevinZonda and other contributors\r\n", Message.Mode.Info);
        }
    }
}
