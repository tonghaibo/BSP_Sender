using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServer.Model
{
    public class JT808_PackageData
    {
        /// <summary>
        /// 消息头
        /// </summary>
        public MsgHeader msgHeader;

        /// <summary>
        /// 终端注册消息 0100
        /// </summary>
        public TerminalRegInfo terminalRegInfo;

        /// <summary>
        /// 位置上报消息 0200
        /// </summary>
        public LocationInfo locationInfo;

        /// <summary>
        /// 消息体内容
        /// </summary>
        protected byte[] msgBodyBytes;

        /// <summary>
        /// 应答消息内容
        /// </summary>
        protected byte[] msgRespBytes;

        /// <summary>
        /// 校验码
        /// </summary>
        public int checkSum;

        /// <summary>
        /// 原始数据
        /// </summary>
        public string all { get; set; }

        /// <summary>
        /// 转义还原
        /// </summary>
        public string all2 { get; set; }

        /// <summary>
        /// 错误日志
        /// </summary>
        public string errorlog { get; set; }

        public byte[] getMsgBodyBytes()
        {
            return msgBodyBytes;
        }

        public void setMsgBodyBytes(byte[] msgBodyBytes)
        {
            this.msgBodyBytes = msgBodyBytes;
        }

        public byte[] getMsgRespBytes()
        {
            return msgRespBytes;
        }

        public void setMsgRespBytes(byte[] msgRespBytes)
        {
            this.msgRespBytes = msgRespBytes;
        }

        public String toString()
        {
            return "PackageData [msgHeader=" + msgHeader + ", msgBodyBytes=" + System.Text.Encoding.Default.GetString(msgBodyBytes) + ", checkSum="
                    + checkSum + ", address=" + "channel" + "]";
        }

        public class MsgHeader
        {
            /// <summary>
            /// 消息ID
            /// </summary>
            public int msgId { get; set; }

            /// <summary>
            /// 消息体属性  byte[2-3]
            /// </summary>
            public int msgBodyPropsField { get; set; }

            /// <summary>
            /// 消息体长度
            /// </summary>
            public int msgBodyLength { get; set; }

            /// <summary>
            /// 数据加密方式
            /// </summary>
            public int encryptionType { get; set; }

            /// <summary>
            /// 是否分包,true==>有消息包封装项
            /// </summary>
            public bool hasSubPackage { get; set; }

            /// <summary>
            /// 保留位[14-15]
            /// </summary>
            public String reservedBit { get; set; }

            /// <summary>
            /// 终端手机号
            /// </summary>
            public String terminalPhone { get; set; }

            /// <summary>
            /// 流水号
            /// </summary>
            public int flowId { get; set; }

            /// <summary>
            /// 消息包封装项 byte[12-15]
            /// </summary>
            public int packageInfoField { get; set; }

            /// <summary>
            /// 消息包总数(word(16))
            /// </summary>
            public long totalSubPackage { get; set; }

            /// <summary>
            /// 包序号(word(16))这次发送的这个消息包是分包中的第几个消息包, 从 1 开始
            /// </summary>
            public long subPackageSeq { get; set; }

            public String toString()
            {
                return "MsgHeader [msgId=" + msgId + ", msgBodyPropsField=" + msgBodyPropsField + ", msgBodyLength="
                        + msgBodyLength + ", encryptionType=" + encryptionType + ", hasSubPackage=" + hasSubPackage
                        + ", reservedBit=" + reservedBit + ", terminalPhone=" + terminalPhone + ", flowId=" + flowId
                        + ", packageInfoField=" + packageInfoField + ", totalSubPackage=" + totalSubPackage
                        + ", subPackageSeq=" + subPackageSeq + "]";
            }

        }

        public class LocationInfo
        {
            //基本信息
            public string alc { get; set; } //报警标志 4位
            public string bst { get; set; }//状态 4位
            public double lon { get; set; }//经度 4位 除以10的6次方 转为度
            public double lat { get; set; }//纬度 4位 除以10的6次方 转为度
            public float hgt { get; set; } //高程 2位
            public float spd { get; set; }//速度 2位 转为十进制 除以10即62.2KM/H
            public float agl { get; set; }//方向 2位
            public DateTime gtm { get; set; }//时间 
            //扩展信息
            public float mlg { get; set; }// 里程  0x01 4
            public float oil { get; set; }// 油量  0x02 2
            public float spd2 { get; set; }// 记录仪速度 0x03 2
            public string est { get; set; }//扩展车辆信号状态位 0x25  4
            public string io { get; set; }// IO状态位 0x2A 2
            public string ad1 { get; set; }// 模拟量 0x2B 4
            public int yte { get; set; }//，无线通信网络信号强度 0x30 1
            public int gnss { get; set; }// 定位卫星数 0x31 1

            //迈的 ECU数据
            public string ecu { get; set; }// ECU透传数据 0xE0 n

        }

        public class TerminalRegInfo
        {
            // 省域ID(WORD),设备安装车辆所在的省域，省域ID采用GB/T2260中规定的行政区划代码6位中前两位
            // 0保留，由平台取默认值
            public int provinceId { get; set; }
            // 市县域ID(WORD) 设备安装车辆所在的市域或县域,市县域ID采用GB/T2260中规定的行 政区划代码6位中后四位
            // 0保留，由平台取默认值
            public int cityId { get; set; }
            // 制造商ID(BYTE[5]) 5 个字节，终端制造商编码
            public String manufacturerId { get; set; }
            // 终端型号(BYTE[8]) 八个字节， 此终端型号 由制造商自行定义 位数不足八位的，补空格。
            public String terminalType { get; set; }
            // 终端ID(BYTE[7]) 七个字节， 由大写字母 和数字组成， 此终端 ID由制造 商自行定义
            public String terminalId { get; set; }
            /**
             * 
             * 车牌颜色(BYTE) 车牌颜色，按照 JT/T415-2006 的 5.4.12 未上牌时，取值为0<br>
             * 0===未上车牌<br>
             * 1===蓝色<br>
             * 2===黄色<br>
             * 3===黑色<br>
             * 4===白色<br>
             * 9===其他
             */
            public int licensePlateColor { get; set; }
            // 车牌(STRING) 公安交 通管理部门颁 发的机动车号牌
            public String licensePlate { get; set; }

            public TerminalRegInfo()
            {
            }

            public String toString()
            {
                return "TerminalRegInfo [provinceId=" + provinceId + ", cityId=" + cityId + ", manufacturerId="
                        + manufacturerId + ", terminalType=" + terminalType + ", terminalId=" + terminalId
                        + ", licensePlateColor=" + licensePlateColor + ", licensePlate=" + licensePlate + "]";
            }
        }

    }
}
