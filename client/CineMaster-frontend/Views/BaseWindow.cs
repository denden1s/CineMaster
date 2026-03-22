using Avalonia.Controls;
using Avalonia.Input;
using System.Diagnostics;
using System.IO;

namespace CineMaster_frontend.Views;

public class BaseWindow : Window
{
    public BaseWindow()
    {
        this.KeyBindings.Add(new KeyBinding
        {
            Gesture = new KeyGesture(Key.F1),
            Command = new DelegateCommand(OpenHelp)
        });
    }

    private void OpenHelp()
    {
        string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "help.pdf");
        if (File.Exists(pdfPath))
        {
            Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
        }
    }
}