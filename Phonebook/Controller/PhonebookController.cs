using Microsoft.EntityFrameworkCore;
using Phonebook.Model;
using Phonebook.View;
using Spectre.Console;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace Phonebook.Controller;

internal class PhonebookController
{
    internal static void AddContact()
    {
        var NewContactName = UserInputController.GetContactName();
        var NewContactEmail = UserInputController.GetContactEmail();
        var NewContactPhone = UserInputController.GetPhoneNumber();
        var categoryBool = AnsiConsole.Prompt(
            new ConfirmationPrompt("Would you like to add it to a category?"));
        using (var context = new PhonebookContext())
        {
            try
            {
                Contact NewContact = new Contact() { ContactName = NewContactName, PhoneNumber = NewContactPhone, Email = NewContactEmail };
                context.Add(NewContact);
                if (categoryBool)
                {
                    UpdateContactCategory(NewContact);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] PhoneNumber/ContactName/Email already exists, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Contact added! Returning to Main Menu [/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }
    internal static void AddCategory()
    {
        var NewCategoryName = UserInputController.GetCategoryName();
        using (var context = new PhonebookContext())
        {
            try
            {
                Category NewCatgory = new Category() { CategoryName = NewCategoryName };
                context.Add(NewCatgory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] Category Name already exists, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Category added! Returning to Main Menu [/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }

    internal static void DeleteContact()
    {
        using (var context = new PhonebookContext())
        {
            try
            {
                var contact = ChooseContact();
                context.Remove(contact);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] Something went wrong, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Contact deleted! Returning to Main Menu [/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }
    internal static void DeleteCategory()
    { //It made no sense to delete all contacts that are in the category so i just set category to null on those
        using (var context = new PhonebookContext())
        {
            try
            {
                var category = ChooseCategory();
                var contactList = context.Contacts.Where(s => s.CategoryName == category.CategoryName).ToList();
                foreach(var contact in contactList)
                {
                    contact.CategoryName = null;
                }
                context.Remove(category);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] Something went wrong, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Category deleted! Returning to Main Menu [/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }

    internal static void SendEmail()
    {
        var mailString = ConfigurationManager.AppSettings["FromMail"];
        var DisplayString = ConfigurationManager.AppSettings["DisplayName"];
        var FromPassword = ConfigurationManager.AppSettings["MailPassword"];
        var FromAddress = new MailAddress(mailString, DisplayString);
        try
        {
            var contact = ChooseContact();
            var ToAddress = new MailAddress(contact.Email, contact.ContactName);

            var subject = AnsiConsole.Prompt(
                new TextPrompt<string>("Subject of the email: "));
            var body = AnsiConsole.Prompt(
                new TextPrompt<string>("Body of the email: "));

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromAddress.Address, FromPassword)
            };
            using (var message = new MailMessage(FromAddress, ToAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        catch
        {
            AnsiConsole.MarkupLine("[red] Something went wrong while sending an email, most likely an invalid email address. Returning to Main Menu [/]");
            Console.ReadKey();
            UserInterface.MainMenu();
        }
        AnsiConsole.MarkupLine("[aqua] Email sent, returning to Main Menu[/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }

    internal static void UpdateContactName()
    {
        using (var context = new PhonebookContext())
        {
            try
            {
                var ContactChoice = ChooseContact();
                var contact = context.Contacts.Find(ContactChoice.ContactId);
                var NewName = UserInputController.GetContactName();
                contact.ContactName = NewName;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] Something went wrong, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Contact Name Updated! Returning to Main Menu [/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }
    internal static void UpdateContactPhoneNumber()
    {
        using (var context = new PhonebookContext())
        {
            try
            {
                var ContactChoice = ChooseContact();
                var contact = context.Contacts.Find(ContactChoice.ContactId);
                var NewPhoneNumber = UserInputController.GetPhoneNumber();
                contact.PhoneNumber = NewPhoneNumber;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] Something went wrong, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Contact Phone Number Updated! Returning to Main Menu [/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }
    internal static void UpdateContactEmail()
    {
        using (var context = new PhonebookContext())
        {
            try
            {
                var ContactChoice = ChooseContact();
                var contact = context.Contacts.Find(ContactChoice.ContactId);
                var NewMail = UserInputController.GetContactEmail();
                contact.Email = NewMail;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] Something went wrong, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Contact Email Updated! Returning to Main Menu [/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }
    internal static void UpdateContactCategory()
    {
        using (var context = new PhonebookContext())
        {
            try
            {
                var ContactChoice = ChooseContact();
                var contact = context.Contacts.Find(ContactChoice.ContactId);
                var NewCategory = ChooseCategory();
                contact.CategoryName = NewCategory.CategoryName;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] Something went wrong, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Contact Category Updated! Returning to Main Menu [/]");
        Console.ReadKey();
        UserInterface.MainMenu();
    }
    internal static void UpdateContactCategory(Contact contact)
    {
        using (var context = new PhonebookContext())
        {
            try
            {
                var NewCategory = ChooseCategory();
                contact.CategoryName = NewCategory.CategoryName;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red] Something went wrong, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
    }

    internal static Contact ChooseContact()
    {
        List<Contact> ContactList = new();
        using (var context = new PhonebookContext())
        {
            ContactList = context.Contacts.ToList();
            if (!ContactList.Any<Contact>())
            {
                AnsiConsole.MarkupLine("[red] Contact List Empty! Returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        var ContactChoice = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
            .Title("Which Contact are you interested in?")
            .UseConverter(s => s.ContactName)
            .AddChoices(ContactList));
        return ContactChoice;
    }
    internal static Category ChooseCategory()
    {
        List<Category> CategoryList = new();
        using (var context = new PhonebookContext())
        {
            CategoryList = context.Categories.ToList();
            if (!CategoryList.Any<Category>())
            {
                AnsiConsole.MarkupLine("[red] Category List Empty! Returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
        var CategoryChoice = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
            .Title("Which Category are you interested in?")
            .UseConverter(s => s.CategoryName)
            .AddChoices(CategoryList));
        return CategoryChoice;
    }

    internal static Contact ChooseContactFromCategory()
    {
        var category = ChooseCategory();
        using (var context = new PhonebookContext())
        {
            var ContactList = context.Contacts.Where(s => s.CategoryName == category.CategoryName).ToList();
            if (!ContactList.Any<Contact>())
            {
                Console.WriteLine("No contacts in said category, returning to Main Menu");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
            var ContactChoice = AnsiConsole.Prompt(
                                new SelectionPrompt<Contact>()
                                .Title("Which Contact are you interested in?")
                                .UseConverter(s => s.ContactName)
                                .AddChoices(ContactList));
            return ContactChoice;
        }
    }
}
