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
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT dbo.Countries ON;

IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 4)
    INSERT INTO dbo.Countries (Id, Code, CreatedAt, IsActive, Name, UpdatedAt) VALUES (4, 'CA', '2024-01-01T00:00:00.0000000Z', 1, 'Canada', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 5)
    INSERT INTO dbo.Countries (Id, Code, CreatedAt, IsActive, Name, UpdatedAt) VALUES (5, 'AU', '2024-01-01T00:00:00.0000000Z', 1, 'Australia', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 6)
    INSERT INTO dbo.Countries (Id, Code, CreatedAt, IsActive, Name, UpdatedAt) VALUES (6, 'DE', '2024-01-01T00:00:00.0000000Z', 1, 'Germany', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 7)
    INSERT INTO dbo.Countries (Id, Code, CreatedAt, IsActive, Name, UpdatedAt) VALUES (7, 'FR', '2024-01-01T00:00:00.0000000Z', 1, 'France', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 8)
    INSERT INTO dbo.Countries (Id, Code, CreatedAt, IsActive, Name, UpdatedAt) VALUES (8, 'JP', '2024-01-01T00:00:00.0000000Z', 1, 'Japan', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 9)
    INSERT INTO dbo.Countries (Id, Code, CreatedAt, IsActive, Name, UpdatedAt) VALUES (9, 'CN', '2024-01-01T00:00:00.0000000Z', 1, 'China', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 10)
    INSERT INTO dbo.Countries (Id, Code, CreatedAt, IsActive, Name, UpdatedAt) VALUES (10, 'SG', '2024-01-01T00:00:00.0000000Z', 1, 'Singapore', '2024-01-01T00:00:00.0000000Z');

SET IDENTITY_INSERT dbo.Countries OFF;

SET IDENTITY_INSERT dbo.Tariffs ON;

IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 10)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (10, 4, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 9.0, 'standard', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 11)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (11, 4, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 13.0, 'express', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 12)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (12, 4, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 5.5, 'economy', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 13)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (13, 5, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 11.0, 'standard', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 14)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (14, 5, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 16.0, 'express', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 15)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (15, 5, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 7.0, 'economy', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 16)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (16, 6, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 8.5, 'standard', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 17)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (17, 6, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 12.5, 'express', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 18)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (18, 6, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 4.5, 'economy', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 19)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (19, 7, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 9.5, 'standard', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 20)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (20, 7, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 14.0, 'express', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 21)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (21, 7, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 5.0, 'economy', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 22)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (22, 8, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 10.5, 'standard', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 23)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (23, 8, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 15.5, 'express', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 24)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (24, 8, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 6.0, 'economy', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 25)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (25, 9, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 7.0, 'standard', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 26)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (26, 9, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 11.0, 'express', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 27)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (27, 9, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 4.0, 'economy', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 28)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (28, 10, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 12.0, 'standard', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 29)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (29, 10, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 17.0, 'express', '2024-01-01T00:00:00.0000000Z');
IF NOT EXISTS (SELECT 1 FROM dbo.Tariffs WHERE Id = 30)
    INSERT INTO dbo.Tariffs (Id, CountryId, CreatedAt, Currency, IsActive, RatePerKg, Type, UpdatedAt) VALUES (30, 10, '2024-01-01T00:00:00.0000000Z', 'USD', 1, 7.5, 'economy', '2024-01-01T00:00:00.0000000Z');

SET IDENTITY_INSERT dbo.Tariffs OFF;
" );
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
