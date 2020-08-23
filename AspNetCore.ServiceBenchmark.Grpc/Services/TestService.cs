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
    }
}
