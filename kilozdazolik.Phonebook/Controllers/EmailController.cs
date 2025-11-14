using kilozdazolik.Phonebook.Models;
using kilozdazolik.Phonebook.Services;

namespace kilozdazolik.Phonebook.Controllers;

public class EmailController
{
    private static EmailService _emailService;
    
    public static void Initialize(EmailService emailService)
    {
        _emailService = emailService;
    }

    public static void SendEmail(Contact contact)
    {
        if (contact == null)
        {
            throw new ArgumentNullException(nameof(contact));
        }
        var subject = _emailService.GetSubjectInput();
        var body = _emailService.GetBodyInput();
        _emailService.SendEmail(contact.Email, subject, body);
    }
    
}