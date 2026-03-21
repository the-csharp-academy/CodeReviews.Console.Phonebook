using System.Text.Json.Serialization;

namespace phoneBook._0lcm.Models;

public class SmsResponseClass
{
    [JsonPropertyName("success")] public required bool Success { get; set; }

    [JsonPropertyName("quotaRemaining")] public int QuotaRemaining { get; set; }

    [JsonPropertyName("error")] public string? Error { get; set; }
}