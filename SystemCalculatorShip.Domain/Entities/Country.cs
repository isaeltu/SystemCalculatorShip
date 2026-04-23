namespace SystemCalculatorShip.Domain.Entities;

/// <summary>
/// Represents a country with shipping tariffs
/// </summary>
public class Country
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation property
    public ICollection<Tariff> Tariffs { get; set; } = new List<Tariff>();
}
