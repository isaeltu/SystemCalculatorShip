namespace SystemCalculatorShip.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SystemCalculatorShip.Application.DTOs;
using SystemCalculatorShip.Application.Interfaces;

/// <summary>
/// Authentication controller for admin login
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticate user and return JWT token
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var (success, token, message) = await _authService.AuthenticateAsync(request.Username, request.Password);
        
        if (!success)
        {
            return Unauthorized(new LoginResponse 
            { 
                Success = false, 
                Message = message 
            });
        }

        return Ok(new LoginResponse 
        { 
            Success = true, 
            Token = token, 
            ExpiresIn = 3600,
            Message = "Login successful"
        });
    }
}
