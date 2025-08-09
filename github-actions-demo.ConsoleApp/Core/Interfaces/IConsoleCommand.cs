namespace github_actions_demo.ConsoleApp.Core.Interfaces;

public interface IConsoleCommand
{
    string CommandKey { get; }
    string Description { get; }
    Task ExecuteAsync();
}