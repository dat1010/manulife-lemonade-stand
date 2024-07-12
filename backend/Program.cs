using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LemonadeStandApi.Repo;
using LemonadeStandApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// POST endpoint for Customer
app.MapPost("/api/customers", async (Customer customer, AppDbContext db) =>
{
    db.Customers.Add(customer);
    await db.SaveChangesAsync();
    return Results.Created($"/api/customers/{customer.Id}", customer);
});

// POST endpoint for Order
app.MapPost("/api/orders", async (Order order, AppDbContext db) =>
{
    db.Orders.Add(order);
    await db.SaveChangesAsync();
    return Results.Created($"/api/orders/{order.Id}", order);
});

// POST endpoint for OrderItem
app.MapPost("/api/orderitems", async (OrderItem orderItem, AppDbContext db) =>
{
    db.OrderItems.Add(orderItem);
    await db.SaveChangesAsync();
    return Results.Created($"/api/orderitems/{orderItem.Id}", orderItem);
});

app.MapGet("api/products", async (AppDbContext db) =>
{
    return Results.Ok(await db.Products.ToListAsync());
});

app.MapGet("api/products/{id}", async (int id, AppDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(product);
});
//
// app.MapPost("/products", async (Product product, AppDbContext db) =>
// {
//     db.Products.Add(product);
//     await db.SaveChangesAsync();
//     return Results.Created($"/products/{product.Id}", product);
// });
//
// app.MapPut("/products/{id}", async (int id, Product updatedProduct, AppDbContext db) =>
// {
//     var product = await db.Products.FindAsync(id);
//     if (product == null)
//     {
//         return Results.NotFound();
//     }
//     product.Name = updatedProduct.Name;
//     product.Price = updatedProduct.Price;
//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });
//
// app.MapDelete("/products/{id}", async (int id, AppDbContext db) =>
// {
//     var product = await db.Products.FindAsync(id);
//     if (product == null)
//     {
//         return Results.NotFound();
//     }
//     db.Products.Remove(product);
//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });


// Automatically apply database migrations
app.UseCors("AllowLocalhost");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
app.MapControllers();

app.Run();
