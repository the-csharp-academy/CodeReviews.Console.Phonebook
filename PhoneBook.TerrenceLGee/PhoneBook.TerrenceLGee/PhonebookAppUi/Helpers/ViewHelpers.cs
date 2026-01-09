using PhoneBook.TerrenceLGee.DTOs.ContactDTOs;
using PhoneBook.TerrenceLGee.Extensions.Pagination;
using Spectre.Console;

namespace PhoneBook.TerrenceLGee.PhonebookAppUi.Helpers;

public static class ViewHelpers
{
    public static void DisplayContacts(PagedList<RetrievedContactDto> contacts)
    {
        AnsiConsole.MarkupLine($"[CadetBlue_1]Page {contacts.PageNumber} " +
            $"of {contacts.TotalPages}[/]");

        var table = new Table()
            .DoubleBorder()
            .BorderColor(Color.Gold1);
        table.AddColumn("[Blue]Id[/]");
        table.AddColumn("[Blue]First Name[/]");
        table.AddColumn("[Blue]Last Name[/]");
        table.AddColumn("[Blue]Email Address[/]");
        table.AddColumn("[Blue]Category[/]");

        foreach (var contact in contacts)
        {
            table.AddRow(
                $"[LightGreen]{contact.Id}[/]",
                $"[LightGreen]{contact.FirstName}[/]",
                $"[LightGreen]{contact.LastName}[/]",
                $"[LightGreen]{contact.Email ?? "N/A"}[/]",
                $"[LightGreen]{contact.Category}[/]");
        }

        AnsiConsole.Write(table);
    }

    public static void DisplayContactDetails(RetrievedContactDto contact)
    {
        var dateOfBirthText = (contact.DateOfBirth.HasValue)
            ? contact.DateOfBirth.Value.ToString()
            : "N/A";

        var rows = new Rows(
            new Markup($"First name: [bold underline PaleGreen1]{contact.FirstName}[/]"),
            new Markup($"Last name: [bold underline PaleGreen1]{contact.LastName}[/]"),
            new Markup($"Suffix: [bold underline PaleGreen1]{contact.Suffix ?? "N/A"}[/]"),
            new Markup($"Nickname: [bold underline PaleGreen1]{contact.NickName ?? "N/A"}[/]"),
            new Markup($"Email address: [bold underline PaleGreen1]{contact.Email ?? "N/A"}[/]"),
            new Markup($"Category: [bold underline PaleGreen1]{contact.Category}[/]"),
            new Markup($"Date of birth: [bold underline PaleGreen1]{dateOfBirthText}[/]"),
            new Markup($"Notes: [bold underline PaleGreen1]{contact.Notes ?? "N/A"}[/]"));

        AnsiConsole.Write(rows);

        if (contact.PhoneNumbers.Count == 0) return;

        AnsiConsole.WriteLine();

        var table = new Table()
            .BorderColor(Color.Black);
        table.AddColumn("[bold underline MediumTurquoise]Phone Number[/]");
        table.AddColumn("[bold underline MediumTurquoise]Country Code[/]");
        table.AddColumn("[bold underline MediumTurquoise]Phone Number Category[/]");

        foreach (var phoneNumber in contact.PhoneNumbers)
        {
            table.AddRow(
                $"[MediumTurquoise]{phoneNumber.Number}[/]",
                $"[MediumTurquoise]{phoneNumber.CountryCode}[/]",
                $"[MediumTurquoise]{phoneNumber.PhoneNumberCategory}[/]");
        }

        AnsiConsole.Write(table);
    }
}


public enum Choice
{
    Previous,
    Next,
    Choose,
    Quit,
}
