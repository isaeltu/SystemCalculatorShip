namespace SystemCalculatorShip.Infrastructure.Interfaces;

// Re-export interfaces from Application layer for use within Infrastructure
using ApplicationInterfaces = global::SystemCalculatorShip.Application.Interfaces;

public interface ICountryRepository : ApplicationInterfaces.ICountryRepository
{
}

public interface ITariffRepository : ApplicationInterfaces.ITariffRepository
{
}
