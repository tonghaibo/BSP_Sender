using System;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase;
using BSP_Sender.Codec;
using BSP_Sender.Model;
using BSP_Sender.Util;

namespace ZDZC_JT808Access
{
    public class HLBeginEndMarkReceiveFilter : BeginEndMarkReceiveFilter<HLProtocolRequestInfo>
    {

        //开始和结束标记也可以是两个或两个以上的字节
        private readonly static byte[] BeginMark = new byte[] { 0x7e };
        private readonly static byte[] EndMark = new byte[] { 0x7e };

        private MsgDecoder decoder;
        //private MsgEncoder encoder;
        //private JT808ProtocolUtils jt808ProtocolUtils = new JT808ProtocolUtils();

        private IAppSession appSession;
        public HLBeginEndMarkReceiveFilter(IAppSession appSession) : base(BeginMark, EndMark)
        {
            this.appSession = appSession;
            decoder = new MsgDecoder();
            //encoder = new MsgEncoder(appSession.Logger);
        }

        /// <summary>
        /// 这里解析的到的数据是会把头和尾部都给去掉的 包括0x7E
        /// </summary>
        /// <param name="readBuffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected override HLProtocolRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {
            //var aJT808_PackageData = new JT808_PackageData();
            string all = "";
            for (int i = 0; i < length; i++)
            {
                all = all + readBuffer[offset + i].ToString("X2") + " ";
            }

            //转义还原
            byte[] message = ExplainUtils.DoEscape4Receive(readBuffer, offset, offset + length);
            string all2 = "";
            for (int i = 0; i < message.Length; i++)
            {
                all2 = all2 + message[i].ToString("X2") + " ";
            }

            //解析消息头
            JT808_PackageData aJT808_PackageData = decoder.Bytes2PackageData(message);
            aJT808_PackageData.all = all;
            aJT808_PackageData.all2 = all2;

            if (aJT808_PackageData.errorlog ==null) {
                //解析消息体
                ProcessPackageData(ref aJT808_PackageData);
            }
           

            return new HLProtocolRequestInfo(aJT808_PackageData);
        }


        private void ProcessPackageData(ref JT808_PackageData packageData) {

            //消息ID
            int msgId = packageData.msgHeader.msgId;
            //消息体属性
            int msgBodyProps = packageData.msgHeader.msgBodyPropsField;
            //终端手机号
            string terminalPhone = packageData.msgHeader.terminalPhone;
            //byte[]手机号
            byte[] telphoneByte = packageData.msgHeader.telphoneByte;
            //消息流水号
            int flowId = packageData.msgHeader.flowId;


            //1. 终端注册 ==> 终端注册应答
            if (msgId == ExplainUtils.msg_id_terminal_register)
            {

                //客户端消息应答
                byte[] sendMsg = ExplainUtils.rtnTerminalRespMsg(0014, terminalPhone, flowId);
                //设置回复信息
                packageData.setMsgRespBytes(sendMsg);
            }
            //2. 终端鉴权 ==> 平台通用应答
            else if (msgId == ExplainUtils.msg_id_terminal_authentication)
            {

                //客户端消息应答
                byte[] sendMsg = ExplainUtils.rtnServerCommonRespMsg(0005, terminalPhone, flowId, msgId);
                //设置回复信息
                packageData.setMsgRespBytes(sendMsg);
            }
            //3. 终端心跳-消息体为空 ==> 平台通用应答
            else if (msgId == ExplainUtils.msg_id_terminal_heart_beat)
            {

                //客户端消息应答
                byte[] sendMsg = ExplainUtils.rtnServerCommonRespMsg(0005, terminalPhone, flowId, msgId);
                //设置回复信息
                packageData.setMsgRespBytes(sendMsg);
            }
            //4. 位置信息汇报 ==> 平台通用应答
            else if (msgId == ExplainUtils.msg_id_terminal_location_info_upload)
            {
                packageData.locationInfo = (decoder.ToLocationInfoMsg(packageData.getMsgBodyBytes()));
                //客户端消息应答
                byte[] sendMsg = ExplainUtils.rtnServerCommonRespMsg(0005, terminalPhone, flowId, msgId);
                //设置回复信息
                packageData.setMsgRespBytes(sendMsg);
            }
            // 其他情况
            else
            {
                packageData.errorlog = ">>>>>[未知消息类型-0x{"+ msgId + "}],phone="+ terminalPhone + ",flowid={"+flowId+"}";
              
            }


        }



    }
}

