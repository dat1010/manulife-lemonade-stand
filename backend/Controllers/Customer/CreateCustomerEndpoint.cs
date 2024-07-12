namespace LemonadeStandApi.Controllers;

using Carter;
using LemonadeStandApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using LemonadeStandApi.Repo;

public class CustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/customers", async (AppDbContext db, Customer customer) =>
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return Results.Created($"/api/customers/{customer.Id}", customer);
        })
        .WithName("CreateCustomer")
        .Produces<Customer>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new customer")
        .WithDescription("Creates a new customer in the system.");
    }
}

