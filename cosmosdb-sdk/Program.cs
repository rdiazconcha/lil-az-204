using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure.Cosmos;

namespace cosmosdb_sdk
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://curso-az204-cosmosdb.documents.azure.com:443/" ;
            var key = "0fI65tcMJAeABhbtEEFJ2BqRQcLyqsmT6aW8qeRSPAqwjjRqzROBrOUO6mqTtVyAh8HpZplLP39MGCufdOm0cA==";
            var cosmosClient = new CosmosClient(url, key);
            var db = await cosmosClient.CreateDatabaseIfNotExistsAsync("curso-az204");
            System.Console.WriteLine("Base de datos creada: " + db.Database.Id);
            var container = await db.Database.CreateContainerIfNotExistsAsync("temas", "/ModuloId");
            System.Console.WriteLine("Contenedor creado: " + container.Container.Id);
            var tema = new Tema(){
                Id = Guid.NewGuid().ToString(),
                ModuloId = 5,
                Nombre = "SDK de Azure Cosmos DB"
            };
            await container.Container.UpsertItemAsync<Tema>(tema, new PartitionKey(tema.ModuloId));
        }
    }

    class Tema
    {
        [JsonPropertyName("id")]
        public string  Id { get; set; }
        public int ModuloId { get; set; } 
        public string Nombre { get; set; }
    }
}
