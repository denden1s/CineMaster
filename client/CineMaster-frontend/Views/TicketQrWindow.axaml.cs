using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CineMaster_frontend.Models;
using QRCoder;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using Avalonia.Input;
using System.Diagnostics;

namespace CineMaster_frontend.Views;

public partial class TicketQrWindow : BaseWindow
{
    private byte[]? _qrPng;
    private TicketQrData? _data;

    public TicketQrWindow()
    {
        InitializeComponent();
    }

    public TicketQrWindow(TicketQrData data) : this()
    {
        SetData(data);
    }

    public void SetData(TicketQrData data)
    {
        _data = data;

        var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        var payload = JsonSerializer.Serialize(data, options);
        var generator = new QRCodeGenerator();
        using var qrData = generator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrData);
        _qrPng = qrCode.GetGraphic(20);

        QrImage.Source = new Bitmap(new MemoryStream(_qrPng));

        TicketInfoText.Text =
            $"Фильм: {data.FilmName}\n" +
            $"Сеанс: {data.ShowingTime:dd.MM.yyyy HH:mm}\n" +
            $"Зал: {data.HallName}\n" +
            $"Места: {string.Join(", ", data.Seats)}\n" +
            $"ID билета: {data.TicketId}";
    }

    private async void SavePng_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var dlg = new SaveFileDialog
        {
            Filters = new System.Collections.Generic.List<FileDialogFilter>
            {
                new FileDialogFilter
                {
                    Name = "PNG",
                    Extensions = new System.Collections.Generic.List<string> { "png" }
                }
            },
            DefaultExtension = "png",
            InitialFileName = $"ticket_{_data.TicketId}.png"
        };

        var fileName = await dlg.ShowAsync(this);
        if (!string.IsNullOrEmpty(fileName) && _qrPng != null)
        {
            await File.WriteAllBytesAsync(fileName, _qrPng);
        }
    }

    private void Close_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}

public class TicketQrData
{
    public string TicketId { get; set; } = string.Empty;
    public int SessionId { get; set; }
    public string FilmName { get; set; } = string.Empty;
    public DateTime ShowingTime { get; set; }
    public string HallName { get; set; } = string.Empty;
    public int[] Seats { get; set; } = Array.Empty<int>();
}
