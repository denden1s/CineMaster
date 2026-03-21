using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CineMaster_frontend.Converters
{
    public class ImageUrlToBitmapConverter : IValueConverter
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string imageUrl || string.IsNullOrWhiteSpace(imageUrl))
                return null;

            try
            {
                if (Uri.TryCreate(imageUrl, UriKind.Absolute, out var uri) &&
                    (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                {
                    var stream = HttpClient.GetStreamAsync(uri).GetAwaiter().GetResult();
                    return new Bitmap(stream);
                }

                // local path or relative file URI
                return new Bitmap(imageUrl);
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
