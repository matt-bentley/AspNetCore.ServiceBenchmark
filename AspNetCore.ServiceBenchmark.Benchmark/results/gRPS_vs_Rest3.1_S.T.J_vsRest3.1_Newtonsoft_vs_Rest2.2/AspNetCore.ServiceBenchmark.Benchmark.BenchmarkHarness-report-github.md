``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.1016 (1903/May2019Update/19H1)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                       Method |     Mean |    Error |   StdDev |   Median |
|----------------------------- |---------:|---------:|---------:|---------:|
|     GrpcGetSmallPayloadAsync | 166.4 ms |  1.77 ms |  1.74 ms | 165.9 ms |
|    Rest2GetSmallPayloadAsync | 216.1 ms |  4.29 ms |  7.40 ms | 217.1 ms |
|    Rest3GetSmallPayloadAsync | 155.0 ms |  2.87 ms |  4.29 ms | 154.3 ms |
|    GrpcGetMediumPayloadAsync | 109.8 ms |  0.82 ms |  0.73 ms | 109.8 ms |
|   Rest2GetMediumPayloadAsync | 490.2 ms |  1.24 ms |  1.16 ms | 490.4 ms |
|   Rest3GetMediumPayloadAsync | 410.6 ms |  1.63 ms |  1.45 ms | 410.1 ms |
|     GrpcGetLargePayloadAsync | 106.8 ms |  2.12 ms |  3.94 ms | 105.0 ms |
|    Rest2GetLargePayloadAsync | 452.4 ms |  1.18 ms |  1.04 ms | 452.1 ms |
|    Rest3GetLargePayloadAsync | 383.3 ms |  2.28 ms |  2.02 ms | 383.7 ms |
|  GrpcStreamLargePayloadAsync | 651.9 ms | 12.51 ms | 13.38 ms | 649.0 ms |
| Rest2StreamLargePayloadAsync | 888.0 ms |  6.24 ms |  5.83 ms | 889.4 ms |
| Rest3StreamLargePayloadAsync | 736.8 ms |  2.28 ms |  1.90 ms | 737.0 ms |
