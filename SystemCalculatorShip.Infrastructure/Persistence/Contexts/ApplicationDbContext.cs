namespace SystemCalculatorShip.Infrastructure.Persistence.Contexts;

using Microsoft.EntityFrameworkCore;
using Entities;

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
        // Seed countries
        var countryId = 1;
        var countries = new[]
        {
            new { Id = countryId++, Code = "IN", Name = "India", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = countryId++, Code = "US", Name = "United States", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = countryId++, Code = "GB", Name = "United Kingdom", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true }
        };

        modelBuilder.Entity<Country>().HasData(
            countries.Select(c => new Country
            {
                Id = c.Id,
                Code = c.Code,
                Name = c.Name,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.IsActive
            })
        );

        // Seed tariffs
        var tariffId = 1;
        var tariffs = new[]
        {
            new { Id = tariffId++, CountryId = 1, Type = "standard", RatePerKg = 5m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = tariffId++, CountryId = 1, Type = "express", RatePerKg = 7.50m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = tariffId++, CountryId = 1, Type = "economy", RatePerKg = 3m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = tariffId++, CountryId = 2, Type = "standard", RatePerKg = 8m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = tariffId++, CountryId = 2, Type = "express", RatePerKg = 12m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = tariffId++, CountryId = 2, Type = "economy", RatePerKg = 5m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = tariffId++, CountryId = 3, Type = "standard", RatePerKg = 10m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = tariffId++, CountryId = 3, Type = "express", RatePerKg = 15m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true },
            new { Id = tariffId++, CountryId = 3, Type = "economy", RatePerKg = 6m, Currency = "USD", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, IsActive = true }
        };

        modelBuilder.Entity<Tariff>().HasData(
            tariffs.Select(t => new Tariff
            {
                Id = t.Id,
                CountryId = t.CountryId,
                Type = t.Type,
                RatePerKg = t.RatePerKg,
                Currency = t.Currency,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                IsActive = t.IsActive
            })
        );
    }
}
