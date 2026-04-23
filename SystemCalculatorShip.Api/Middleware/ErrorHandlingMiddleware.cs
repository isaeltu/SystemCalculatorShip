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

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // ILoggerService is now injected here
    public async Task InvokeAsync(HttpContext context, ILoggerService logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            logger.Error("Unhandled exception", ex);
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
