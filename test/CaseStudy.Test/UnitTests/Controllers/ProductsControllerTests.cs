using CaseStudy.WebApi.Controllers;
using CaseStudy.WebApi.Infrastructure;
using CaseStudy.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CaseStudy.Test.UnitTests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly MockRepository mockRepository;

        private readonly Mock<IRepository> mockDataContext;

        public ProductsControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockDataContext = this.mockRepository.Create<IRepository>();
        }

        private ProductsController CreateProductsController()
        {
            return new ProductsController(this.mockDataContext.Object);
        }

        [Fact]
        public async Task GetProductsTest()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            IAsyncEnumerable<Product> context = GetProducts();
            this.mockDataContext.Setup(m => m.GetProducts()).Returns(context);
            // Act
            var result = productsController.GetProducts();
            var results = new List<Product>();
            await foreach (var item in result)
                results.Add(item);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, results.Count);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductsPaginatedTest()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            const int pageIndex = 2;
            const int pageSize = 11;

            IAsyncEnumerable<Product> context = GetProducts();

            this.mockDataContext.Setup(m => m.GetPaginatedProducts(pageIndex, pageSize)).Returns(context);

            // Act
            var result = productsController.GetProducts(pageIndex, pageSize);
            var results = new List<Product>();
            await foreach (var item in result)
                results.Add(item);
            Assert.Equal(100, results.Count);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetProductTest()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            const long id = 10;
            var data = new Product
            {
                Name = "Product",
                Price = 275,
                Description = "Description",
                ImgUri = new Uri("http\\\\web.com\\g.png", UriKind.RelativeOrAbsolute)
            };
            this.mockDataContext.Setup(m => m.GetProduct(id)).Returns(Task.FromResult(data));
            // Act
            var result = await productsController.GetProduct(id).ConfigureAwait(false);

            var okResult = result as ObjectResult;
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Product product = okResult.Value as Product;
            Assert.NotNull(product);
            Assert.Equal(data, product);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task PutProductTest()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            Product product = new Product
            {
                Id = 1,
                Name = "Product",
                Price = 275,
                Description = "Description",
                ImgUri = new Uri("http\\\\web.com\\ghh.png", UriKind.RelativeOrAbsolute)
            };
            var expected = new NoContentResult();
            this.mockDataContext.Setup(m => m.PutProduct(product)).Returns(Task.FromResult((IActionResult)expected));
            // Act
            var result = await productsController.PutProduct(product).ConfigureAwait(false);

            // Assert
            Assert.Equal(expected, result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task PutDescriptionTest()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            const long id = 10;
            ProductDescription description = new ProductDescription { Description = "null and some text" };
            var expected = new NoContentResult();
            this.mockDataContext.Setup(m => m.PutDescription(id, description.Description)).Returns(Task.FromResult((IActionResult)expected));
            // Act
            var result = await productsController.PutDescription(id, description).ConfigureAwait(false);

            // Assert
            Assert.Equal(expected, result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task PostProductTest()
        {
            // Arrange
            var productsController = this.CreateProductsController();
            ProductCreate product = new ProductCreate
            {
                Name = "Product",
                Price = 275,
                Description = "Description",
                ImgUri = new Uri("http\\\\web.com\\ghh.png", UriKind.RelativeOrAbsolute)
            };

            Product productExpected = product.ToProduct();

            this.mockDataContext.Setup(m => m.PostProduct(product)).Returns(Task.FromResult(productExpected));
            // Act
            var result = await productsController.PostProduct(product).ConfigureAwait(false);

            var okResult = result as CreatedAtActionResult;
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Product productOutput = okResult.Value as Product;
            Assert.NotNull(product);
            Assert.Equal(productExpected, productOutput);
            this.mockRepository.VerifyAll();
        }

        private static async IAsyncEnumerable<Product> GetProducts()
        {
            for (int i = 1; i <= 100; i++)
            {
                var data = new Product
                {
                    Name = $"{i} Product",
                    Price = 275 * i + 1,
                    Description = $"Description {i}",
                    ImgUri = new Uri($"http\\\\web.com\\{i}.png", UriKind.RelativeOrAbsolute)
                };
                yield return await Task.Run(() => data).ConfigureAwait(false);
            }
        }
    }
}
