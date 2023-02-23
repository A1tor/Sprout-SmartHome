using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sprout.Sevices
{
    public class MQTTService
    {
        private static MqttFactory mqttFactory = new MqttFactory();
        public static IMqttClient mqttClient = mqttFactory.CreateMqttClient();
        public static async Task ConnectToBroker()
        {
            MqttClientOptions options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("mqtt.by", 1883)
                .WithCredentials("Aitor", "4an5wrlg")
                .Build();
            await mqttClient.ConnectAsync(options);
            await Subscribe();
        }
        public static async Task Disconnect()
        {
            await mqttClient.DisconnectAsync(new MqttClientDisconnectOptionsBuilder()
                .WithReason(MqttClientDisconnectReason.NormalDisconnection).Build());
        }
        public static async Task Publish(string messagePayload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("user/Aitor/shub0")
                .WithPayload(messagePayload)
                .Build();
            if (mqttClient.IsConnected)
                await mqttClient.PublishAsync(message);
        }
        public static async Task Subscribe()
        {
            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                return Task.CompletedTask;
            };

            MqttClientSubscribeOptions soptions = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter("user/Aitor/shub0")
                .Build();
            await mqttClient.SubscribeAsync(soptions);
        }
    }
}
