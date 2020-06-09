using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Producer;

namespace eventhubstest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Endpoint=sb://cursoaz204.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=pXKOF2G02O3ZWteoN0GZPEgVS8TaTbL7jWDqbeDK1CE=;EntityPath=curso-az204";
            var eventHubName = "curso-az204";

            var producerClient = new EventHubProducerClient(connectionString, eventHubName);
            
            for (int i = 0; i <= 100; i++)
            {
                var batch = await producerClient.CreateBatchAsync();
                batch.TryAdd(new Azure.Messaging.EventHubs.EventData(Encoding.UTF8.GetBytes($"Curso AZ-204")));
                batch.TryAdd(new Azure.Messaging.EventHubs.EventData(Encoding.UTF8.GetBytes("¡No olvides estudiar!")));
                await producerClient.SendAsync(batch);
                System.Console.WriteLine($"Eventos enviados: {i+1}");
            }
        }
    }
}