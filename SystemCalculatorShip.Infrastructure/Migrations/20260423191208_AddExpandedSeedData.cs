using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SystemCalculatorShip.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExpandedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, "CA", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Canada", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "AU", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Australia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "DE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Germany", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "FR", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "France", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, "JP", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Japan", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, "CN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "China", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, "SG", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "Singapore", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "Id", "CountryId", "CreatedAt", "Currency", "IsActive", "RatePerKg", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 10, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 9m, "standard", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 13m, "express", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 5.5m, "economy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 11m, "standard", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 16m, "express", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 7m, "economy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 8.5m, "standard", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 12.5m, "express", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 4.5m, "economy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 9.5m, "standard", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 14m, "express", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 5m, "economy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 10.5m, "standard", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 15.5m, "express", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 6m, "economy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 7m, "standard", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 11m, "express", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 4m, "economy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 12m, "standard", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 17m, "express", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USD", true, 7.5m, "economy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
