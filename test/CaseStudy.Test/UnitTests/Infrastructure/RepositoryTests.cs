using CaseStudy.WebApi.Infrastructure;
using CaseStudy.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CaseStudy.Test.UnitTests.Infrastructure
{
    public class RepositoryTests : IDisposable
    {

        private readonly DataContext _mockDataContext;

        public RepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            this._mockDataContext = new DataContext(options);

            SeedData.SeedDatabase(this._mockDataContext);
        }

        private Repository CreateRepository()
        {
            return new Repository(this._mockDataContext);
        }

        [Fact]
        public async Task GetPaginatedProductsTest()
        {
            // Arrange
            var repository = this.CreateRepository();
            const int pageIndex = 3;
            const int pageSize = 12;

            // Act
            var result = repository.GetPaginatedProducts(pageIndex, pageSize);

            // Assert
            Assert.NotNull(result);
            var results = new List<Product>();
            await foreach (var item in result)
                results.Add(item);
            Assert.Equal(pageSize, results.Count);
        }

        [Fact]
        public async Task GetProductsTest()
        {
            // Arrange
            var repository = this.CreateRepository();

            // Act
            var result = repository.GetProducts();

            // Assert
            Assert.NotNull(result);
            var results = new List<Product>();
            await foreach (var item in result)
                results.Add(item);
            Assert.True(results.Count >= 100);
        }

        [Fact]
        public async Task GetProductTest()
        {
            // Arrange
            var repository = this.CreateRepository();
            const long id = 10;

            // Act
            var result = await repository.GetProduct(id).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task PostProductTest()
        {
            // Arrange
            var repository = this.CreateRepository();
            ProductCreate product = new ProductCreate
            {
                Name = "Product",
                Price = 275,
                Description = "Description",
                ImgUri = new Uri("http\\\\web.com\\ghh.png", UriKind.RelativeOrAbsolute)
            };

            // Act
            var result = await repository.PostProduct(product).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Price, result.Price);
            Assert.True(await repository.ProductExists(result.Id).ConfigureAwait(false));

        }

        [Fact]
        public async Task PutDescriptionTest()
        {
            // Arrange
            var repository = this.CreateRepository();
            const long id = 10;
            const string description = "Description new";

            // Act
            var result = await repository.PutDescription(id, description).ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
            var savedItem = await repository.GetProduct(id).ConfigureAwait(false);
            Assert.Equal(description, savedItem.Description);
        }

        [Fact]
        public async Task ProductExistsTest()
        {
            // Arrange
            var repository = this.CreateRepository();
            const long id = 10;

            // Act
            var result = await repository.ProductExists(id).ConfigureAwait(false);

            // Assert
            Assert.True(result);
        }

        protected bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (!disposing)
            {
                this._mockDataContext.Dispose();
                this._mockDataContext.Database.EnsureDeleted();
            }

            IsDisposed = true;
        }
    }
}
