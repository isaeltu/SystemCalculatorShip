namespace SystemCalculatorShip.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemCalculatorShip.Application.DTOs;
using SystemCalculatorShip.Application.Interfaces;
using SystemCalculatorShip.Domain.Entities;

/// <summary>
/// Tariffs management controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TariffsController : ControllerBase
{
    private readonly ITariffManagementService _tariffService;
    private readonly ILoggerService _logger;

    public TariffsController(ITariffManagementService tariffService, ILoggerService logger)
    {
        _tariffService = tariffService;
        _logger = logger;
    }

    /// <summary>
    /// Get all tariffs (admin only)
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ApiResponse<IEnumerable<TariffDto>>>> GetTariffs()
    {
        try
        {
            var tariffs = await _tariffService.GetTariffsAsync();
            var dtos = tariffs.Select(MapToDto);

            return Ok(new ApiResponse<IEnumerable<TariffDto>> 
            { 
                Success = true, 
                Data = dtos 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error getting tariffs", ex);
            return BadRequest(new ApiResponse<IEnumerable<TariffDto>> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }

    /// <summary>
    /// Get tariffs by country code (public endpoint)
    /// </summary>
    [HttpGet("country/{countryCode}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<TariffDto>>>> GetTariffsByCountry(string countryCode)
    {
        try
        {
            // This would require a country lookup - simplified for now
            return Ok(new ApiResponse<IEnumerable<TariffDto>> 
            { 
                Success = true, 
                Data = new List<TariffDto>() 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error getting tariffs", ex);
            return BadRequest(new ApiResponse<IEnumerable<TariffDto>> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }

    /// <summary>
    /// Create a new tariff (admin only)
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse<TariffDto>>> CreateTariff(CreateTariffRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<TariffDto> 
                { 
                    Success = false, 
                    Message = "Invalid input data" 
                });
            }

            var tariff = await _tariffService.CreateTariffAsync(request);
            var dto = MapToDto(tariff);

            return Created($"/api/tariffs/{tariff.Id}", new ApiResponse<TariffDto> 
            { 
                Success = true, 
                Data = dto 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error creating tariff", ex);
            return BadRequest(new ApiResponse<TariffDto> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }

    /// <summary>
    /// Update a tariff (admin only)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<TariffDto>>> UpdateTariff(int id, UpdateTariffRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<TariffDto> 
                { 
                    Success = false, 
                    Message = "Invalid input data" 
                });
            }

            var tariff = await _tariffService.UpdateTariffAsync(id, request);
            var dto = MapToDto(tariff);

            return Ok(new ApiResponse<TariffDto> 
            { 
                Success = true, 
                Data = dto 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error updating tariff", ex);
            return BadRequest(new ApiResponse<TariffDto> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }

    /// <summary>
    /// Delete a tariff (admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<string>>> DeleteTariff(int id)
    {
        try
        {
            await _tariffService.DeleteTariffAsync(id);
            return Ok(new ApiResponse<string> 
            { 
                Success = true, 
                Message = "Tariff deleted successfully" 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error deleting tariff", ex);
            return BadRequest(new ApiResponse<string> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }

    private TariffDto MapToDto(Tariff tariff) => new()
    {
        Id = tariff.Id,
        CountryId = tariff.CountryId,
        CountryName = tariff.Country?.Name ?? string.Empty,
        Type = tariff.Type,
        RatePerKg = tariff.RatePerKg,
        Currency = tariff.Currency
    };
}
