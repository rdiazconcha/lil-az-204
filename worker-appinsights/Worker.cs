using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace worker_appinsights
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private TelemetryClient telemetryClient;
        private static HttpClient httpClient = new HttpClient();

        public Worker(ILogger<Worker> logger, TelemetryClient telemetryClient)
        {
            this.logger = logger;
            this.telemetryClient = telemetryClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using (telemetryClient.StartOperation<RequestTelemetry>("operation"))
                {
                    logger.LogWarning("A sample warning message. By default, logs with severity Warning or higher is captured by Application Insights");
                    logger.LogInformation("Calling rdiazconcha.com");
                    var response = await httpClient.GetAsync("https://rdiazconcha.com");
                    logger.LogInformation("Calling rdiazconcha.com completed with status:" + response.StatusCode);
                    telemetryClient.TrackEvent("rdiazconcha.com call event completed");
                }

                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
