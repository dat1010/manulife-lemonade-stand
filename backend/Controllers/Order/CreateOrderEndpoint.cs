using Carter;
using LemonadeStandApi.Models;
using LemonadeStandApi.Repo;

public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders", async (AppDbContext db, Order order) =>
        {
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return Results.Created($"/api/orders/{order.Id}", order);
        })
        .WithName("CreateOrder")
        .Produces<Order>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new order")
        .WithDescription("Creates a new order in the system.");
    }
}

