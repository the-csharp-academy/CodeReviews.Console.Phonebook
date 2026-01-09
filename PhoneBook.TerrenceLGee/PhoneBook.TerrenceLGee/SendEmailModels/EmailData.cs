namespace PhoneBook.TerrenceLGee.SendEmailModels;

public class EmailData
{
    public string SenderName { get; set; } = string.Empty;
    public string RecieverName { get; set; } = string.Empty;
    public string RecieverEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
