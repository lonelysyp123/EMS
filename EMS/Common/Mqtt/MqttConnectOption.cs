using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.Mqtt
{
    public class MqttConnectOption
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string UseName { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
    }
}
