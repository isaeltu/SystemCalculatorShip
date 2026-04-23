namespace SystemCalculatorShip.Application.Services;

using SystemCalculatorShip.Domain.Entities;
using SystemCalculatorShip.Domain.Validators;
using SystemCalculatorShip.Application.DTOs;
using SystemCalculatorShip.Application.Interfaces;

/// <summary>
/// Service for calculating shipping tariffs
/// </summary>
public class TariffCalculationService : ITariffCalculationService
{
    private readonly ICountryManagementService _countryService;
    private readonly ITariffManagementService _tariffService;
    private readonly IWeightValidator _weightValidator;
    private readonly ITariffTypeValidator _tariffTypeValidator;
    private readonly ILoggerService _logger;

    public TariffCalculationService(
        ICountryManagementService countryService,
        ITariffManagementService tariffService,
        IWeightValidator weightValidator,
        ITariffTypeValidator tariffTypeValidator,
        ILoggerService logger)
    {
        _countryService = countryService;
        _tariffService = tariffService;
        _weightValidator = weightValidator;
        _tariffTypeValidator = tariffTypeValidator;
        _logger = logger;
    }

    public async Task<CalculateShippingResponse> CalculateAsync(CalculateShippingRequest request)
    {
        // Validate weight
        _weightValidator.Validate(request.Weight);

        // Validate tariff type
        _tariffTypeValidator.Validate(request.TariffType);

        // Get country by code
        var country = await _countryService.GetCountryByCodeAsync(request.CountryCode);
        if (country == null)
        {
            throw new InvalidOperationException($"Country with code '{request.CountryCode}' not found.");
        }

        // Get tariff for country and type
        var tariff = await _tariffService.GetTariffByCountryAndTypeAsync(country.Id, request.TariffType.ToLower());
        if (tariff == null)
        {
            throw new InvalidOperationException($"Tariff of type '{request.TariffType}' not found for country '{country.Name}'.");
        }

        // Calculate total cost
        decimal totalCost = tariff.RatePerKg * (decimal)request.Weight;

        var response = new CalculateShippingResponse
        {
            Weight = request.Weight,
            Country = country.Name,
            TariffType = request.TariffType,
            RatePerKg = tariff.RatePerKg,
            TotalCost = totalCost,
            Currency = tariff.Currency
        };

        _logger.Info($"Calculated shipping: {request.Weight}kg x ${tariff.RatePerKg}/kg = ${totalCost} for {country.Name} ({request.TariffType})");

        return response;
    }
}
