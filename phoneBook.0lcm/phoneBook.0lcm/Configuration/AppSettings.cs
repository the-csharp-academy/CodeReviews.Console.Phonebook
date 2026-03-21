namespace phoneBook._0lcm.Configuration;

public class AppSettings
{
    public string? UserEmail { get; set; }
    public string? UserEmailDefault { get; } = null;
    public string? UserName { get; set; }
    public string? UserNameDefault { get; } = null;
    public string? UserAppPassword { get; set; }
    public string? UserAppPasswordDefault { get; } = null;
    public string TextBeltKey { get; set; } = "textbelt_test";
    public string TextBeltKeyDefault { get; } = "textbelt_test";
}