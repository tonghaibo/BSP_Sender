using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChatServer
{
    /// <summary>
    /// 报文解析工具类
    /// </summary>
    public class ExplainUtils
    {
        //开始和结束标记也可以是两个或两个以上的字节
        private readonly static byte[] BeginMark = new byte[] { 0x7e };
        private readonly static byte[] EndMark = new byte[] { 0x7e };
        public static int pkg_delimiter = 0x7e;// 标识位
        
        //msg是上行消息  cmd是下行指令
        public static int msg_id_terminal_register = 0x0100;// 终端注册
        public static int msg_id_terminal_authentication = 0x0102;// 终端鉴权
        public static int msg_id_terminal_heart_beat = 0x0002;// 终端心跳
        public static int msg_id_terminal_location_info_upload = 0x0200;// 位置信息汇报

        public static int cmd_terminal_register_resp = 0x8100;// 终端注册应答
        public static int cmd_common_resp = 0x8001;// 平台通用应答
        public static string string_encoding = "GBK";//字符编码格式
        public static string replyToken = "1234567890Z";//鉴权码

        /// <summary>
        /// 带空格16进制字符串转换为字节数组
        /// </summary>
        /// <param name="hexSpaceString">带空格的十六进制字符串</param>
        /// <returns></returns>
        public static byte[] HexSpaceStringToByteArray(string hexSpaceString)
            {
                hexSpaceString = hexSpaceString.Replace(" ", string.Empty);
                if (hexSpaceString.Length % 2 != 0)
                {
                    hexSpaceString += " ";
                }
                byte[] array = new byte[hexSpaceString.Length / 2];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = Convert.ToByte(hexSpaceString.Substring(i * 2, 2), 16);
                }
                return array;
            }
        //将获取的消息转换为字符串
        public static string convertStrMsg(byte[] buffer)
        {
            string content = "";
            content = BitConverter.ToString(buffer).Replace("-", " ");
            //TODO:此处截取多余位数待优化
            int index = content.ToUpper().LastIndexOf("7E");
            content = content.Substring(0, index + 2);
            return content;
        }

        /**
         * 把byte[]转化位整形,通常为指令用
         * 
         * @param value
         * @return
         * @throws Exception
         */
        public static int byteToInteger(byte[] value)
        {
            int result;
            if (value.Length == 1)
            {
                result = oneByteToInteger(value[0]);
            }
            else if (value.Length == 2)
            {
                result = twoBytesToInteger(value);
            }
            else if (value.Length == 3)
            {
                result = threeBytesToInteger(value);
            }
            else if (value.Length == 4)
            {
                result = fourBytesToInteger(value);
            }
            else
            {
                result = fourBytesToInteger(value);
            }
            return result;
        }
        /**
         * 把一个byte转化位整形,通常为指令用
         * 
         * @param value
         * @return
         * @throws Exception
         */
        public static int oneByteToInteger(byte value)
        {
            return (int)value & 0xFF;
        }

        /**
         * 把一个2位的数组转化位整形
         * 
         * @param value
         * @return
         * @throws Exception
         */
        public static int twoBytesToInteger(byte[] value)
        {
            // if (value.length < 2) {
            // throw new Exception("Byte array too short!");
            // }
            int temp0 = value[0] & 0xFF;
            int temp1 = value[1] & 0xFF;
            return ((temp0 << 8) + temp1);
        }

        /**
         * 把一个3位的数组转化位整形
         * 
         * @param value
         * @return
         * @throws Exception
         */
        public static int threeBytesToInteger(byte[] value)
        {
            int temp0 = value[0] & 0xFF;
            int temp1 = value[1] & 0xFF;
            int temp2 = value[2] & 0xFF;
            return ((temp0 << 16) + (temp1 << 8) + temp2);
        }

        /**
         * 把一个4位的数组转化位整形,通常为指令用
         * 
         * @param value
         * @return
         * @throws Exception
         */
        public static int fourBytesToInteger(byte[] value)
        {
            // if (value.length < 4) {
            // throw new Exception("Byte array too short!");
            // }
            int temp0 = value[0] & 0xFF;
            int temp1 = value[1] & 0xFF;
            int temp2 = value[2] & 0xFF;
            int temp3 = value[3] & 0xFF;
            return ((temp0 << 24) + (temp1 << 16) + (temp2 << 8) + temp3);
        }

        /**
         * 把一个4位的数组转化位整形
         * 
         * @param value
         * @return
         * @throws Exception
         */
        public long fourBytesToLong(byte[] value)
        {
            // if (value.length < 4) {
            // throw new Exception("Byte array too short!");
            // }
            int temp0 = value[0] & 0xFF;
            int temp1 = value[1] & 0xFF;
            int temp2 = value[2] & 0xFF;
            int temp3 = value[3] & 0xFF;
            return (((long)temp0 << 24) + (temp1 << 16) + (temp2 << 8) + temp3);
        }

        /**
         * 把一个整形该为1位的byte数组
         * 
         * @param value
         * @return
         * @throws Exception
         */
        public static byte[] integerTo1Bytes(int value)
        {
            byte[] result = new byte[1];
            result[0] = (byte)(value & 0xFF);
            return result;
        }

        /**
         * 把一个整形改为2位的byte数组
         * 
         * @param value
         * @return
         * @throws Exception
         */
        public static byte[] integerTo2Bytes(int value)
        {
            byte[] result = new byte[2];
            result[0] = (byte)((value >> 8) & 0xFF);
            result[1] = (byte)(value & 0xFF);
            return result;
        }

        /**
         * 字符串==>BCD字节数组
         * 
         * @param str
         * @return BCD字节数组
         */
        public static byte[] string2Bcd(string str)
        {
            // 奇数,前补零
            if ((str.Length & 0x1) == 1)
            {
                str = "0" + str;
            }

            byte[] ret = new byte[str.Length / 2];
            byte[] bs = Encoding.UTF8.GetBytes(str);
            for (int i = 0; i < ret.Length; i++)
            {

                byte high = ascII2Bcd(bs[2 * i]);
                byte low = ascII2Bcd(bs[2 * i + 1]);

                // TODO 只遮罩BCD低四位?
                ret[i] = (byte)((high << 4) | low);
            }
            return ret;
        }

        public static int getCheckSum4JT808(byte[] bs, int start, int end)
        {
            //if (start < 0 || end > bs.length)
            //    throw new ArrayIndexOutOfBoundsException("getCheckSum4JT808 error : index out of bounds(start=" + start
            //            + ",end=" + end + ",bytes length=" + bs.length + ")");
            int cs = 0;
            for (int i = start; i < end; i++)
            {
                cs ^= bs[i];
            }
            return cs;
        }

        /**
         * 
         * 发送消息时转义<br>
         * 
         * <pre>
         *  0x7e <====> 0x7d02
         * </pre>
         * 
         * @param bs
         *            要转义的字节数组
         * @param start
         *            起始索引
         * @param end
         *            结束索引
         * @return 转义后的字节数组
         * @throws Exception
         */
        public static byte[] DoEscape4Send(byte[] bs, int start, int end)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                for (int i = 0; i < start; i++)
                {
                    ms.WriteByte(bs[i]);
                }
                for (int i = start; i < end; i++)
                {
                    if (bs[i] == 0x7e)
                    {
                        ms.WriteByte(0x7d);
                        ms.WriteByte(0x02);
                    }
                    else
                    {
                        ms.WriteByte(bs[i]);
                    }
                }
                for (int i = end; i < bs.Length; i++)
                {
                    ms.WriteByte(bs[i]);
                }
                return ms.ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static byte ascII2Bcd(byte asc)
        {
            if ((asc >= '0') && (asc <= '9'))
                return (byte)(asc - '0');
            else if ((asc >= 'A') && (asc <= 'F'))
                return (byte)(asc - 'A' + 10);
            else if ((asc >= 'a') && (asc <= 'f'))
                return (byte)(asc - 'a' + 10);
            else
                return (byte)(asc - 48);
        }

        public static int ParseIntFromBytes(byte[] data, int startIndex, int length)
        {
            return ParseIntFromBytes(data, startIndex, length, 0);
        }

        public static int ParseIntFromBytes(byte[] data, int startIndex, int length, int defaultVal)
        {
            try
            {
                // 字节数大于4,从起始索引开始向后处理4个字节,其余超出部分丢弃
                int len = length > 4 ? 4 : length;
                byte[] tmp = new byte[len];
                Buffer.BlockCopy(data, startIndex, tmp, 0, len);
                return ExplainUtils.byteToInteger(tmp);
            }
            catch (Exception e)
            {
                return defaultVal;
            }
        }

        public static String ParseBcdStringFromBytes(byte[] data, int startIndex, int lenth)
        {
            return ParseBcdStringFromBytes(data, startIndex, lenth, null);
        }

        public static String ParseBcdStringFromBytes(byte[] data, int startIndex, int lenth, String defaultVal)
        {
            try
            {
                byte[] tmp = new byte[lenth];
                Buffer.BlockCopy(data, startIndex, tmp, 0, lenth);
                return bcd2String(tmp);
            }
            catch (Exception e)
            {
                //log.Error(string.Format("解析BCD(8421码)出错:{0}", e.Message));
                //e.printStackTrace();
                return defaultVal;
            }
        }

        public static String bcd2String(byte[] bytes)
        {
            StringBuilder temp = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
            {
                // 高四位
                temp.Append((bytes[i] & 0xf0) >> 4);
                // 低四位
                temp.Append(bytes[i] & 0x0f);
            }
            return temp.ToString().Substring(0, 1) == ("0") ? temp.ToString().Substring(1) : temp.ToString();
        }

        //组装应答消息
        public static byte[] rtnRespMsg(int msgId,int msgBodyProps, string phone, int flowId)
        {
            //7E
            //8100            消息ID
            //0004            消息体属性
            //018512345678    手机号
            //0015            消息流水号
            //0015            应答流水号
            //04              结果(00成功, 01车辆已被注册, 02数据库中无该车辆, 03终端已被注册, 04数据库中无该终端)  无车辆与无终端有什么区别 ?
            //313C             鉴权码
            //7E

            List<byte> byteSource = new List<byte>();
            if(msgId == ExplainUtils.msg_id_terminal_register)
            {       
                //终端注册消息应答
                byteSource = registerMsgResp(msgBodyProps,phone,flowId);
            }else if(msgId == ExplainUtils.msg_id_terminal_authentication)
            {
                //终端鉴权
                byteSource = threeMsgResp(msgId, msgBodyProps, phone, flowId);
            }
            else if(msgId == ExplainUtils.msg_id_terminal_heart_beat)
            {
                //终端心跳
                byteSource = threeMsgResp(msgId, msgBodyProps, phone, flowId);
            }
            else if(msgId == ExplainUtils.msg_id_terminal_location_info_upload)
            {
                //位置信息汇报
                byteSource = threeMsgResp(msgId, msgBodyProps, phone, flowId);
            }
            // 转义
            if (byteSource.Count == 0) return null;
            return ExplainUtils.DoEscape4Send(byteSource.ToArray(), 1, byteSource.ToArray().Length - 1);
        }

        //终端注册应答消息组装
        public static List<byte> registerMsgResp(int msgBodyProps, string phone, int flowId)
        {
            List<byte> byteSource = new List<byte>();
            // 1. 0x7e
            //ms.Write(ExplainUtils.integerTo1Bytes(pkg_delimiter), 0, 1);
            byte[] bt1 = ExplainUtils.integerTo1Bytes(pkg_delimiter);
            // 2. 消息ID word(16)
            //ms.Write(ExplainUtils.integerTo2Bytes(cmd_terminal_register_resp), 0, 2);
            byte[] bt2 = ExplainUtils.integerTo2Bytes(cmd_terminal_register_resp);

            // 3. 终端手机号 bcd[6]
            //ms.Write(ExplainUtils.string2Bcd(phone), 0, 6);
            byte[] bt3 = ExplainUtils.string2Bcd(phone);
            // 4. 消息流水号 word(16),按发送顺序从 0 开始循环累加
            //ms.Write(ExplainUtils.integerTo2Bytes(flowId), 0, 2);
            byte[] bt4 = ExplainUtils.integerTo2Bytes(flowId);
            // 5. 应答流水号
            //ms.Write(ExplainUtils.integerTo2Bytes(flowId), 0, 2);
            byte[] bt5 = ExplainUtils.integerTo2Bytes(flowId);
            // 6. 成功
            //ms.Write(ExplainUtils.integerTo1Bytes(0), 0, 1);
            byte[] bt6 = ExplainUtils.integerTo1Bytes(0);
            // 7. 鉴权码
            //ms.Write(System.Text.Encoding.GetEncoding(string_encoding).GetBytes(replyToken), 0, replyToken.Length);
            byte[] bt7 = System.Text.Encoding.GetEncoding(string_encoding).GetBytes(replyToken);
            //8.消息体属性
            byte[] bt8 = ExplainUtils.integerTo2Bytes(bt7.Length + 1 + 2);

            byteSource.AddRange(bt1);
            byteSource.AddRange(bt2);
            byteSource.AddRange(bt8);
            byteSource.AddRange(bt3);
            byteSource.AddRange(bt4);
            byteSource.AddRange(bt5);
            byteSource.AddRange(bt6);
            byteSource.AddRange(bt7);
            // 9. BA 校验码
            // 校验码
            int checkSum = ExplainUtils.getCheckSum4JT808(byteSource.ToArray(), 1, (int)(byteSource.Count));
            //ms.Write(ExplainUtils.integerTo1Bytes(checkSum), 0, 1);
            byte[] bt9 = ExplainUtils.integerTo1Bytes(checkSum);
            // 11. 0x7e
            //ms.Write(ExplainUtils.integerTo1Bytes(pkg_delimiter), 0, 1);
            byteSource.AddRange(bt9);
            byteSource.AddRange(bt1);

            // 转义
            return byteSource;
        }

        //鉴权、心跳、位置汇报应答消息组装
        public static List<byte> threeMsgResp(int msgId, int msgBodyProps, string phone, int flowId)
        {
            List<byte> byteSource = new List<byte>();
            // 1. 0x7e
            //ms.Write(ExplainUtils.integerTo1Bytes(pkg_delimiter), 0, 1);
            byte[] bt1 = ExplainUtils.integerTo1Bytes(pkg_delimiter);
            // 2. 消息ID word(16)
            //ms.Write(ExplainUtils.integerTo2Bytes(cmd_terminal_register_resp), 0, 2);
            byte[] bt2 = ExplainUtils.integerTo2Bytes(cmd_common_resp);

            // 3. 终端手机号 bcd[6]
            //ms.Write(ExplainUtils.string2Bcd(phone), 0, 6);
            byte[] bt3 = ExplainUtils.string2Bcd(phone);
            // 4. 消息流水号 word(16),按发送顺序从 0 开始循环累加
            //ms.Write(ExplainUtils.integerTo2Bytes(flowId), 0, 2);
            byte[] bt4 = ExplainUtils.integerTo2Bytes(flowId);
            // 5. 应答流水号
            //ms.Write(ExplainUtils.integerTo2Bytes(flowId), 0, 2);
            byte[] bt5 = ExplainUtils.integerTo2Bytes(flowId);
            //6.应答id，对应终端消息id
            byte[] bt6 = ExplainUtils.integerTo2Bytes(msgId);
            // 7. 成功，todo:此处比对鉴权码与预留鉴权码是否一致，一致则返回成功，否则返回失败
            //ms.Write(ExplainUtils.integerTo1Bytes(0), 0, 1);
            byte[] bt7 = ExplainUtils.integerTo1Bytes(0);
            //8.消息体属性
            byte[] bt8 = ExplainUtils.integerTo2Bytes(bt4.Length + bt5.Length + bt7.Length);

            byteSource.AddRange(bt1);
            byteSource.AddRange(bt2);
            byteSource.AddRange(bt8);
            byteSource.AddRange(bt3);
            byteSource.AddRange(bt4);
            byteSource.AddRange(bt5);
            byteSource.AddRange(bt6);
            byteSource.AddRange(bt7);
            // 9. BA 校验码
            // 校验码
            int checkSum = ExplainUtils.getCheckSum4JT808(byteSource.ToArray(), 1, (int)(byteSource.Count));
            //ms.Write(ExplainUtils.integerTo1Bytes(checkSum), 0, 1);
            byte[] bt9 = ExplainUtils.integerTo1Bytes(checkSum);
            // 11. 0x7e
            //ms.Write(ExplainUtils.integerTo1Bytes(pkg_delimiter), 0, 1);
            byteSource.AddRange(bt9);
            byteSource.AddRange(bt1);

            // 转义
            return byteSource;
        }

        //检验消息是否有效,无效直接舍弃
        public static bool msgValid(byte[] data)
        {
            int checkSumInPkg = data[data.Length - 1 - 1];
            //消息头开始直到校验码前一个字节异或计算得到的校验码结果
            int calculatedCheckSum = getCheckSum4JT808(data, 0 + 1, data.Length - 1 - 1);
            if (checkSumInPkg != calculatedCheckSum)
            {
                //检验码不一致
                return false;
            }
            return true;
        }

        /// <summary> 
        /// 字符串转16进制字节数组 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

    }
}
