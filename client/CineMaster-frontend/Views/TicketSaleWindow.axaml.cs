using Avalonia.Controls;
using Avalonia.Collections;
using CineMaster_frontend.Models;
using CineMaster_frontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CineMaster_frontend.Views;

public partial class TicketSaleWindow : Window
{
    private readonly ApiClient _apiClient;
    private List<SessionOption> _options = new();
    private List<int> _selectedSeats = new();

    public void SetSessionAndSeats(int sessionId, List<int> seats)
    {
        if (_options == null || _options.Count == 0)
            return;

        var option = _options.FirstOrDefault(o => o.Session.ID == sessionId);
        if (option != null)
            SessionCombo.SelectedItem = option;

        SetSelectedSeats(seats);
    }

    public TicketSaleWindow() : this(new ApiClient(ServerConfig.LoadFromFile().BaseUrl)) { }

    public TicketSaleWindow(ApiClient apiClient)
    {
        InitializeComponent();
        _apiClient = apiClient;
        LoadSessions();
    }

    private async void LoadSessions()
    {
        try
        {
            var sessions = await _apiClient.GetSessionsAsync();
            var nextWeek = DateTime.Now.AddDays(7);
            _options = sessions
                .Where(s => s.ShowingTime >= DateTime.Now && s.ShowingTime <= nextWeek)
                .OrderBy(s => s.ShowingTime)
                .Select(s => new SessionOption(s))
                .ToList();

            if (SessionCombo.Items is System.Collections.IList list)
            {
                list.Clear();
                foreach (var option in _options)
                    list.Add(option);
            }
            if (_options.Count > 0)
                SessionCombo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            StatusText.Foreground = Avalonia.Media.Brushes.Red;
            StatusText.Text = "Не удалось загрузить сеансы: " + ex.Message;
        }
    }

    public void SetSelectedSeats(List<int> seats)
    {
        _selectedSeats = seats.ToList();

        if (_selectedSeats.Count > 0)
            SeatNumber.Text = Convert.ToString(_selectedSeats.First());
    }

    private async void SellButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        StatusText.Text = "";
        if (SessionCombo.SelectedItem is not SessionOption option)
        {
            StatusText.Foreground = Avalonia.Media.Brushes.Red;
            StatusText.Text = "Выберите сеанс.";
            return;
        }

        var seatsToSell = _selectedSeats.Count > 0 ? _selectedSeats : new List<int> { Convert.ToInt32(SeatNumber.Text) };

        var (soldWithIds, failed) = await SellTickets(option, seatsToSell);

        var sold = soldWithIds.Keys.ToList();

        if (sold.Count > 0)
        {
            StatusText.Foreground = Avalonia.Media.Brushes.Green;
            StatusText.Text = $"Продано место: {string.Join(", ", sold)}.";
        }

        if (failed.Count > 0)
        {
            StatusText.Foreground = Avalonia.Media.Brushes.Red;
            StatusText.Text += $" Не удалось продать: {string.Join(", ", failed)}.";
        }

        _selectedSeats.Clear();
    }

private async Task<(Dictionary<int, string> soldWithIds, List<int> failed)> SellTickets(SessionOption option, List<int> seatsToSell)
{
    var soldWithIds = new Dictionary<int, string>();
    var failed = new List<int>();

    foreach (var seat in seatsToSell.OrderBy(x => x))
    {
        var ticketId = await _apiClient.SellTicketAsync(option.Session.ID, seat);
        if (!string.IsNullOrEmpty(ticketId))
            soldWithIds[seat] = ticketId;
        else
            failed.Add(seat);
    }

    return (soldWithIds, failed);
}

private async void SellAndPrintQrButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
{
    StatusText.Text = "";
    if (SessionCombo.SelectedItem is not SessionOption option)
    {
        StatusText.Foreground = Avalonia.Media.Brushes.Red;
        StatusText.Text = "Выберите сеанс.";
        return;
    }

    var seatsToSell = _selectedSeats.Count > 0 ? _selectedSeats : new List<int> { Convert.ToInt32(SeatNumber.Text) };

    var (soldWithIds, failed) = await SellTickets(option, seatsToSell);

    if (soldWithIds.Count > 0)
    {
        var qrData = new TicketQrData
        {
            TicketId = string.Join(",", soldWithIds.Values),
            SessionId = option.Session.ID,
            FilmName = option.Session.FilmName,
            ShowingTime = option.Session.ShowingTime,
            HallName = option.Session.HallName,
            Seats = soldWithIds.Keys.ToArray()
        };

        var qrWindow = new TicketQrWindow(qrData);
        qrWindow.Show();
    }

    var sold = soldWithIds.Keys.ToList();

    if (sold.Count > 0)
    {
        StatusText.Foreground = Avalonia.Media.Brushes.Green;
        StatusText.Text = $"Продано место: {string.Join(", ", sold)}.";
    }

    if (failed.Count > 0)
    {
        StatusText.Foreground = Avalonia.Media.Brushes.Red;
        StatusText.Text += $" Не удалось продать: {string.Join(", ", failed)}.";
    }

    _selectedSeats.Clear();
}
}
