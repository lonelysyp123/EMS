using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.Mqtt
{
    public class MqttClientService
    {
        public static IMqttClient _mqttClient;

        /// <summary>
        /// 创建MqttClient实例并连接服务器
        /// </summary>
        /// <param name="Option">配置类</param>
        /// <param name="MqttClient_ConnectedAsync">客户端连接成功事件</param>
        /// <param name="MqttClient_DisconnectedAsync">客户端连接关闭事件</param>
        /// <param name="MqttClient_MessageReceivedAsync">收到消息事件</param>
        public void MqttClientStart(MqttConnectOption Option, 
            Func<MqttClientConnectedEventArgs, Task> MqttClient_ConnectedAsync=null, 
            Func<MqttClientDisconnectedEventArgs, Task> MqttClient_DisconnectedAsync = null,
            Func<MqttApplicationMessageReceivedEventArgs, Task> MqttClient_MessageReceivedAsync = null)
        {
            var optionsBuilder = new MqttClientOptionsBuilder()
                .WithTcpServer(Option.Address, Option.Port) // 要访问的mqtt服务端的 ip 和 端口号
                .WithCredentials(Option.UseName, Option.Password) // 要访问的mqtt服务端的用户名和密码
                .WithClientId(Option.ClientId) // 设置客户端id
                .WithCleanSession()
                .WithTls(new MqttClientOptionsBuilderTlsParameters
                {
                    UseTls = false  // 是否使用 tls加密
                });

            var clientOptions = optionsBuilder.Build();
            _mqttClient = new MqttFactory().CreateMqttClient();
            _mqttClient.ConnectedAsync += MqttClient_ConnectedAsync; // 客户端连接成功事件
            _mqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync; // 客户端连接关闭事件
            _mqttClient.ApplicationMessageReceivedAsync += MqttClient_MessageReceivedAsync; // 收到消息事件
            _mqttClient.ConnectAsync(clientOptions);
        }

        /// <summary>
        /// 客户端连接关闭事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine($"客户端已断开与服务端的连接……");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine($"客户端已连接服务端……");

            // 订阅消息主题
            // MqttQualityOfServiceLevel: （QoS）:  0 最多一次，接收者不确认收到消息，并且消息不被发送者存储和重新发送提供与底层 TCP 协议相同的保证。
            // 1: 保证一条消息至少有一次会传递给接收方。发送方存储消息，直到它从接收方收到确认收到消息的数据包。一条消息可以多次发送或传递。
            // 2: 保证每条消息仅由预期的收件人接收一次。级别2是最安全和最慢的服务质量级别，保证由发送方和接收方之间的至少两个请求/响应（四次握手）。
            _mqttClient.SubscribeAsync("topic1", MqttQualityOfServiceLevel.AtLeastOnce); //topic_02

            return Task.CompletedTask;
        }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task _mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine($"ApplicationMessageReceivedAsync：客户端ID=【{arg.ClientId}】接收到消息。 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 发布信息
        /// </summary>
        /// <param name="Topic">主题</param>
        /// <param name="Msg">信息</param>
        public void Publish(string Topic, string Msg)
        {
            var message = new MqttApplicationMessage
            {
                Topic = Topic,
                Payload = Encoding.Default.GetBytes(Msg),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = true  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
            };
            _mqttClient.PublishAsync(message);
        }
    }
}
