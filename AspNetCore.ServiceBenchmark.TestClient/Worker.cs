using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.ServiceBenchmark.Grpc.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore.ServiceBenchmark.TestClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new TestDataService.TestDataServiceClient(channel);
            var reply = await client.GetByIndexAsync(
                              new IndexMessage { Index = 1 });
            Console.WriteLine("Generated: " + reply.Id);
            Console.WriteLine("Press any key to exit...");
        }
    }
}
