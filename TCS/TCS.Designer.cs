namespace TCS
{
    partial class TCS
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TCS));
            this.Exit = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.isSock5 = new System.Windows.Forms.CheckBox();
            this.RemoteAddressBox = new System.Windows.Forms.TextBox();
            this.RemotePortBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.isHttp = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.isVerifyCert = new System.Windows.Forms.CheckBox();
            this.Run = new System.Windows.Forms.Button();
            this.Global = new System.Windows.Forms.RadioButton();
            this.GFWList = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.ShowPassword = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.shareStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.ShareLinkBox = new System.Windows.Forms.TextBox();
            this.EnableShareLink = new System.Windows.Forms.CheckBox();
            this.GeoIP = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.isVerifyHostname = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.HttpPortBox = new System.Windows.Forms.TextBox();
            this.SocksPortBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.httpPortLabel = new System.Windows.Forms.Label();
            this.SniBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.NodeTree = new System.Windows.Forms.TreeView();
            this.AddNode = new System.Windows.Forms.Button();
            this.DeleteNode = new System.Windows.Forms.Button();
            this.Subscription = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.NodeNameBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AddGroup = new System.Windows.Forms.Button();
            this.contextMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit.Location = new System.Drawing.Point(227, 3);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(106, 22);
            this.Exit.TabIndex = 16;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Cancle_Click);
            // 
            // Stop
            // 
            this.Stop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stop.Location = new System.Drawing.Point(115, 3);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(106, 22);
            this.Stop.TabIndex = 15;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Remote Address:";
            // 
            // isSock5
            // 
            this.isSock5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isSock5.AutoSize = true;
            this.isSock5.Checked = true;
            this.isSock5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isSock5.Enabled = false;
            this.isSock5.Location = new System.Drawing.Point(3, 3);
            this.isSock5.Name = "isSock5";
            this.isSock5.Size = new System.Drawing.Size(72, 19);
            this.isSock5.TabIndex = 5;
            this.isSock5.Text = "Sock5";
            this.isSock5.UseVisualStyleBackColor = true;
            // 
            // RemoteAddressBox
            // 
            this.RemoteAddressBox.Location = new System.Drawing.Point(368, 42);
            this.RemoteAddressBox.Name = "RemoteAddressBox";
            this.RemoteAddressBox.Size = new System.Drawing.Size(232, 21);
            this.RemoteAddressBox.TabIndex = 1;
            this.RemoteAddressBox.TextChanged += new System.EventHandler(this.RemoteAddressBox_TextChanged);
            // 
            // RemotePortBox
            // 
            this.RemotePortBox.Location = new System.Drawing.Point(368, 69);
            this.RemotePortBox.Name = "RemotePortBox";
            this.RemotePortBox.Size = new System.Drawing.Size(232, 21);
            this.RemotePortBox.TabIndex = 2;
            this.RemotePortBox.TextChanged += new System.EventHandler(this.RemotePortBox_TextChanged);
            this.RemotePortBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Remote Port:";
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(368, 96);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(208, 21);
            this.PasswordBox.TabIndex = 3;
            this.PasswordBox.TextChanged += new System.EventHandler(this.PasswordBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password:";
            // 
            // isHttp
            // 
            this.isHttp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isHttp.AutoSize = true;
            this.isHttp.Checked = true;
            this.isHttp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isHttp.Location = new System.Drawing.Point(81, 3);
            this.isHttp.Name = "isHttp";
            this.isHttp.Size = new System.Drawing.Size(72, 19);
            this.isHttp.TabIndex = 6;
            this.isHttp.Text = "HTTP";
            this.isHttp.UseVisualStyleBackColor = true;
            this.isHttp.CheckedChanged += new System.EventHandler(this.IsHttp_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "Proxy:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "Verify";
            // 
            // isVerifyCert
            // 
            this.isVerifyCert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isVerifyCert.AutoSize = true;
            this.isVerifyCert.Checked = true;
            this.isVerifyCert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isVerifyCert.Location = new System.Drawing.Point(3, 28);
            this.isVerifyCert.Name = "isVerifyCert";
            this.isVerifyCert.Size = new System.Drawing.Size(72, 19);
            this.isVerifyCert.TabIndex = 7;
            this.isVerifyCert.Text = "Cert";
            this.isVerifyCert.UseVisualStyleBackColor = true;
            this.isVerifyCert.CheckedChanged += new System.EventHandler(this.IsVerifyCert_CheckedChanged);
            // 
            // Run
            // 
            this.Run.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Run.Location = new System.Drawing.Point(3, 3);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(106, 22);
            this.Run.TabIndex = 14;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // Global
            // 
            this.Global.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Global.AutoSize = true;
            this.Global.Checked = true;
            this.Global.Location = new System.Drawing.Point(3, 53);
            this.Global.Name = "Global";
            this.Global.Size = new System.Drawing.Size(72, 21);
            this.Global.TabIndex = 9;
            this.Global.TabStop = true;
            this.Global.Text = "Global";
            this.Global.UseVisualStyleBackColor = true;
            this.Global.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
            // 
            // GFWList
            // 
            this.GFWList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GFWList.AutoSize = true;
            this.GFWList.Location = new System.Drawing.Point(81, 53);
            this.GFWList.Name = "GFWList";
            this.GFWList.Size = new System.Drawing.Size(72, 21);
            this.GFWList.TabIndex = 10;
            this.GFWList.Text = "GFWList";
            this.GFWList.UseVisualStyleBackColor = true;
            this.GFWList.CheckedChanged += new System.EventHandler(this.GFWList_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(270, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "Type:";
            // 
            // ShowPassword
            // 
            this.ShowPassword.Location = new System.Drawing.Point(582, 96);
            this.ShowPassword.Name = "ShowPassword";
            this.ShowPassword.Size = new System.Drawing.Size(18, 21);
            this.ShowPassword.TabIndex = 20;
            this.ShowPassword.Text = "*";
            this.ShowPassword.UseVisualStyleBackColor = true;
            this.ShowPassword.MouseLeave += new System.EventHandler(this.ShowPassword_MouseLeave);
            this.ShowPassword.MouseHover += new System.EventHandler(this.ShowPassword_MouseHover);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "TCS";
            this.notifyIcon.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutStripMenuItem,
            this.toolStripSeparator3,
            this.runToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator1,
            this.shareStripMenuItem,
            this.importStripMenuItem,
            this.StartupToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenu.ShowCheckMargin = true;
            this.contextMenu.ShowImageMargin = false;
            this.contextMenu.Size = new System.Drawing.Size(260, 176);
            // 
            // aboutStripMenuItem
            // 
            this.aboutStripMenuItem.Name = "aboutStripMenuItem";
            this.aboutStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.aboutStripMenuItem.Text = "TCS v3.0.0 Insider";
            this.aboutStripMenuItem.Click += new System.EventHandler(this.AboutStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(256, 6);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.RunToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // shareStripMenuItem
            // 
            this.shareStripMenuItem.Name = "shareStripMenuItem";
            this.shareStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.shareStripMenuItem.Text = "Share trojan:// link";
            this.shareStripMenuItem.Click += new System.EventHandler(this.ShareStripMenuItem_Click);
            // 
            // importStripMenuItem
            // 
            this.importStripMenuItem.Name = "importStripMenuItem";
            this.importStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.importStripMenuItem.Text = "Import trojan:// from clipboard";
            this.importStripMenuItem.Click += new System.EventHandler(this.ImportStripMenuItem_Click);
            // 
            // StartupToolStripMenuItem
            // 
            this.StartupToolStripMenuItem.Name = "StartupToolStripMenuItem";
            this.StartupToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.StartupToolStripMenuItem.Text = "Run when startup";
            this.StartupToolStripMenuItem.Click += new System.EventHandler(this.StartupToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(256, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(270, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "Share Link:";
            // 
            // ShareLinkBox
            // 
            this.ShareLinkBox.Location = new System.Drawing.Point(388, 237);
            this.ShareLinkBox.Name = "ShareLinkBox";
            this.ShareLinkBox.ReadOnly = true;
            this.ShareLinkBox.Size = new System.Drawing.Size(212, 21);
            this.ShareLinkBox.TabIndex = 13;
            this.ShareLinkBox.TextChanged += new System.EventHandler(this.ShareLinkBox_TextChanged);
            this.ShareLinkBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ShareLinkBox_MouseUp);
            // 
            // EnableShareLink
            // 
            this.EnableShareLink.AutoSize = true;
            this.EnableShareLink.Location = new System.Drawing.Point(368, 240);
            this.EnableShareLink.Name = "EnableShareLink";
            this.EnableShareLink.Size = new System.Drawing.Size(15, 14);
            this.EnableShareLink.TabIndex = 12;
            this.EnableShareLink.UseVisualStyleBackColor = true;
            this.EnableShareLink.CheckedChanged += new System.EventHandler(this.EnableShareLink_CheckedChanged);
            // 
            // GeoIP
            // 
            this.GeoIP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GeoIP.AutoSize = true;
            this.GeoIP.Location = new System.Drawing.Point(159, 53);
            this.GeoIP.Name = "GeoIP";
            this.GeoIP.Size = new System.Drawing.Size(73, 21);
            this.GeoIP.TabIndex = 11;
            this.GeoIP.Text = "GeoIP";
            this.GeoIP.UseVisualStyleBackColor = true;
            this.GeoIP.CheckedChanged += new System.EventHandler(this.GeoIP_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.Global, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.GFWList, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.GeoIP, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.isVerifyCert, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.isSock5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.isHttp, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.isVerifyHostname, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(368, 154);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(235, 77);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // isVerifyHostname
            // 
            this.isVerifyHostname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.isVerifyHostname.AutoSize = true;
            this.isVerifyHostname.Location = new System.Drawing.Point(81, 28);
            this.isVerifyHostname.Name = "isVerifyHostname";
            this.isVerifyHostname.Size = new System.Drawing.Size(72, 19);
            this.isVerifyHostname.TabIndex = 8;
            this.isVerifyHostname.Text = "Hostname";
            this.isVerifyHostname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.isVerifyHostname.UseVisualStyleBackColor = true;
            this.isVerifyHostname.CheckedChanged += new System.EventHandler(this.IsVerifyHostname_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.Run, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Stop, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Exit, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(267, 264);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(336, 28);
            this.tableLayoutPanel2.TabIndex = 26;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.HttpPortBox, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.SocksPortBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.httpPortLabel, 2, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(267, 298);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(336, 28);
            this.tableLayoutPanel3.TabIndex = 27;
            // 
            // HttpPortBox
            // 
            this.HttpPortBox.Location = new System.Drawing.Point(255, 3);
            this.HttpPortBox.Name = "HttpPortBox";
            this.HttpPortBox.Size = new System.Drawing.Size(78, 21);
            this.HttpPortBox.TabIndex = 18;
            this.HttpPortBox.TextChanged += new System.EventHandler(this.HttpPortBox_TextChanged);
            this.HttpPortBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // SocksPortBox
            // 
            this.SocksPortBox.Location = new System.Drawing.Point(87, 3);
            this.SocksPortBox.Name = "SocksPortBox";
            this.SocksPortBox.Size = new System.Drawing.Size(78, 21);
            this.SocksPortBox.TabIndex = 17;
            this.SocksPortBox.TextChanged += new System.EventHandler(this.SocksPortBox_TextChanged);
            this.SocksPortBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "Socks Port:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // httpPortLabel
            // 
            this.httpPortLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.httpPortLabel.AutoSize = true;
            this.httpPortLabel.Location = new System.Drawing.Point(171, 8);
            this.httpPortLabel.Name = "httpPortLabel";
            this.httpPortLabel.Size = new System.Drawing.Size(65, 12);
            this.httpPortLabel.TabIndex = 29;
            this.httpPortLabel.Text = "HTTP Port:";
            this.httpPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SniBox
            // 
            this.SniBox.Location = new System.Drawing.Point(368, 123);
            this.SniBox.Name = "SniBox";
            this.SniBox.Size = new System.Drawing.Size(232, 21);
            this.SniBox.TabIndex = 4;
            this.SniBox.TextChanged += new System.EventHandler(this.SniBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(270, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 29;
            this.label9.Text = "SNI:";
            // 
            // NodeTree
            // 
            this.NodeTree.Location = new System.Drawing.Point(12, 12);
            this.NodeTree.Name = "NodeTree";
            this.NodeTree.Size = new System.Drawing.Size(244, 240);
            this.NodeTree.TabIndex = 30;
            this.NodeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.NodeTree_AfterSelect);
            this.NodeTree.Validated += new System.EventHandler(this.NodeTree_Validated);
            // 
            // AddNode
            // 
            this.AddNode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNode.Location = new System.Drawing.Point(3, 3);
            this.AddNode.Name = "AddNode";
            this.AddNode.Size = new System.Drawing.Size(116, 25);
            this.AddNode.TabIndex = 31;
            this.AddNode.Text = "Add Node";
            this.AddNode.UseVisualStyleBackColor = true;
            this.AddNode.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // DeleteNode
            // 
            this.DeleteNode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteNode.Location = new System.Drawing.Point(125, 3);
            this.DeleteNode.Name = "DeleteNode";
            this.DeleteNode.Size = new System.Drawing.Size(116, 25);
            this.DeleteNode.TabIndex = 32;
            this.DeleteNode.Text = "Delete";
            this.DeleteNode.UseVisualStyleBackColor = true;
            this.DeleteNode.Click += new System.EventHandler(this.DeleteNode_Click);
            // 
            // Subscription
            // 
            this.Subscription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Subscription.Location = new System.Drawing.Point(125, 34);
            this.Subscription.Name = "Subscription";
            this.Subscription.Size = new System.Drawing.Size(116, 25);
            this.Subscription.TabIndex = 33;
            this.Subscription.Text = "Subscribe";
            this.Subscription.UseVisualStyleBackColor = true;
            this.Subscription.Click += new System.EventHandler(this.Subscription_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.AddGroup, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.Subscription, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.AddNode, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.DeleteNode, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(12, 264);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(244, 62);
            this.tableLayoutPanel4.TabIndex = 34;
            // 
            // NodeNameBox
            // 
            this.NodeNameBox.Location = new System.Drawing.Point(368, 14);
            this.NodeNameBox.Name = "NodeNameBox";
            this.NodeNameBox.Size = new System.Drawing.Size(232, 21);
            this.NodeNameBox.TabIndex = 35;
            this.NodeNameBox.TextChanged += new System.EventHandler(this.NodeNameBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(270, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 36;
            this.label10.Text = "Node Name:";
            // 
            // AddGroup
            // 
            this.AddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddGroup.Location = new System.Drawing.Point(3, 34);
            this.AddGroup.Name = "AddGroup";
            this.AddGroup.Size = new System.Drawing.Size(116, 25);
            this.AddGroup.TabIndex = 34;
            this.AddGroup.Text = "Add Group";
            this.AddGroup.UseVisualStyleBackColor = true;
            this.AddGroup.Click += new System.EventHandler(this.AddGroup_Click);
            // 
            // TCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(619, 337);
            this.Controls.Add(this.NodeNameBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.NodeTree);
            this.Controls.Add(this.SniBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.EnableShareLink);
            this.Controls.Add(this.ShareLinkBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ShowPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RemotePortBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RemoteAddressBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TCS";
            this.Text = "TCS v3.0.0 Insider Preview 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCS_FormClosing);
            this.Load += new System.EventHandler(this.TCS_Load);
            this.SizeChanged += new System.EventHandler(this.TCS_SizeChanged);
            this.contextMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox isSock5;
        public System.Windows.Forms.TextBox RemoteAddressBox;
        public System.Windows.Forms.TextBox RemotePortBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ShowPassword;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ShareLinkBox;
        private System.Windows.Forms.CheckBox EnableShareLink;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem shareStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.RadioButton GeoIP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public System.Windows.Forms.TextBox HttpPortBox;
        public System.Windows.Forms.TextBox SocksPortBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label httpPortLabel;
        public System.Windows.Forms.CheckBox isHttp;
        public System.Windows.Forms.CheckBox isVerifyCert;
        public System.Windows.Forms.RadioButton Global;
        public System.Windows.Forms.RadioButton GFWList;
        public System.Windows.Forms.CheckBox isVerifyHostname;
        private System.Windows.Forms.ToolStripMenuItem StartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.TextBox SniBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TreeView NodeTree;
        private System.Windows.Forms.Button AddNode;
        private System.Windows.Forms.Button DeleteNode;
        private System.Windows.Forms.Button Subscription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        public System.Windows.Forms.TextBox NodeNameBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button AddGroup;
    }
}

