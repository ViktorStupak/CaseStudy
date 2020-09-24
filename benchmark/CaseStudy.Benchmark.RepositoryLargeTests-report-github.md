# RepositoryLargeTests

* get data from the `CaseStudy.WebApi`'s repository. 
* Database has 10000 entities.

### Resources

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
AMD Ryzen 5 3600X, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT  [AttachedDebugger]
  Job-FHVDCZ : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

Runtime=.NET Core 3.1  LaunchCount=2  

```

### Legends
``` YML
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Min       : Minimum
  Max       : Maximum
  Median    : Value separating the higher half of all measurements (50th percentile)
  Gen 0     : GC Generation 0 collects per 1000 operations
  Gen 1     : GC Generation 1 collects per 1000 operations
  Gen 2     : GC Generation 2 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 ns      : 1 Nanosecond (0.000000001 sec)

```

### Result

|                   Method |            Mean |         Error |        StdDev |             Min |             Max |          Median |     Gen 0 |   Gen 1 | Gen 2 |  Allocated |
|------------------------- |----------------:|--------------:|--------------:|----------------:|----------------:|----------------:|----------:|--------:|------:|-----------:|
| GetPaginatedProductsTest |    219,721.5 ns |   6,814.50 ns |  10,406.46 ns |    204,848.0 ns |    232,715.7 ns |    217,300.0 ns |    3.6621 |  0.2441 |     - |    30643 B |
|          GetProductsTest | 21,489,612.9 ns | 283,967.80 ns | 416,236.32 ns | 20,851,390.6 ns | 22,377,281.2 ns | 21,553,090.6 ns | 1468.7500 | 62.5000 |     - | 12317581 B |
|           GetProductTest |        644.0 ns |       7.45 ns |      10.93 ns |        631.7 ns |        669.6 ns |        641.8 ns |    0.0429 |       - |     - |      360 B |
|        ProductExistsTest |    159,683.1 ns |   3,653.85 ns |   5,355.76 ns |    153,005.4 ns |    166,508.5 ns |    157,285.4 ns |    2.1973 |       - |     - |    19076 B |
