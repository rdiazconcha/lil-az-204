using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppConfigTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();
                        config.AddAzureAppConfiguration(options =>{
                            options.Connect(settings["ConnectionStrings:AppConfig"])
                                .ConfigureRefresh(refresh =>{
                                    refresh.Register("TestApp:Settings:ConfigVersion", refreshAll: true)
                                        .SetCacheExpiration(TimeSpan.FromSeconds(10));
                                })
                                .ConfigureKeyVault( kv => {
                                    kv.SetCredential(new DefaultAzureCredential());
                                });
                        });
                    })
                .UseStartup<Startup>());
    }
}