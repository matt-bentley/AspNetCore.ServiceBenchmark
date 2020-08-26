using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ServiceBenchmark.Grpc.Factories;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace AspNetCore.ServiceBenchmark.Grpc.Services
{
    public class TestService : TestDataService.TestDataServiceBase
    {
        private readonly ILogger<TestService> _logger;
        private readonly TestItemMessageFactory _factory;

        public TestService(ILogger<TestService> logger)
        {
            _logger = logger;
            _factory = new TestItemMessageFactory();
        }

        public override Task<TestItemMessage> GetByIndex(IndexMessage request, ServerCallContext context)
        {
            return Task.FromResult(_factory.Generate(request.Index));
        }

        public override Task<TestItemPacket> GetRange(IndexMessage request, ServerCallContext context)
        {
            var items = Enumerable.Range(1, request.Index).Select(i => _factory.Generate(i));
            var packet = new TestItemPacket();
            packet.Items.AddRange(items);
            return Task.FromResult(packet);
        }

        public override async Task GetStream(IndexMessage request, IServerStreamWriter<TestItemMessage> responseStream, ServerCallContext context)
        {
            for(int i = 0; i < request.Index; i++)
            {
                await responseStream.WriteAsync(_factory.Generate(i));
            }
        }
    }
}
