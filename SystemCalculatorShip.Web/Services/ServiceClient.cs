namespace SystemCalculatorShip.Web.Services;

using System.Net.Http.Headers;
using System.Text.Json;
using SystemCalculatorShip.Application.DTOs;

/// <summary>
/// Service client for communicating with the API.
/// </summary>
public class ServiceClient
{
    private readonly HttpClient _httpClient;
    private string? _token;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
            return JsonSerializer.Deserialize<T>(content, JsonOptions);
        }
        catch
        {
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
            return JsonSerializer.Deserialize<TResponse>(content, JsonOptions);
        }
        catch
        {
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
            return JsonSerializer.Deserialize<TResponse>(content, JsonOptions);
        }
        catch
        {
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
        catch
        {
            // Ignore in demo client
        }
    }

    public Task SetTokenAsync(string token)
    {
        _token = token;
        return Task.CompletedTask;
    }

    public Task ClearTokenAsync()
    {
        _token = null;
        return Task.CompletedTask;
    }

    private void AddAuthHeader(HttpRequestMessage request)
    {
        if (!string.IsNullOrEmpty(_token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }
}
