using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using PhoneBook.Controllers;
using PhoneBook.Models;
using PhoneBook.Views;
using Spectre.Console;

namespace PhoneBook;

internal class UserService
{
    private static UserController userController = new UserController();
    private static string? gmailAddress = null;
    private static string? gmailKey = null;
    internal static void AddUser()
    {
        var name = Validator.GetStringInput("Enter your name: ");
        var email = Validator.GetEmailInput();
        var phoneNumber = Validator.GetPhoneNumber();
        var address = Validator.GetStringInput("Enter your address: ");
        var category = Validator.ChooseCategory();

        var newUser = new User()
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            Category = category,
        };

        userController.Add(newUser);
    }

    internal static void GetAll()
    {
        var list = userController.GetAll();
        TableVisualisation.PrintUsersList(list);
    }

    internal static void GetUser()
    {
        var user = SelectUser();

        if (user == null)
        {
            AnsiConsole.MarkupLine("[red]No users found[/]");
            Console.ReadKey();
            return;
        }

        TableVisualisation.ShowUser(user);
    }

    internal static void GetUsersBycategory()
    {
        var category = Validator.ChooseCategory();

        var list = userController.GetByCategories(category);

        TableVisualisation.PrintUsersList(list);
    }
    internal static void RemoveUser()
    {
        var user = SelectUser();

        if (user == null)
        {
            AnsiConsole.MarkupLine("[red]No users found[/]");
            Console.ReadKey();
            return;
        }

        userController.Remove(user);
    }

    internal static void UpdateUser()
    {
        var user = SelectUser();

        if (user is null)
        {
            AnsiConsole.MarkupLine("[red]No users found[/]");
            Console.ReadKey();
            return;
        }

        user.Name = AnsiConsole.Confirm("Update user name? ")
            ? Validator.GetStringInput("Enter your new name: ")
            : user.Name;

        user.Email = AnsiConsole.Confirm("Update user email? ")
            ? Validator.GetEmailInput()
            : user.Email;

        user.PhoneNumber = AnsiConsole.Confirm("Update user's phone number? ")
            ? Validator.GetPhoneNumber()
            : user.PhoneNumber;

        user.Address = AnsiConsole.Confirm("Update user's address? ")
            ? Validator.GetStringInput("Enter your new address: ")
            : user.Address;

        user.Category = AnsiConsole.Confirm("Update user's category? ")
            ? Validator.ChooseCategory()
            : user.Category;

        userController.Update(user);
    }

    internal static User? SelectUser()
    {
        var list = userController.GetAll();

        if (!list.Any())
        {
            return null;
        }

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<User>()
            .Title("Choose your user: ")
            .UseConverter(u => u.Name)
            .AddChoices(list));

        var user = list.Single(x => x.Id == choice.Id);

        return user;
    }

    internal static void SendEmail()
    {
        try
        {
            Panel panel = new(new Rows(
                new Markup(@"[yellow]You can send an email with a Gmail account.
For this to work you need to:  
1 - Enable 2FA in your Google Security Settings.
2 - Go to the App Passwords page.\r\n
3 - Select 'Mail' and your device, then click 'Generate' to get a 16-character code.[/]

[red]Any email or password entered won't be saved in a database. It will only persist in memory (as soon as you close the app is gone).[/]").Centered())
);
            AnsiConsole.Write(panel);

            Console.ReadKey();
            bool useSameEmail = true;
            bool useSameKey = true;

            if (!gmailAddress.IsNullOrEmpty())
                useSameEmail = AnsiConsole.Confirm($"Do you wish to use the current email? {gmailAddress}");

            if (!gmailKey.IsNullOrEmpty())
                useSameKey = AnsiConsole.Confirm($"Do you wish to use the same password? {gmailKey}");

            gmailAddress ??= Validator.GetEmailInput();
            gmailKey ??= Validator.GetStringInput("Enter your 16-character code: ");

            if (!useSameEmail)
                gmailAddress = Validator.GetEmailInput();

            if (!useSameKey)
                gmailKey = Validator.GetStringInput("Enter your 16-character code: ");

            AnsiConsole.MarkupLine("Select the user you want to send en email to");
            var user = SelectUser();
            var subject = Validator.GetStringInput("Select the [red]subject[/] of the email: ");
            var body = Validator.GetStringInput("Select the [blue]body[/] of the email: ");
            var name = Validator.GetStringInput("Enter your name: ");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(name, gmailAddress));
            message.To.Add(new MailboxAddress($"{user.Name}", $"{user.Email}"));
            message.Subject = $"{subject} [{user.Category}]";

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var client = new SmtpClient();

            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(gmailAddress, gmailKey);

            client.Send(message);

            AnsiConsole.MarkupLine("[green]Email sent successfully![/]");
            client.Disconnect(true);

            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine(ex.Message);
            Console.ReadKey();
        }
    }
}
