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
    public class CustomerEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CustomerEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCreatedStatusAndCustomer()
        {
            // Arrange
            var client = _factory.CreateClient();
            var customer = new Customer
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                Email = "johndoe@example.com"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/customers", customer);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 201-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var returnedCustomer = await response.Content.ReadFromJsonAsync<Customer>();
            Assert.NotNull(returnedCustomer);
            Assert.Equal("John Doe", returnedCustomer.Name);
        }
    }
}

