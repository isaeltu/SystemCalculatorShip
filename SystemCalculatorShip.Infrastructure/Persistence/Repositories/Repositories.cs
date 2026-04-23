namespace SystemCalculatorShip.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using SystemCalculatorShip.Domain.Entities;
using SystemCalculatorShip.Application.Interfaces;
using SystemCalculatorShip.Infrastructure.Persistence.Contexts;

/// <summary>
/// Repository implementation for Country entity
/// </summary>
public class CountryRepository : ICountryRepository
{
    private readonly ApplicationDbContext _context;

    public CountryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Country?> GetByCodeAsync(string code)
    {
        return await _context.Countries
            .FirstOrDefaultAsync(c => c.Code == code.ToUpper() && c.IsActive);
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        return await _context.Countries
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
    }

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _context.Countries
            .Where(c => c.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Country country)
    {
        _context.Countries.Update(country);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country != null)
        {
            country.IsActive = false;
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }
    }
}

/// <summary>
/// Repository implementation for Tariff entity
/// </summary>
public class TariffRepository : ITariffRepository
{
    private readonly ApplicationDbContext _context;

    public TariffRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Tariff?> GetByCountryAndTypeAsync(int countryId, string type)
    {
        return await _context.Tariffs
            .Include(t => t.Country)
            .FirstOrDefaultAsync(t => t.CountryId == countryId && t.Type == type.ToLower() && t.IsActive);
    }

    public async Task<Tariff?> GetByIdAsync(int id)
    {
        return await _context.Tariffs
            .Include(t => t.Country)
            .FirstOrDefaultAsync(t => t.Id == id && t.IsActive);
    }

    public async Task<IEnumerable<Tariff>> GetByCountryAsync(int countryId)
    {
        return await _context.Tariffs
            .Include(t => t.Country)
            .Where(t => t.CountryId == countryId && t.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tariff>> GetAllAsync()
    {
        return await _context.Tariffs
            .Include(t => t.Country)
            .Where(t => t.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(Tariff tariff)
    {
        _context.Tariffs.Add(tariff);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tariff tariff)
    {
        _context.Tariffs.Update(tariff);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tariff = await _context.Tariffs.FindAsync(id);
        if (tariff != null)
        {
            tariff.IsActive = false;
            _context.Tariffs.Update(tariff);
            await _context.SaveChangesAsync();
        }
    }
}
