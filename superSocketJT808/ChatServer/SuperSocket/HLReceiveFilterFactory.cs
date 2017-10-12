
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System.Net;


namespace ZDZC_JT808Access
{
    public class HLReceiveFilterFactory : IReceiveFilterFactory<HLProtocolRequestInfo>
    {
        public IReceiveFilter<HLProtocolRequestInfo> CreateFilter(IAppServer appServer, IAppSession appSession, IPEndPoint remoteEndPoint)
        {
            return new HLBeginEndMarkReceiveFilter(appSession);
        }
    }
}
