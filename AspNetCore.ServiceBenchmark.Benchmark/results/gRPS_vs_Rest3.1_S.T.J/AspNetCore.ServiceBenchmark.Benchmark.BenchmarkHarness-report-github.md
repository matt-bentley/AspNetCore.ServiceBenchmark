``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.1016 (1903/May2019Update/19H1)
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                      Method |     Mean |   Error |  StdDev |
|---------------------------- |---------:|--------:|--------:|
|    GrpcGetSmallPayloadAsync | 168.1 ms | 1.39 ms | 1.71 ms |
|    RestGetSmallPayloadAsync | 159.6 ms | 3.18 ms | 5.96 ms |
|   GrpcGetMediumPayloadAsync | 110.7 ms | 0.53 ms | 0.58 ms |
|   RestGetMediumPayloadAsync | 408.6 ms | 0.67 ms | 0.56 ms |
|    GrpcGetLargePayloadAsync | 107.5 ms | 2.14 ms | 3.27 ms |
|    RestGetLargePayloadAsync | 401.0 ms | 1.84 ms | 1.72 ms |
| GrpcStreamLargePayloadAsync | 630.9 ms | 5.42 ms | 5.07 ms |
| RestStreamLargePayloadAsync | 729.0 ms | 6.41 ms | 6.00 ms |
