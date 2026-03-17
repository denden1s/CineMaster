using Avalonia;
using Avalonia.Controls;
using Avalonia.Collections;
using CineMaster_frontend.Models;
using CineMaster_frontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineMaster_frontend.Views;

public partial class MoviesWindow : Window
{
    private readonly ApiClient _apiClient;
    private readonly TicketSaleWindow _ticketSaleWindow;

    public MoviesWindow() : this(new ApiClient(ServerConfig.LoadFromFile().BaseUrl), new TicketSaleWindow()) { }

    public MoviesWindow(ApiClient apiClient, TicketSaleWindow ticketSaleWindow)
    {
        InitializeComponent();
        _apiClient = apiClient;
        _ticketSaleWindow = ticketSaleWindow;
        LoadSessions();
    }

    private async void LoadSessions()
    {
        try
        {
            var sessions = await _apiClient.GetSessionsAsync();
            var nextWeek = DateTime.Now.AddDays(7);
            var filtered = sessions
                .Where(s => s.ShowingTime >= DateTime.Now && s.ShowingTime <= nextWeek)
                .OrderBy(s => s.ShowingTime)
                .Select(s => new SessionDisplayItem(s))
                .ToList();

            if (SessionsList.Items is System.Collections.IList list)
            {
                list.Clear();
                foreach (var item in filtered)
                    list.Add(item);
            }
        }
        catch (Exception ex)
        {
            await MessageBox.Show(this, "Не удалось загрузить сеансы: " + ex.Message, "Ошибка");
        }
    }

    private async void SelectSeat_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.DataContext is not SessionDisplayItem item)
            return;

        var seatWindow = new SeatMapWindow(_apiClient, item.Session.ID, item.FilmName, item.ShowingTime)
        {
            SeatsSelectedCallback = selectedSeats =>
            {
                if (selectedSeats != null && selectedSeats.Count > 0)
                {
                    _ticketSaleWindow.SetSessionAndSeats(item.Session.ID, selectedSeats);
                    _ticketSaleWindow.Activate();
                }
            }
        };

        seatWindow.Show();
    }

}

internal static class MessageBox
{
    public static async Task Show(Window owner, string message, string title)
    {
        var dialog = new Window
        {
            Title = title,
            Width = 400,
            Height = 160,
            Content = new StackPanel
            {
                Margin = new Thickness(10),
                Children =
                {
                    new TextBlock { Text = message, TextWrapping = Avalonia.Media.TextWrapping.Wrap },
                    new Button
                    {
                        Content = "ОК",
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                        Margin = new Thickness(0,10,0,0)
                    }
                }
            }
        };

        if (dialog.Content is StackPanel sp && sp.Children.LastOrDefault() is Button okButton)
        {
            okButton.Click += (_, _) => dialog.Close();
        }

        await dialog.ShowDialog(owner);
    }
}
