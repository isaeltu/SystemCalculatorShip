namespace SystemCalculatorShip.Domain.Entities;

/// <summary>
/// Represents a shipping tariff for a country
/// </summary>
public class Tariff
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal RatePerKg { get; set; }
    public string Currency { get; set; } = "USD";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation property
    public Country? Country { get; set; }
}
