namespace SystemCalculatorShip.Application.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Interfaces;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Authentication service implementation with JWT token support
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly string _secretKey = "your-secret-key-min-32-chars-long!";
    private const string AdminUsername = "admin";
    private const string AdminPassword = "admin";

    public async Task<(bool Success, string? Token, string Message)> AuthenticateAsync(string username, string password)
    {
        // Temporary hardcoded credentials for testing
        if (username == AdminUsername && password == AdminPassword)
        {
            string token = GenerateJwtToken(username);
            return (true, token, "Login successful");
        }

        return (false, null, "Invalid credentials");
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
                new Claim("role", "admin")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
