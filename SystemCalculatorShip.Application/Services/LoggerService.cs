namespace SystemCalculatorShip.Application.Services;

using Interfaces;

/// <summary>
/// Logger service implementation
/// </summary>
public class LoggerService : ILoggerService
{
    public void Info(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
    }

    public void Error(string message, Exception? exception = null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
        if (exception != null)
        {
            Console.WriteLine($"Exception: {exception.Message}");
            Console.WriteLine($"Stack Trace: {exception.StackTrace}");
        }
        Console.ResetColor();
    }

    public void Warning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WARNING] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
        Console.ResetColor();
    }
}
