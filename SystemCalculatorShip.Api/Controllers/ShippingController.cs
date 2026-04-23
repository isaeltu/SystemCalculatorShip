namespace SystemCalculatorShip.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;

/// <summary>
/// Shipping calculation controller (public endpoint)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ShippingController : ControllerBase
{
    private readonly ITariffCalculationService _tariffCalculationService;
    private readonly ILoggerService _logger;

    public ShippingController(ITariffCalculationService tariffCalculationService, ILoggerService logger)
    {
        _tariffCalculationService = tariffCalculationService;
        _logger = logger;
    }

    /// <summary>
    /// Calculate shipping cost based on weight, country, and tariff type
    /// </summary>
    [HttpPost("calculate")]
    public async Task<ActionResult<ApiResponse<CalculateShippingResponse>>> Calculate(CalculateShippingRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<CalculateShippingResponse> 
                { 
                    Success = false, 
                    Message = "Invalid input data" 
                });
            }

            var result = await _tariffCalculationService.CalculateAsync(request);
            _logger.Info($"Shipping calculated: {result.Weight}kg x ${result.RatePerKg}/kg = ${result.TotalCost}");
            
            return Ok(new ApiResponse<CalculateShippingResponse> 
            { 
                Success = true, 
                Data = result 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error calculating shipping", ex);
            return BadRequest(new ApiResponse<CalculateShippingResponse> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }
}
