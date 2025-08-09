namespace github_actions_demo.ConsoleApp.Core.Interfaces;

public interface IConsoleReader
{
    string? GetInput(string? prompt);

    void WaitForEnterKey();
}