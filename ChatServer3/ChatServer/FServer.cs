using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;


namespace ChatServer
{
    public partial class FServer : Form
    {

        //private HLBeginEndMarkReceiveFilter receiveFilter = new HLBeginEndMarkReceiveFilter();
        public FServer()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        //分别创建一个监听客户端的线程和套接字
        Thread threadWatch = null;
        Socket socketWatch = null;

        public const int SendBufferSize = 2 * 1024;
        public const int ReceiveBufferSize = 1 * 1024;


        private void btnStartService_Click(object sender, EventArgs e) 
        {
            //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //发送信息 需要1个IP地址和端口号
            //获取服务端IPv4地址
            IPAddress ipAddress = GetLocalIPv4Address();
            lblIP.Text = ipAddress.ToString();
            //给服务端赋予一个端口号
            int port = 8088;
            lblPort.Text = port.ToString();

            //将IP地址和端口号绑定到网络节点endpoint上 
            IPEndPoint endpoint = new IPEndPoint(ipAddress, port);
            //将负责监听的套接字绑定网络端点
            socketWatch.Bind(endpoint);
            //将套接字的监听队列长度设置为20
            socketWatch.Listen(20);
            //创建一个负责监听客户端的线程 
            threadWatch = new Thread(WatchConnecting);
            //将窗体线程设置为与后台同步
            threadWatch.IsBackground = true;
            //启动线程
            threadWatch.Start();
            txtMsg.AppendText("服务器已经启动,开始监听客户端传来的信息!" + "\r\n");
            btnStartService.Enabled = false;
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

        //用于保存所有通信客户端的Socket
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();

        //创建与客户端建立连接的套接字
        Socket socConnection = null;
        string clientName = null; //创建访问客户端的名字
        IPAddress clientIP; //访问客户端的IP
        int clientPort; //访问客户端的端口号
        /// <summary>
        /// 持续不断监听客户端发来的请求, 用于不断获取客户端发送过来的连续数据信息
        /// </summary>
        private void WatchConnecting()
        {
            while (true)
            {
                try
                {
                    socConnection = socketWatch.Accept();
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText(ex.Message); //提示套接字监听异常
                    break;
                }
                //获取访问客户端的IP
                clientIP = (socConnection.RemoteEndPoint as IPEndPoint).Address;
                //获取访问客户端的Port
                clientPort = (socConnection.RemoteEndPoint as IPEndPoint).Port;
                //创建访问客户端的唯一标识 由IP和端口号组成 
                clientName = "IP:" + clientIP + ":" + clientPort;
                lstClients.Items.Add(clientName); //在客户端列表添加该访问客户端的唯一标识
                dicSocket.Add(clientName, socConnection); //将客户端名字和套接字添加到添加到数据字典中

                //创建通信线程 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
                Thread thread = new Thread(pts);
                thread.IsBackground = true;
                //启动线程
                thread.Start(socConnection);
                txtMsg.AppendText("IP: " + clientIP + " Port: " + clientPort + " 的客户端与您连接成功,现在你们可以开始通信了...\r\n");
            }
        }

        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ServerSendMsg(string sendMsg)
        {
            sendMsg = txtSendMsg.Text.Trim();
            //将输入的字符串转换成 机器可以识别的字节数组
            byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //向客户端列表选中的客户端发送信息
            if (!string.IsNullOrEmpty(lstClients.Text.Trim()))
            {
                //获得相应的套接字 并将字节数组信息发送出去
                dicSocket[lstClients.Text.Trim()].Send(arrSendMsg);
                //通过Socket的send方法将字节数组发送出去
                txtMsg.AppendText("您在 " + GetCurrentTime() + " 向 IP:" + clientIP + " Port:" + clientPort + " 的客户端发送了:\r\n" + sendMsg + "\r\n");
            }
            else //如果未选择任何客户端 则默认为群发信息
            {
                //遍历所有的客户端
                for (int i = 0; i < lstClients.Items.Count; i++)
                {
                    dicSocket[lstClients.Items[i].ToString()].Send(arrSendMsg);
                }
                txtMsg.AppendText("您在 " + GetCurrentTime() + " 群发了信息:\r\n" + sendMsg + " \r\n");
            }
        }

        string strSRecMsg = "";
        /// <summary>
        /// 接收客户端发来的信息
        /// </summary>
        private void ServerRecMsg(object socketClientPara)
        {
            Socket socketServer = socketClientPara as Socket;
           
            while (true)
            {
                int firstReceived = 0;
                byte[] buffer = new byte[ReceiveBufferSize];
                //获取当前接收到客户端消息IP
                string clientIp = "IP:" + socketServer.RemoteEndPoint;
                try
                {
                    //获取接收的数据,并存入内存缓冲区  返回一个字节数组的长度
                    if (socketServer != null) firstReceived = socketServer.Receive(buffer);

                    if (firstReceived > 0) //接受到的长度大于0 说明有信息或文件传来
                    {
                        //strSRecMsg = Encoding.UTF8.GetString(buffer, 0, firstReceived);//真实有用的文本信息要比接收到的少1(标识符),此方法乱码
                        strSRecMsg = ExplainUtils.convertStrMsg(buffer);
                        txtMsg.AppendText("" + GetCurrentTime() + "收到客户端信息:\r\n" + strSRecMsg + "\r\n");
                        //receiveFilter.processMatchedRequest(buffer, 0, firstReceived);
                        //客户端消息应答
                        //校验消息是否正确
                        if (!ExplainUtils.msgValid(ExplainUtils.strToToHexByte(strSRecMsg))) break;
                        Console.WriteLine("received:{0}", strSRecMsg);
                        byte[] bytes = ExplainUtils.HexSpaceStringToByteArray(strSRecMsg);
                        int msgBodyProps = ExplainUtils.ParseIntFromBytes(bytes, 2 + 1, 2);
                        string terminalPhone = (ExplainUtils.ParseBcdStringFromBytes(bytes, 4 + 1, 6));
                        int flowId = ExplainUtils.ParseIntFromBytes(bytes, 10 + 1, 2);
                        //客户端消息应答
                        ServerSendMsgAuto(clientIp, msgBodyProps, terminalPhone, flowId);

                    }
                }
                catch (Exception ex)
                {
                    //txtMsg.AppendText("系统异常消息:" + ex.Message);
                    txtMsg.AppendText("客户端" + socketServer.RemoteEndPoint + "已经中断连接" + "\r\n"); //提示套接字监听异常 
                    lstClients.Items.Remove(clientIp);//从listbox中移除断开连接的客户端
                    socketServer.Close();//关闭之前accept出来的和客户端进行通信的套接字
                    dicSocket.Remove(clientIp);
                    break;
                }
            }
        }


        /// <summary>
        /// 自动回复信息到客户端的方法,设备注册消息应答测试
        /// </summary>
        /// <param name="clientIp">发送的客户端ip地址</param>
        private void ServerSendMsgAuto(string clientIp,int msgBodyProps, string phone, int flowId)
        {
            //将输入的字符串转换成 机器可以识别的字节数组
            byte[] arrSendMsg = ExplainUtils.rtnRespMsg(msgBodyProps, phone, flowId);
            //向客户端发送字节数组信息
            dicSocket[clientIp].Send(arrSendMsg);
            //将发送的字符串信息附加到文本框txtMsg上
            txtMsg.AppendText("" + GetCurrentTime() + "向客户端<" + clientIp + ">回执:\r\n" + ExplainUtils.convertStrMsg(arrSendMsg) + "\r\n");

        }



        //将信息发送到到客户端
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            ServerSendMsg(txtSendMsg.Text);
        }

        //快捷键 Enter 发送信息
        private void txtSendMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ServerSendMsg(txtSendMsg.Text);
            }
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
            Application.Exit();
        }

        //取消客户端列表选中状态
        private void btnClearSelectedState_Click(object sender, EventArgs e)
        {
            lstClients.SelectedItem = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMsg.Text = "";
        }
    }
}
