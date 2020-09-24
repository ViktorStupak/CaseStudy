# RestFullCreateTest

* create Rest client - [RestSharp](https://restsharp.dev/)
* Sync call to `CaseStudy.WebApi` 
  * post 1 new product.
  * put change to exist product.
  * put new description to to exist product.
* check if response `IsSuccessful`
* Database has 100 entities.

> For run this one test make sure that start CaseStudy.WebApi

### Resources

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
AMD Ryzen 5 3600X, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT  [AttachedDebugger]
  Job-UJRMIJ : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

Runtime=.NET Core 3.1  IterationCount=10  LaunchCount=1  
WarmupCount=5  

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
  1 ms      : 1 Millisecond (0.001 sec)


```

### Result
|                Method |     Mean |    Error |   StdDev |      Min |      Max |   Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------- |---------:|---------:|---------:|---------:|---------:|---------:|------:|------:|------:|----------:|
|           PostProduct | 20.24 ms | 2.609 ms | 1.552 ms | 18.50 ms | 23.35 ms | 20.10 ms |     - |     - |     - |  68.11 KB |
|            PutProduct | 16.92 ms | 0.738 ms | 0.488 ms | 16.07 ms | 17.49 ms | 17.12 ms |     - |     - |     - |  60.47 KB |
| PutProductDescription | 18.41 ms | 0.633 ms | 0.419 ms | 17.68 ms | 19.00 ms | 18.39 ms |     - |     - |     - |   60.5 KB |
