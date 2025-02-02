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


    }

    private List<Category> GetCategories()
    {
        throw new NotImplementedException();
    }

    public void ShowContacts()
    {

    }

    public void UpdateContact()
    {

    }

    public void DeleteContact()
    {

    }

    public void ShowCategories()
    {

    }

    public void Exit()
    {

    }

}



