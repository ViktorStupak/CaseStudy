# RestFullAsyncTest

* create Rest client - [RestSharp](https://restsharp.dev/)
* Async call to `CaseStudy.WebApi` 
  * get all products
  * get paginated product's list (due to page index and size)
  * get one product by `id`.
* check if response `IsSuccessful`
* Database has 100 entities.
  
> For run this one test make sure that start CaseStudy.WebApi

### Resources

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
AMD Ryzen 5 3600X, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT  [AttachedDebugger]
  Job-GXNYGP : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

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
  1 ms      : 1 Millisecond (0.001 sec)

```

### Result
|                  Method |     Mean |    Error |   StdDev |      Min |      Max |   Median |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |---------:|---------:|---------:|---------:|---------:|---------:|--------:|------:|------:|----------:|
|          GetAllProducts | 19.84 ms | 0.812 ms | 2.555 ms | 13.86 ms | 24.69 ms | 20.13 ms | 15.6250 |     - |     - | 125.52 KB |
| GetAllPaginatedProducts | 14.02 ms | 0.141 ms | 0.212 ms | 13.71 ms | 14.52 ms | 14.03 ms |       - |     - |     - |  68.05 KB |
|              GetProduct | 17.30 ms | 0.222 ms | 0.406 ms | 16.26 ms | 17.99 ms | 17.37 ms |       - |     - |     - |  62.17 KB |
