namespace SystemCalculatorShip.Domain.Validators;

using Errors;

/// <summary>
/// Interface for weight validation
/// </summary>
public interface IWeightValidator
{
    void Validate(double weight);
}

/// <summary>
/// Implementation of weight validator
/// </summary>
public class WeightValidator : IWeightValidator
{
    public void Validate(double weight)
    {
        if (weight <= 0)
        {
            throw new InvalidWeightException("Weight must be positive and greater than zero.");
        }
    }
}

/// <summary>
/// Interface for country validation
/// </summary>
public interface ICountryValidator
{
    void Validate(string countryCode);
}

/// <summary>
/// Implementation of country validator
/// </summary>
public class CountryValidator : ICountryValidator
{
    public void Validate(string countryCode)
    {
        if (string.IsNullOrWhiteSpace(countryCode) || countryCode.Length != 2)
        {
            throw new InvalidCountryException("Country code must be exactly 2 characters.");
        }
    }
}

/// <summary>
/// Interface for tariff type validation
/// </summary>
public interface ITariffTypeValidator
{
    void Validate(string tariffType);
}

/// <summary>
/// Implementation of tariff type validator
/// </summary>
public class TariffTypeValidator : ITariffTypeValidator
{
    private static readonly string[] ValidTypes = { "standard", "express", "economy", "premium" };

    public void Validate(string tariffType)
    {
        if (!ValidTypes.Contains(tariffType.ToLower()))
        {
            throw new InvalidTariffTypeException($"Tariff type must be one of: {string.Join(", ", ValidTypes)}");
        }
    }
}
