namespace SystemCalculatorShip.Api.Middleware;

using SystemCalculatorShip.Application.DTOs;
using SystemCalculatorShip.Application.Interfaces;
using System.Text.Json;

/// <summary>
/// Middleware for global error handling
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error("Unhandled exception", ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new ApiResponse<string>
        {
            Success = false,
            Message = ex.Message
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
