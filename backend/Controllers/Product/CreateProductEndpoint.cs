namespace LemonadeStandApi.Controllers;

using Carter;
using LemonadeStandApi.Models;
using Microsoft.AspNetCore.Http;
using LemonadeStandApi.Repo;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (AppDbContext db, Product product) =>
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/api/products/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new product")
        .WithDescription("Creates a new product in the system.");
    }
}


