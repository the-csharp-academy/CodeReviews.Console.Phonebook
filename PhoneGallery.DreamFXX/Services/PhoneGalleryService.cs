using PhoneGallery.DreamFXX.Data;
using PhoneGallery.DreamFXX.Models;
using PhoneGallery.DreamFXX.UserInput;
using Spectre.Console;

namespace PhoneGallery.DreamFXX.Services;

public class PhoneGalleryService
{
    private readonly PhoneGalleryContext _context;

    public PhoneGalleryService(PhoneGalleryContext context)
    {
        _context = context;
    }

    public void Start()
    {
        while (true)
        {
            Console.Clear();

            var menuSelections = new List<MenuSelection>
            {
                new("Add Contact", AddContact),
                new("Show Contact List", ShowContacts),
                new("Update Contact", UpdateContact),
                new("Delete Contact", DeleteContact),
                new("Show Categories", ShowCategories),
                new("Exit", Exit)
            };

            var userSelection = AnsiConsole.Prompt(
                new SelectionPrompt<MenuSelection>()
                .Title(
                "[yellow]Welcome in your personal phonebook[/]\n[grey]info: choose, what you want to do.[/]")
                .PageSize(6)
                .AddChoices(menuSelections)
                .UseConverter(n => n.Name));

            userSelection.Action.Invoke();

            if (userSelection.Name == "Exit")
                break;

        }
    }

    public void AddContact()
    {
        string? name = ContactInput.GetNameInput();
        if (name == null)
            return;

        string? email = ContactInput.GetEmailInput();
        if (email == null)
            return;

        string? phoneNumber = ContactInput.GetPhoneNumberInput();
        if (phoneNumber == null)
            return;

        int categoryId = ContactInput.GetCategorySelection(GetCategories());

        var contact = new Contact
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            CategoryId = categoryId
        };

        try
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            Console.WriteLine("Contact was saved successfully.");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public List<Contact>? GetContacts()
    {
        List<Contact> contacts = new();
        _context.Contacts.ToList();
        if (contacts.Count == 0)
        {
            Console.WriteLine("No saved contacts found. Press any key to go back.");
            Console.ReadKey();
            return null;
        }
        return contacts;
    }

    private List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public void ShowContacts()
    {
        var contacts = GetContacts();
        if (contacts == null) return;
        foreach (var contact in contacts)
        {
            Console.WriteLine($"Name - {contact.Name}");
            Console.WriteLine($"Phone Number - {contact.PhoneNumber}");
            Console.WriteLine($"Email - {contact.Email}");
            Console.WriteLine($"Category - {contact.Category.Name}\n");
        }
    }

    public void ShowCategories()
    {
        var categories = GetCategories();
        if (categories == null) return;

        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Id}: {category.Name}");
            Console.WriteLine();
        }

    }

    public void UpdateContact()
    {
        Contact contact = ContactInput.GetSpecificContact(GetContacts());
        if (contact == null) 
            return;

        contact.PhoneNumber = ContactInput.GetPhoneNumberInput();
        if (contact.PhoneNumber == null) 
            return;

        contact.Email = ContactInput.GetEmailInput();
        if (contact.Email == null)
            return;

        contact.CategoryId = ContactInput.GetCategorySelection(GetCategories());
        
        if (!ContactInput.ConfirmAction())
        {
            Console.WriteLine("Changes were cancelled.");
            return;
        }

        _context.SaveChanges();
        Console.WriteLine("Contact was updated.");
    }

    public void DeleteContact()
    {
        Contact contact = ContactInput.GetSpecificContact(GetContacts());
        if (contact == null) 
            return;

        if (!ContactInput.ConfirmAction())
        {
            Console.WriteLine("Contact was not deleted.");
            return;
        }
        _context.Remove(contact);
        _context.SaveChanges();
        Console.WriteLine("Contact was deleted.");
    }


    public void Exit()
    {
        AnsiConsole.MarkupLine("[red]Goodbye.\n\n- press any key to exit[/]");
        Console.ReadKey();
        Environment.Exit(0);
    }

}



