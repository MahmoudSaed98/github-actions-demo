using github_actions_demo.ConsoleApp.Core.Interfaces;

namespace github_actions_demo.ConsoleApp.Presentation.Console.Host;

public sealed class ApplicationHost : IApplicationHost
{
    private readonly IConsoleView _consoleView;
    private readonly IConsoleReader _consoleReader;
    private readonly IReadOnlyDictionary<string, IConsoleCommand> _commands;

    public ApplicationHost(IEnumerable<IConsoleCommand> commands, IConsoleView consoleView, IConsoleReader consoleReader)
    {
        _consoleView = consoleView;
        _commands = commands.ToDictionary(c => c.CommandKey);
        _consoleReader = consoleReader;
    }

    public async Task RunAsync()
    {

        while (true)
        {

            _consoleView.ShowMenu(_commands.Values);

            string? choice = _consoleReader.GetInput("Enter your choice:");

            if (choice == CommandKeys.Exit)
            {
                _consoleView.ShowMessage("Exiting application! press any key to continue...");
                _consoleReader.WaitForEnterKey();
                break;
            }

            if (_commands.TryGetValue(choice!, out var command))
            {
                await command.ExecuteAsync();
            }
            else
            {
                _consoleView.ShowMessage("Unknown command.");
                _consoleReader.WaitForEnterKey();
                break;
            }

        }
    }
}