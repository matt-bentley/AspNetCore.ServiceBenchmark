using AspNetCore.ServiceBenchmark.Grpc.Services;
using AspNetCore.ServiceBenchmark.TestClient.Interfaces.Services;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AspNetCore.ServiceBenchmark.TestClient.Services
{
    public class GrpcTestService : ITestService
    {
        private readonly TestDataService.TestDataServiceClient _client;

        public GrpcTestService(TestDataService.TestDataServiceClient client)
        {
            _client = client;
        }

        public async Task<int> GetAsync(int index)
        {
            var reply = await _client.GetByIndexAsync(new IndexMessage { Index = index });
            return reply.Index;
        }

        public async Task<int> GetRangeAsync(int count)
        {
            var packet = await _client.GetRangeAsync(new IndexMessage { Index = count });
            return packet.Items.Count;
        }

        public async Task<int> GetStreamAsync(int count)
        {
            int idx = 0;
            using (var streamingCall = _client.GetStream(new IndexMessage { Index = count }))
            {
                await foreach(var item in streamingCall.ResponseStream.ReadAllAsync())
                {
                    if(item.Index > -1)
                    {
                        idx++;
                    }
                }
            }
            return idx;
        }
    }
}
