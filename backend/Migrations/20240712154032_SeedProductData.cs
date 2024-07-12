using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LemonadeStandApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "Name", "Price", "Size", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc), "Lemonade", 1.00m, "Regular", "Drink", new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc), "Pink Lemonade", 1.00m, "Regular", "Drink", new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc), "Lemonade", 1.50m, "Large", "Drink", new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc) },
                    { 4, new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc).AddTicks(10), "Pink Lemonade", 1.50m, "Large", "Drink", new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc).AddTicks(10) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
