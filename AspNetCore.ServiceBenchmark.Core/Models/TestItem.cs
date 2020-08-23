using System;

namespace AspNetCore.ServiceBenchmark.Core.Models
{
    public class TestItem
    {
        public TestItem(int index)
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
            Index = index;
        }

        public Guid Id { get; set; }
        public int Index { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
