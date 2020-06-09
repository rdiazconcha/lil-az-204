using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace rediscache
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "curso-az204.redis.cache.windows.net:6380,password=jniasXA+LarhTp5X5Z4NiPiJEIPz4i09cHuZJ6OUhlk=,ssl=True,abortConnect=False";
            var connection = await ConnectionMultiplexer.ConnectAsync(connectionString);
            var database = connection.GetDatabase();
            await database.StringSetAsync("nombre", "Rodrigo");
            var nombre = await database.StringGetAsync("nombre");
            System.Console.WriteLine(nombre.ToString());
        }
    }
}