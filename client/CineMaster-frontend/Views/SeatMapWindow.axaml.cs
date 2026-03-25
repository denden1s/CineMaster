using Avalonia;
using Avalonia.Controls;
using CineMaster_frontend.Services;
namespace CineMaster_frontend.Views;

public partial class SeatMapWindow : BaseWindow
{
    private readonly ApiClient _apiClient;
    private readonly int _sessionId;
    private readonly HashSet<int> _selectedSeats = new();
    private bool _sentResult;

    private record SeatInfo(int Number, bool IsSold);

    /// <summary>
    /// Callback invoked when the window closes with the selected seats.
    /// </summary>
    public Action<List<int>>? SeatsSelectedCallback { get; set; }

    public SeatMapWindow(ApiClient apiClient, int sessionId, string filmName, DateTime showTime)
    {
        InitializeComponent();
        this.Closing += SeatMapWindow_Closing;

        _apiClient = apiClient;
        _sessionId = sessionId;

        TitleText.Text = $"{filmName} — {showTime:dd.MM.yyyy HH:mm}";
        SubtitleText.Text = $"Сеанс #{sessionId}";

        ApiClient.TicketSold += OnTicketSold;

        LoadSeats();
    }

    public List<int> SelectedSeats => _selectedSeats.OrderBy(x => x).ToList();

    private void SeatMapWindow_Closing(object? sender, WindowClosingEventArgs e)
    {
        SendResult();
        ApiClient.TicketSold -= OnTicketSold;
    }

    private void SendResult()
    {
        if (_sentResult)
            return;

        _sentResult = true;
        SeatsSelectedCallback?.Invoke(SelectedSeats);
    }

    private void OnTicketSold(int sessionId)
    {
        if (sessionId != _sessionId)
            return;

        // Refresh seat map when tickets are sold for this session.
        LoadSeats();
    }

    private async void LoadSeats()
    {
        StatusText.Text = "";
        _selectedSeats.Clear();
        _sentResult = false;

        try
        {
            var seatInfo = await _apiClient.GetSessionSeatInfoAsync(_sessionId);
            if (seatInfo == null)
            {
                StatusText.Text = "Не удалось получить данные о местах.";
                return;
            }

            BuildSeatButtons(seatInfo.Capacity, seatInfo.SoldSeats);
        }
        catch (Exception ex)
        {
            StatusText.Text = "Ошибка: " + ex.Message;
        }
    }

    private void BuildSeatButtons(int capacity, List<int> soldSeats)
    {
        SeatsPanel.Children.Clear();

        for (int seat = 1; seat <= capacity; seat++)
        {
            var isSold = soldSeats.Contains(seat);
            var button = new Button
            {
                Content = seat.ToString(),
                Width = 44,
                Height = 36,
                Margin = new Thickness(2),
                Tag = new SeatInfo(seat, isSold),
                Background = isSold ? Avalonia.Media.Brushes.Red : Avalonia.Media.Brushes.LightGray,
                Foreground = isSold ? Avalonia.Media.Brushes.White : Avalonia.Media.Brushes.Black,
                Cursor = isSold ? new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.No) : null
            };

            if (isSold)
            {
                button.IsHitTestVisible = false;
                ToolTip.SetTip(button, "Занято");
            }
            else
            {
                button.PointerPressed += OnSeatPointerPressed;
            }

            SeatsPanel.Children.Add(button);
        }
    }

    private void OnSeatPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is not Button btn)
            return;

        if (btn.Tag is not SeatInfo info)
            return;

        if (info.IsSold)
            return;

        var seat = info.Number;

        bool multiSelectModifier = e.KeyModifiers.HasFlag(Avalonia.Input.KeyModifiers.Control)
            || e.KeyModifiers.HasFlag(Avalonia.Input.KeyModifiers.Meta)
            || e.KeyModifiers.HasFlag(Avalonia.Input.KeyModifiers.Shift);

        if (!multiSelectModifier)
        {
            _selectedSeats.Clear();
        }

        if (_selectedSeats.Contains(seat))
            _selectedSeats.Remove(seat);
        else
            _selectedSeats.Add(seat);

        UpdateSeatButtonStyles();
    }

    private void UpdateSeatButtonStyles()
    {
        foreach (var child in SeatsPanel.Children.OfType<Button>())
        {
            if (child.Tag is not int seat)
                continue;

            // sold seats are already disabled
            if (child.Tag is not SeatInfo childInfo)
                continue;

            if (childInfo.IsSold)
            {
                child.Background = Avalonia.Media.Brushes.Red;
                child.Foreground = Avalonia.Media.Brushes.White;
                continue;
            }

            if (_selectedSeats.Contains(seat))
                child.Background = Avalonia.Media.Brushes.LightGreen;
            else
                child.Background = Avalonia.Media.Brushes.LightGray;
        }
    }
}
