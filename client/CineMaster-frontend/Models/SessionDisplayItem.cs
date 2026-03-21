using Avalonia.Media.Imaging;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CineMaster_frontend.Models;

public class SessionDisplayItem : INotifyPropertyChanged
{
    public CinemaSessionDto Session { get; set; }
    public string FilmName => Session.FilmName;
    public DateTime ShowingTime => Session.ShowingTime;
    public string HallName => Session.HallName;
    public int SoldSeats => Session.SoldSeats;
    public int RemainingSeats => Session.RemainingSeats;
    public string ImageUrl => Session.ImageUrl;

    private Bitmap? _image;
    public Bitmap? Image
    {
        get => _image;
        private set
        {
            if (_image != value)
            {
                _image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public SessionDisplayItem(CinemaSessionDto session)
    {
        Session = session;
        _ = LoadImageAsync();
    }

    private async Task LoadImageAsync()
    {
        if (string.IsNullOrWhiteSpace(ImageUrl))
            return;

        try
        {
            if (Uri.TryCreate(ImageUrl, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
                var data = await client.GetByteArrayAsync(uri);
                using var stream = new MemoryStream(data);
                Image = new Bitmap(stream);
            }
            else
            {
                Image = new Bitmap(ImageUrl);
            }
        }
        catch
        {
            Image = null;
        }
    }
}
