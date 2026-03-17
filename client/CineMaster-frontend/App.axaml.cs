using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CineMaster_frontend.Models;
using CineMaster_frontend.Views;

namespace CineMaster_frontend;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var config = ServerConfig.LoadFromFile();
            desktop.MainWindow = new LoginWindow(config.BaseUrl);
        }

        base.OnFrameworkInitializationCompleted();
    }

}