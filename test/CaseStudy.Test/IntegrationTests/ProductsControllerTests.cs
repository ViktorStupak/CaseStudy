using CaseStudy.WebApi;
using CaseStudy.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CaseStudy.Test.IntegrationTests
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(WebApplicationFactory<Startup> factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            _factory = factory;
        }

        [Fact]
        public async Task AllProductTest()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            // Act
            var response = await client.GetAsync(new Uri("/api/Products", UriKind.Relative)).ConfigureAwait(false);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<List<Product>>(stringResponse);


            Assert.True(products.Count >= 100);
        }

        [Fact]
        public async Task AllProductTestPaginated()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            // Act
            var response = await client.GetAsync(new Uri("/api/Products/1/10", UriKind.Relative)).ConfigureAwait(false);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<List<Product>>(stringResponse);


            Assert.Equal(10, products.Count);
        }

        [Fact]
        public async Task ProductTest()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            // Act
            var response = await client.GetAsync(new Uri("/api/Products/10", UriKind.Relative)).ConfigureAwait(false);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var product = JsonConvert.DeserializeObject<Product>(stringResponse);

            Assert.NotNull(product);
            Assert.Equal(10, product.Id);
        }

        [Fact]
        public async Task PostProductTest()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            var responseAllBefore = await client.GetAsync(new Uri("/api/Products", UriKind.Relative)).ConfigureAwait(false);
            var stringResponseAllBefore = await responseAllBefore.Content.ReadAsStringAsync().ConfigureAwait(false);
            var productAllBefore = JsonConvert.DeserializeObject<List<Product>>(stringResponseAllBefore);
            using var content = new StringContent(
                JsonConvert.SerializeObject(new ProductCreate
                {
                    Name = "Product",
                    Price = 275,
                    Description = "Description",
                    ImgUri = new Uri("http\\\\web.com\\ghh.png", UriKind.RelativeOrAbsolute)
                }), Encoding.Default, "application/json");
            // Act
            var response = await client.PostAsync(new Uri("/api/Products", UriKind.Relative), content).ConfigureAwait(false);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var product = JsonConvert.DeserializeObject<Product>(stringResponse);

            var responseAll = await client.GetAsync(new Uri("/api/Products", UriKind.Relative)).ConfigureAwait(false);
            var stringResponseAll = await responseAll.Content.ReadAsStringAsync().ConfigureAwait(false);
            var productAll = JsonConvert.DeserializeObject<List<Product>>(stringResponseAll);
            Assert.NotNull(product);
            Assert.True(productAll.Count > productAllBefore.Count);
        }

        [Fact]
        public async Task PutProductTest()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            using var content = new StringContent(
                JsonConvert.SerializeObject(new Product
                {
                    Id = 1,
                    Name = "Product",
                    Price = 275,
                    Description = "Description",
                    ImgUri = new Uri("http\\\\web.com\\ghh.png", UriKind.RelativeOrAbsolute)
                }), Encoding.Default, "application/json");
            // Act
            var response = await client.PutAsync(new Uri("/api/Products", UriKind.Relative), content).ConfigureAwait(false);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var responseAll = await client.GetAsync(new Uri("/api/Products/1", UriKind.Relative)).ConfigureAwait(false);
            var stringResponseAll = await responseAll.Content.ReadAsStringAsync().ConfigureAwait(false);
            var productAll = JsonConvert.DeserializeObject<Product>(stringResponseAll);
            Assert.Equal("Description", productAll.Description, true);
        }

        [Fact]
        public async Task PutProductDescriptionTest()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            using var content = new StringContent(
                JsonConvert.SerializeObject(new ProductDescription
                {
                    Description = "Description newe"
                }), Encoding.Default, "application/json");
            // Act
            var response = await client.PutAsync(new Uri("/api/Products/description/2", UriKind.Relative), content).ConfigureAwait(false);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var responseAll = await client.GetAsync(new Uri("/api/Products/2", UriKind.Relative)).ConfigureAwait(false);
            var stringResponseAll = await responseAll.Content.ReadAsStringAsync().ConfigureAwait(false);
            var productAll = JsonConvert.DeserializeObject<Product>(stringResponseAll);
            Assert.Equal("Description newe", productAll.Description, true);
        }
    }
}
