namespace SystemCalculatorShip.Application.DTOs;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Request DTO for calculating shipping costs
/// </summary>
public class CalculateShippingRequest
{
    [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be positive")]
    public double Weight { get; set; }

    [Required(ErrorMessage = "Country code is required")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Country code must be exactly 2 characters")]
    public string CountryCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tariff type is required")]
    [RegularExpression("^(standard|express|economy|premium)$", 
        ErrorMessage = "Tariff type must be one of: standard, express, economy, premium")]
    public string TariffType { get; set; } = string.Empty;
}

/// <summary>
/// Response DTO for shipping calculation result
/// </summary>
public class CalculateShippingResponse
{
    public double Weight { get; set; }
    public string Country { get; set; } = string.Empty;
    public string TariffType { get; set; } = string.Empty;
    public decimal RatePerKg { get; set; }
    public decimal TotalCost { get; set; }
    public string Currency { get; set; } = "USD";
}

/// <summary>
/// DTO for Country data transfer
/// </summary>
public class CountryDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Request DTO for creating a country
/// </summary>
public class CreateCountryRequest
{
    [Required(ErrorMessage = "Country code is required")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Country code must be exactly 2 characters")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Country name must be between 3 and 100 characters")]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Request DTO for updating a country
/// </summary>
public class UpdateCountryRequest
{
    [Required(ErrorMessage = "Country name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Country name must be between 3 and 100 characters")]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// DTO for Tariff data transfer
/// </summary>
public class TariffDto
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal RatePerKg { get; set; }
    public string Currency { get; set; } = "USD";
}

/// <summary>
/// Request DTO for creating a tariff
/// </summary>
public class CreateTariffRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Country ID must be valid")]
    public int CountryId { get; set; }

    [Required(ErrorMessage = "Tariff type is required")]
    [RegularExpression("^(standard|express|economy|premium)$",
        ErrorMessage = "Tariff type must be one of: standard, express, economy, premium")]
    public string Type { get; set; } = string.Empty;

    [Range(0.01, 9999.99, ErrorMessage = "Rate per kg must be between 0.01 and 9999.99")]
    public decimal RatePerKg { get; set; }
}

/// <summary>
/// Request DTO for updating a tariff
/// </summary>
public class UpdateTariffRequest
{
    [RegularExpression("^(standard|express|economy|premium)$",
        ErrorMessage = "Tariff type must be one of: standard, express, economy, premium")]
    public string? Type { get; set; }

    [Range(0.01, 9999.99, ErrorMessage = "Rate per kg must be between 0.01 and 9999.99")]
    public decimal? RatePerKg { get; set; }
}

/// <summary>
/// Request DTO for authentication login
/// </summary>
public class LoginRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Response DTO for authentication result
/// </summary>
public class LoginResponse
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public int ExpiresIn { get; set; }
    public string? Message { get; set; }
}

/// <summary>
/// Generic API response wrapper
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}
