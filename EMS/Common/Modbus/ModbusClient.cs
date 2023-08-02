using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using System.IO.Ports;
using System.Diagnostics.Contracts;

namespace EMS.Common.Modbus.ModbusTCP
{
    public class ModbusClient
    {
        private string _ip = "127.0.0.1";
        private int _port = 0;
        private TcpClient _client;

        private string _serialport = "COM1";
        private int _rate = 9600;
        private int _parity = 0;
        private int _databits = 8;
        private int _stopbits = 0;
        private SerialPort _serial;

        /// <summary>
        /// RTU : ture
        /// TCP : false
        /// </summary>
        private bool _RtuOrTcp;

        private ModbusMaster _master;

        /// <summary>
        /// 以TCP格式生成ModbusClient实例
        /// </summary>
        /// <param name="IP">ip地址</param>
        /// <param name="Port">端口号</param>
        public ModbusClient(string IP, int Port)
        {
            _ip = IP;
            _port = Port;
            _RtuOrTcp = false;
        }

        /// <summary>
        /// 以RTU格式生成ModbusClient实例
        /// </summary>
        /// <param name="Port">串口号</param>
        /// <param name="Rate">波特率</param>
        /// <param name="Parity">奇偶校验</param>
        /// <param name="Databits">数据位</param>
        /// <param name="Stopbits">停止位</param>
        public ModbusClient(string Port, int Rate, int Parity, int Databits, int Stopbits)
        {
            _serialport = Port;
            _rate = Rate;
            _parity = Parity;
            _databits = Databits;
            _stopbits = Stopbits;
            _RtuOrTcp = true;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public void Connect()
        {
            try
            {
                if (_RtuOrTcp)
                {
                    _serial = new SerialPort(_serialport, _rate, (Parity)_parity, _databits, (StopBits)_stopbits);
                    _serial.Open();
                    _master = ModbusSerialMaster.CreateRtu(_serial);
                }
                else
                {
                    _client = new TcpClient();
                    _client.Connect(IPAddress.Parse(_ip), _port);
                    _master = ModbusIpMaster.CreateIp(_client);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (_RtuOrTcp)
                {
                    _master.Transport.Dispose();
                    _serial.Close();
                    _serial.Dispose();
                }
                else
                {
                    _master.Transport.Dispose();
                    _client.Close();
                    _client.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通用读取函数
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="num">读取位数</param>
        /// <returns>读取值</returns>
        public byte[] ReadFunc(ushort address, ushort num)
        {
            try
            {
                ushort[] holding_register = _master.ReadHoldingRegisters(0, address, num);
                byte[] ret = new byte[holding_register.Length * 2];
                for (int i = 0; i < holding_register.Length; i++)
                {
                    var objs = BitConverter.GetBytes(holding_register[i]);
                    ret[i * 2] = objs[0];
                    ret[i * 2 + 1] = objs[1];
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 异步通用读取函数
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="num">读取位数</param>
        /// <returns>读取值</returns>
        private async Task<byte[]> ReadFuncAsync(ushort address, ushort num)
        {
            try
            {
                ushort[] holding_register = await Task.Run(() => _master.ReadHoldingRegistersAsync(0, address, num));
                var ret = new byte[holding_register.Length * 2];
                for (int i = 0; i < holding_register.Length; i++)
                {
                    var objs = BitConverter.GetBytes(holding_register[i]);
                    ret[i * 2] = objs[0];
                    ret[i * 2 + 1] = objs[1];
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通用写入函数
        /// </summary>
        /// <param name="address">寄存器</param>
        /// <param name="value">写入值</param>
        public void WriteFunc(ushort address, ushort value)
        {
            try
            {
                _master.WriteSingleRegister(0, address, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 异步通用写入函数
        /// </summary>
        /// <param name="address">寄存器</param>
        /// <param name="value">写入值</param>
        private async void WriteFuncAsync(ushort address, ushort value)
        {
            try
            {
                await Task.Run(() => _master.WriteSingleRegisterAsync(0, address, value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通用批量写入函数
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="values">写入值</param>
        private void WriteFunc(ushort address, ushort[] values)
        {
            try
            {
                _master.WriteMultipleRegisters(0, address, values);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 异步通用批量写入函数
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="values">写入值</param>
        private async void WriteFuncAsync(ushort address, ushort[] values)
        {
            try
            {
                await Task.Run(() => _master.WriteMultipleRegistersAsync(0, address, values));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取ushort格式数据
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <returns>读取值</returns>
        public ushort ReadU16(ushort address)
        {
            ushort value = BitConverter.ToUInt16(ReadFunc(address, 1), 0);
            return value;
        }

        /// <summary>
        /// 批量读取ushort格式数据
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="num">读取个数</param>
        /// <returns>读取值</returns>
        public ushort[] ReadU16Array(ushort address, ushort num)
        {
            var bytes = ReadFunc(address, num);
            List<ushort> values = new List<ushort>();
            for (int i = 0; i < bytes.Length/2; i++)
            {
                values.Add(BitConverter.ToUInt16(bytes, i * 2));
            }
            return values.ToArray();
        }

        /// <summary>
        /// 读取short格式数据
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <returns>读取值</returns>
        public short ReadS16(ushort address)
        {
            short value = BitConverter.ToInt16(ReadFunc(address, 1), 0);
            return value;
        }

        /// <summary>
        /// 读取int格式数据
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <returns>读取值</returns>
        public int ReadS32(ushort address)
        {
            int value = BitConverter.ToInt32(ReadFunc(address, 2), 0);
            return value;
        }

        /// <summary>
        /// 读取uint格式数据
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <returns>读取值</returns>
        public uint ReadU32(ushort address)
        {
            uint value = BitConverter.ToUInt32(ReadFunc(address, 2), 0);
            return value;
        }

        /// <summary>
        /// 读取string类型数据
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="num">读取位数</param>
        /// <returns>读取值</returns>
        public string ReadString(ushort address, ushort num)
        {
            string value = BitConverter.ToString(ReadFunc(address, num), 0);
            return value;
        }
    }
}
