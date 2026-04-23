namespace SystemCalculatorShip.Api.Services;

using SystemCalculatorShip.Application.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

/// <summary>
/// Service client for communicating with the API
/// </summary>
public class ServiceClient
{
    private readonly HttpClient _httpClient;
    private string? _token;

    public ServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _token = null; // Would load from storage in real Blazor app
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            AddAuthHeader(request);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return default;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetAsync: {ex.Message}");
            return default;
        }
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(
                JsonSerializer.Serialize(data),
                System.Text.Encoding.UTF8,
                "application/json");
            AddAuthHeader(request);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in PostAsync: {ex.Message}");
            return default;
        }
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, endpoint);
            request.Content = new StringContent(
                JsonSerializer.Serialize(data),
                System.Text.Encoding.UTF8,
                "application/json");
            AddAuthHeader(request);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in PutAsync: {ex.Message}");
            return default;
        }
    }

    public async Task DeleteAsync(string endpoint)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
            AddAuthHeader(request);

            await _httpClient.SendAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteAsync: {ex.Message}");
        }
    }

    public async Task SetTokenAsync(string token)
    {
        _token = token;
        // In a real Blazor app, save to localStorage
    }

    public async Task ClearTokenAsync()
    {
        _token = null;
        // In a real Blazor app, remove from localStorage
    }

    private void AddAuthHeader(HttpRequestMessage request)
    {
        if (!string.IsNullOrEmpty(_token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }
}
