using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.AMQP;
using RabbitMQ.Stream.Client.Reliable;
using System.Text;
using WeNerds.Commons.Extensions;

namespace WeNerds.WeRabbitMQStreams.Extensions
{
    public static class WeRabbitMQStreamsExtensions
    {
        public static async Task<Guid?> PublishAsync(this Producer value, string message)
        {
            try
            {
                var newMessage = new Message(Encoding.Default.GetBytes($"{message.Serialize()}"))
                {
                    Properties = new Properties() { MessageId = $"{Guid.NewGuid()}" }
                };

                await value.Send(newMessage);
                return newMessage.Properties.MessageId.ToString().ToGuid();
            }
            catch
            {
                return null;                
            }
        }
    }
}
