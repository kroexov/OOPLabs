using System.Collections.Generic;
using Spectre.Console;

namespace Banks.Services
{
    public class ConsoleService
    {
        public string AskData(string message)
        {
            string returnData = AnsiConsole.Ask<string>("[green]" + message + "[/]");
            return returnData;
        }

        public void WriteMessage(string message)
        {
            AnsiConsole.Markup("[green]" + message + "[/]" + "\n");
        }

        public string OptionsAsking(string message, List<string> options)
        {
            var answer = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[red]" + message + "[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to choose)[/]")
                    .AddChoices(options));
            return answer;
        }
    }
}