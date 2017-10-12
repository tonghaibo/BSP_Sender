using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace ZDZC_JT808Access
{
    public class JT808ProtocolServer : AppServer<HLProtocolSession, HLProtocolRequestInfo>
    {
        /// <summary>
        /// 使用自定义协议工厂
        /// </summary>
        public JT808ProtocolServer()
            : base(new HLReceiveFilterFactory())
        {

        }


    }
}
