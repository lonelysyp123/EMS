using EMS.Common.Modbus.ModbusTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common
{
    public class EMSCommunication : ModbusClient
    {
        public EMSCommunication(string IP, int Port) : base(IP, Port)
        {

        }

        public EMSCommunication(string Port, int Rate, int Parity, int Databits, int Stopbits) : base(Port, Rate, Parity, Databits, Stopbits)
        {

        }
    }
}
