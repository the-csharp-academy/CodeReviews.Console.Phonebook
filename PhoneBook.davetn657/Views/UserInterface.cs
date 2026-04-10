using Microsoft.EntityFrameworkCore;
using PhoneBook.davetn657.Controllers;
using PhoneBook.davetn657.Data;
using Spectre.Console;
using System.Net;
using System.Net.Mail;

namespace PhoneBook.davetn657.Views;
public class UserInterface
{
    private readonly PhoneBookContext _dBContext;
    private readonly OptionUtils _optionUtils;
    private readonly Validation _validation;

    public UserInterface(PhoneBookContext phoneBookContext, OptionUtils optionUtils, Validation validation)
    {
        _dBContext = phoneBookContext;
        _optionUtils = optionUtils;
        _validation = validation;
    }

    internal async void Start()
    {
        while (true)
        {
            var title = "Main Menu";
            TitleCard(title);

            var menuOptions = _optionUtils.GetAllStringValues(typeof(MainMenuOptions));

            var contactNames = new Dictionary<string, PhoneProperties>();
            var contacts = await _dBContext.PhoneProperties
                .OrderBy(p => p.Category)
                .ToListAsync();

            var selectionPrompt = new SelectionPrompt<string>();
            var currentCategory = string.Empty;
            var categories = new List<string>();

            foreach (var contact in contacts)
            {
                if (currentCategory != contact.Category)
                {
                    if(currentCategory != string.Empty) categories.Add(currentCategory);

                    selectionPrompt.AddChoiceGroup(currentCategory, menuOptions);
                    currentCategory = contact.Category;
                    menuOptions.Clear();
                }

                contactNames.Add(contact.Name, contact);
                menuOptions.Add(contact.Name);
            }

            categories.Add(currentCategory);
            selectionPrompt.AddChoiceGroup(currentCategory, menuOptions);

            var selected = AnsiConsole.Prompt(selectionPrompt);

            switch (_optionUtils.GetEnumValue(selected, typeof(MainMenuOptions)))
            {
                case MainMenuOptions.Exit:
                    return;
                case MainMenuOptions.AddContacts:
                    AddContacts();
                    break;
                default:
                    EditContact(contactNames[selected], categories);
                    break;
            }
        }
    }

    private async void AddContacts()
    {
        var title = "Add a new Contact!";
        TitleCard(title);

        var newContact = new PhoneProperties();

        var name = EnterName(title);
        if (name.ToLower() == "r") return;
        newContact.Name = name;

        var menuOptions = _optionUtils.GetAllStringValues(typeof(AddNewContactOptions));
        var selected = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
        var enumSelected = _optionUtils.GetEnumValue(selected, typeof(AddNewContactOptions));

        if(enumSelected.Equals(AddNewContactOptions.Return)) return;

        if(enumSelected is AddNewContactOptions.Both or AddNewContactOptions.PhoneNumber)
        {
            newContact.PhoneNumber = EnterPhoneNumber(title);
        }

        if (enumSelected is AddNewContactOptions.Both or AddNewContactOptions.Email)
        {
            newContact.Email = EnterEmail(title);
        }

        _dBContext.Add(newContact);
        await _dBContext.SaveChangesAsync();

        AnsiConsole.WriteLine($"Successfully added {newContact.Name} to contacts");
        AnsiConsole.Prompt(new TextPrompt<string>("Press any key to return").AllowEmpty());
    }

    private async void EditContact(PhoneProperties contact, List<string> categories)
    {
        var title = $"Edit: {contact.Name}";
        TitleCard(title);

        var contactDetails = new Table();

        contactDetails.AddColumns(new string[] { "Name", "Phone Number", "Email" });
        contactDetails.AddRow(contact.Name, contact.PhoneNumber, contact.Email);

        AnsiConsole.Write(contactDetails);

        var menuOptions = _optionUtils.GetAllStringValues(typeof(EditContactOptions));
        var selected = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));

        switch (_optionUtils.GetEnumValue(selected, typeof(EditContactOptions)))
        {
            case EditContactOptions.Return:
                return;
            case EditContactOptions.SendEmail:
                SendEmail(contact);
                break;
            case EditContactOptions.AddGroup:
                AddContactToCategory(contact, categories);
                break;
            case EditContactOptions.Delete:
                _dBContext.Remove(contact);
                break;
            default:
                ChangeDetails(selected, contact);
                break;
        }

        await _dBContext.SaveChangesAsync();
    }

    private async void AddContactToCategory(PhoneProperties contact, List<string> categories)
    {
        TitleCard($"Add {contact.Name} to");

        var menuOptions = _optionUtils.GetAllStringValues(typeof(AddCategoryOptions));
        menuOptions.AddRange(categories);

        var selected = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(menuOptions));
        var category = string.Empty;

        switch (_optionUtils.GetEnumValue(selected, typeof(AddCategoryOptions)))
        {
            case AddCategoryOptions.Return:
                return;
            case AddCategoryOptions.CreateCategory:
                category = AnsiConsole.Ask<string>("Create a new category: ");
                break;
            default:
                category = selected;
                break;
        }
        
        contact.Category = category;
        await _dBContext.SaveChangesAsync();
    }

    private async void ChangeDetails(string optionChange, PhoneProperties contact)
    {
        TitleCard(optionChange);

        switch (_optionUtils.GetEnumValue(optionChange, typeof(EditContactOptions)))
        {
            case EditContactOptions.ChangeName:
                var newName = EnterName(optionChange);
                if (newName.ToLower() == "r") return;
                contact.Name = newName;
                break;
            case EditContactOptions.ChangePhone:
                contact.PhoneNumber = EnterPhoneNumber(optionChange);
                break;
            case EditContactOptions.ChangeEmail:
                contact.Email = EnterEmail(optionChange);
                break;
        }

        await _dBContext.SaveChangesAsync();
    }

    private void SendEmail(PhoneProperties contact)
    {
        var title = $"Sending email to {contact.Name}";
        TitleCard(title);

        var client = new SmtpClient("smtp.gmail.com");

        try
        {
            var userEmail = EnterEmail(title);
            var password = AnsiConsole.Ask<string>("Enter your password");
            var credentials = new NetworkCredential(userEmail, password);

            client.Port = 587;
            client.Credentials = credentials;

            var sender = new MailAddress(userEmail);
            var reciever = new MailAddress(contact.Email);

            var message = new MailMessage(sender, reciever);
            message.Subject = AnsiConsole.Ask<string>("Enter a subject line: ");
            message.Body = AnsiConsole.Ask<string>("Enter your message: ");

            var userState = "Test Message";

            client.SendAsync(message, userState);

            message.Dispose();
            client.Dispose();

            AnsiConsole.WriteLine("Successfully sent message!");
            AnsiConsole.Prompt(new TextPrompt<string>("Press any key to return").AllowEmpty());
        }
        catch(FormatException)
        {
            AnsiConsole.WriteLine("!!! Email is not valid !!!");
        }
    }

    private string EnterName(string title)
    {
        TitleCard(title);
        var newDetail = AnsiConsole.Ask<string>("Enter name (or type r to return): ");

        while (string.IsNullOrEmpty(newDetail) && !newDetail.ToLower().Equals("r"))
        {
            TitleCard(title);
            newDetail = AnsiConsole.Ask<string>("Enter name (or type r to return): ");
        }

        return newDetail;
    }

    private string EnterPhoneNumber(string title)
    {
        TitleCard(title);

        var contactPhone = AnsiConsole.Ask<string>("Enter a phone number (e.g 123-456-7890):");

        while (!_validation.ValidatePhoneNumber(contactPhone))
        {
            TitleCard(title);
            AnsiConsole.WriteLine("!! Invalid Phone Number !!");
            contactPhone = AnsiConsole.Ask<string>("Enter a phone number (e.g 123-456-7890):");
        }

        return contactPhone;
    }

    private string EnterEmail(string title)
    {
        TitleCard(title);

        var contactEmail = AnsiConsole.Ask<string>("Email (e.g tempmail@gmail.com): ");

        while (!_validation.ValidateEmail(contactEmail))
        {
            TitleCard(title);
            AnsiConsole.WriteLine("!! Invalid Email !!");
            contactEmail = AnsiConsole.Ask<string>("Email (e.g tempmail@gmail.com): ");
        }

        return contactEmail;
    }

    private void TitleCard(string titleCard)
    {
        var title = new FigletText(titleCard)
        {
            Justification = Justify.Center
        };

        AnsiConsole.Clear();
        AnsiConsole.Write(title);
    }
}