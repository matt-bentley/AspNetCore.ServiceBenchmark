using BenchmarkDotNet.Running;
using System;

namespace AspNetCore.ServiceBenchmark.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting benchmarks...");
            BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
