using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.Modbus
{
    public class ModbusBase
    {
        // 将modbus命令分解
        public MBAPHeader header { get; set; }
        public byte fcn { get; set; }
        public byte[] address { get; set; }
        public byte[] dataLength { get; set; }
    }

    public enum FCN
    {
        /// <summary>
        /// 读寄存器数据
        /// </summary>
        ReadData = 0x03,
        /// <summary>
        /// 写寄存器数据
        /// </summary>
        WriteData = 0x10,
        /// <summary>
        /// 写ID寄存器命令
        /// </summary>
        WriteFcn = 0x11,
    }

    public enum Address
    {
        /// <summary>
        /// 设备信息 R/W
        /// </summary>
        DevInfo = 0x1000,
        /// <summary>
        /// 通道信息 R
        /// </summary>
        ChannelInfo = 0x2000,
        /// <summary>
        /// 工步配置信息 R/W
        /// </summary>
        StepInfo = 0x3000,
        /// <summary>
        /// 历史履历 R
        /// </summary>
        History = 0x4000,
        /// <summary>
        /// 校准数据 R/W
        /// </summary>
        CalibrationData = 0x5000,
        /// <summary>
        /// 调试命令 R/W
        /// </summary>
        TestFcn = 0x6000,
        /// <summary>
        /// 系统配置 R/W
        /// </summary>
        SystemSet = 0x7000,
        /// <summary>
        /// 系统控制 R/W
        /// </summary>
        SystemCtrl = 0x8000,
    }

    public class MBAPHeader
    {
        /// <summary>
        /// 传输标志
        /// </summary>
        /// <Length>2 Bytes</Length>
        /// <Description>标志一次MODBUS请求回应</Description>
        /// <Client>由客户端初始化</Client>
        /// <Server>服务端拷贝回应</Server>
        public byte[] TransFlag { get; set; }

        /// <summary>
        /// 协议标志
        /// </summary>
        /// <Length>2 Bytes</Length>
        /// <Description>0 = MODBUS协议</Description>
        /// <Client>由客户端初始化</Client>
        /// <Server>服务端拷贝回应</Server>
        public byte[] ProtocolFlag { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        /// <Length>2 Bytes</Length>
        /// <Description>接下来总共的数据字节数量</Description>
        /// <Client>由客户端初始化</Client>
        /// <Server>回应请求的长度由服务器设定</Server>
        public byte[] Length { get; set; }

        /// <summary>
        /// 单元号标志
        /// </summary>
        /// <Length>1 Byte</Length>
        /// <Description>指定本节点单元号</Description>
        /// <Client>标志请求的单元号</Client>
        /// <Server>回应请求拷贝此标志</Server>
        public byte UnitFlag { get; set; }

        public MBAPHeader()
        {
            TransFlag = new byte[2];
            ProtocolFlag = new byte[2];
            Length = new byte[2];
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[7];
            bytes[0] = TransFlag[0];
            bytes[1] = TransFlag[1];
            bytes[2] = ProtocolFlag[0];
            bytes[3] = ProtocolFlag[1];
            bytes[4] = Length[0];
            bytes[5] = Length[1];
            bytes[6] = UnitFlag;
            return bytes;
        }
    }
}
