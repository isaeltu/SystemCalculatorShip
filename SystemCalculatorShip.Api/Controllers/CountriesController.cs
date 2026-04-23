namespace SystemCalculatorShip.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;

/// <summary>
/// Countries management controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryManagementService _countryService;
    private readonly ILoggerService _logger;

    public CountriesController(ICountryManagementService countryService, ILoggerService logger)
    {
        _countryService = countryService;
        _logger = logger;
    }

    /// <summary>
    /// Get all countries (public endpoint)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CountryDto>>>> GetCountries()
    {
        try
        {
            var countries = await _countryService.GetCountriesAsync();
            var dtos = countries.Select(c => new CountryDto 
            { 
                Id = c.Id, 
                Code = c.Code, 
                Name = c.Name 
            });

            return Ok(new ApiResponse<IEnumerable<CountryDto>> 
            { 
                Success = true, 
                Data = dtos 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error getting countries", ex);
            return BadRequest(new ApiResponse<IEnumerable<CountryDto>> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }

    /// <summary>
    /// Create a new country (admin only)
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse<CountryDto>>> CreateCountry(CreateCountryRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<CountryDto> 
                { 
                    Success = false, 
                    Message = "Invalid input data" 
                });
            }

            var country = await _countryService.CreateCountryAsync(request);
            var dto = new CountryDto 
            { 
                Id = country.Id, 
                Code = country.Code, 
                Name = country.Name 
            };

            return Created($"/api/countries/{country.Id}", new ApiResponse<CountryDto> 
            { 
                Success = true, 
                Data = dto 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error creating country", ex);
            return BadRequest(new ApiResponse<CountryDto> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }

    /// <summary>
    /// Update a country (admin only)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<CountryDto>>> UpdateCountry(int id, UpdateCountryRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<CountryDto> 
                { 
                    Success = false, 
                    Message = "Invalid input data" 
                });
            }

            var country = await _countryService.UpdateCountryAsync(id, request);
            var dto = new CountryDto 
            { 
                Id = country.Id, 
                Code = country.Code, 
                Name = country.Name 
            };

            return Ok(new ApiResponse<CountryDto> 
            { 
                Success = true, 
                Data = dto 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error updating country", ex);
            return BadRequest(new ApiResponse<CountryDto> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }

    /// <summary>
    /// Delete a country (admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<string>>> DeleteCountry(int id)
    {
        try
        {
            await _countryService.DeleteCountryAsync(id);
            return Ok(new ApiResponse<string> 
            { 
                Success = true, 
                Message = "Country deleted successfully" 
            });
        }
        catch (Exception ex)
        {
            _logger.Error("Error deleting country", ex);
            return BadRequest(new ApiResponse<string> 
            { 
                Success = false, 
                Message = ex.Message 
            });
        }
    }
}
