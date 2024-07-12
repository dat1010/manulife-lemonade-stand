namespace LemonadeStandApi.Controllers;

using Carter;
using LemonadeStandApi.Models;
using Microsoft.AspNetCore.Http;
using LemonadeStandApi.Repo;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/products/{id}", async (int id, Product updatedProduct, AppDbContext db) =>
       {
           var product = await db.Products.FindAsync(id);
           if (product == null)
           {
               return Results.NotFound();
           }
           product.Name = updatedProduct.Name;
           product.Price = updatedProduct.Price;
           product.Type = updatedProduct.Type;
           product.Size = updatedProduct.Size;
           product.UpdatedAt = DateTime.UtcNow;
           await db.SaveChangesAsync();
           return Results.NoContent();
       })
       .WithName("UpdateProduct")
       .Produces(StatusCodes.Status204NoContent)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Update a product")
       .WithDescription("Updates an existing product in the system.");
    }

}
