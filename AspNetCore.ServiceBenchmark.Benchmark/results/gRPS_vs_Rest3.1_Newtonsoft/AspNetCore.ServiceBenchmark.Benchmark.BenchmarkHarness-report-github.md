``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.1016 (1903/May2019Update/19H1)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                      Method |       Mean |    Error |    StdDev |
|---------------------------- |-----------:|---------:|----------:|
|    GrpcGetSmallPayloadAsync |   168.3 ms |  3.25 ms |   6.03 ms |
|    RestGetSmallPayloadAsync |   156.6 ms |  2.09 ms |   1.96 ms |
|   GrpcGetMediumPayloadAsync |   107.7 ms |  0.79 ms |   0.74 ms |
|   RestGetMediumPayloadAsync | 1,389.0 ms | 22.65 ms |  17.68 ms |
|    GrpcGetLargePayloadAsync |   107.0 ms |  2.14 ms |   2.54 ms |
|    RestGetLargePayloadAsync | 1,333.1 ms | 25.22 ms |  25.90 ms |
| GrpcStreamLargePayloadAsync |   626.5 ms |  9.04 ms |   8.46 ms |
| RestStreamLargePayloadAsync | 2,781.9 ms | 55.49 ms | 117.05 ms |
