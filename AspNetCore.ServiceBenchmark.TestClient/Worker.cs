using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.ServiceBenchmark.Grpc.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore.ServiceBenchmark.TestClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration Configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            // The port number(5001) must match the port of the gRPC server.
            try
            {
                await TestLoop(1000);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
                
            Console.WriteLine("Press any key to exit...");
        }

        private async Task TestLoop(int count)
        {
            using (var channel = GrpcChannel.ForAddress(Configuration["Service:Url"]))
            {
                var client = new TestDataService.TestDataServiceClient(channel);

                var sw = new Stopwatch();
                sw.Start();

                for(int i = 0; i < count; i++)
                {
                    var reply = await client.GetByIndexAsync(new IndexMessage { Index = i });
                    if (i % 100 == 0)
                    {
                        _logger.LogInformation($"Processed {i}/{count}");
                    }
                }
                sw.Stop();

                _logger.LogInformation($"Processed {count}/{count} in {sw.ElapsedMilliseconds}ms");
            }
        }
    }
}
