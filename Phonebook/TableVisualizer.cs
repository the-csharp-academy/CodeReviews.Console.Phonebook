using System.Collections.Generic;
using System.Reflection;
using Spectre.Console;

namespace Phonebook
{
    public class TableVisualizer
    {
        public static void ShowTable<T>(List<T> data, string? tableName) where T : class
        {
            if(data == null || data.Count == 0)
            {
                AnsiConsole.Write("No data!");
                return;
            }

            var table = new Table()
                        .Title(tableName?.ToUpper() ?? "");

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach(var property in properties)
            {
                table.AddColumn(new TableColumn(property.Name));
            }

            foreach(var item in data)
            {
                List<string> rowValues = new List<string>();
                foreach(var property in properties)
                {
                    var value = property.GetValue(item);
                    rowValues.Add(value?.ToString() ?? "null");
                }
                table.AddRow(rowValues.ToArray());
            }

            Console.Clear();
            AnsiConsole.Write(table);
        }

        public static void ShowChoices(List<string> choices, string? tableName)
        {
            var table = new Table()
                        .Title(tableName?.ToUpper() ?? "");

            table.AddColumn(new TableColumn("No"));
            table.AddColumn(new TableColumn("Action"));

            int i = 0;
            foreach(var choice in choices)
            {
                List<string> rowValues = new List<string>();
                rowValues.Add(i.ToString());
                rowValues.Add(choice);
                table.AddRow(rowValues.ToArray());
                i++;
            }

            AnsiConsole.Write(table);

            AnsiConsole.Write("\n\nEnter a number to choose an action:\n> ");
        }

        public static void ShowSingleColumn(List<string> data, string? colName, string? tableName)
        {
            var table = new Table()
                        .Title(tableName?.ToUpper() ?? "");

            table.AddColumn(new TableColumn(colName?.ToUpper() ?? ""));

            foreach (var item in data)
            {
                List<string> rowValues = new List<string>();
                rowValues.Add(item);
                table.AddRow(rowValues.ToArray());
            }

            Console.Clear();
            AnsiConsole.Write(table);
        }
    }
}
