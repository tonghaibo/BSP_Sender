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
            this.location_port = new System.Windows.Forms.Label();
            this.location_portVal = new System.Windows.Forms.TextBox();
            this.location_username = new System.Windows.Forms.Label();
            this.location_usernameVal = new System.Windows.Forms.TextBox();
            this.location_pwd = new System.Windows.Forms.Label();
            this.location_pwdVal = new System.Windows.Forms.TextBox();
            this.alarm_pwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.alarm_username = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.alarmPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.alarm_queueCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.alarmAddr = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.alarm_channelCount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.alarm_connCount = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.alarm_count = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            // connMsg
            // 
            this.connMsg.Location = new System.Drawing.Point(372, 329);
            this.connMsg.Multiline = true;
            this.connMsg.Name = "connMsg";
            this.connMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.connMsg.Size = new System.Drawing.Size(614, 123);
            this.connMsg.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 359);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "连接信息:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(270, 391);
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
            this.pubMsgCount.Location = new System.Drawing.Point(555, 148);
            this.pubMsgCount.Name = "pubMsgCount";
            this.pubMsgCount.Size = new System.Drawing.Size(95, 12);
            this.pubMsgCount.TabIndex = 38;
            this.pubMsgCount.Text = "当前计数器值：0";
            // 
            // connCountlabel
            // 
            this.connCountlabel.AutoSize = true;
            this.connCountlabel.Location = new System.Drawing.Point(289, 64);
            this.connCountlabel.Name = "connCountlabel";
            this.connCountlabel.Size = new System.Drawing.Size(53, 12);
            this.connCountlabel.TabIndex = 39;
            this.connCountlabel.Text = "连接数：";
            // 
            // connCount
            // 
            this.connCount.Location = new System.Drawing.Point(343, 61);
            this.connCount.Name = "connCount";
            this.connCount.Size = new System.Drawing.Size(119, 21);
            this.connCount.TabIndex = 40;
            this.connCount.Text = "1";
            // 
            // channelCountlabel
            // 
            this.channelCountlabel.AutoSize = true;
            this.channelCountlabel.Location = new System.Drawing.Point(478, 64);
            this.channelCountlabel.Name = "channelCountlabel";
            this.channelCountlabel.Size = new System.Drawing.Size(53, 12);
            this.channelCountlabel.TabIndex = 41;
            this.channelCountlabel.Text = "通道数：";
            // 
            // channelCount_tb
            // 
            this.channelCount_tb.Location = new System.Drawing.Point(537, 61);
            this.channelCount_tb.Name = "channelCount_tb";
            this.channelCount_tb.Size = new System.Drawing.Size(100, 21);
            this.channelCount_tb.TabIndex = 42;
            this.channelCount_tb.Text = "40";
            // 
            // mqAddr_label
            // 
            this.mqAddr_label.AutoSize = true;
            this.mqAddr_label.Location = new System.Drawing.Point(289, 102);
            this.mqAddr_label.Name = "mqAddr_label";
            this.mqAddr_label.Size = new System.Drawing.Size(53, 12);
            this.mqAddr_label.TabIndex = 43;
            this.mqAddr_label.Text = "mq地址：";
            // 
            // mqAddr_tb
            // 
            this.mqAddr_tb.Location = new System.Drawing.Point(343, 99);
            this.mqAddr_tb.Name = "mqAddr_tb";
            this.mqAddr_tb.Size = new System.Drawing.Size(119, 21);
            this.mqAddr_tb.TabIndex = 44;
            this.mqAddr_tb.Text = "192.168.1.47";
            // 
            // clientCount_label
            // 
            this.clientCount_label.AutoSize = true;
            this.clientCount_label.Location = new System.Drawing.Point(289, 148);
            this.clientCount_label.Name = "clientCount_label";
            this.clientCount_label.Size = new System.Drawing.Size(101, 12);
            this.clientCount_label.TabIndex = 45;
            this.clientCount_label.Text = "客户端在线数量：";
            // 
            // clientCount_tb
            // 
            this.clientCount_tb.Enabled = false;
            this.clientCount_tb.Location = new System.Drawing.Point(396, 144);
            this.clientCount_tb.Name = "clientCount_tb";
            this.clientCount_tb.Size = new System.Drawing.Size(119, 21);
            this.clientCount_tb.TabIndex = 46;
            this.clientCount_tb.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(655, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 47;
            this.label5.Text = "队列数：";
            // 
            // queueCount_tb
            // 
            this.queueCount_tb.Location = new System.Drawing.Point(715, 61);
            this.queueCount_tb.Name = "queueCount_tb";
            this.queueCount_tb.Size = new System.Drawing.Size(100, 21);
            this.queueCount_tb.TabIndex = 48;
            this.queueCount_tb.Text = "10";
            // 
            // location_port
            // 
            this.location_port.AutoSize = true;
            this.location_port.Location = new System.Drawing.Point(478, 101);
            this.location_port.Name = "location_port";
            this.location_port.Size = new System.Drawing.Size(53, 12);
            this.location_port.TabIndex = 51;
            this.location_port.Text = "端口号：";
            // 
            // location_portVal
            // 
            this.location_portVal.Location = new System.Drawing.Point(537, 99);
            this.location_portVal.Name = "location_portVal";
            this.location_portVal.Size = new System.Drawing.Size(100, 21);
            this.location_portVal.TabIndex = 52;
            this.location_portVal.Text = "5672";
            // 
            // location_username
            // 
            this.location_username.AutoSize = true;
            this.location_username.Location = new System.Drawing.Point(655, 101);
            this.location_username.Name = "location_username";
            this.location_username.Size = new System.Drawing.Size(53, 12);
            this.location_username.TabIndex = 53;
            this.location_username.Text = "用户名：";
            // 
            // location_usernameVal
            // 
            this.location_usernameVal.Location = new System.Drawing.Point(715, 98);
            this.location_usernameVal.Name = "location_usernameVal";
            this.location_usernameVal.Size = new System.Drawing.Size(100, 21);
            this.location_usernameVal.TabIndex = 54;
            this.location_usernameVal.Text = "admin";
            // 
            // location_pwd
            // 
            this.location_pwd.AutoSize = true;
            this.location_pwd.Location = new System.Drawing.Point(839, 102);
            this.location_pwd.Name = "location_pwd";
            this.location_pwd.Size = new System.Drawing.Size(41, 12);
            this.location_pwd.TabIndex = 55;
            this.location_pwd.Text = "密码：";
            // 
            // location_pwdVal
            // 
            this.location_pwdVal.Location = new System.Drawing.Point(886, 98);
            this.location_pwdVal.Name = "location_pwdVal";
            this.location_pwdVal.PasswordChar = '*';
            this.location_pwdVal.Size = new System.Drawing.Size(100, 21);
            this.location_pwdVal.TabIndex = 56;
            this.location_pwdVal.Text = "123456";
            // 
            // alarm_pwd
            // 
            this.alarm_pwd.Location = new System.Drawing.Point(886, 272);
            this.alarm_pwd.Name = "alarm_pwd";
            this.alarm_pwd.PasswordChar = '*';
            this.alarm_pwd.Size = new System.Drawing.Size(100, 21);
            this.alarm_pwd.TabIndex = 73;
            this.alarm_pwd.Text = "123456";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(839, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 72;
            this.label3.Text = "密码：";
            // 
            // alarm_username
            // 
            this.alarm_username.Location = new System.Drawing.Point(715, 272);
            this.alarm_username.Name = "alarm_username";
            this.alarm_username.Size = new System.Drawing.Size(100, 21);
            this.alarm_username.TabIndex = 71;
            this.alarm_username.Text = "alarm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(655, 275);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 70;
            this.label8.Text = "用户名：";
            // 
            // alarmPort
            // 
            this.alarmPort.Location = new System.Drawing.Point(537, 273);
            this.alarmPort.Name = "alarmPort";
            this.alarmPort.Size = new System.Drawing.Size(100, 21);
            this.alarmPort.TabIndex = 69;
            this.alarmPort.Text = "5673";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(478, 275);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 68;
            this.label9.Text = "端口号：";
            // 
            // alarm_queueCount
            // 
            this.alarm_queueCount.Location = new System.Drawing.Point(715, 235);
            this.alarm_queueCount.Name = "alarm_queueCount";
            this.alarm_queueCount.Size = new System.Drawing.Size(100, 21);
            this.alarm_queueCount.TabIndex = 67;
            this.alarm_queueCount.Text = "10";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(655, 240);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 66;
            this.label10.Text = "队列数：";
            // 
            // alarmAddr
            // 
            this.alarmAddr.Location = new System.Drawing.Point(343, 273);
            this.alarmAddr.Name = "alarmAddr";
            this.alarmAddr.Size = new System.Drawing.Size(119, 21);
            this.alarmAddr.TabIndex = 63;
            this.alarmAddr.Text = "192.168.1.47";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(289, 276);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 62;
            this.label12.Text = "mq地址：";
            // 
            // alarm_channelCount
            // 
            this.alarm_channelCount.Location = new System.Drawing.Point(537, 235);
            this.alarm_channelCount.Name = "alarm_channelCount";
            this.alarm_channelCount.Size = new System.Drawing.Size(100, 21);
            this.alarm_channelCount.TabIndex = 61;
            this.alarm_channelCount.Text = "40";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(478, 238);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 60;
            this.label13.Text = "通道数：";
            // 
            // alarm_connCount
            // 
            this.alarm_connCount.Location = new System.Drawing.Point(343, 235);
            this.alarm_connCount.Name = "alarm_connCount";
            this.alarm_connCount.Size = new System.Drawing.Size(119, 21);
            this.alarm_connCount.TabIndex = 59;
            this.alarm_connCount.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(289, 238);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 58;
            this.label14.Text = "连接数：";
            // 
            // alarm_count
            // 
            this.alarm_count.AutoSize = true;
            this.alarm_count.Location = new System.Drawing.Point(839, 235);
            this.alarm_count.Name = "alarm_count";
            this.alarm_count.Size = new System.Drawing.Size(95, 12);
            this.alarm_count.TabIndex = 57;
            this.alarm_count.Text = "当前计数器值：0";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(270, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(730, 145);
            this.groupBox3.TabIndex = 74;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "位置消息mq配置";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(270, 196);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(730, 119);
            this.groupBox4.TabIndex = 75;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "报警消息mq配置";
            // 
            // FServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 484);
            this.Controls.Add(this.location_pwdVal);
            this.Controls.Add(this.location_pwd);
            this.Controls.Add(this.location_usernameVal);
            this.Controls.Add(this.location_username);
            this.Controls.Add(this.location_portVal);
            this.Controls.Add(this.location_port);
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
            this.Controls.Add(this.alarm_pwd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.alarm_username);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.alarmPort);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.alarm_queueCount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.alarmAddr);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.alarm_channelCount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.alarm_connCount);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.alarm_count);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.connMsg);
            this.Controls.Add(this.lstClients);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStartService);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Name = "FServer";
            this.Text = "服务端";
            this.Load += new System.EventHandler(this.FServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPort;
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
        private System.Windows.Forms.Label location_port;
        private System.Windows.Forms.TextBox location_portVal;
        private System.Windows.Forms.Label location_username;
        private System.Windows.Forms.TextBox location_usernameVal;
        private System.Windows.Forms.Label location_pwd;
        private System.Windows.Forms.TextBox location_pwdVal;
        private System.Windows.Forms.TextBox alarm_pwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox alarm_username;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox alarmPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox alarm_queueCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox alarmAddr;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox alarm_channelCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox alarm_connCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label alarm_count;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

