using Avalonia.Controls;
using CineMaster_frontend.Models;
using CineMaster_frontend.Services;
using System;

namespace CineMaster_frontend.Views;

public partial class LoginWindow : Window
{
    private readonly ApiClient _apiClient;
    private TextBox _loginBox = null!;
    private TextBox _passwordBox = null!;
    private Button _loginButton = null!;
    private TextBlock _statusText = null!;

    public LoginWindow() : this(ServerConfig.LoadFromFile().BaseUrl) { }

    public LoginWindow(string baseUrl)
    {
        InitializeComponent();
        _apiClient = new ApiClient(baseUrl);

        _loginBox = this.FindControl<TextBox>("LoginBox")!;
        _passwordBox = this.FindControl<TextBox>("PasswordBox")!;
        _loginButton = this.FindControl<Button>("LoginButton")!;
        _statusText = this.FindControl<TextBlock>("StatusText")!;
    }

    private async void LoginButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _statusText.Text = "";
        _loginButton.IsEnabled = false;
        try
        {
            var login = _loginBox.Text?.Trim();
            var password = _passwordBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                _statusText.Text = "Логин и пароль обязательны.";
                return;
            }
            var user = await _apiClient.AuthenticateAsync(login, password);
            if (user == null)
            {
                _statusText.Text = "Неверный логин или пароль.";
                return;
            }

            if (user.Role == UserRole.kUser)
            {
                var tickets = new TicketSaleWindow(_apiClient);
                var movies = new MoviesWindow(_apiClient, tickets);
                movies.Show();
                tickets.Show();
                Close();
                return;
            }

            var other = new OtherRoleWindow(user.Role.ToString());
            other.Show();
            Close();
        }
        catch (Exception ex)
        {
            _statusText.Text = "Ошибка при подключении к серверу: " + ex.Message;
        }
        finally
        {
            _loginButton.IsEnabled = true;
        }
    }
}
