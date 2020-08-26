using System.Threading.Tasks;

namespace AspNetCore.ServiceBenchmark.TestClient.Interfaces.Services
{
    public interface ITestService
    {
        Task<int> GetAsync(int index);
        Task<int> GetRangeAsync(int count);
        Task<int> GetStreamAsync(int count);
    }
}
