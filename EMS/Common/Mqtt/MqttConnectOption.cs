using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.Mqtt
{
    public class MqttConnectOption
    {
        /// <summary>
        /// ip地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UseName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get; set; }
    }
}
