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
        SelectedSeatsText.Text = "-";
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
        SelectedSeatsText.Text = _selectedSeats.Count == 0 ? "-" : string.Join(", ", _selectedSeats);

        if (_selectedSeats.Count > 0)
            SeatNumber.Value = _selectedSeats.First();
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

        var seatsToSell = _selectedSeats.Count > 0 ? _selectedSeats : new List<int> { (int)SeatNumber.Value };
        var sold = new List<int>();
        var failed = new List<int>();

        try
        {
            foreach (var seat in seatsToSell.OrderBy(x => x))
            {
                var ok = await _apiClient.SellTicketAsync(option.Session.ID, seat);
                if (ok)
                    sold.Add(seat);
                else
                    failed.Add(seat);
            }

            if (sold.Count > 0)
            {
                StatusText.Foreground = Avalonia.Media.Brushes.Green;
                StatusText.Text = $"Проданы места: {string.Join(", ", sold)}.";
            }

            if (failed.Count > 0)
            {
                StatusText.Foreground = Avalonia.Media.Brushes.Red;
                StatusText.Text += $" Не удалось продать: {string.Join(", ", failed)}.";
            }

            // reset selection after sale
            _selectedSeats.Clear();
            SelectedSeatsText.Text = "-";
        }
        catch (Exception ex)
        {
            StatusText.Foreground = Avalonia.Media.Brushes.Red;
            StatusText.Text = "Ошибка: " + ex.Message;
        }
    }
}
