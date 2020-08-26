using AspNetCore.ServiceBenchmark.Grpc.Services;
using AspNetCore.ServiceBenchmark.TestClient.Services;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Validators;
using Grpc.Net.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore.ServiceBenchmark.Benchmark
{
    [AsciiDocExporter]
    [CsvExporter]
    [HtmlExporter]
    public class BenchmarkHarness
    {
        private readonly GrpcTestService _grpcService;
        private readonly RestTestService _rest2Service;
        private readonly RestTestService _rest3Service;

        public BenchmarkHarness()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001/");
            var grpcClient = new TestDataService.TestDataServiceClient(channel);
            _grpcService = new GrpcTestService(grpcClient);

            var httpClient2 = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7001/")
            };
            _rest2Service = new RestTestService(httpClient2);

            var httpClient3 = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:6001/")
            };
            _rest3Service = new RestTestService(httpClient3);
        }

        [Benchmark]
        public async Task GrpcGetSmallPayloadAsync()
        {
            for(int i = 0; i < 1000; i++)
            {
                await _grpcService.GetAsync(1);
            }
        }

        [Benchmark]
        public async Task Rest2GetSmallPayloadAsync()
        {
            for (int i = 0; i < 1000; i++)
            {
                await _rest2Service.GetAsync(1);
            }
        }

        [Benchmark]
        public async Task Rest3GetSmallPayloadAsync()
        {
            for (int i = 0; i < 1000; i++)
            {
                await _rest3Service.GetAsync(1);
            }
        }

        [Benchmark]
        public async Task GrpcGetMediumPayloadAsync()
        {
            for (int i = 0; i < 100; i++)
            {
                await _grpcService.GetRangeAsync(1000);
            }
        }

        [Benchmark]
        public async Task Rest2GetMediumPayloadAsync()
        {
            for (int i = 0; i < 100; i++)
            {
                await _rest2Service.GetRangeAsync(1000);
            }
        }

        [Benchmark]
        public async Task Rest3GetMediumPayloadAsync()
        {
            for (int i = 0; i < 100; i++)
            {
                await _rest3Service.GetRangeAsync(1000);
            }
        }

        [Benchmark]
        public async Task GrpcGetLargePayloadAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                await _grpcService.GetRangeAsync(10000);
            }
        }

        [Benchmark]
        public async Task Rest2GetLargePayloadAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                await _rest2Service.GetRangeAsync(10000);
            }
        }

        [Benchmark]
        public async Task Rest3GetLargePayloadAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                await _rest3Service.GetRangeAsync(10000);
            }
        }

        [Benchmark]
        public async Task GrpcStreamLargePayloadAsync()
        {
            await _grpcService.GetStreamAsync(200000);
        }

        [Benchmark]
        public async Task Rest2StreamLargePayloadAsync()
        {
            await _rest2Service.GetStreamAsync(200000);
        }

        [Benchmark]
        public async Task Rest3StreamLargePayloadAsync()
        {
            await _rest3Service.GetStreamAsync(200000);
        }
    }

    public class AllowNonOptimized : ManualConfig
    {
        public AllowNonOptimized()
        {
            AddValidator(JitOptimizationsValidator.DontFailOnError);

            AddLogger(DefaultConfig.Instance.GetLoggers().ToArray());
            AddExporter(DefaultConfig.Instance.GetExporters().ToArray());
            AddColumnProvider(DefaultConfig.Instance.GetColumnProviders().ToArray());
        }
    }
}
