namespace SystemCalculatorShip.Application.Interfaces;

using SystemCalculatorShip.Domain.Entities;
using SystemCalculatorShip.Application.DTOs;

/// <summary>
/// Interface for authentication service
/// </summary>
public interface IAuthenticationService
{
    Task<(bool Success, string? Token, string Message)> AuthenticateAsync(string username, string password);
    bool ValidateToken(string token);
}

/// <summary>
/// Interface for tariff calculation service
/// </summary>
public interface ITariffCalculationService
{
    Task<CalculateShippingResponse> CalculateAsync(CalculateShippingRequest request);
}

/// <summary>
/// Interface for country management service
/// </summary>
public interface ICountryManagementService
{
    Task<Country> CreateCountryAsync(CreateCountryRequest request);
    Task<Country> UpdateCountryAsync(int id, UpdateCountryRequest request);
    Task<IEnumerable<Country>> GetCountriesAsync();
    Task<Country?> GetCountryByCodeAsync(string code);
    Task DeleteCountryAsync(int id);
}

/// <summary>
/// Interface for tariff management service
/// </summary>
public interface ITariffManagementService
{
    Task<Tariff> CreateTariffAsync(CreateTariffRequest request);
    Task<Tariff> UpdateTariffAsync(int id, UpdateTariffRequest request);
    Task<IEnumerable<Tariff>> GetTariffsAsync();
    Task<IEnumerable<Tariff>> GetTariffsByCountryAsync(int countryId);
    Task<Tariff?> GetTariffByCountryAndTypeAsync(int countryId, string type);
    Task DeleteTariffAsync(int id);
}

/// <summary>
/// Interface for logging service
/// </summary>
public interface ILoggerService
{
    void Info(string message);
    void Error(string message, Exception? exception = null);
    void Warning(string message);
}
