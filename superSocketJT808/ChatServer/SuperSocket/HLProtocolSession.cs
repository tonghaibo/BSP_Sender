using ChatServer.Model;
using SuperSocket.SocketBase;
using System;

namespace ZDZC_JT808Access
{
    public class HLProtocolSession : AppSession<HLProtocolSession, HLProtocolRequestInfo>
    {
        protected override void HandleException(Exception e)
        {

        }

        public string id { get; set; }
        public string terminalPhone { get; set; }
        public JT808_PackageData.TerminalRegInfo terminalRegInfo { get; set; }

        //private bool isAuthenticated = false;
        // 消息流水号 word(16) 按发送顺序从 0 开始循环累加
        private int currentFlowId = 0;
        // private ChannelGroup channelGroup = new
        // DefaultChannelGroup(GlobalEventExecutor.INSTANCE);
        // 客户端上次的连接时间，该值改变的情况:
        // 1. terminal --> server 心跳包
        // 2. terminal --> server 数据包
        public DateTime lastCommunicateTimeStamp = DateTime.Parse("2017-1-1");

        public int SyncCurrentFlowId()
        {
            if (currentFlowId >= 0xffff)
                currentFlowId = 0;
            return currentFlowId++;
        }

    }
}
