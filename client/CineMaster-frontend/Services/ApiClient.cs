using System.Net.Http.Json;
using System.Text.Json;
using CineMaster_frontend.Models;

namespace CineMaster_frontend.Services;

public class ApiClient : IDisposable
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public static event Action<int>? TicketSold;

    public ApiClient(string baseUrl)
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri(baseUrl.TrimEnd('/')),
            Timeout = TimeSpan.FromSeconds(20)
        };
    }

    public async Task<UserDto?> AuthenticateAsync(string login, string password)
    {
        var hashed = Encryption.HashPassword(login, password);
        var payload = new { Login = login, Password = hashed };
        using var response = await _http.PostAsJsonAsync("api/Users/auth", payload);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserDto>(_jsonOptions);
    }

    public async Task<List<CinemaSessionDto>> GetSessionsAsync(CancellationToken cancellationToken = default)
    {
        using var response = await _http.GetAsync("api/CinemaSession", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<CinemaSessionDto>>(_jsonOptions, cancellationToken)
               ?? new List<CinemaSessionDto>();
    }

    // TODO: show ticket number like qr code for printing
    public async Task<string?> SellTicketAsync(int sessionId, int seatNumber, CancellationToken cancellationToken = default)
    {
        var ticket = new { SessionID = sessionId, SitNumber = seatNumber };
        using var response = await _http.PostAsJsonAsync("api/CinemaSession/ticket", ticket, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var ticketData = await response.Content.ReadAsStringAsync();
            if (ticketData != null)
                TicketSold?.Invoke(sessionId);
            return ticketData;
        }
        return null;
    }

    private class TicketResponse
    {
        public string Id { get; set; } = string.Empty;
    }

    public async Task<SessionSeatInfoDto?> GetSessionSeatInfoAsync(int sessionId, CancellationToken cancellationToken = default)
    {
        using var response = await _http.GetAsync($"api/CinemaSession/{sessionId}/seats", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<SessionSeatInfoDto>(_jsonOptions, cancellationToken);
    }

    public void Dispose() => _http.Dispose();
}
