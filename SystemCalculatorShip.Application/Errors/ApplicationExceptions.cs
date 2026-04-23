namespace SystemCalculatorShip.Application.Errors;

/// <summary>
/// Base exception for application errors
/// </summary>
public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message) { }
    public ApplicationException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>
/// Thrown when validation fails in the application layer
/// </summary>
public class ValidationException : ApplicationException
{
    public ValidationException(string message) : base(message) { }
    public IReadOnlyDictionary<string, string[]>? Errors { get; set; }
}
