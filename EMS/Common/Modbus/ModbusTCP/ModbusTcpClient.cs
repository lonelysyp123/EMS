using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;

namespace EMS.Common.Modbus.ModbusTCP
{
    public class ModbusTcpClient : IModbus
    {
        public string IP { get; set; }
        public int Port { get; set; }
        private TcpClient client;
        private ModbusIpMaster master;

        public bool Connect()
        {
            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse(IP), Port);
                master = ModbusIpMaster.CreateIp(client);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool ReadRegister(ref byte[] ret, Address address, int offset, int length)
        {
            ushort[] holding_register = master.ReadHoldingRegisters(address, num);
            byte[] bytes = new byte[holding_register.Length * 2];
            for (int i = 0; i < holding_register.Length; i++)
            {
                var objs = BitConverter.GetBytes(holding_register[i]);
                bytes[i * 2] = objs[0];
                bytes[i * 2 + 1] = objs[1];
            }
            return bytes;
        }

        public bool WriteRegister(ref byte[] ret, Address address, int offset, byte[] value)
        {
            throw new NotImplementedException();
        }
    }
}
