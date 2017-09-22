using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using ChatServer.Util;
using ChatServer.Codec;
using ChatServer.Model;
using System.IO;
using ChatServer.DB;
using RabbitMQ.Client;
using System.Diagnostics;

namespace ChatServer
{
    public partial class FServer : Form
    {

        public FServer()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        AccessManager opAccessManager = new AccessManager();
        //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
        Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        byte[] buffer = new byte[1024];

        private void btnStartService_Click(object sender, EventArgs e)
        {
            

            //发送信息 需要1个IP地址和端口号
            IPAddress ipAddress = null;
            //获取服务端IPv4地址
            ipAddress = GetLocalIPv4Address();
            //获取多网卡外网IP
            //ipAddress = IPAddress.Parse(GetIP());

            lblIP.Text = ipAddress.ToString();
            //给服务端赋予一个端口号
            int port = 10003;
            lblPort.Text = port.ToString();

            //将IP地址和端口号绑定到网络节点endpoint上 
            IPEndPoint endpoint = new IPEndPoint(ipAddress, port);
            //将负责监听的套接字绑定网络端点
            socketWatch.Bind(endpoint);
            //将套接字的监听队列长度设置为20
            socketWatch.Listen(int.MaxValue);

            ///*** 新建连接方法***///
            socketWatch.BeginAccept(new AsyncCallback(WatchConnecting), socketWatch);

            connMsg.AppendText("---->>>服务器已经启动,开始监听客户端传来的信息!" + "\r\n");
            btnStartService.Enabled = false;
        }


        //用于保存所有通信客户端的Socket
        // Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        int count = 0;
        string clientName = null; //创建访问客户端的名字
        IPAddress clientIP; //访问客户端的IP
        int clientPort; //访问客户端的端口号
        static List<Socket> client = new List<Socket>();
        

        /// <summary>
        /// 持续不断监听客户端发来的请求, 用于不断获取客户端发送过来的连续数据信息
        /// </summary>
        private  void WatchConnecting(IAsyncResult r)
        {

            ///***新建连接///
            var socket = r.AsyncState as Socket;
            try
            {
                FServer.client.Add(socket);
                var client = socket.EndAccept(r);
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ServerRecMsg), client);
                socketWatch.BeginAccept(new AsyncCallback(WatchConnecting), socketWatch);

                //获取访问客户端的IP
                clientIP = (client.RemoteEndPoint as IPEndPoint).Address;
                //获取访问客户端的Port
                clientPort = (client.RemoteEndPoint as IPEndPoint).Port;
                //创建访问客户端的唯一标识 由IP和端口号组成 
                clientName = "IP:" + clientIP + ":" + clientPort;
                //lstClients.Items.Add(clientName); //在客户端列表添加该访问客户端的唯一标识
                //dicSocket.Add(clientName, socket); //将客户端名字和套接字添加到添加到数据字典中

                Console.WriteLine("接收到新连接:" + count++);
                connMsg.AppendText("\r\nIP:【" + clientIP + " ：" + clientPort + "】 的客户端与您连接成功,现在你们可以开始通信了...\r\n");
            }
            catch (Exception ex)
            {
                connMsg.AppendText("\r\n客户端【" + clientIP + "】已经中断连接！" + "\r\n"); //提示套接字监听异常
                LogHelper.WriteLog(typeof(FServer), ex);
                socket.Close();//关闭之前accept出来的和客户端进行通信的套接字
                GC.Collect();//强制内存回收

            }          
        }

      
        string strMsg = null;
        int msgCount = 0;

        /// <summary>
        /// 接收客户端发来的信息
        /// </summary>
        private void ServerRecMsg(IAsyncResult ar)
        {
            var st = ar.AsyncState as Socket;
            //获取当前接收到客户端消息IP
            string clientIp = "IP:" + st.RemoteEndPoint;
            try
            {

                var length = st.EndReceive(ar);
                ar.AsyncWaitHandle.Close();
                //Socket socketServer = ar.AsyncState as Socket;
                MsgDecoder msgDecoder = new MsgDecoder();

                
                if (length == 0)
                {
                    Console.WriteLine("有客户端掉线");
                    return;
                }
                msgCount++;
               
                //接收16进制进行字符串转换
                strMsg = ExplainUtils.convertStrMsg(buffer);
                
                Console.WriteLine("收到客户端消息：===>" + msgCount + "条," + strMsg+"端口："+ clientIp);
                //粘包处理
                List<string> recMsgList = MsgDecoder.stickPackage(strMsg);
                foreach (string strSRecMsg in recMsgList)
                {
                    txtMsg.AppendText("\r\n" + GetCurrentTime() + "\n 收到客户端信息：\r\n" + strSRecMsg + "\r\n");

                   
                    //Console.WriteLine("收到客户端消息：===>" + msgCount + "条," + strSRecMsg);
                    byte[] tmsg = ExplainUtils.strToToHexByte(strSRecMsg);
                    //对消息中出现的7d,7e进行转义处理
                    byte[] message = ExplainUtils.DoEscape4Receive(tmsg, 0, tmsg.Length);

                    //校验消息是否正确
                    if (!ExplainUtils.msgValid(message))
                    {
                        //Trace.Write("\r\n消息解析失败，格式错误！\r\n" + strSRecMsg);
                        LogHelper.WriteLog(typeof(FServer), "\r\n消息解析失败，格式错误！\r\n" + clientIp+"发送："+ strSRecMsg);
                        connMsg.AppendText("\r\n消息解析失败，格式错误！\r\n" + strSRecMsg);

                    }
                    else
                    {

                        //解析消息头
                        JT808_PackageData packageData = msgDecoder.Bytes2PackageData(message);
                        //消息ID
                        int msgId = packageData.msgHeader.msgId;
                        //消息体属性
                        int msgBodyProps = packageData.msgHeader.msgBodyPropsField;
                        //终端手机号
                        string terminalPhone = packageData.msgHeader.terminalPhone;
                        //消息流水号
                        int flowId = packageData.msgHeader.flowId;


                        //1. 终端注册 ==> 终端注册应答
                        if (msgId == ExplainUtils.msg_id_terminal_register)
                        {

                            //客户端消息应答
                            byte[] sendMsg = ExplainUtils.rtnTerminalRespMsg(0014, terminalPhone, flowId);
                            ServerSendMsgAuto(st,clientIp, sendMsg);
                        }
                        //2. 终端鉴权 ==> 平台通用应答
                        else if (msgId == ExplainUtils.msg_id_terminal_authentication)
                        {

                            //客户端消息应答
                            byte[] sendMsg = ExplainUtils.rtnServerCommonRespMsg(0005, terminalPhone, flowId, msgId);
                            ServerSendMsgAuto(st, clientIp, sendMsg);
                        }
                        //3. 终端心跳-消息体为空 ==> 平台通用应答
                        else if (msgId == ExplainUtils.msg_id_terminal_heart_beat)
                        {

                            //客户端消息应答
                            byte[] sendMsg = ExplainUtils.rtnServerCommonRespMsg(0005, terminalPhone, flowId, msgId);
                            ServerSendMsgAuto(st, clientIp, sendMsg);
                        }
                        //4. 位置信息汇报 ==> 平台通用应答
                        else if (msgId == ExplainUtils.msg_id_terminal_location_info_upload)
                        {
                            packageData.locationInfo = (msgDecoder.ToLocationInfoMsg(packageData.getMsgBodyBytes()));
                            //客户端消息应答
                            byte[] sendMsg = ExplainUtils.rtnServerCommonRespMsg(0005, terminalPhone, flowId, msgId);

                            string bodymsg = " 报警--->" + packageData.locationInfo.alc
                                + "\r\n 状态--->" + packageData.locationInfo.bst
                                + "\r\n 经度--->" + packageData.locationInfo.lon.ToString()
                                + "\r\n 纬度--->" + packageData.locationInfo.lat.ToString()
                                + "\r\n 高程--->" + packageData.locationInfo.hgt.ToString()
                                + "\r\n 速度--->" + packageData.locationInfo.spd.ToString()
                                + "\r\n 方向--->" + packageData.locationInfo.agl.ToString()
                                + "\r\n 时间--->" + packageData.locationInfo.gtm.ToString()
                                + "\r\n 里程--->" + packageData.locationInfo.mlg.ToString()
                                + "\r\n 油量--->" + packageData.locationInfo.oil.ToString()
                                + "\r\n 记录仪速度--->" + packageData.locationInfo.spd2.ToString()
                                + "\r\n 信号状态--->" + packageData.locationInfo.est
                                + "\r\n IO状态位--->" + packageData.locationInfo.io
                                + "\r\n 模拟量--->" + packageData.locationInfo.ad1.ToString()
                                + "\r\n 信号强度--->" + packageData.locationInfo.yte.ToString()
                                + "\r\n 定位卫星数--->" + packageData.locationInfo.gnss.ToString();


                            txtMsg.AppendText("\r\n【解析消息内容:】\r\n" + bodymsg + "\r\n");

                            //判断是否有报警消息
                            string stralc = packageData.locationInfo.alc;
                            int alc = Convert.ToInt32(stralc, 2);

                            if (alc > 0)
                            {
                                //插入报警数据
                                //opAccessManager.inster_JT808alarmData(packageData);
                            }
                            //保存位置信息到数据库
                            //opAccessManager.inster_JT808data(packageData);

                            string queueName = "";
                            //if (i==100000) {
                            //    i = 0;
                            //}

                            //if (i % 2 == 1)
                            //{
                            //    queueName = "queue1";
                            //}
                            //else
                            //{
                            //    queueName = "queue2";
                            //}
                            //i++;

                            queueName = "hello";

                            //创建消息队列连接
                            //var factory = new ConnectionFactory() { HostName = "localhost" };
                            //using (var connection = factory.CreateConnection())
                            //using (var channel = connection.CreateModel())
                            //{
                            //    //声明queue
                            //    channel.QueueDeclare(queue: queueName,//队列名
                            //                         durable: false,//是否持久化
                            //                         exclusive: false,//排它性
                            //                         autoDelete: false,//一旦客户端连接断开则自动删除queue
                            //                         arguments: null);//如果安装了队列优先级插件则可以设置优先级

                            //    //string message = "Hello World 808 xieyi!";//
                            //    string sendMessage = ExplainUtils.convertStrMsg(packageData.getMsgBodyBytes());//待发送的消息
                            //    var body = Encoding.UTF8.GetBytes(sendMessage);

                            //    channel.BasicPublish(exchange: "",//exchange名称
                            //                         routingKey: queueName,//如果存在exchange,则消息被发送到名称为hello的queue的客户端
                            //                         basicProperties: null,
                            //                         body: packageData.getMsgBodyBytes());//消息体
                            //    Console.WriteLine(" [x] Sent body msg:{0}", sendMessage);
                            //}

                            //消息回执
                            //ServerSendMsgAuto(clientIp, sendMsg);
                            st.Send(sendMsg);
                            txtMsg.AppendText("\r\n" + GetCurrentTime() + "\n 向客户端【" + clientIp + "】\n回执消息:\r\n" + ExplainUtils.convertStrMsg(sendMsg) + "\r\n");
                        }
                        // 其他情况
                        else
                        {
                            txtMsg.AppendText(string.Format(">>>>>[未知消息类型-0x{2}],phone={0},flowid={1}", terminalPhone, flowId, msgId) + "\r\n");
                        }
                    }
                }
                st.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ServerRecMsg), st);
            }
            catch (Exception ex)
            {
                //Trace.Write(ex, "--系统异常！");                    
                connMsg.AppendText("\r\n客户端【" + st.RemoteEndPoint + "】已经中断连接！" + "\r\n"); //提示套接字监听异常
                client.Remove(st);  
                //lstClients.Items.Remove(clientIp);//从listbox中移除断开连接的客户端
                LogHelper.WriteLog(typeof(FServer), ex);
                st.Close();//关闭之前accept出来的和客户端进行通信的套接字
                //dicSocket.Remove(clientIp);
                GC.Collect();//强制内存回收
            }
        
        }


        /// <summary>
        /// 自动回复信息到客户端的方法
        /// </summary>
        /// <param name="clientIp">发送的客户端ip地址</param>
        private void ServerSendMsgAuto(Socket st ,string clientIp, byte[] arrSendMsg)
        {
            //socketWatch.Send(arrSendMsg);
            //向客户端发送字节数组信息
            //dicSocket[clientIp].Send(arrSendMsg);
            //将发送的字符串信息附加到文本框txtMsg上
            st.Send(arrSendMsg);
            txtMsg.AppendText("\r\n" + GetCurrentTime() + "\n 向客户端【" + clientIp + "】\n回执消息:\r\n" + ExplainUtils.convertStrMsg(arrSendMsg) + "\r\n");

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



        //将信息发送到到客户端
        //private void btnSendMsg_Click(object sender, EventArgs e)
        //{
        //    ServerSendMsg(txtSendMsg.Text);
        //}

        //快捷键 Enter 发送信息
        //private void txtSendMsg_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        ServerSendMsg(txtSendMsg.Text);
        //    }
        //}


        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        //private void ServerSendMsg(string sendMsg)
        //{
        //    sendMsg = txtSendMsg.Text.Trim();
        //    //将输入的字符串转换成 机器可以识别的字节数组
        //    byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
        //    //向客户端列表选中的客户端发送信息
        //    if (!string.IsNullOrEmpty(lstClients.Text.Trim()))
        //    {
        //        //获得相应的套接字 并将字节数组信息发送出去
        //        dicSocket[lstClients.Text.Trim()].Send(arrSendMsg);
        //        //通过Socket的send方法将字节数组发送出去
        //        txtMsg.AppendText("您在 " + GetCurrentTime() + " 向 IP:" + clientIP + " Port:" + clientPort + " 的客户端发送了:\r\n" + sendMsg + "\r\n");
        //    }
        //    else //如果未选择任何客户端 则默认为群发信息
        //    {
        //        //遍历所有的客户端
        //        for (int i = 0; i < lstClients.Items.Count; i++)
        //        {
        //            dicSocket[lstClients.Items[i].ToString()].Send(arrSendMsg);
        //        }
        //        txtMsg.AppendText("您在 " + GetCurrentTime() + " 群发了信息:\r\n" + sendMsg + " \r\n");
        //    }
        //}


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
            Application.Exit();
        }

        //取消客户端列表选中状态
        //private void btnClearSelectedState_Click(object sender, EventArgs e)
        //{
        //    lstClients.SelectedItem = null;
        //}

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
    }
}
