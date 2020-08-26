using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.ServiceBenchmark.Grpc.Services;
using AspNetCore.ServiceBenchmark.TestClient.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore.ServiceBenchmark.LoadSimulator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly GrpcTestService _grpcService;
        private readonly RestTestService _restService;
        private readonly int _concurrency;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            var channel = GrpcChannel.ForAddress("https://localhost:5001/");
            var grpcClient = new TestDataService.TestDataServiceClient(channel);
            _grpcService = new GrpcTestService(grpcClient);

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:6001/")
            };
            _restService = new RestTestService(httpClient);
            _concurrency = configuration.GetValue<int>("Concurrency");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation($"Simulating load with concurrency: {_concurrency}");
            while (!stoppingToken.IsCancellationRequested)
            {
                var tasks = new List<Task>();

                for(int i = 0; i < _concurrency; i++)
                {
                    tasks.Add(_grpcService.GetRangeAsync(100));
                    tasks.Add(_restService.GetRangeAsync(100));
                }

                await Task.WhenAll(tasks);
            }
        }
    }
}
