using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AspNetCore.ServiceBenchmark.TestClient.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore.ServiceBenchmark.TestClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration Configuration;
        private readonly ITestService _testService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ITestService testService)
        {
            _logger = logger;
            Configuration = configuration;
            _testService = testService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting test execution");

            try
            {
                await TestLoop(100000);
                //await TestBatchLoop(10000, 1000);
                //await TestStream(100000);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
        }

        private async Task TestLoop(int count)
        {
            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < count; i++)
            {
                var reply = await _testService.GetAsync(i);
                if(reply != i)
                {
                    _logger.LogError($"The response index {reply} does not match the provided index {i}");
                }
                if (i % 1000 == 0)
                {
                    _logger.LogInformation($"Processed {i}/{count}");
                }
            }
            sw.Stop();

            _logger.LogInformation($"Processed {count}/{count} in {sw.ElapsedMilliseconds}ms");
        }

        private async Task TestBatchLoop(int count, int batchSize)
        {
            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < count; i++)
            {
                var reply = await _testService.GetRangeAsync(batchSize);
                if (reply != batchSize)
                {
                    _logger.LogError($"The response size {reply} does not match the provided range {i}");
                }
                if (i % 1000 == 0)
                {
                    _logger.LogInformation($"Processed {i}/{count}");
                }
            }
            sw.Stop();

            _logger.LogInformation($"Processed {count}/{count} in {sw.ElapsedMilliseconds}ms");
        }

        private async Task TestStream(int count)
        {
            var sw = new Stopwatch();
            sw.Start();

            var reply = await _testService.GetStreamAsync(count);
            if (reply != count)
            {
                _logger.LogError($"The response size {reply} does not match the provided stream count {count}");
            }

            sw.Stop();

            _logger.LogInformation($"Streamed {count} in {sw.ElapsedMilliseconds}ms");
        }
    }
}
