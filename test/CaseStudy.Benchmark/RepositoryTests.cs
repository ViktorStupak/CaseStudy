using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using CaseStudy.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CaseStudy.Benchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31, launchCount: 2)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [MemoryDiagnoser]
    [MarkdownExporter, HtmlExporter, CsvExporter(CsvSeparator.Semicolon)]
    public class RepositoryTests
    {
        private DataContext _mockDataContext;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProductCaseBanchmark;Trusted_Connection=True;MultipleActiveResultSets=true")
                .EnableSensitiveDataLogging()
                .Options;

            this._mockDataContext = new DataContext(options);
            this._mockDataContext.Database.EnsureDeleted();
            this._mockDataContext.Database.EnsureCreated();
            SeedData.SeedDatabase(this._mockDataContext);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            this._mockDataContext.Database.EnsureDeleted();
            this._mockDataContext.Dispose();
        }

        private Repository CreateRepository()
        {
            return new Repository(this._mockDataContext);
        }

        [Benchmark]
        public async Task GetPaginatedProductsTest()
        {
            // Arrange
            var repository = this.CreateRepository();
            const int pageIndex = 3;
            const int pageSize = 12;
            var result = repository.GetPaginatedProducts(pageIndex, pageSize);
            await foreach (var item in result)
            {
                _ = item;
            }
        }

        [Benchmark]
        public async Task GetProductsTest()
        {
            // Arrange
            var repository = this.CreateRepository();

            // Act
            var result = repository.GetProducts();
            await foreach (var item in result)
            {
                _ = item;
            }
        }

        [Benchmark]
        public async Task GetProductTest()
        {
            var repository = this.CreateRepository();
            const long id = 10;
            _ = await repository.GetProduct(id).ConfigureAwait(false);

        }

        [Benchmark]
        public async Task ProductExistsTest()
        {
            var repository = this.CreateRepository();
            const long id = 10;
            _ = await repository.ProductExists(id).ConfigureAwait(false);
        }
    }
}