namespace SystemCalculatorShip.Domain.Entities;

/// <summary>
/// Represents an administrative user for managing countries and tariffs
/// </summary>
public class AdminUser
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
