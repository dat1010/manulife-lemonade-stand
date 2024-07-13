using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LemonadeStandApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 13, 0, 10, 58, 385, DateTimeKind.Utc).AddTicks(370), "Lemonade", new DateTime(2024, 7, 13, 0, 10, 58, 385, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 13, 0, 10, 58, 385, DateTimeKind.Utc).AddTicks(370), "Pink Lemonade", new DateTime(2024, 7, 13, 0, 10, 58, 385, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 13, 0, 10, 58, 385, DateTimeKind.Utc).AddTicks(370), "Lemonde", new DateTime(2024, 7, 13, 0, 10, 58, 385, DateTimeKind.Utc).AddTicks(380) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 13, 0, 10, 58, 385, DateTimeKind.Utc).AddTicks(380), "Pink Lemonade", new DateTime(2024, 7, 13, 0, 10, 58, 385, DateTimeKind.Utc).AddTicks(380) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc), "Drink", new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc), "Drink", new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc), "Drink", new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Type", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc).AddTicks(10), "Drink", new DateTime(2024, 7, 12, 15, 40, 32, 526, DateTimeKind.Utc).AddTicks(10) });
        }
    }
}
