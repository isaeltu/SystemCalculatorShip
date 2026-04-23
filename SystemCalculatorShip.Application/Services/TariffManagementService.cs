namespace SystemCalculatorShip.Application.Services;

using DTOs;
using Entities;
using Infrastructure.Interfaces;
using Interfaces;

/// <summary>
/// Service for managing tariffs
/// </summary>
public class TariffManagementService : ITariffManagementService
{
    private readonly ITariffRepository _tariffRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ILoggerService _logger;

    public TariffManagementService(
        ITariffRepository tariffRepository,
        ICountryRepository countryRepository,
        ILoggerService logger)
    {
        _tariffRepository = tariffRepository;
        _countryRepository = countryRepository;
        _logger = logger;
    }

    public async Task<Tariff> CreateTariffAsync(CreateTariffRequest request)
    {
        // Check if country exists
        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country == null)
        {
            throw new InvalidOperationException($"Country with id {request.CountryId} not found.");
        }

        // Check if tariff already exists for this country and type
        var existing = await _tariffRepository.GetByCountryAndTypeAsync(request.CountryId, request.Type.ToLower());
        if (existing != null)
        {
            throw new InvalidOperationException($"Tariff of type '{request.Type}' already exists for country '{country.Name}'.");
        }

        var tariff = new Tariff
        {
            CountryId = request.CountryId,
            Type = request.Type.ToLower(),
            RatePerKg = request.RatePerKg,
            Currency = "USD",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _tariffRepository.AddAsync(tariff);
        _logger.Info($"Tariff created: {country.Name} - {request.Type} - ${request.RatePerKg}/kg");
        return tariff;
    }

    public async Task<Tariff> UpdateTariffAsync(int id, UpdateTariffRequest request)
    {
        var tariff = await _tariffRepository.GetByIdAsync(id);
        if (tariff == null)
        {
            throw new InvalidOperationException($"Tariff with id {id} not found.");
        }

        if (!string.IsNullOrEmpty(request.Type))
        {
            // Check if new type already exists for this country
            var existing = await _tariffRepository.GetByCountryAndTypeAsync(tariff.CountryId, request.Type.ToLower());
            if (existing != null && existing.Id != id)
            {
                throw new InvalidOperationException($"Tariff of type '{request.Type}' already exists for this country.");
            }
            tariff.Type = request.Type.ToLower();
        }

        if (request.RatePerKg.HasValue && request.RatePerKg > 0)
        {
            tariff.RatePerKg = request.RatePerKg.Value;
        }

        tariff.UpdatedAt = DateTime.UtcNow;
        await _tariffRepository.UpdateAsync(tariff);
        _logger.Info($"Tariff updated: id {id} - {tariff.Type} - ${tariff.RatePerKg}/kg");
        return tariff;
    }

    public async Task<IEnumerable<Tariff>> GetTariffsAsync()
    {
        return await _tariffRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Tariff>> GetTariffsByCountryAsync(int countryId)
    {
        return await _tariffRepository.GetByCountryAsync(countryId);
    }

    public async Task<Tariff?> GetTariffByCountryAndTypeAsync(int countryId, string type)
    {
        return await _tariffRepository.GetByCountryAndTypeAsync(countryId, type.ToLower());
    }

    public async Task DeleteTariffAsync(int id)
    {
        var tariff = await _tariffRepository.GetByIdAsync(id);
        if (tariff == null)
        {
            throw new InvalidOperationException($"Tariff with id {id} not found.");
        }

        await _tariffRepository.DeleteAsync(id);
        _logger.Info($"Tariff deleted: id {id}");
    }
}
