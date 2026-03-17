using Avalonia.Controls;

namespace CineMaster_frontend.Views;

public partial class OtherRoleWindow : Window
{
    public OtherRoleWindow()
    {
        InitializeComponent();
        MessageText.Text = "Недостаточно прав для открытия кассовой панели.";
    }

    public OtherRoleWindow(string roleName) : this()
    {
        MessageText.Text = $"Ваша роль: {roleName}. Для кассира доступны окна с фильмами и продажей билетов.";
    }

    private void CloseButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}
