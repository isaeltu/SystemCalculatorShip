namespace SystemCalculatorShip.Domain.Errors;

/// <summary>
/// Base exception for domain errors
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
    public DomainException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>
/// Thrown when weight validation fails
/// </summary>
public class InvalidWeightException : DomainException
{
    public InvalidWeightException(string message) : base(message) { }
}

/// <summary>
/// Thrown when country validation fails
/// </summary>
public class InvalidCountryException : DomainException
{
    public InvalidCountryException(string message) : base(message) { }
}

/// <summary>
/// Thrown when tariff type validation fails
/// </summary>
public class InvalidTariffTypeException : DomainException
{
    public InvalidTariffTypeException(string message) : base(message) { }
}
