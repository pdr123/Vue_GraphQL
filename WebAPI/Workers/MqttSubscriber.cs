
using HotChocolate.Subscriptions;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System.Text;

namespace WebAPI.Workers
{
    public class MqttSubscriber : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var mqttFactory = new MqttFactory();

                using (var mqttClient = mqttFactory.CreateMqttClient())
                {
                    // MQTT broker connection settings
                    var options = new MqttClientOptionsBuilder()
                    .WithTcpServer("localhost", 1883) // Replace with your MQTT broker's address and port
                    .Build();

                    mqttClient.ApplicationMessageReceivedAsync += e =>
                    {
                        Console.WriteLine($"Received application message. {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");

                        return Task.CompletedTask;
                    };

                    await mqttClient.ConnectAsync(options);

                    var topic = "test/topic";

                    var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                        .WithTopicFilter(
                            f =>
                            {
                                f.WithTopic(topic);
                            })
                        .Build();

                    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                    Console.WriteLine("MQTT client subscribed to topic.");

                    Console.WriteLine("Press enter to exit.");
                    Console.ReadLine();

                    await mqttClient.UnsubscribeAsync(topic);
                    await mqttClient.DisconnectAsync();

                }


        
            } catch (Exception ex) 
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
