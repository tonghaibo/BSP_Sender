namespace BSP_Sender
{
    partial class FServer
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnStartService = new System.Windows.Forms.Button();
            this.lstClients = new System.Windows.Forms.ListBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.portBox = new System.Windows.Forms.TextBox();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.connMsg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.pubMsgCount = new System.Windows.Forms.Label();
            this.connCountlabel = new System.Windows.Forms.Label();
            this.connCount = new System.Windows.Forms.TextBox();
            this.channelCountlabel = new System.Windows.Forms.Label();
            this.channelCount_tb = new System.Windows.Forms.TextBox();
            this.mqAddr_label = new System.Windows.Forms.Label();
            this.mqAddr_tb = new System.Windows.Forms.TextBox();
            this.clientCount_label = new System.Windows.Forms.Label();
            this.clientCount_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.queueCount_tb = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "消息内容:";
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(282, 58);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsg.Size = new System.Drawing.Size(666, 131);
            this.txtMsg.TabIndex = 19;
            // 
            // btnStartService
            // 
            this.btnStartService.Location = new System.Drawing.Point(60, 446);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(68, 23);
            this.btnStartService.TabIndex = 15;
            this.btnStartService.Text = "启动服务";
            this.btnStartService.UseVisualStyleBackColor = true;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // lstClients
            // 
            this.lstClients.FormattingEnabled = true;
            this.lstClients.ItemHeight = 12;
            this.lstClients.Location = new System.Drawing.Point(60, 172);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(197, 256);
            this.lstClients.TabIndex = 24;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.ForeColor = System.Drawing.Color.Red;
            this.lblIP.Location = new System.Drawing.Point(35, 15);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(0, 12);
            this.lblIP.TabIndex = 27;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(158, 448);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 21);
            this.btnExit.TabIndex = 29;
            this.btnExit.Text = "退出服务";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "IP:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.portBox);
            this.groupBox1.Controls.Add(this.ipBox);
            this.groupBox1.Controls.Add(this.lblPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Location = new System.Drawing.Point(56, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 97);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务端";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(55, 58);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(132, 21);
            this.portBox.TabIndex = 39;
            this.portBox.Text = "10008";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(55, 23);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(132, 21);
            this.ipBox.TabIndex = 38;
            this.ipBox.Text = "192.168.1.47";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblPort.Location = new System.Drawing.Point(41, 36);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(0, 12);
            this.lblPort.TabIndex = 28;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(56, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 294);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "客户端列表";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(359, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "清空消息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(457, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 33;
            this.button2.Text = "复制消息内容";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // connMsg
            // 
            this.connMsg.Location = new System.Drawing.Point(282, 289);
            this.connMsg.Multiline = true;
            this.connMsg.Name = "connMsg";
            this.connMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.connMsg.Size = new System.Drawing.Size(666, 163);
            this.connMsg.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "连接信息:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(359, 255);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 36;
            this.button3.Text = "清空消息";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pubMsgCount
            // 
            this.pubMsgCount.AutoSize = true;
            this.pubMsgCount.Location = new System.Drawing.Point(734, 29);
            this.pubMsgCount.Name = "pubMsgCount";
            this.pubMsgCount.Size = new System.Drawing.Size(95, 12);
            this.pubMsgCount.TabIndex = 38;
            this.pubMsgCount.Text = "当前计数器值：0";
            // 
            // connCountlabel
            // 
            this.connCountlabel.AutoSize = true;
            this.connCountlabel.Location = new System.Drawing.Point(286, 223);
            this.connCountlabel.Name = "connCountlabel";
            this.connCountlabel.Size = new System.Drawing.Size(53, 12);
            this.connCountlabel.TabIndex = 39;
            this.connCountlabel.Text = "连接数：";
            // 
            // connCount
            // 
            this.connCount.Location = new System.Drawing.Point(359, 220);
            this.connCount.Name = "connCount";
            this.connCount.Size = new System.Drawing.Size(100, 21);
            this.connCount.TabIndex = 40;
            this.connCount.Text = "1";
            // 
            // channelCountlabel
            // 
            this.channelCountlabel.AutoSize = true;
            this.channelCountlabel.Location = new System.Drawing.Point(494, 223);
            this.channelCountlabel.Name = "channelCountlabel";
            this.channelCountlabel.Size = new System.Drawing.Size(53, 12);
            this.channelCountlabel.TabIndex = 41;
            this.channelCountlabel.Text = "通道数：";
            // 
            // channelCount_tb
            // 
            this.channelCount_tb.Location = new System.Drawing.Point(553, 220);
            this.channelCount_tb.Name = "channelCount_tb";
            this.channelCount_tb.Size = new System.Drawing.Size(100, 21);
            this.channelCount_tb.TabIndex = 42;
            this.channelCount_tb.Text = "40";
            // 
            // mqAddr_label
            // 
            this.mqAddr_label.AutoSize = true;
            this.mqAddr_label.Location = new System.Drawing.Point(677, 223);
            this.mqAddr_label.Name = "mqAddr_label";
            this.mqAddr_label.Size = new System.Drawing.Size(53, 12);
            this.mqAddr_label.TabIndex = 43;
            this.mqAddr_label.Text = "mq地址：";
            // 
            // mqAddr_tb
            // 
            this.mqAddr_tb.Location = new System.Drawing.Point(736, 220);
            this.mqAddr_tb.Name = "mqAddr_tb";
            this.mqAddr_tb.Size = new System.Drawing.Size(119, 21);
            this.mqAddr_tb.TabIndex = 44;
            this.mqAddr_tb.Text = "192.168.1.53";
            // 
            // clientCount_label
            // 
            this.clientCount_label.AutoSize = true;
            this.clientCount_label.Location = new System.Drawing.Point(629, 260);
            this.clientCount_label.Name = "clientCount_label";
            this.clientCount_label.Size = new System.Drawing.Size(101, 12);
            this.clientCount_label.TabIndex = 45;
            this.clientCount_label.Text = "客户端在线数量：";
            // 
            // clientCount_tb
            // 
            this.clientCount_tb.Enabled = false;
            this.clientCount_tb.Location = new System.Drawing.Point(736, 256);
            this.clientCount_tb.Name = "clientCount_tb";
            this.clientCount_tb.Size = new System.Drawing.Size(119, 21);
            this.clientCount_tb.TabIndex = 46;
            this.clientCount_tb.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(456, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 47;
            this.label5.Text = "队列数：";
            // 
            // queueCount_tb
            // 
            this.queueCount_tb.Location = new System.Drawing.Point(515, 255);
            this.queueCount_tb.Name = "queueCount_tb";
            this.queueCount_tb.Size = new System.Drawing.Size(100, 21);
            this.queueCount_tb.TabIndex = 48;
            this.queueCount_tb.Text = "10";
            // 
            // FServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 510);
            this.Controls.Add(this.queueCount_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.clientCount_tb);
            this.Controls.Add(this.clientCount_label);
            this.Controls.Add(this.mqAddr_tb);
            this.Controls.Add(this.mqAddr_label);
            this.Controls.Add(this.channelCount_tb);
            this.Controls.Add(this.channelCountlabel);
            this.Controls.Add(this.connCount);
            this.Controls.Add(this.connCountlabel);
            this.Controls.Add(this.pubMsgCount);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.connMsg);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lstClients);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnStartService);
            this.Name = "FServer";
            this.Text = "服务端";
            this.Load += new System.EventHandler(this.FServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox connMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Label pubMsgCount;
        private System.Windows.Forms.Label connCountlabel;
        private System.Windows.Forms.TextBox connCount;
        private System.Windows.Forms.Label channelCountlabel;
        private System.Windows.Forms.TextBox channelCount_tb;
        private System.Windows.Forms.Label mqAddr_label;
        private System.Windows.Forms.TextBox mqAddr_tb;
        private System.Windows.Forms.Label clientCount_label;
        private System.Windows.Forms.TextBox clientCount_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox queueCount_tb;
    }
}

