using Spectre.Console;

namespace PhoneBook.TerrenceLGee.PhonebookAppUi.Helpers;

public static class UiHelpers
{
    public static void PressAnyKeyToContinue(string message = "")
    {
        AnsiConsole.MarkupLine($"[DarkOliveGreen1_1]{message}[/]");
        AnsiConsole.MarkupLine($"[Aquamarine1_1]Press any key to continue[/]");
        Console.ReadKey();
        AnsiConsole.Clear();
    }

    public static void PressAnyKeyToContinueError(string errorMessage)
    {
        AnsiConsole.MarkupLine($"[bold red]{errorMessage}[/]");
        AnsiConsole.MarkupLine($"[Aquamarine1_1]Press any key to continue[/]");
        Console.ReadKey();
        AnsiConsole.Clear();
    }
}
