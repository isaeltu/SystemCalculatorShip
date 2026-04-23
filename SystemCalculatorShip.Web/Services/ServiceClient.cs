namespace SystemCalculatorShip.Web.Services;

using Microsoft.JSInterop;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using SystemCalculatorShip.Application.DTOs;

/// <summary>
/// Service client for communicating with the API.
/// </summary>
public class ServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private string? _token;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ServiceClient(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            await LoadTokenAsync();
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
            await LoadTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(
                JsonSerializer.Serialize(data),
                System.Text.Encoding.UTF8,
                "application/json");
            AddAuthHeader(request);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return CreateFallbackErrorResponse<TResponse>(response.StatusCode, content);
            }

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
            await LoadTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Put, endpoint);
            request.Content = new StringContent(
                JsonSerializer.Serialize(data),
                System.Text.Encoding.UTF8,
                "application/json");
            AddAuthHeader(request);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return CreateFallbackErrorResponse<TResponse>(response.StatusCode, content);
            }

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
            await LoadTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
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
            await LoadTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
            AddAuthHeader(request);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return CreateFallbackErrorResponse<TResponse>(response.StatusCode, content);
            }

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
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "auth_token", token);
    }

    public async Task ClearTokenAsync()
    {
        _token = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "auth_token");
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        if (!string.IsNullOrEmpty(_token))
        {
            return true;
        }

        var token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "auth_token");
        if (!string.IsNullOrEmpty(token))
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return !string.IsNullOrEmpty(_token);
    }

    public async Task LoadTokenAsync()
    {
        if (!string.IsNullOrEmpty(_token))
        {
            return;
        }

        _token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "auth_token");
        if (!string.IsNullOrEmpty(_token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }

    private void AddAuthHeader(HttpRequestMessage request)
    {
        if (!string.IsNullOrEmpty(_token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }

    private static TResponse? CreateFallbackErrorResponse<TResponse>(HttpStatusCode statusCode, string content)
    {
        var responseType = typeof(TResponse);
        if (!responseType.IsGenericType || responseType.GetGenericTypeDefinition() != typeof(ApiResponse<>))
        {
            return default;
        }

        var dataType = responseType.GetGenericArguments()[0];
        var apiResponseType = typeof(ApiResponse<>).MakeGenericType(dataType);
        var instance = Activator.CreateInstance(apiResponseType);
        if (instance is null)
        {
            return default;
        }

        apiResponseType.GetProperty(nameof(ApiResponse<object>.Success), BindingFlags.Public | BindingFlags.Instance)
            ?.SetValue(instance, false);

        var fallbackMessage = statusCode == HttpStatusCode.Unauthorized
            ? "No autorizado (401). Inicia sesion nuevamente en /login."
            : !string.IsNullOrWhiteSpace(content)
                ? content
                : $"Request failed with status {(int)statusCode}.";

        apiResponseType.GetProperty(nameof(ApiResponse<object>.Message), BindingFlags.Public | BindingFlags.Instance)
            ?.SetValue(instance, fallbackMessage);

        return (TResponse)instance;
    }
}
