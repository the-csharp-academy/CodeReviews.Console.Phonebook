using Spectre.Console;

namespace Utilities.GoldRino456;
public static class DisplayUtils
{
    public static void ClearScreen()
    {
        AnsiConsole.Clear();
    }

    public static int PromptUserForIndexSelection(string promptText, Dictionary<string, int> choices)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(promptText)
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to see additional options)[/]")
            .AddChoices(choices.Keys.ToArray()));

        return choices[selection];
    }

    public static string PromptUserForSelectionFromList(string promptText, List<string> choices)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(promptText)
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to see additional options)[/]")
            .AddChoices(choices.ToArray()));

        return selection;
    }

    public static bool PromptUserForYesOrNoSelection(string promptText)
    {
        Dictionary<string, bool> choices = new Dictionary<string, bool>()
        {
            {"Yes", true},
            {"No", false}
        };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(promptText)
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to see additional options)[/]")
            .AddChoices(choices.Keys.ToArray()));

        return choices[selection];
    }

    public static void DisplayListAsTable(string[] columns, List<string[]> rows)
    {
        var table = new Table();

        table.AddColumns(columns);

        foreach (var row in rows)
        {
            table.AddRow(row);
        }

        AnsiConsole.Write(table);
    }

    public static string PromptUserForStringInput(string promptText)
    {
        var input = AnsiConsole.Prompt(
            new TextPrompt<string>(promptText)
            .Validate(n =>
            {
                if (string.IsNullOrEmpty(n))
                {
                    return ValidationResult.Error();
                }
                else
                {
                    return ValidationResult.Success();
                }
            }));

        return input;
    }

    public static void DisplayMessageToUser(string message)
    {
        AnsiConsole.WriteLine(message);
    }

    public static void PressAnyKeyToContinue()
    {
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}