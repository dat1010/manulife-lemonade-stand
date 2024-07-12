namespace LemonadeStandApi.Controllers;

using Carter;
using LemonadeStandApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using LemonadeStandApi.Repo;

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (AppDbContext db) =>
        {
            var products = await db.Products.ToListAsync();
            return Results.Ok(products);
        })
        .WithName("GetProducts")
        .Produces<Product[]>(StatusCodes.Status200OK)
        .WithSummary("Get all products")
        .WithDescription("Gets all products from the system.");
    }
}
