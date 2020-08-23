using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCore.ServiceBenchmark.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore.ServiceBenchmark.Rest._3._1.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IEnumerable<TestItem> GetRange(int count)
        {
            return Enumerable.Range(1, count).Select(index => new TestItem(index));
        }
    }
}
