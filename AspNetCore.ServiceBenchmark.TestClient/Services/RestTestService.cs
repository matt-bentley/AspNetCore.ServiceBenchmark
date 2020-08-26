using AspNetCore.ServiceBenchmark.Core.Models;
using AspNetCore.ServiceBenchmark.TestClient.Interfaces.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore.ServiceBenchmark.TestClient.Services
{
    public class RestTestService : ITestService
    {
        private readonly HttpClient _client;

        public RestTestService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetAsync(int index)
        {
            var response = await _client.GetAsync($"data/{index}");
            var testItem = JsonConvert.DeserializeObject<TestItem>(await response.Content.ReadAsStringAsync());
            return testItem.Index;
        }

        public async Task<int> GetRangeAsync(int count)
        {
            var response = await _client.GetAsync($"data/range/{count}");
            var testItems = JsonConvert.DeserializeObject<List<TestItem>>(await response.Content.ReadAsStringAsync());
            return testItems.Count;
        }

        public async Task<int> GetStreamAsync(int count)
        {
            int idx = 0;
            var response = await _client.GetAsync($"data/stream/{count}");
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                try
                {
                    var serializer = new JsonSerializer();

                    using (StreamReader sr = new StreamReader(stream))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType != JsonToken.StartArray && reader.TokenType != JsonToken.EndArray)
                            {
                                var item = serializer.Deserialize<TestItem>(reader);
                                if (item.Index > -1)
                                {
                                    idx++;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    stream.Dispose();
                }
            }
            return idx;
        }
    }
}
