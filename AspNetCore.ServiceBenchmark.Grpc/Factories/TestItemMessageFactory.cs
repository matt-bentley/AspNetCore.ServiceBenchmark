using AspNetCore.ServiceBenchmark.Grpc.Services;
using Google.Protobuf.WellKnownTypes;
using System;

namespace AspNetCore.ServiceBenchmark.Grpc.Factories
{
    public class TestItemMessageFactory
    {
        public TestItemMessage Generate(int index)
        {
            return new TestItemMessage
            {
                Id = Guid.NewGuid().ToString(),
                Index = index,
                CreatedDate = Timestamp.FromDateTime(DateTime.UtcNow)
            };
        }
    }
}
