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
    public class OrderItemEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public OrderItemEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });
        }

        [Fact]
        public async Task CreateOrderItem_ReturnsCreatedStatusAndOrderItem()
        {
            // Arrange
            var client = _factory.CreateClient();

            // First create a customer
            var customer = new Customer
            {
                Name = "John Doe",
                PhoneNumber = "1234567890",
                Email = "johndoe@example.com"
            };
            var customerResponse = await client.PostAsJsonAsync("/api/customers", customer);
            customerResponse.EnsureSuccessStatusCode();
            var createdCustomer = await customerResponse.Content.ReadFromJsonAsync<Customer>();

            // Then create an order for that customer
            var order = new Order
            {
                CustomerId = createdCustomer.Id,
                OrderDate = System.DateTime.UtcNow
            };
            var orderResponse = await client.PostAsJsonAsync("/api/orders", order);
            orderResponse.EnsureSuccessStatusCode();
            var createdOrder = await orderResponse.Content.ReadFromJsonAsync<Order>();

            // Then create a product
            var product = new Product
            {
                Name = "Lemonade Classic",
                Type = "Drink",
                Size = "500ml",
                Price = 1.50m
            };
            var productResponse = await client.PostAsJsonAsync("/api/products", product);
            productResponse.EnsureSuccessStatusCode();
            var createdProduct = await productResponse.Content.ReadFromJsonAsync<Product>();

            var orderItem = new OrderItem
            {
                OrderId = createdOrder.Id,
                ProductId = createdProduct.Id,
                Quantity = 2
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/orderitems", orderItem);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 201-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var returnedOrderItem = await response.Content.ReadFromJsonAsync<OrderItem>();
            Assert.NotNull(returnedOrderItem);
            Assert.Equal(createdOrder.Id, returnedOrderItem.OrderId);
            Assert.Equal(createdProduct.Id, returnedOrderItem.ProductId);
        }
    }
}

