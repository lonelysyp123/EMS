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

        public ModbusClient(string IP, int Port)
        {
            _ip = IP;
            _port = Port;
            _RtuOrTcp = false;
        }

        public ModbusClient(string Port, int Rate, int Parity, int Databits, int Stopbits)
        {
            _serialport = Port;
            _rate = Rate;
            _parity = Parity;
            _databits = Databits;
            _stopbits = Stopbits;
            _RtuOrTcp = true;
        }

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

        private byte[] ReadFunc(ushort address, ushort num)
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

        private void WriteFunc(ushort address, ushort value)
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

        public ushort ReadU16(ushort address)
        {
            ushort value = BitConverter.ToUInt16(ReadFunc(address, 1), 0);
            return value;
        }

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

        public short ReadS16(ushort address)
        {
            short value = BitConverter.ToInt16(ReadFunc(address, 1), 0);
            return value;
        }

        public int ReadS32(ushort address)
        {
            int value = BitConverter.ToInt32(ReadFunc(address, 2), 0);
            return value;
        }

        public uint ReadU32(ushort address)
        {
            uint value = BitConverter.ToUInt32(ReadFunc(address, 2), 0);
            return value;
        }

        public string ReadString(ushort address, ushort num)
        {
            string value = BitConverter.ToString(ReadFunc(address, num), 0);
            return value;
        }
    }
}
