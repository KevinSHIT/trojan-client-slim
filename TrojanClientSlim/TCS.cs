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

        public TCS()
        {
            InitializeComponent();
            Config.tcs = this;
        }

        private void TCS_Load(object sender, EventArgs e)
        {
            InitialTemp();

            ReadConfig();

            if (IsPortUsed(Config.localSocksPort))
                Message.Show($"Port {Config.localSocksPort} is in use!\r\nTrojan may fail to work.", Message.Mode.Warning);
            if (IsPortUsed(Config.localSocksPort))
                Message.Show($"Port {Config.localHttpPort} is in use!\r\nHTTP proxy may fail to work.", Message.Mode.Warning);
            if (File.Exists("node.tcsdb"))
            {
                string[] tmp = ShareLink.ConvertShareToTrojanConf(File.ReadAllText("node.tcsdb"));
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
            Directory.Delete("temp", true);
            System.Environment.Exit(0);
        }

        private void StopTrojan()
        {
            try
            {
                Proxy.UnsetProxy();
            }
            catch
            {
                //FIXME: UNSET FAILED
            }
            KillProcess();
        }
        private void RunTrojan()
        {
            if (IsConfigValid())
            {
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
                    Proxy.SetProxy("127.0.0.1:" + HttpPortBox.Text);
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


        /*private string GenerateCurrentTrojanConf()
        {
            return Config.GenerateTrojanJson(Config.localTrojanPort, RemoteAddressBox.Text,
                    int.Parse(RemotePortBox.Text), PasswordBox.Text, isVerifyCert.Checked, isVerifyHostname.Checked);
        }*/

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
                Config.proxyMode = Config.ProxyMode.Full;
        }

        private void GeoIP_CheckedChanged(object sender, EventArgs e)
        {
            if (GeoIP.Checked)
                Config.proxyMode = Config.ProxyMode.Clash;
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
            //IniData i = iniParser.ReadFile("config.ini");
            //i["TCS"]["VerifyCert"] = isVerifyCert.Checked.ToString();
            //iniParser.WriteFile("config.ini", i);
            Config.verifyCert = isVerifyCert.Checked;
        }

        private void IsVerifyHostname_CheckedChanged(object sender, EventArgs e)
        {
            //IniData i = iniParser.ReadFile("config.ini");
            //i["TCS"]["VerifyHostname"] = isVerifyCert.Checked.ToString();
            //iniParser.WriteFile("config.ini", i);
            Config.verifyHostname = isVerifyCert.Checked;
        }

        private void IsHttp_CheckedChanged(object sender, EventArgs e)
        {
            Config.httpProxy = isHttp.Checked;
            if (Config.httpProxy)
            {
                HttpPortBox.Enabled = true;
            }
            else
            {
                HttpPortBox.Enabled = false;
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

            }
        }
    }
}
