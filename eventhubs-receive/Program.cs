using System;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using System.Text;

namespace eventhubs_receive
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var eventHubConnectionString = "Endpoint=sb://cursoaz204.servicebus.windows.net/;SharedAccessKeyName=receive;SharedAccessKey=EYXBssyaDJtp6sktCY+GAO2DcCiJhTymcSwPN58ByAc=;EntityPath=curso-az204";
            var eventHubName = "curso-az204";
            var blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=cursoaz204storageaccount;AccountKey=/4aRjlAKzLAEYIHGVBYPc2k5CrrvAdCw6Q8d+CyePg8CrG3NXVs0MRkb35TGpPeagpC17431ceOklB97W1Lp1A==;EndpointSuffix=core.windows.net";
            var blobStorageContainerName = "eventhub-checkpoints";

            var blobContainerClient = new BlobContainerClient(blobStorageConnectionString, blobStorageContainerName);
            var eventProcessorClient = new EventProcessorClient(blobContainerClient, 
                        EventHubConsumerClient.DefaultConsumerGroupName,
                        eventHubConnectionString, eventHubName);

            eventProcessorClient.ProcessEventAsync += async (a) => {
                    Console.WriteLine("Recibido: {0}", Encoding.UTF8.GetString(a.Data.Body.ToArray()));
                    await a.UpdateCheckpointAsync(a.CancellationToken);
            };
            eventProcessorClient.ProcessErrorAsync += (a) => {
                Console.WriteLine(a.Exception.Message);
                return Task.CompletedTask;
            };

            await eventProcessorClient.StartProcessingAsync();
            Console.ReadLine();
            Console.WriteLine("Fin");
            await eventProcessorClient.StopProcessingAsync();
        }
    }
}