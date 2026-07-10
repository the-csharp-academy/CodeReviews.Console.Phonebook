using System.Text.Json;

namespace PhoneBook.Configuration;

public static class ConfigManager
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    private static string ConfigPath => Path.Combine(Environment.CurrentDirectory, "config.json");

    public static AppConfig Load()
    {
        if (!File.Exists(ConfigPath))
        {
            return new AppConfig();
        }

        try
        {
            return JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(ConfigPath), SerializerOptions)
                ?? new AppConfig();
        }
        catch (JsonException)
        {
            return new AppConfig();
        }
    }

    public static void Save(AppConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        File.WriteAllText(ConfigPath, JsonSerializer.Serialize(config, SerializerOptions));
    }
}
