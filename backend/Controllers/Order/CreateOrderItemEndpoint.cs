using Carter;
using LemonadeStandApi.Models;
using LemonadeStandApi.Repo;

public class OrderItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orderitems", async (AppDbContext db, OrderItem orderItem) =>
        {
            db.OrderItems.Add(orderItem);
            await db.SaveChangesAsync();
            return Results.Created($"/api/orderitems/{orderItem.Id}", orderItem);
        })
        .WithName("CreateOrderItem")
        .Produces<OrderItem>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new order item")
        .WithDescription("Creates a new order item in the system.");
    }
}

