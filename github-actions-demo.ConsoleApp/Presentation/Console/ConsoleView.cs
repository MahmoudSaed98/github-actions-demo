using github_actions_demo.ConsoleApp.Core.Interfaces;

namespace github_actions_demo.ConsoleApp.Presentation.Console;

public sealed class ConsoleView : IConsoleView
{
    private readonly IConsoleWriter _writer;

    public ConsoleView(IConsoleWriter writer)
    {
        _writer = writer;
    }

    public void ShowMenu(IEnumerable<IConsoleCommand> commands)
    {
        _writer.Clear();
        _writer.WriteLine("=== User Management Console ===");

        foreach (var command in commands)
        {
            _writer.WriteLine($"{command.CommandKey}: {command.Description}");
        }

        _writer.WriteLine("0. Exit");
        _writer.WriteLine("=============================");
    }

    public void ShowMessage(string message)
    {
        _writer.WriteLine(message);
    }
}