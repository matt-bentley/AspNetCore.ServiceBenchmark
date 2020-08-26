# AspNetCore.ServiceBenchmark
This repository provides benchmarks between gRPC and REST in ASP.NET Core 3.1. There are also benchmarks between different JSON serializers in .NET 3.1 and .NET 2.2.

### Caveats
These benchmarks were only run on 1 Windows Desktop on a single thread with no additional load on each ASP.NET server. gRPC works very well at low power, it is likely that it would further outperform REST if the servers were working at a higher load.

## Results

| Test Method Prefix       |     Server Configuration  |
|------------------------- |--------------------------|
| Grpc | ASP.NET Core 3.1 rRPC |
| Rest2.2Newtonsoft | ASP.NET Core 2.2 REST (Uses Newtonsoft by default) |
| Rest3.1S.T.J | ASP.NET Core 3.1 REST (Uses System.Text.Json by default) |
| Rest3.1Newtonsoft | ASP.NET Core 3.1 REST Configured to use Newtonsoft |

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.1016 (1903/May2019Update/19H1)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT

```
|                       Method |     Mean |    Error |   StdDev |
|----------------------------- |---------:|---------:|---------:|
|     GrpcGetSmallPayloadAsync | 166.4 ms |  1.77 ms |  1.74 ms |
|    Rest2.2NewtonsoftGetSmallPayloadAsync | 216.1 ms |  4.29 ms |  7.40 ms |
|    **Rest3.1S.T.JGetSmallPayloadAsync** | **155.0 ms** |  **2.87 ms** |  **4.29 ms** |
|    Rest3.1NewtonsoftGetSmallPayloadAsync | 156.6 ms |  2.09 ms |  1.96 ms |
|    **GrpcGetMediumPayloadAsync** | **109.8 ms** |  **0.82 ms** |  **0.73 ms** |
|   Rest2.2NewtonsoftGetMediumPayloadAsync | 490.2 ms |  1.24 ms |  1.16 ms |
|   Rest3.1S.T.JGetMediumPayloadAsync | 410.6 ms |  1.63 ms |  1.45 ms |
|   Rest3.1NewtonsoftGetMediumPayloadAsync | 1,389.0 ms | 22.65 ms | 17.68 ms |
|     **GrpcGetLargePayloadAsync** | **106.8 ms** |  **2.12 ms** |  **3.94 ms** |
|    Rest2.2NewtonsoftGetLargePayloadAsync | 452.4 ms |  1.18 ms |  1.04 ms |
|    Rest3.1S.T.JGetLargePayloadAsync | 383.3 ms |  2.28 ms |  2.02 ms |
|    Rest3.1NewtonsoftGetLargePayloadAsync | 1,333.1 ms | 25.22 ms | 25.90 ms |
|  **GrpcStreamLargePayloadAsync** | **651.9 ms** | **12.51 ms** | **13.38 ms** |
| Rest2.2NewtonsoftStreamLargePayloadAsync | 888.0 ms |  6.24 ms |  5.83 ms |
| Rest3.1S.T.JStreamLargePayloadAsync | 736.8 ms |  2.28 ms |  1.90 ms |
| Rest3.1NewtonsoftStreamLargePayloadAsync | 2,781.9 ms | 55.49 ms | 117.05 ms |

## Analysis

When using small payloads REST was slightly quicker however it is likely that this would change when the servers are under higher load as gRPC is designed to use less CPU and Memory.

At larger payloads gRPC was much faster than REST and it was also quicker at streaming large datasets.

The performance of ASP.NET Core 3.1 when configured to use Newtonsoft was very poor and actually a lot worse than ASP.NET Core 2.2.

Memory and CPU were not measured here however it is likely that the gRPC server would be using much less CPU and Memory due to using protobuf in transit which is encrypted and compressed. The memory usage of .NET Core 3.1 when using System.Text.Json will also be much lower than 3.1 or 2.2 using Newtonsoft; this is because System.Text.Json uses UTF8 instead of UTF16 and it also uses Span<T> to do string manipulation without memory allocations.