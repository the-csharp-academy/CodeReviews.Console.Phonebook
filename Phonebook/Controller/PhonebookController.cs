using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phonebook.Model;
using Spectre.Console;
using System.Net;
using System.Net.Mail;

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
        bool IsOkay = true;
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
            catch (Exception)
            {
                IsOkay = false;
                AnsiConsole.MarkupLine("[red] PhoneNumber/ContactName/Email already exists, returning to Main Menu[/]");
            }
        }
        if (IsOkay) AnsiConsole.MarkupLine("[aqua bold] Contact added! Returning to Main Menu [/]");
    }
    internal static void AddCategory()
    {
        var NewCategoryName = UserInputController.GetCategoryName();
        bool IsOkay = true;
        using (var context = new PhonebookContext())
        {
            try
            {
                Category NewCatgory = new Category() { CategoryName = NewCategoryName };
                context.Add(NewCatgory);
                context.SaveChanges();
            }
            catch (Exception)
            {
                IsOkay = false;
                AnsiConsole.MarkupLine("[red] Category Name already exists, returning to Main Menu[/]");
            }
        }
        if (IsOkay) AnsiConsole.MarkupLine("[aqua bold] Category added! Returning to Main Menu [/]");
    }

    internal static void DeleteContact()
    {
        bool IsOkay = true;
        using (var context = new PhonebookContext())
        {
            try
            {
                var contact = ChooseContact();
                if (contact == null) throw new Exception("Contact not found, returning to Main Menu.");
                context.Remove(contact);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                IsOkay = false;
                AnsiConsole.MarkupLine($"[red] Something went wrong{ex}[/]");
            }
        }
        if (IsOkay) AnsiConsole.MarkupLine("[aqua bold] Contact deleted! Returning to Main Menu [/]");
    }
    internal static void DeleteCategory()
    { //It made no sense to delete all contacts that are in the category so i just set category to null on those
        using (var context = new PhonebookContext())
        {
            try
            {
                var category = ChooseCategory();
                if (category == null) throw new Exception("Category not found, returning to Main Menu.");
                var contactList = context.Contacts.Where(s => s.CategoryName == category.CategoryName).ToList();
                foreach (var contact in contactList)
                {
                    contact.CategoryName = null;
                }
                context.Remove(category);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red] Something went wrong{ex}[/]");
            }
        }
        AnsiConsole.MarkupLine("[aqua bold] Category deleted! Returning to Main Menu [/]");
    }

    internal static void SendEmail()
    {
        var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var mailString = MyConfig.GetValue<string>("MyEmail:FromMail");
        var DisplayString = MyConfig.GetValue<string>("MyEmail:DisplayName");
        var FromPassword = MyConfig.GetValue<string>("MyEmail:MailPassword");
        var FromAddress = new MailAddress(mailString, DisplayString);
        bool IsOkay = true;
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
            IsOkay = false;
            AnsiConsole.MarkupLine("[red] Something went wrong while sending an email, most likely an invalid email address. Returning to Main Menu [/]");
        }
        if (IsOkay) AnsiConsole.MarkupLine("[aqua] Email sent, returning to Main Menu[/]");
    }

    internal static void UpdateContactName()
    {
        bool IsOkay = true;
        using (var context = new PhonebookContext())
        {
            try
            {
                var ContactChoice = ChooseContact();
                if (ContactChoice == null) throw new Exception("Contact choice is null, returning to Main Menu.");

                var contact = context.Contacts.Find(ContactChoice.ContactId);
                if (contact == null) throw new Exception("Contact not found, returning to Main Menu.");

                var NewName = UserInputController.GetContactName();
                contact.ContactName = NewName;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                IsOkay = false;
                AnsiConsole.MarkupLine($"[red] Something went wrong{ex}[/]");
            }
        }
        if (IsOkay) AnsiConsole.MarkupLine("[aqua bold] Contact Name Updated! Returning to Main Menu [/]");
    }
    internal static void UpdateContactPhoneNumber()
    {
        bool IsOkay = true;
        using (var context = new PhonebookContext())
        {
            try
            {
                var ContactChoice = ChooseContact();
                if (ContactChoice == null) throw new Exception("Contact choice is null, returning to Main Menu.");

                var contact = context.Contacts.Find(ContactChoice.ContactId);
                if (contact == null) throw new Exception("Contact not found, returning to Main Menu.");

                var NewPhoneNumber = UserInputController.GetPhoneNumber();
                contact.PhoneNumber = NewPhoneNumber;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                IsOkay = false;
                AnsiConsole.MarkupLine($"[red] Something went wrong{ex}[/]");
            }
        }
        if (IsOkay) AnsiConsole.MarkupLine("[aqua bold] Contact Phone Number Updated! Returning to Main Menu [/]");
    }
    internal static void UpdateContactEmail()
    {
        bool IsOkay = true;
        using (var context = new PhonebookContext())
        {
            try
            {
                var ContactChoice = ChooseContact();
                if (ContactChoice == null) throw new Exception("Contact choice is null, returning to Main Menu.");

                var contact = context.Contacts.Find(ContactChoice.ContactId);
                if (contact == null) throw new Exception("Contact not found, returning to Main Menu.");
                var NewMail = UserInputController.GetContactEmail();
                contact.Email = NewMail;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                IsOkay = false;
                AnsiConsole.MarkupLine($"[red] Something went wrong{ex}[/]");
            }
        }
        if (IsOkay) AnsiConsole.MarkupLine("[aqua bold] Contact Email Updated! Returning to Main Menu [/]");
    }
    internal static void UpdateContactCategory()
    {
        bool IsOkay = true;
        using (var context = new PhonebookContext())
        {
            try
            {
                var ContactChoice = ChooseContact();
                if (ContactChoice == null) throw new Exception("Contact choice is null, returning to Main Menu.");

                var contact = context.Contacts.Find(ContactChoice.ContactId);
                if (contact == null) throw new Exception("Contact not found, returning to Main Menu.");

                var NewCategory = ChooseCategory();
                contact.CategoryName = NewCategory.CategoryName;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                IsOkay = false;
                AnsiConsole.MarkupLine($"[red] Something went wrong{ex}[/]");
            }
        }
        if (IsOkay) AnsiConsole.MarkupLine("[aqua bold] Contact Category Updated! Returning to Main Menu [/]");
    }
    internal static void UpdateContactCategory(Contact contact)
    {
        using (var context = new PhonebookContext())
        {
            try
            {
                var NewCategory = ChooseCategory();
                if (NewCategory == null) throw new Exception("Category not found, returning to Main Menu.");
                contact.CategoryName = NewCategory.CategoryName;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red] Something went wrong{ex}[/]");
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
                return null;
            }
            else
            {
                var ContactChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<Contact>()
                    .Title("Which Contact are you interested in?")
                    .UseConverter(s => s.ContactName)
                    .AddChoices(ContactList));
                return ContactChoice;
            }
        }
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
                return null;
            }
            else
            {
                var CategoryChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<Category>()
                    .Title("Which Category are you interested in?")
                    .UseConverter(s => s.CategoryName)
                    .AddChoices(CategoryList));
                return CategoryChoice;
            }
        }
    }

    internal static Contact ChooseContactFromCategory()
    {
        var category = ChooseCategory();
        using (var context = new PhonebookContext())
        {
            var ContactList = context.Contacts.Where(s => s.CategoryName == category.CategoryName).ToList();
            if (!ContactList.Any<Contact>())
            {
                return null;
            }
            else
            {
                var ContactChoice = AnsiConsole.Prompt(
                                    new SelectionPrompt<Contact>()
                                    .Title("Which Contact are you interested in?")
                                    .UseConverter(s => s.ContactName)
                                    .AddChoices(ContactList));
                return ContactChoice;
            }
        }
    }
}
