using Microsoft.EntityFrameworkCore;

using LemonadeStandApi.Repo;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var environment = builder.Environment.EnvironmentName;

    if (environment == "Testing")
    {
        options.UseInMemoryDatabase("LemonadeStandTesting");
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

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

builder.Services.AddCarter();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");


if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Testing")
{
    app.UseDeveloperExceptionPage();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}

app.MapCarter();

app.Run();

// Make the Program class public for testing
public partial class Program { }
