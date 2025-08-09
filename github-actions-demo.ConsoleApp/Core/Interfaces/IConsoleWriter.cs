namespace github_actions_demo.ConsoleApp.Core.Interfaces;

public interface IConsoleWriter
{
    void WriteLine(string value);

    void Clear();
}