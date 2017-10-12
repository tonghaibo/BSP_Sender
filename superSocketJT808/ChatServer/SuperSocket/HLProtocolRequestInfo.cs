using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Protocol;
using ChatServer.Model;

namespace ZDZC_JT808Access
{
    public class HLProtocolRequestInfo : RequestInfo<JT808_PackageData>
    {
        public HLProtocolRequestInfo(JT808_PackageData aJT808_PackageData)
        {
            //如果需要使用命令行协议的话，那么命令类名称HLData相同
            Initialize("JT808_PackageData", aJT808_PackageData);
        }
    }
}
