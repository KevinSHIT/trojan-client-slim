namespace TrojanClientSlim
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
            this.isVerifyHostname = new System.Windows.Forms.CheckBox();
            this.Run = new System.Windows.Forms.Button();
            this.Global = new System.Windows.Forms.RadioButton();
            this.GFWList = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.ShowPassword = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(213, 179);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 23);
            this.Exit.TabIndex = 1;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Cancle_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(112, 179);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 2;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Remote Address:";
            // 
            // isSock5
            // 
            this.isSock5.AutoSize = true;
            this.isSock5.Checked = true;
            this.isSock5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isSock5.Enabled = false;
            this.isSock5.Location = new System.Drawing.Point(113, 102);
            this.isSock5.Name = "isSock5";
            this.isSock5.Size = new System.Drawing.Size(54, 16);
            this.isSock5.TabIndex = 4;
            this.isSock5.Text = "Sock5";
            this.isSock5.UseVisualStyleBackColor = true;
            // 
            // RemoteAddressBox
            // 
            this.RemoteAddressBox.Location = new System.Drawing.Point(113, 15);
            this.RemoteAddressBox.Name = "RemoteAddressBox";
            this.RemoteAddressBox.Size = new System.Drawing.Size(175, 21);
            this.RemoteAddressBox.TabIndex = 5;
            // 
            // RemotePortBox
            // 
            this.RemotePortBox.Location = new System.Drawing.Point(113, 42);
            this.RemotePortBox.Name = "RemotePortBox";
            this.RemotePortBox.Size = new System.Drawing.Size(175, 21);
            this.RemotePortBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Remote Port:";
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(113, 69);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(151, 21);
            this.PasswordBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password:";
            // 
            // isHttp
            // 
            this.isHttp.AutoSize = true;
            this.isHttp.Checked = true;
            this.isHttp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isHttp.Location = new System.Drawing.Point(216, 103);
            this.isHttp.Name = "isHttp";
            this.isHttp.Size = new System.Drawing.Size(48, 16);
            this.isHttp.TabIndex = 10;
            this.isHttp.Text = "HTTP";
            this.isHttp.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "Proxy:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "Verify";
            // 
            // isVerifyCert
            // 
            this.isVerifyCert.AutoSize = true;
            this.isVerifyCert.Checked = true;
            this.isVerifyCert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isVerifyCert.Location = new System.Drawing.Point(113, 126);
            this.isVerifyCert.Name = "isVerifyCert";
            this.isVerifyCert.Size = new System.Drawing.Size(48, 16);
            this.isVerifyCert.TabIndex = 12;
            this.isVerifyCert.Text = "Cert";
            this.isVerifyCert.UseVisualStyleBackColor = true;
            // 
            // isVerifyHostname
            // 
            this.isVerifyHostname.AutoSize = true;
            this.isVerifyHostname.Checked = true;
            this.isVerifyHostname.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isVerifyHostname.Location = new System.Drawing.Point(216, 126);
            this.isVerifyHostname.Name = "isVerifyHostname";
            this.isVerifyHostname.Size = new System.Drawing.Size(72, 16);
            this.isVerifyHostname.TabIndex = 14;
            this.isVerifyHostname.Text = "Hostname";
            this.isVerifyHostname.UseVisualStyleBackColor = true;
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(12, 179);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 15;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // Global
            // 
            this.Global.AutoSize = true;
            this.Global.Checked = true;
            this.Global.Location = new System.Drawing.Point(113, 150);
            this.Global.Name = "Global";
            this.Global.Size = new System.Drawing.Size(59, 16);
            this.Global.TabIndex = 16;
            this.Global.TabStop = true;
            this.Global.Text = "Global";
            this.Global.UseVisualStyleBackColor = true;
            this.Global.CheckedChanged += new System.EventHandler(this.Global_CheckedChanged);
            // 
            // GFWList
            // 
            this.GFWList.AutoSize = true;
            this.GFWList.Location = new System.Drawing.Point(216, 150);
            this.GFWList.Name = "GFWList";
            this.GFWList.Size = new System.Drawing.Size(65, 16);
            this.GFWList.TabIndex = 17;
            this.GFWList.Text = "GFWList";
            this.GFWList.UseVisualStyleBackColor = true;
            this.GFWList.CheckedChanged += new System.EventHandler(this.GFWList_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "Type:";
            // 
            // ShowPassword
            // 
            this.ShowPassword.Location = new System.Drawing.Point(270, 69);
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
            this.runToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenu.ShowImageMargin = false;
            this.contextMenu.Size = new System.Drawing.Size(79, 70);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(78, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.RunToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(78, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(78, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // TCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(306, 220);
            this.Controls.Add(this.ShowPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.GFWList);
            this.Controls.Add(this.Global);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.isVerifyHostname);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.isVerifyCert);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.isHttp);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RemotePortBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RemoteAddressBox);
            this.Controls.Add(this.isSock5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TCS";
            this.Text = "TCS v2.0.1 by Kevin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCS_FormClosing);
            this.Load += new System.EventHandler(this.TCS_Load);
            this.SizeChanged += new System.EventHandler(this.TCS_SizeChanged);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox isSock5;
        private System.Windows.Forms.TextBox RemoteAddressBox;
        private System.Windows.Forms.TextBox RemotePortBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox isHttp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox isVerifyCert;
        private System.Windows.Forms.CheckBox isVerifyHostname;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.RadioButton Global;
        private System.Windows.Forms.RadioButton GFWList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ShowPassword;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

