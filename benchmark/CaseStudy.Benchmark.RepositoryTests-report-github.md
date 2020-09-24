# RepositoryTests

* get data from the `CaseStudy.WebApi`'s repository. 
* Database has 100 entities.

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

database has 100 entities.

|                   Method |         Mean |       Error |      StdDev |          Min |          Max |       Median |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------- |-------------:|------------:|------------:|-------------:|-------------:|-------------:|--------:|-------:|------:|----------:|
| GetPaginatedProductsTest | 204,463.0 ns | 1,356.30 ns | 2,030.04 ns | 201,044.9 ns | 208,678.0 ns | 204,334.0 ns |  3.4180 | 0.2441 |     - |   30083 B |
|          GetProductsTest | 351,238.8 ns | 1,105.62 ns | 1,620.61 ns | 347,863.7 ns | 353,831.8 ns | 351,485.3 ns | 14.1602 | 0.4883 |     - |  121807 B |
|           GetProductTest |     625.9 ns |     3.57 ns |     4.88 ns |     618.3 ns |     633.4 ns |     627.4 ns |  0.0429 |      - |     - |     360 B |
|        ProductExistsTest | 153,479.2 ns | 1,057.21 ns | 1,549.65 ns | 150,930.6 ns | 157,353.1 ns | 152,984.0 ns |  2.1973 |      - |     - |   19252 B |
