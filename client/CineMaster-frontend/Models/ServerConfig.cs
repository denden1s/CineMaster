using System.IO;
using System.Text.Json;

namespace CineMaster_frontend.Models;

public class ServerConfig
{
    public string BaseUrl { get; set; } = "http://localhost:5067";

    public static ServerConfig LoadFromFile(string? configPath = null)
    {
        try
        {
            var baseDir = AppContext.BaseDirectory;
            var path = configPath ?? Path.Combine(baseDir, "serverConfig.json");
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var cfg = JsonSerializer.Deserialize<ServerConfig>(json);
                if (cfg != null && !string.IsNullOrWhiteSpace(cfg.BaseUrl))
                    return cfg;
            }
        }
        catch
        {
            // Ignore errors and fall back to defaults
        }

        return new ServerConfig();
    }
}
