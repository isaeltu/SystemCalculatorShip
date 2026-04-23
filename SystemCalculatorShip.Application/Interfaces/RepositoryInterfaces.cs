namespace SystemCalculatorShip.Application.Interfaces;

using SystemCalculatorShip.Domain.Entities;

/// <summary>
/// Interface for Country repository
/// </summary>
public interface ICountryRepository
{
    Task<Country?> GetByCodeAsync(string code);
    Task<Country?> GetByIdAsync(int id);
    Task<IEnumerable<Country>> GetAllAsync();
    Task AddAsync(Country country);
    Task UpdateAsync(Country country);
    Task DeleteAsync(int id);
}

/// <summary>
/// Interface for Tariff repository
/// </summary>
public interface ITariffRepository
{
    Task<Tariff?> GetByCountryAndTypeAsync(int countryId, string type);
    Task<Tariff?> GetByIdAsync(int id);
    Task<IEnumerable<Tariff>> GetByCountryAsync(int countryId);
    Task<IEnumerable<Tariff>> GetAllAsync();
    Task AddAsync(Tariff tariff);
    Task UpdateAsync(Tariff tariff);
    Task DeleteAsync(int id);
}
