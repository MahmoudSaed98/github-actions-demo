using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;

namespace github_actions_demo.ConsoleApp.Presentation.Console.Commands;

public abstract class BaseUserConsoleCommand<TCommand> : IConsoleCommand where TCommand : class
{
    protected readonly IConsoleWriter ConsoleWriter;
    protected readonly IConsoleReader ConsoleReader;
    protected readonly IUserService UserService;

    protected BaseUserConsoleCommand(IConsoleWriter consoleWriter, IConsoleReader consoleReader, IUserService userService)
    {
        ConsoleWriter = consoleWriter;
        ConsoleReader = consoleReader;
        UserService = userService;
    }

    public abstract string CommandKey { get; }

    public abstract string Description { get; }

    public abstract Task ExecuteAsync();
}