using github_actions_demo.ConsoleApp.Core.Interfaces;

namespace github_actions_demo.ConsoleApp.Infrastructure;

public sealed class ConsoleWrapper : IConsoleWriter, IConsoleReader
{
    public void Clear() => Console.Clear();

    public string? GetInput(string? prompt)
    {
        Console.WriteLine(prompt);

        return Console.ReadLine() ?? string.Empty;
    }

    public void WaitForEnterKey() => Console.ReadKey();
    public void WriteLine(string value) => Console.WriteLine(value);
}