using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using BSP_Sender.Util;
using BSP_Sender.Codec;
using BSP_Sender.Model;
using System.IO;
using BSP_Sender.DB;
using RabbitMQ.Client;
using System.Diagnostics;
using ZDZC_JT808Access;
using SuperSocket.SocketBase.Config;
using System.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing.Impl;
using SuperSocket.SocketBase;
using System.Collections.Concurrent;

namespace BSP_Sender
{
    public partial class FServer : Form
    {
        // 队列名称  
        private readonly static string QUEUE_NAME = "task_queue_";
        //全局变量，长连接，如果在接收消息方法体内声明对象则会不断创建销毁socket连接，消耗系统资源，极大影响消息推送速率
        private static ConnectionFactory factory = new ConnectionFactory();
        private static List<IConnection> connectionList = new List<IConnection>();
        Dictionary<IConnection, List<IModel>> channelList = new Dictionary<IConnection, List<IModel>>();
        private JT808ProtocolServer protocolServer = new JT808ProtocolServer();
        private ServerConfig serverConfig = new ServerConfig();

        public static int clientCount = 0;//客户端在线数量
        public static object locker = new object();//添加一个对象作为锁

        public int msgCount = 0;//发送到消息队列的消息条数

        public static readonly int MAX_COUNT = 65535;   //计数器轮询最大值，超过该值重置为0
        public static readonly int ORIGIN_COUNT = 0;   //计数器初始值0

        public FServer()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
            Label.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnStartService_Click(object sender, EventArgs e)
        {
            string ipAddress = null;
            //获取服务端IPv4地址
            ipAddress = GetLocalIPv4Address().ToString();

            //ip: 服务器监听的ip地址。你可以设置具体的地址，也可以设置为下面的值 Any
            if (ipBox.Text.Trim() != "" && portBox.Text.Trim() != "")
            {
                try {
                    serverConfig.Ip = ipBox.Text.Trim();
                    serverConfig.Port = int.Parse(portBox.Text.Trim());
                    serverConfig.MaxConnectionNumber = Int32.Parse(ConfigurationManager.AppSettings["maxConnectionNumber"]);
                } catch {
                    connMsg.AppendText("---->>>请输入正确的IP及端口!" + "\r\n");
                    return;
                }
                
            }
            else {
                connMsg.AppendText("---->>>请输入IP地址!" + "\r\n");
                return;
            }
           
            //启动应用服务端口
            if (!protocolServer.Setup(serverConfig)) //启动时监听端口
            {
                connMsg.AppendText("---->>>服务启动失败，请检查IP地址!" + "\r\n");
                return;
            }


            //注册连接事件
            protocolServer.NewSessionConnected += protocolServer_NewSessionConnected;
            //注册请求事件
            protocolServer.NewRequestReceived += protocolServer_NewRequestReceived;
            //注册Session关闭事件
            protocolServer.SessionClosed += protocolServer_SessionClosed;
            //尝试启动应用服务
            if (!protocolServer.Start())
            {
                connMsg.AppendText("---->>>服务启动失败!" + "\r\n");
                return;
            }

            connMsg.AppendText("---->>>服务器已经启动,开始监听客户端传来的信息!" + "\r\n");


            btnStartService.Enabled = false;
            btnExit.Enabled = true;

            //启动好服务即将相应的connection和channel创建好
            factory.HostName = mqAddr_tb.Text.Trim();
            factory.Port = 5672;
            factory.UserName = "admin";
            factory.Password = "123456";
            factory.RequestedHeartbeat = 60;
            factory.AutomaticRecoveryEnabled = true;   //设置端口后自动恢复连接属性即可
            IConnection connection;
            IModel channel;
            List<IModel> channels;
            for (int queueNum = 1; queueNum <= Int32.Parse(queueCount_tb.Text.Trim()); queueNum++) {
                //最多只允许创建connCount个socket连接
                for (int linkNum = 0; linkNum < Int32.Parse(connCount.Text.Trim()); linkNum++)
                {
                    connection = factory.CreateConnection();//创建Socket连接
                    connectionList.Add(connection);
                    channels = new List<IModel>();
                    //最多只允许创建channelCount个channel
                    for (int channelNum = 0; channelNum < Int32.Parse(channelCount_tb.Text.Trim()); channelNum++)
                    {
                        channel = connection.CreateModel();//channel中包含几乎所有的API来供我们操作queue
                        //声明queue
                        channel.QueueDeclare(queue: QUEUE_NAME + queueNum,//队列名
                                            durable: true,//是否持久化,在RabbitMQ服务重启的情况下，也不会丢失消息
                                            exclusive: false,//排他性
                                            autoDelete: false,//一旦客户端连接断开则自动删除queue
                                            arguments: null);//如果安装了队列优先级插件则可以设置优先级
                        channels.Add(channel);
                    }
                    channelList.Add(connection, channels);
                }
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        /// notify:使用AppendText向控件写信息将极大影响消息写入rabbitmq的速度，为了保证rabbitmq的写入速率，建议关闭向控件写入信息，用日志记录即可
        void protocolServer_NewRequestReceived(HLProtocolSession session, HLProtocolRequestInfo requestInfo)
        {
            if (requestInfo.Body.errorlog != null) {
                session.Logger.Error("\r\n消息解析失败，格式错误！IP:" + session.RemoteEndPoint + "发送：：\r\n" + requestInfo.Body.all2);
            }

            //答应消息发送
            if (requestInfo.Body.getMsgRespBytes() != null)
            {
                    session.Send(requestInfo.Body.getMsgRespBytes(), 0, requestInfo.Body.getMsgRespBytes().Length);
            }
            //只有位置上报信息才往rabbitmq里面扔，过滤掉心跳包
            if (ExplainUtils.msg_id_terminal_location_info_upload == requestInfo.Body.msgHeader.msgId)
            {
                rabbitMqTest(session, requestInfo);
            }
        }

        
        int connMod;//当前消息数模connection数,确定消息进哪个connection
        int channelMod;//当前消息数模channel数,确定消息进哪个channel
        int queueMod;//当前消息数模queue数,确定消息进哪个queue
        IBasicProperties properties;
        //rabbitmq消息测试
        public void rabbitMqTest(HLProtocolSession session, HLProtocolRequestInfo requestInfo)
        {
            //消息体前拼接设备号（手机号），2个byte数组合并
            byte[] sendBody = ExplainUtils.twoByteConcat(requestInfo.Body.msgHeader.telphoneByte, requestInfo.Body.getMsgBodyBytes());
            try
            {
                
                connMod = msgCount % Int32.Parse(connCount.Text.Trim());
                channelMod = msgCount % Int32.Parse(channelCount_tb.Text.Trim());
                queueMod = msgCount % Int32.Parse(queueCount_tb.Text.Trim()) + 1;
                if (channelList.ContainsKey(connectionList[connMod]))
                {
                    properties = channelList[connectionList[connMod]][channelMod].CreateBasicProperties();
                    properties.Persistent = true;
                    channelList[connectionList[connMod]][channelMod].BasicPublish(exchange: "",//exchange名称
                                    routingKey: QUEUE_NAME + queueMod,//如果存在exchange，则消息被发送到名为task_queue的客户端
                                    basicProperties: properties,
                                    body: sendBody);//消息体
                    if (msgCount <= MAX_COUNT)
                    {
                        msgCount++;
                    }
                    else
                    {
                        msgCount = ORIGIN_COUNT;
                    }
                    pubMsgCount.Text = "当前计数器值：" + msgCount;
                }
            }
            catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException ex)
            {
                session.Logger.Error("\r\n" + ex.Message.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// Session关闭
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        void protocolServer_SessionClosed(HLProtocolSession session, SuperSocket.SocketBase.CloseReason value)
        {
            session.Logger.Info(GetCurrentTime() + "\r\n客户端【" + session.RemoteEndPoint + "】已经中断连接，连接数：" + clientCount.ToString() + ",断开原因：" + value + "\r\n");
            session.Close();
            //锁
            lock (locker)
            {
                clientCount--;
                clientCount_tb.Text = clientCount.ToString();
            }
        }

        /// <summary>
        /// 注册连接
        /// </summary>
        /// <param name="session"></param>
        void protocolServer_NewSessionConnected(HLProtocolSession session)
        {
            //锁
            lock (locker)   
            {
                clientCount++;
                clientCount_tb.Text = clientCount.ToString();
            }
                session.Logger.Info(GetCurrentTime() + "\r\nIP:【" + session.RemoteEndPoint + "】 的客户端与您连接成功,连接数："+ clientCount.ToString() + "\r\n");
        }

        //关闭通道和连接
        public void closeChanConn()
        {
            //关闭channel Dictionary<IConnection, List<IModel>>
            if (null != channelList)
            {
                foreach (List<IModel> channels in channelList.Values)
                {
                    foreach(IModel channel in channels)
                    {
                        if(null != channel)
                        {
                            channel.Close();
                        }
                    }
                }
            }
            //关闭connection
            if (null != connectionList)
            {
                foreach (IConnection connection in connectionList)
                {
                    if (null != connection)
                    {
                        connection.Close();
                    }
                }
            }
        }


        /// <summary>  
        /// 获取外网ip地址  
        /// </summary>  
        private static string GetIP()
        {
            string tempip = "";
            try
            {
                WebRequest wr = WebRequest.Create("http://city.ip138.com/ip2city.asp");
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据

                int start = all.IndexOf("您的IP地址是：[") + 9;
                int end = all.IndexOf("]", start);
                tempip = all.Substring(start, end - start);
                sr.Close();
                s.Close();
            }
            catch
            {
            }
            return tempip;
        }

        /// <summary>
        /// 获取本地IPv4地址
        /// </summary>
        /// <returns>本地IPv4地址</returns>
        public IPAddress GetLocalIPv4Address()
        {
            IPAddress localIPv4 = null;
            //获取本机所有的IP地址列表
            IPAddress[] ipAddressList = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipAddress in ipAddressList)
            {
                //判断是否是IPv4地址
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork) //AddressFamily.InterNetwork表示IPv4 
                {
                    localIPv4 = ipAddress;
                }
                else
                    continue;
            }
            return localIPv4;
        }


        /// <summary>
        /// 获取当前系统时间
        /// </summary>
        public DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

        //关闭服务端
        private void btnExit_Click(object sender, EventArgs e)
        {
            closeChanConn();
            //btnStartService.Enabled = true;
            //btnExit.Enabled = false;
            Application.Exit();
        }

        
        //测试消息发送
        public void testMsgSend()
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtMsg.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMsg.Text == "") return;
            Clipboard.SetDataObject(txtMsg.Text);
            MessageBox.Show("文本内容已复制到剪切板！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connMsg.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connMsg.Text == "") return;
            Clipboard.SetDataObject(connMsg.Text);
            MessageBox.Show("文本内容已复制到剪切板！");
        }

        //窗口初始化时注册窗口关闭程序
        private void FServer_Load(object sender, EventArgs e)
        {
            btnExit.Enabled = false;
            this.FormClosing += new FormClosingEventHandler(FServer_FormClosing);
            this.FormClosed += new FormClosedEventHandler(FServer_FormClosed);
        }

        private void FServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);//最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净
        }

        //关闭窗口程序
        private void FServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0); //最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净
        }
    }
}
