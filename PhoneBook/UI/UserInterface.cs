using PhoneBook.Configuration;
using PhoneBook.Helpers;
using PhoneBook.Models;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook.UI;

public class UserInterface
{
    private readonly ContactService _contactService;
    private readonly CategoryService _categoryService;
    private readonly EmailService _emailService;
    private readonly SmsService _smsService;

    public UserInterface(
        ContactService contactService,
        CategoryService categoryService,
        EmailService emailService,
        SmsService smsService)
    {
        _contactService = contactService;
        _categoryService = categoryService;
        _emailService = emailService;
        _smsService = smsService;
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            AnsiConsole.Clear();
            DisplayHeader();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[bold cyan]What would you like to do?[/]")
                .PageSize(15)
                .HighlightStyle(new Style(Color.Cyan1))
                .AddChoiceGroup("Contacts",
                    "View all contacts",
                    "Add Contact",
                    "Update Contact",
                    "Delete Contact",
                    "Search Contact")
                .AddChoiceGroup("Categories",
                    "Manage Categories")
                .AddChoiceGroup("Communication",
                    "Send Email",
                    "Send SMS")
                .AddChoices("Exit"));
            switch (choice)
            {
                case "View all contacts":
                    ViewAllContact();
                    break;
                case "Add Contact":
                    AddContact();
                    break;
                case "Update Contact":
                    UpdateContact();
                    break;
                case "Delete Contact":
                    DeleteContact();
                    break;
                case "Search Contact":
                    SearchContact();
                    break;
                case "Manage Categories":
                    ManageCategories();
                    break;
                case "Send Email":
                    SendEmail();
                    break;
                case "Send SMS":
                    SendSms();
                    break;
                case "Exit":
                    running = false;
                    AnsiConsole.MarkupLine("[bold green]Goodbye![/]");
                    break;
            }
        }
    }

    private void DisplayHeader()
    {
        AnsiConsole.Write(
            new FigletText("PhoneBook")
                .Centered()
                .Color(Color.Cyan1));

        AnsiConsole.Write(new Rule("[grey]Manage your contacts[/]").RuleStyle("grey"));
        AnsiConsole.WriteLine();
    }

    

    private void ViewAllContact()
    {
        var contacts = _contactService.GetAllContacts();
        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No contacts found. Add one first![/]");
        }
        else
        {
            DisplayContactsTable(contacts);
        }
        PressAnyKey();
    }

    private void AddContact()
    {
        AnsiConsole.Write(new Rule("[green]Add New Contact[/]").RuleStyle("green"));

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[cyan]Name:[/]")
                .Validate(input => Validation.IsValidName(input)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]Name must be 1-100 characters.[/]")));

        var phone = AnsiConsole.Prompt(
             new TextPrompt<string>("[cyan]Phone Number:[/]")
             .Validate(input => Validation.IsValidPhoneNumber(input)
             ? ValidationResult.Success()
             : ValidationResult.Error("[red]Invalid phone number format.[/]")));

        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("[cyan]Email (optional, press Enter to skip):[/]")
                .AllowEmpty()
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input)) return ValidationResult.Success();
                    return Validation.IsValidEmail(input)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid email format.[/]");
                }));

        // Category selection
        var categories = _categoryService.GetAllCategories();
        int? categoryId = null;
        if (categories.Count > 0)
        {
            var categoryChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Assign a category (optional):[/]")
                    .AddChoices(["None", .. categories.Select(c => c.Name)]));

            if (categoryChoice != "None")
            {
                categoryId = categories.First(c => c.Name == categoryChoice).Id;
            }
        }

        var contact = new Contact
        {
            Name = name.Trim(),
            PhoneNumber = phone.Trim(),
            EmailAddress = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
            CategoryId = categoryId
        };
        _contactService.AddContact(contact);
        AnsiConsole.MarkupLine($"\n[bold green]Contact '{contact.Name}' added successfully![/]");
        PressAnyKey();
    }

    private void UpdateContact()
    {
        var contacts = _contactService.GetAllContacts();
        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No contacts to update.[/]");
            PressAnyKey();
            return;
        }
        AnsiConsole.Write(new Rule("[blue]Update Contact[/]").RuleStyle("blue"));
        DisplayContactsTable(contacts);
        var selectedContact = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
                .Title("[cyan]Select a contact to update:[/]")
                .PageSize(10)
                .UseConverter(c => $"{c.Name} ({c.PhoneNumber})")
                .AddChoices(contacts));

        var name = AnsiConsole.Prompt(
           new TextPrompt<string>($"[cyan]Name[/] [grey](current: {selectedContact.Name})[/]:")
             .DefaultValue(selectedContact.Name)
               .Validate(input => Validation.IsValidName(input)
               ? ValidationResult.Success()
               : ValidationResult.Error("[red]Invalid Name.[/]")));

        var phone = AnsiConsole.Prompt(
             new TextPrompt<string>($"[cyan]Phone[/] [grey](current: {selectedContact.PhoneNumber})[/]:")
             .DefaultValue(selectedContact.PhoneNumber)
             .Validate(input => Validation.IsValidPhoneNumber(input)
             ? ValidationResult.Success()
             : ValidationResult.Error("[red]Invalid phone number format.[/]")));

        var email = AnsiConsole.Prompt(
           new TextPrompt<string>($"[cyan]Email[/] [grey](current: {selectedContact.EmailAddress ?? "none"})[/]:")
               .DefaultValue(selectedContact.EmailAddress ?? "")
               .AllowEmpty()
               .Validate(input =>
               {
                   if (string.IsNullOrWhiteSpace(input)) return ValidationResult.Success();
                   return Validation.IsValidEmail(input)
                       ? ValidationResult.Success()
                       : ValidationResult.Error("[red]Invalid email.[/]");
               }));

        // Category selection
        var categories = _categoryService.GetAllCategories();
        int? categoryId = selectedContact.CategoryId;
        if (categories.Count > 0)
        {
            var currentCategory = selectedContact.Category?.Name ?? "None";
            var categoryChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[cyan]Category[/] [grey](current: {currentCategory})[/]:")
                    .AddChoices(["None", .. categories.Select(c => c.Name)]));

            categoryId = categoryChoice == "None"
                ? null
                : categories.First(c => c.Name == categoryChoice).Id;
        }

        selectedContact.Name = name.Trim();
        selectedContact.PhoneNumber = phone.Trim();
        selectedContact.EmailAddress = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        selectedContact.CategoryId = categoryId;

        if (_contactService.UpdateContact(selectedContact))
        {
            AnsiConsole.MarkupLine($"\n[bold green]Contact '{Markup.Escape(selectedContact.Name)}' updated successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("\n[yellow]The contact no longer exists.[/]");
        }
        PressAnyKey();

    }

    private void DeleteContact()
    {
        var contacts = _contactService.GetAllContacts();
        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No contacts to delete.[/]");
            PressAnyKey();
            return;
        }
        AnsiConsole.Write(new Rule("[red]Delete contact[/]").RuleStyle("red"));
        DisplayContactsTable(contacts);
        var selectedContact = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
                .Title("[cyan]Select contact to delete:[/]")
                .PageSize(10)
                .UseConverter(c => $"{c.Name} ({c.PhoneNumber})")
                .AddChoices(contacts));
        if (AnsiConsole.Confirm($"[red]Are you sure you want to delete '{selectedContact.Name}'?[/]"))
        {
            _contactService.DeleteContact(selectedContact.Id);
            AnsiConsole.MarkupLine($"\n[bold red]Contact '{selectedContact.Name}' deleted.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("\n[grey]Deletion cancelled.[/]");
        }

        PressAnyKey();
    }

    private void SearchContact()
    {
        AnsiConsole.Write(new Rule("[yellow]Search Contacts[/]").RuleStyle("yellow"));

        var searchTerm = AnsiConsole.Prompt(
                new TextPrompt<string>("[cyan]Enter search term:[/]")
                .Validate(input => !string.IsNullOrWhiteSpace(input)
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]Search term cannot be empty.[/]")));
        
        var results = _contactService.SearchContact(searchTerm);
        if (results.Count == 0) {
            AnsiConsole.MarkupLine($"[yellow]No contacts found matching '{Markup.Escape(searchTerm)}'.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[green]Found {results.Count} contact(s):[/]");
            DisplayContactsTable(results);
        }
        PressAnyKey();
    }

    // ─── Category Management ─────────────────────────────────────

    private void ManageCategories()
    {
        bool managing = true;
        while (managing)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold magenta]Category Management[/]").RuleStyle("magenta"));

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Category options:[/]")
                    .HighlightStyle(new Style(Color.Cyan1))
                    .AddChoices(
                        "View all categories",
                        "Add Category",
                        "Update Category",
                        "Delete Category",
                        "Back to main menu"));

            switch (choice)
            {
                case "View all categories":
                    ViewAllCategories();
                    break;
                case "Add Category":
                    AddCategory();
                    break;
                case "Update Category":
                    UpdateCategory();
                    break;
                case "Delete Category":
                    DeleteCategory();
                    break;
                case "Back to main menu":
                    managing = false;
                    break;
            }
        }
    }

    private void ViewAllCategories()
    {
        var categories = _categoryService.GetAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No categories found.[/]");
        }
        else
        {
            DisplayCategoriesTable(categories);
        }
        PressAnyKey();
    }

    private void AddCategory()
    {
        AnsiConsole.Write(new Rule("[green]Add New Category[/]").RuleStyle("green"));

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[cyan]Category name:[/]")
                .Validate(input => Validation.IsValidCategoryName(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Category name must be 1-50 characters.[/]")));

        var category = new Category { Name = name.Trim() };
        if (_categoryService.AddCategory(category))
        {
            AnsiConsole.MarkupLine($"\n[bold green]Category '{Markup.Escape(category.Name)}' added successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"\n[yellow]A category named '{Markup.Escape(category.Name)}' already exists.[/]");
        }
        PressAnyKey();
    }

    private void UpdateCategory()
    {
        var categories = _categoryService.GetAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No categories to update.[/]");
            PressAnyKey();
            return;
        }

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
                .Title("[cyan]Select a category to update:[/]")
                .UseConverter(c => c.Name)
                .AddChoices(categories));

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>($"[cyan]Name[/] [grey](current: {selected.Name})[/]:")
                .DefaultValue(selected.Name)
                .Validate(input => Validation.IsValidCategoryName(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Category name must be 1-50 characters.[/]")));

        selected.Name = name.Trim();
        if (_categoryService.UpdateCategory(selected))
        {
            AnsiConsole.MarkupLine($"\n[bold green]Category updated to '{Markup.Escape(selected.Name)}'![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"\n[yellow]A category named '{Markup.Escape(selected.Name)}' already exists.[/]");
        }
        PressAnyKey();
    }

    private void DeleteCategory()
    {
        var categories = _categoryService.GetAllCategories();
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No categories to delete.[/]");
            PressAnyKey();
            return;
        }

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
                .Title("[cyan]Select a category to delete:[/]")
                .UseConverter(c => c.Name)
                .AddChoices(categories));

        if (AnsiConsole.Confirm($"[red]Delete category '{selected.Name}'? Contacts in this category will become uncategorized.[/]"))
        {
            if (_categoryService.DeleteCategory(selected.Id))
            {
                AnsiConsole.MarkupLine($"\n[bold red]Category '{Markup.Escape(selected.Name)}' deleted.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[yellow]The category no longer exists.[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("\n[grey]Deletion cancelled.[/]");
        }
        PressAnyKey();
    }

    private void SendEmail()
    {
        AnsiConsole.Write(new Rule("[bold blue]Send Email[/]").RuleStyle("blue"));

        // Check if SMTP is configured
        var config = ConfigManager.Load();
        if (config.Smtp is null)
        {
            AnsiConsole.MarkupLine("[yellow]SMTP is not configured yet. Let's set it up.[/]");
            ConfigureSmtp(config);
        }

        // Select contact or enter email manually
        var contacts = _contactService.GetAllContacts()
            .Where(c => !string.IsNullOrEmpty(c.EmailAddress))
            .ToList();

        string toAddress;
        string toName;

        if (contacts.Count > 0)
        {
            var source = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Send to:[/]")
                    .AddChoices("Select from contacts", "Enter email manually"));

            if (source == "Select from contacts")
            {
                var selected = AnsiConsole.Prompt(
                    new SelectionPrompt<Contact>()
                        .Title("[cyan]Select contact:[/]")
                        .UseConverter(c => $"{c.Name} ({c.EmailAddress})")
                        .AddChoices(contacts));
                toAddress = selected.EmailAddress!;
                toName = selected.Name;
            }
            else
            {
                toAddress = PromptEmail();
                toName = AnsiConsole.Ask<string>("[cyan]Recipient name:[/]");
            }
        }
        else
        {
            toAddress = PromptEmail();
            toName = AnsiConsole.Ask<string>("[cyan]Recipient name:[/]");
        }

        var subject = PromptRequiredText("[cyan]Subject:[/]");
        var body = PromptRequiredText("[cyan]Message:[/]");

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("[cyan]Sending email...[/]", _ =>
            {
                var result = _emailService.SendEmail(
                    config.Smtp!, toAddress, toName, subject, body);

                if (result.Success)
                    AnsiConsole.MarkupLine($"\n[bold green]Email sent to {Markup.Escape(toAddress)}![/]");
                else
                    AnsiConsole.MarkupLine($"\n[bold red]Failed to send email: {Markup.Escape(result.Message)}[/]");
            });

        PressAnyKey();
    }

    private void ConfigureSmtp(AppConfig config)
    {
        var host = PromptRequiredText("[cyan]SMTP Host[/] [grey](e.g. smtp.gmail.com)[/]:");
        var port = AnsiConsole.Prompt(
            new TextPrompt<int>("[cyan]SMTP Port[/] [grey](e.g. 587)[/]:")
                .Validate(value => value is > 0 and <= 65535
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Enter a valid port number.[/]")));
        var username = PromptRequiredText("[cyan]Username:[/]");
        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("[cyan]Password/App Password:[/]")
                .Secret()
                .Validate(input => !string.IsNullOrWhiteSpace(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]A password is required.[/]")));
        var senderEmail = PromptEmail();
        var senderName = PromptRequiredText("[cyan]Sender display name:[/]");
        var useStartTls = AnsiConsole.Confirm("[cyan]Use STARTTLS when supported?[/]", true);

        config.Smtp = new SmtpSettings
        {
            Host = host,
            Port = port,
            Username = username,
            Password = password,
            SenderEmail = senderEmail,
            SenderName = senderName,
            UseStartTls = useStartTls
        };
        ConfigManager.Save(config);
        AnsiConsole.MarkupLine("[green]SMTP settings saved![/]\n");
    }

    // ─── SMS ─────────────────────────────────────────────────────

    private void SendSms()
    {
        AnsiConsole.Write(new Rule("[bold green]Send SMS[/]").RuleStyle("green"));

        var config = ConfigManager.Load();
        if (config.Twilio is null)
        {
            AnsiConsole.MarkupLine("[yellow]Twilio is not configured yet. Let's set it up.[/]");
            ConfigureTwilio(config);
        }

        // Select contact or enter phone manually
        var contacts = _contactService.GetAllContacts();
        string toPhone;

        if (contacts.Count > 0)
        {
            var source = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Send to:[/]")
                    .AddChoices("Select from contacts", "Enter phone number manually"));

            if (source == "Select from contacts")
            {
                var selected = AnsiConsole.Prompt(
                    new SelectionPrompt<Contact>()
                        .Title("[cyan]Select contact:[/]")
                        .UseConverter(c => $"{c.Name} ({c.PhoneNumber})")
                        .AddChoices(contacts));
                toPhone = selected.PhoneNumber;
            }
            else
            {
                toPhone = PromptE164Phone("[cyan]Phone number:[/]");
            }
        }
        else
        {
            toPhone = PromptE164Phone("[cyan]Phone number:[/]");
        }

        var message = AnsiConsole.Prompt(
            new TextPrompt<string>("[cyan]Message:[/]")
                .Validate(input => Validation.IsValidSmsMessage(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]SMS messages must contain 1 to 1600 characters.[/]")));

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("[cyan]Sending SMS...[/]", _ =>
            {
                var result = _smsService.SendSms(config.Twilio!, toPhone, message);

                if (result.Success)
                    AnsiConsole.MarkupLine($"\n[bold green]SMS sent to {Markup.Escape(toPhone)}![/]");
                else
                    AnsiConsole.MarkupLine($"\n[bold red]Failed to send SMS: {Markup.Escape(result.Message)}[/]");
            });

        PressAnyKey();
    }

    private void ConfigureTwilio(AppConfig config)
    {
        var sid = PromptRequiredText("[cyan]Twilio Account SID:[/]");
        var token = AnsiConsole.Prompt(
            new TextPrompt<string>("[cyan]Twilio Auth Token:[/]")
                .Secret()
                .Validate(input => !string.IsNullOrWhiteSpace(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]An auth token is required.[/]")));
        var fromPhone = PromptE164Phone("[cyan]Twilio Phone Number[/] [grey](e.g. +15551234567)[/]:");

        config.Twilio = new TwilioSettings
        {
            AccountSid = sid,
            AuthToken = token,
            FromPhoneNumber = fromPhone
        };
        ConfigManager.Save(config);
        AnsiConsole.MarkupLine("[green]Twilio settings saved![/]\n");
    }

    // ─── Helpers ─────────────────────────────────────────────────

    private static string PromptEmail()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[cyan]Email address:[/]")
                .Validate(input => Validation.IsValidEmail(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Invalid email format.[/]")));
    }

    private static string PromptPhone()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[cyan]Phone number:[/]")
                .Validate(input => Validation.IsValidPhoneNumber(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Invalid phone number format.[/]")));
    }

    private static string PromptE164Phone(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .Validate(input => Validation.IsValidE164PhoneNumber(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Use E.164 format, for example +15551234567.[/]")));
    }

    private static string PromptRequiredText(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
                .Validate(input => !string.IsNullOrWhiteSpace(input)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]This field is required.[/]")));
    }

    private static void DisplayContactsTable(List<Contact> contacts)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Cyan1)
            .Title("[bold cyan]Contacts[/]");

        table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Name[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold]Phone Number[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold]Email[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold]Category[/]").LeftAligned());

        foreach (var contact in contacts)
        {
            table.AddRow(
                contact.Id.ToString(),
                $"[white]{Markup.Escape(contact.Name)}[/]",
                $"[cyan]{Markup.Escape(contact.PhoneNumber)}[/]",
                contact.EmailAddress is not null
                    ? $"[blue]{Markup.Escape(contact.EmailAddress)}[/]"
                    : "[grey]—[/]",
                contact.Category is not null
                    ? $"[magenta]{Markup.Escape(contact.Category.Name)}[/]"
                    : "[grey]—[/]"
            );
        }

        AnsiConsole.Write(table);
    }

    private static void DisplayCategoriesTable(List<Category> categories)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Magenta1)
            .Title("[bold magenta]Categories[/]");

        table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
        table.AddColumn(new TableColumn("[bold]Name[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold]Contacts[/]").Centered());

        foreach (var category in categories)
        {
            table.AddRow(
                category.Id.ToString(),
                $"[white]{Markup.Escape(category.Name)}[/]",
                category.Contacts.Count.ToString());
        }

        AnsiConsole.Write(table);
    }

    private static void PressAnyKey()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[grey]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
