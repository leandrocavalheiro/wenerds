using RabbitMQ.Stream.Client.Reliable;
using WeNerds.Commons;
using WeNerds.WeRabbitMQStreams.Extensions;
using WeNerds.WeRabbitMQStreams.Interfaces;

namespace WeNerds.Test
{
    public class Worker : BackgroundService
    {
        private IWeRabbitMQStream _weRabbitMQStream;
        public Worker(IWeRabbitMQStream weRabbitMQStream)
        {
            _weRabbitMQStream = weRabbitMQStream;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var addressesList = GetAddresses();

            ICollection<Producer> producerList = new List<Producer>();
            foreach (var address in addressesList)             
                producerList.Add(await _weRabbitMQStream.CreateNewProducerAsync($"address_{address}"));
            
            
            

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var producer in producerList)
                {
                    var result = await producer.PublishAsync("{message:helloworld}");
                    Console.WriteLine($"{WeMethods.Now()}||Stream {producer.Info.Stream}||SucessSend: {result != null}||MessageId:{result}");
                }

                await Task.Delay(1000);
            }            
        }


        private ICollection<Guid> GetAddresses()
        => new List<Guid>()
            {
                new Guid("a7bf659a-26f9-47eb-8833-1fb2e913c979"),
                new Guid("f12a8d4f-aed3-491a-8bb3-938fdee036e6"),
                new Guid("7c2cbaa8-45e4-46df-adad-78fba10f88bf"),
                new Guid("8b67490f-36ce-49f5-a376-9a3c80550f4f"),
                new Guid("3e6c2a97-ee2b-47f5-9e9a-1c2b410d65e3"),
            };
        

    }
}