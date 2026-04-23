namespace SystemCalculatorShip.Infrastructure.Persistence.Contexts;

using Microsoft.EntityFrameworkCore;
using SystemCalculatorShip.Domain.Entities;

/// <summary>
/// Entity Framework Core DbContext for the application
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<AdminUser> AdminUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Country entity
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(2);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasMany(e => e.Tariffs)
                .WithOne(t => t.Country)
                .HasForeignKey(t => t.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Tariff entity
        modelBuilder.Entity<Tariff>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.RatePerKg).HasPrecision(10, 2);
            entity.Property(e => e.Currency).HasMaxLength(3).HasDefaultValue("USD");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.HasIndex(e => new { e.CountryId, e.Type }).IsUnique();
        });

        // Configure AdminUser entity
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.HasIndex(e => e.Username).IsUnique();
        });

        // Seed initial data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var seedCreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var seedUpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Seed countries
        var countries = new[]
        {
            new Country { Id = 1, Code = "IN", Name = "India", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 2, Code = "US", Name = "United States", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 3, Code = "GB", Name = "United Kingdom", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 4, Code = "CA", Name = "Canada", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 5, Code = "AU", Name = "Australia", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 6, Code = "DE", Name = "Germany", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 7, Code = "FR", Name = "France", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 8, Code = "JP", Name = "Japan", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 9, Code = "CN", Name = "China", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Country { Id = 10, Code = "SG", Name = "Singapore", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true }
        };

        modelBuilder.Entity<Country>().HasData(countries);

        // Seed tariffs
        var tariffs = new[]
        {
            new Tariff { Id = 1, CountryId = 1, Type = "standard", RatePerKg = 5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 2, CountryId = 1, Type = "express", RatePerKg = 7.50m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 3, CountryId = 1, Type = "economy", RatePerKg = 3m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 4, CountryId = 2, Type = "standard", RatePerKg = 8m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 5, CountryId = 2, Type = "express", RatePerKg = 12m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 6, CountryId = 2, Type = "economy", RatePerKg = 5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 7, CountryId = 3, Type = "standard", RatePerKg = 10m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 8, CountryId = 3, Type = "express", RatePerKg = 15m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 9, CountryId = 3, Type = "economy", RatePerKg = 6m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 10, CountryId = 4, Type = "standard", RatePerKg = 9m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 11, CountryId = 4, Type = "express", RatePerKg = 13m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 12, CountryId = 4, Type = "economy", RatePerKg = 5.5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 13, CountryId = 5, Type = "standard", RatePerKg = 11m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 14, CountryId = 5, Type = "express", RatePerKg = 16m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 15, CountryId = 5, Type = "economy", RatePerKg = 7m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 16, CountryId = 6, Type = "standard", RatePerKg = 8.5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 17, CountryId = 6, Type = "express", RatePerKg = 12.5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 18, CountryId = 6, Type = "economy", RatePerKg = 4.5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 19, CountryId = 7, Type = "standard", RatePerKg = 9.5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 20, CountryId = 7, Type = "express", RatePerKg = 14m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 21, CountryId = 7, Type = "economy", RatePerKg = 5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 22, CountryId = 8, Type = "standard", RatePerKg = 10.5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 23, CountryId = 8, Type = "express", RatePerKg = 15.5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 24, CountryId = 8, Type = "economy", RatePerKg = 6m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 25, CountryId = 9, Type = "standard", RatePerKg = 7m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 26, CountryId = 9, Type = "express", RatePerKg = 11m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 27, CountryId = 9, Type = "economy", RatePerKg = 4m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 28, CountryId = 10, Type = "standard", RatePerKg = 12m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 29, CountryId = 10, Type = "express", RatePerKg = 17m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true },
            new Tariff { Id = 30, CountryId = 10, Type = "economy", RatePerKg = 7.5m, Currency = "USD", CreatedAt = seedCreatedAt, UpdatedAt = seedUpdatedAt, IsActive = true }
        };

        modelBuilder.Entity<Tariff>().HasData(tariffs);
    }
}
