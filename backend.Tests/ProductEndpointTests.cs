using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LemonadeStandApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace LemonadeStand.Tests
{
    public class ProductEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });
        }

        [Fact]
        public async Task GetProducts_ReturnsSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/products");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedStatusAndProduct()
        {
            // Arrange
            var client = _factory.CreateClient();
            var product = new Product
            {
                Name = "Test Product",
                Type = "Drink",
                Size = "500ml",
                Price = 2.5m
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/products", product);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 201-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var returnedProduct = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(returnedProduct);
            Assert.Equal("Test Product", returnedProduct.Name);
        }
    }
}

