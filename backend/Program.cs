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

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// app.MapGet("/products", async (AppDbContext db) =>
// {
//     return Results.Ok(await db.Products.ToListAsync());
// });
//
// app.MapGet("/products/{id}", async (int id, AppDbContext db) =>
// {
//     var product = await db.Products.FindAsync(id);
//     if (product == null)
//     {
//         return Results.NotFound();
//     }
//     return Results.Ok(product);
// });
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Automatically apply database migrations
app.UseCors("AllowLocalhost");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
