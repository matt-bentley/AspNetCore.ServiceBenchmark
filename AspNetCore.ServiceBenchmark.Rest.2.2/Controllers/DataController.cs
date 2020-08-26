using System.Collections.Generic;
using System.Linq;
using AspNetCore.ServiceBenchmark.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore.ServiceBenchmark.Rest._2._2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TestItem> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new TestItem(index));
        }

        [HttpGet("{index}")]
        public TestItem GetByIndex(int index)
        {
            return new TestItem(index);
        }

        [HttpGet("range/{count}")]
        public List<TestItem> GetRange(int count)
        {
            return Enumerable.Range(1, count).Select(index => new TestItem(index)).ToList();
        }

        [HttpGet("stream/{count}")]
        public IEnumerable<TestItem> GetStream(int count)
        {
            return Enumerable.Range(1, count).Select(index => new TestItem(index));
        }
    }
}
