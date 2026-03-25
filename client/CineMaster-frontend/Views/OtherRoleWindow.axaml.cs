using System.Diagnostics;
using System.IO;
namespace CineMaster_frontend.Views;

public partial class OtherRoleWindow : BaseWindow
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

    // private void OpenHelp()
    // {
    //     string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "help.pdf");
    //     if (File.Exists(pdfPath))
    //     {
    //         Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
    //     }
    // }

    private void CloseButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}
