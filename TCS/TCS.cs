using IniParser;

using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

using TCS.Util;

using Message = TCS.Util.Message;

namespace TCS
{
    public partial class TCS : Form
    {

        readonly public static FileIniDataParser iniParser = new FileIniDataParser();

        #region Startup

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
            InitializeComponent();
            Config.tcs = this;

            InitialTemp();

            if (!Directory.Exists("db"))
                Directory.CreateDirectory("db");

            ReadConfig();


            if (File.Exists(TCSPath.Sni))
            {
                try
                {
                    Config.sniList = new SniList(File.ReadAllLines(TCSPath.Sni));
                }
                catch
                {
                    File.Create(TCSPath.Sni);
                }
            }
            else
            {
                File.Create(TCSPath.Sni);
                Config.sniList = new SniList();
            }

            //FIXME: node.tcs make failed? May cause problem!
            //if (File.Exists(TCSPath.Node))
            //{
            //    string[] tmp = ShareLink.ConvertShareToTrojanConf(File.ReadAllText(TCSPath.Node));
            //    if (!SetTrojanConf(File.ReadAllText(TCSPath.Node)))
            //        File.Create(TCSPath.Node).Dispose();
            //}
            //else
            //    File.Create(TCSPath.Node);

            this.SniBox.Text = Config.sniList[this.RemoteAddressBox.Text];

            if (Config.Debug)
                this.Text = "[D]" + this.Text;

            //TODO:NODELIST
            if (!File.Exists(TCSPath.NodeList))
                File.WriteAllText(TCSPath.NodeList, Config.DEFAULT_NODELIST_JSON);
            string a = string.Empty;
            try
            {
                a = Encrypt.DeBase64(File.ReadAllText(TCSPath.NodeList)).Trim();
                if (string.IsNullOrEmpty(a))
                    throw new ArgumentException();
                Newtonsoft.Json.Linq.JObject.Parse(a);
            }
            catch (Exception ex)
            {
                Message.Show(ex.ToString());
                File.WriteAllText(TCSPath.Node, Config.DEFAULT_NODELIST_JSON);
                a = Encrypt.DeBase64(File.ReadAllText(TCSPath.NodeList)).Trim();
            }
            finally
            {
                NodeList.BindTreeView(NodeTree, a);
            }

            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "TCS.lnk")))
                StartupToolStripMenuItem.Checked = true;
            else
                StartupToolStripMenuItem.Checked = false;

            if (args.Length == 1 && args[0].Trim().ToLower() == "silence")
            {
                //this.WindowState = FormWindowState.Minimized;

                RunTrojan(File.ReadAllText(TCSPath.Node), RunMode.Silence);
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
                    if (bool.Parse(Config.iniData["TCS"]["Debug"]))
                        Config.Debug = true;
                }
                catch { }


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
        #endregion

        #region Button Click
        private void Run_Click(object sender, EventArgs e) => RunTrojan(ShareLinkBox.Text);

        private void Stop_Click(object sender, EventArgs e)
        {
            StopTrojan();
            InitialTemp();
            Message.Show("Stop Trojan succeeded!", Message.Mode.Info);
        }

        private void Cancle_Click(object sender, EventArgs e) => ExitTCS();
        #endregion

        #region Helper
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
            Command.StopProcess();
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

        private void RunTrojan(string sharelink, RunMode mode = RunMode.Normal)
        {
            if (IsConfigValid(sharelink))
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

                Command.StopProcess();
                InitialTemp();
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
            }
            else
            {
                Message.Show("Config invalid! Please enter current trojan information.", Message.Mode.Error);
            }
            final:;
        }

        private bool IsConfigValid(string sharelink)
        {
            if (ShareLink.ConvertShareToTrojanConf(sharelink) == null)
                return false;
            else
                return true;
        }

        private bool SetTrojanConf(string TcsShareLink) => SetTrojanConf((string[])ShareLink.ConvertShareToTrojanConf(TcsShareLink));

        private bool SetTrojanConf(string[] trojanConf)
        {
            if (trojanConf != null)
            {
                RemotePortBox.Text = trojanConf[1];
                RemoteAddressBox.Text = trojanConf[0];
                PasswordBox.Text = trojanConf[2];
                NodeNameBox.Text = trojanConf[3];
                return true;
            }
            RemotePortBox.Text = RemoteAddressBox.Text = PasswordBox.Text = NodeNameBox.Text = string.Empty;
            return false;
        }

        private void Conf2ShareLink() => ShareLinkBox.Text = ShareLink.Generate(RemoteAddressBox.Text, RemotePortBox.Text, PasswordBox.Text, NodeNameBox.Text);

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
        #endregion

        private void TCS_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Proxy.UnsetProxy();
            }
            catch
            { }
            Command.StopProcess();
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
            if (e.Button == MouseButtons.Left && !EnableShareLink.Checked)
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

        private void RunToolStripMenuItem_Click(object sender, EventArgs e) => RunTrojan(ShareLinkBox.Text);

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
                string[] clipboardLines = ((string)iData.GetData(DataFormats.Text)).Split(new string[] { "\n" }, StringSplitOptions.None);
                foreach (string clipboardLine in clipboardLines)
                {
                    bool hasDefaultNode = false;
                    int defaultIndex = 0;
                    for (int i = 0; i < NodeTree.Nodes.Count; i++)
                    {
                        if (NodeTree.Nodes[i].Text == "Default")
                        {
                            hasDefaultNode = true;
                            defaultIndex = i;
                        }
                    }
                    if (!hasDefaultNode)
                    {
                        NodeTree.Nodes.Add(new TreeNode { Text = "Default" });
                        defaultIndex = NodeTree.Nodes.Count - 1;
                    }
                    if (ShareLink.ConvertShareToTrojanConf(clipboardLine) != null)
                    {
                        string v = "Untitled";
                        if (clipboardLine.Split('#').Length == 2)
                            v = clipboardLine.Split('#')[1];
                        NodeTree.Nodes[defaultIndex].Nodes.Add(new TreeNode { Text = v, Tag = clipboardLine });
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

        #region TextChanged
        private void RemoteAddressBox_TextChanged(object sender, EventArgs e)
        {
            SniBox.Text = Config.sniList[RemoteAddressBox.Text];

            if (!string.IsNullOrWhiteSpace(RemoteAddressBox.Text))
                Conf2ShareLink();
        }

        private void NodeNameBox_TextChanged(object sender, EventArgs e)
        {
            if (NodeTree.SelectedNode != null)
            {
                NodeTree.SelectedNode.Text = NodeNameBox.Text;
                if (NodeTree.SelectedNode.Level == 0)
                {
                    ShareLinkBox.TextChanged -= ShareLinkBox_TextChanged;
                    bool Has = false;
                    int inx = 0;
                    for (int i = 0; i < NodeTree.Nodes.Count; i++)
                    {
                        if (NodeTree.Nodes[i].Text == NodeNameBox.Text)
                        {
                            ++inx;
                            if (inx == 2)
                            {
                                Has = true;
                                break;
                            }
                        }
                    }
                    if (Has)
                    {
                        Message.Show("Please set different group name!");
                        NodeTree.Enabled = false;
                    }
                    else
                    {
                        NodeTree.Enabled = true;
                        File.WriteAllText(TCSPath.NodeList, NodeTree.ToJObject().ToString());
                        ShareLinkBox.TextChanged += ShareLinkBox_TextChanged;

                    }
                }
                else
                    File.WriteAllText(TCSPath.NodeList, NodeTree.ToJObject().ToString());

            }
            NodeNameBox.Text = NodeNameBox.Text.Replace("#", "");
            if (!string.IsNullOrWhiteSpace(NodeNameBox.Text))
                Conf2ShareLink();
        }

        private string lastRP = "443";
        private void RemotePortBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RemotePortBox.Text))
            {
                Conf2ShareLink();
                //int i = int.Parse(RemotePortBox.Text.Trim());
                /*if (i >= 1 && i <= 65535)
                    lastRP = RemotePortBox.Text;
                else
                {
                    Message.Show("Range of port is 1-65535!", Message.Mode.Error);
                    RemotePortBox.Text = lastRP;
                }
                */
            }
        }


        private void PasswordBox_TextChanged(object sender, EventArgs e) => Conf2ShareLink();

        private void ShareLinkBox_TextChanged(object sender, EventArgs e)
        {
            QrCodeBox.Text = ShareLinkBox.Text;
            SetTrojanConf(ShareLinkBox.Text);
            try
            {
                File.WriteAllText(TCSPath.Node,
                    ShareLink.Generate(RemoteAddressBox.Text, RemotePortBox.Text, PasswordBox.Text, NodeNameBox.Text));
                if (NodeTree.SelectedNode != null)
                    if (NodeTree.SelectedNode.Level != 0)
                        NodeTree.SelectedNode.Tag = ShareLinkBox.Text;
            }
            catch
            {
                Message.Show("Node written failed!", Message.Mode.Error);
            }

            File.WriteAllText(TCSPath.NodeList, Encrypt.Base64(NodeTree.ToJObject().ToString()));
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

        private void SniBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SniBox.Text))
                Config.sniList.Remove(Config.remoteAddress);
            else
                if (!string.IsNullOrWhiteSpace(RemoteAddressBox.Text))
                Config.sniList[RemoteAddressBox.Text] = SniBox.Text;
            File.WriteAllLines(TCSPath.Sni, Config.sniList.ToArray());
        }
        #endregion

        #region CheckedChanged
        private void IsVerifyCert_CheckedChanged(object sender, EventArgs e) => Config.verifyCert = isVerifyCert.Checked;

        private void IsVerifyHostname_CheckedChanged(object sender, EventArgs e) => Config.verifyHostname = isVerifyHostname.Checked;

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
                    GeoIP.Checked = true;
                if (Global.Checked)
                    HttpPortBox.Enabled = false;
            }
        }
        #endregion

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

        private void AboutStripMenuItem_Click(object sender, EventArgs e)
        {
            Message.Show(
                "[Trojan]\r\n" +
                "Ahthor: trojan-gfw contributors\r\n" +
                "[Clash]\r\n" +
                "Author: Dreamacro and other contributors\r\n" +
                "[TCS]\r\n" +
                "Author: KevinZonda and other contributors\r\n", Message.Mode.Info);
        }



        private void AddNode_Click(object sender, EventArgs e)
        {
            TreeNode tn = new TreeNode()
            {
                Text = "Default",
                Tag = "trojan://HelloWorld@google.com:443#Default"
            };

            if (NodeTree.SelectedNode != null)
            {
                if (NodeTree.SelectedNode.Level == 0)
                {
                    NodeTree.SelectedNode.Nodes.Add(tn);
                    NodeTree.SelectedNode = NodeTree.SelectedNode.Nodes[NodeTree.SelectedNode.Nodes.Count - 1];
                }
                else
                {
                    NodeTree.SelectedNode.Parent.Nodes.Add(tn);
                    NodeTree.SelectedNode = NodeTree.SelectedNode.Parent.Nodes[NodeTree.SelectedNode.Parent.Nodes.Count - 1];
                }

            }
        }

        private void DeleteNode_Click(object sender, EventArgs e)
        {
            int v = NodeTree.SelectedNode.Index;
            TreeNode tv;
            bool isRoot = false;
            if (NodeTree.SelectedNode.Level == 0)
            {
                isRoot = true;
                tv = NodeTree.SelectedNode;
                if (MessageBox.Show("Do you want to remove this group?", "Info",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    goto final;
            }
            else
                tv = NodeTree.SelectedNode.Parent;

            NodeTree.SelectedNode.Remove();

            if (v == tv.Nodes.Count)
                v -= 1;
            if (v > 0 && !isRoot)
                NodeTree.SelectedNode = tv.Nodes[v];

            final:;
        }

        private TreeNode previousSelectedNode;
        private void NodeTree_Validated(object sender, EventArgs e)
        {

            if (NodeTree.SelectedNode != null)
            {
                NodeTree.SelectedNode.BackColor = SystemColors.Highlight;
                NodeTree.SelectedNode.ForeColor = Color.White;
                previousSelectedNode = NodeTree.SelectedNode;
            }
        }

        private void NodeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (previousSelectedNode != null)
            {
                previousSelectedNode.BackColor = NodeTree.BackColor;
                previousSelectedNode.ForeColor = NodeTree.ForeColor;
            }

            if (NodeTree.SelectedNode.Level != 0)
            {
                if (NodeTree.SelectedNode.Tag != null)
                {
                    string v = NodeTree.SelectedNode.Tag.ToString();
                    ShareLinkBox.Text = v;
                }
            }
            else
            {
                RemoteAddressBox.Text = PasswordBox.Text = string.Empty;
                NodeNameBox.Text = NodeTree.SelectedNode.Text;
                RemotePortBox.Text = "0";
            }
        }

        private void AddGroup_Click(object sender, EventArgs e)
        {
            NodeTree.Nodes.Add(GetRandomString(8));
        }

        public static string GetRandomString(int length)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = "";
            str += "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

        private void Subscription_Click(object sender, EventArgs e)
        {
            UrlNode u = new UrlNode(NodeTree);
            u.ShowDialog();
            //Message.Show("Undeveloped Module");
        }
    }
}