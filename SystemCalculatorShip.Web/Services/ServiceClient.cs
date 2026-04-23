namespace SystemCalculatorShip.Web.Services;

using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Text.Json;
using SystemCalculatorShip.Application.DTOs;

/// <summary>
/// Service client for communicating with the API.
/// </summary>
public class ServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedLocalStorage _localStorage;
    private string? _token;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ServiceClient(HttpClient httpClient, ProtectedLocalStorage localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    private async Task EnsureTokenLoadedAsync()
    {
        if (!string.IsNullOrEmpty(_token))
        {
            return;
        }

        var tokenResult = await _localStorage.GetAsync<string>("auth_token");
        _token = tokenResult.Success ? tokenResult.Value : null;
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            await EnsureTokenLoadedAsync();
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
            await EnsureTokenLoadedAsync();
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
            await EnsureTokenLoadedAsync();
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
            await EnsureTokenLoadedAsync();
            AddAuthHeader(request);
            await _httpClient.SendAsync(request);
        }
        catch
        {
            // Ignore in demo client
        }
    }

    public async Task<TResponse?> DeleteAsync<TResponse>(string endpoint)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
            await EnsureTokenLoadedAsync();
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

    public async Task SetTokenAsync(string token)
    {
        _token = token;
        await _localStorage.SetAsync("auth_token", token);
    }

    public async Task ClearTokenAsync()
    {
        _token = null;
        await _localStorage.DeleteAsync("auth_token");
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        await EnsureTokenLoadedAsync();
        return !string.IsNullOrEmpty(_token);
    }

    private void AddAuthHeader(HttpRequestMessage request)
    {
        if (!string.IsNullOrEmpty(_token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }
}
