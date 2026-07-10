using System.Text.Json.Serialization;

namespace PhoneBook.Configuration;

public sealed class AppConfig
{
    public SmtpSettings? Smtp { get; set; }

    public TwilioSettings? Twilio { get; set; }
}

public sealed class SmtpSettings
{
    public string Host { get; set; } = string.Empty;

    public int Port { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string SenderEmail { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;

    public bool UseStartTls { get; set; } = true;

    [JsonIgnore]
    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(Host) &&
        Port is > 0 and <= 65535 &&
        !string.IsNullOrWhiteSpace(Username) &&
        !string.IsNullOrWhiteSpace(Password) &&
        !string.IsNullOrWhiteSpace(SenderEmail);
}

public sealed class TwilioSettings
{
    public string AccountSid { get; set; } = string.Empty;

    public string AuthToken { get; set; } = string.Empty;

    public string FromPhoneNumber { get; set; } = string.Empty;

    [JsonIgnore]
    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(AccountSid) &&
        !string.IsNullOrWhiteSpace(AuthToken) &&
        !string.IsNullOrWhiteSpace(FromPhoneNumber);
}
