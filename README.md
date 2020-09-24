# CaseStudy 
To create REST API service providing all available products of an eshop and enabling the partialupdate of one product.

## Api specification (endpoints):
> See more after start app on the [Swagger Documentation](https://localhost:55554/swagger)

> See more in the [Postman's collection](Postman\CaseStudy.postman_collection.json)
### version 1
#### All product 
``` bash
curl -X GET "https://localhost:55554/api/Products" -H  "accept: application/json"
```
#### Get product by ID
``` bash
curl -X GET "https://localhost:55554/api/Products/45" -H  "accept: */*"
```
#### Post product (create new)
``` bash
curl -X POST "https://localhost:55554/api/Products" -H  "accept: */*" -H  "Content-Type: application/json-patch+json" -d "{\"name\":\"Men's basketball shoes\",\"imgUri\":\"http\\\\\\\\test.com\",\"price\":10,\"description\":\"Description of the product\"}"
```
#### Put product (modify exist)
``` bash
curl -X PUT "https://localhost:55554/api/Products" -H  "accept: */*" -H  "Content-Type: application/json-patch+json" -d "{\"id\":58,\"name\":\"Men's basketball shoes\",\"imgUri\":\"http\\\\\\\\test.com\",\"price\":10,\"description\":\"Description of the product\"}"
```
#### Put description to the product (modify exist)
``` bash
curl -X PUT "https://localhost:55554/api/Products/description/55" -H  "accept: */*" -H  "Content-Type: application/json-patch+json" -d "{\"description\":\"Description of the product\"}"
```
### version 2
#### All product with support for pagination (default page size 10)
``` bash
curl -X GET "https://localhost:55554/api/Products/4/15" -H  "accept: application/json"
```

## Installation

Use the cmd to run ``CaseStudy.WebApi`` app.

``` bash
cd {rootRepository}/src
dotnet run --configuration {Release/Debug}
```

## Unit tests

Use the cmd to run unit test.

``` bash
cd {solution root}
dotnet test --configuration {Release/Debug}
```
## Benchmark tests

Use the cmd to run Benchmark test.
* run ``CaseStudy.WebApi`` app.
* run benchmark tests
``` bash
cd {rootRepository}/src
dotnet run --configuration {Release/Debug}

cd {rootRepository}/test/CaseStudy.Benchmark
dotnet run -c Release
```
### Benchmark result

#### Main result RepositoryLargeTests

|                   Method |            Mean |         Error |        StdDev |             Min |             Max |          Median |     Gen 0 |   Gen 1 | Gen 2 |  Allocated |
|------------------------- |----------------:|--------------:|--------------:|----------------:|----------------:|----------------:|----------:|--------:|------:|-----------:|
| GetPaginatedProductsTest |    219,721.5 ns |   6,814.50 ns |  10,406.46 ns |    204,848.0 ns |    232,715.7 ns |    217,300.0 ns |    3.6621 |  0.2441 |     - |    30643 B |
|          GetProductsTest | 21,489,612.9 ns | 283,967.80 ns | 416,236.32 ns | 20,851,390.6 ns | 22,377,281.2 ns | 21,553,090.6 ns | 1468.7500 | 62.5000 |     - | 12317581 B |
|           GetProductTest |        644.0 ns |       7.45 ns |      10.93 ns |        631.7 ns |        669.6 ns |        641.8 ns |    0.0429 |       - |     - |      360 B |
|        ProductExistsTest |    159,683.1 ns |   3,653.85 ns |   5,355.76 ns |    153,005.4 ns |    166,508.5 ns |    157,285.4 ns |    2.1973 |       - |     - |    19076 B |

##### Legends
```
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
* [RepositoryLargeTests](\benchmark\CaseStudy.Benchmark.RepositoryLargeTests-report-github.md)
* [RepositoryTests](\benchmark\CaseStudy.Benchmark.RepositoryTests-report-github.md)
* [RestFullAsyncTest](\benchmark\CaseStudy.Benchmark.RestFullAsyncTest-report-github.md)
* [RestFullCreateAsyncTest](\benchmark\CaseStudy.Benchmark.RestFullCreateAsyncTest-report-github.md)
* [RestFullCreateTest](\benchmark\CaseStudy.Benchmark.RestFullCreateTest-report-github.md)
* [RestFullTest](\benchmark\CaseStudy.Benchmark.RestFullTest-report-github.md)
