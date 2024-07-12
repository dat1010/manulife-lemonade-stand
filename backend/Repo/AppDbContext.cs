using Microsoft.EntityFrameworkCore;
using LemonadeStandApi.Models;

namespace LemonadeStandApi.Repo;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Order>().ToTable("Order");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItem");

        // Seed data for Product
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Lemonade", Type = "Lemonade", Size = "Regular", Price = 1.00m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Product { Id = 2, Name = "Pink Lemonade", Type = "Pink Lemonade", Size = "Regular", Price = 1.00m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Product { Id = 3, Name = "Lemonade", Type = "Lemonde", Size = "Large", Price = 1.50m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Product { Id = 4, Name = "Pink Lemonade", Type = "Pink Lemonade", Size = "Large", Price = 1.50m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}

