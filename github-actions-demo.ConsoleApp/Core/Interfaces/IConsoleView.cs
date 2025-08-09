namespace github_actions_demo.ConsoleApp.Core.Interfaces;

public interface IConsoleView
{
    void ShowMenu(IEnumerable<IConsoleCommand> commands);

    void ShowMessage(string message);
}