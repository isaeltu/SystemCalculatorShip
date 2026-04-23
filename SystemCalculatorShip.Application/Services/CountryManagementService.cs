namespace SystemCalculatorShip.Application.Services;

using SystemCalculatorShip.Domain.Entities;
using SystemCalculatorShip.Application.DTOs;
using SystemCalculatorShip.Application.Interfaces;

/// <summary>
/// Service for managing countries
/// </summary>
public class CountryManagementService : ICountryManagementService
{
    private readonly ICountryRepository _countryRepository;
    private readonly ILoggerService _logger;

    public CountryManagementService(ICountryRepository countryRepository, ILoggerService logger)
    {
        _countryRepository = countryRepository;
        _logger = logger;
    }

    public async Task<Country> CreateCountryAsync(CreateCountryRequest request)
    {
        // Check if country already exists
        var existing = await _countryRepository.GetByCodeAsync(request.Code);
        if (existing != null)
        {
            throw new InvalidOperationException($"Country with code '{request.Code}' already exists.");
        }

        var country = new Country
        {
            Code = request.Code.ToUpper(),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _countryRepository.AddAsync(country);
        _logger.Info($"Country created: {country.Name} ({country.Code})");
        return country;
    }

    public async Task<Country> UpdateCountryAsync(int id, UpdateCountryRequest request)
    {
        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
        {
            throw new InvalidOperationException($"Country with id {id} not found.");
        }

        country.Name = request.Name;
        country.UpdatedAt = DateTime.UtcNow;

        await _countryRepository.UpdateAsync(country);
        _logger.Info($"Country updated: {country.Name} ({country.Code})");
        return country;
    }

    public async Task<IEnumerable<Country>> GetCountriesAsync()
    {
        return await _countryRepository.GetAllAsync();
    }

    public async Task<Country?> GetCountryByCodeAsync(string code)
    {
        return await _countryRepository.GetByCodeAsync(code.ToUpper());
    }

    public async Task DeleteCountryAsync(int id)
    {
        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
        {
            throw new InvalidOperationException($"Country with id {id} not found.");
        }

        await _countryRepository.DeleteAsync(id);
        _logger.Info($"Country deleted: {country.Name} ({country.Code})");
    }
}
