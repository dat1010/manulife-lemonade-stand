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
    public class OrderEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public OrderEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });
        }

        [Fact]
        public async Task CreateOrder_ReturnsCreatedStatusAndOrder()
        {
            // Arrange
            var client = _factory.CreateClient();
            var customer = new Customer
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                Email = "johndoe@example.com"
            };

            // First create a customer
            var customerResponse = await client.PostAsJsonAsync("/api/customers", customer);
            customerResponse.EnsureSuccessStatusCode();
            var createdCustomer = await customerResponse.Content.ReadFromJsonAsync<Customer>();

            var order = new Order
            {
                CustomerId = createdCustomer.Id,
                OrderDate = System.DateTime.UtcNow
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/orders", order);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 201-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var returnedOrder = await response.Content.ReadFromJsonAsync<Order>();
            Assert.NotNull(returnedOrder);
            Assert.Equal(createdCustomer.Id, returnedOrder.CustomerId);
        }
    }
}

