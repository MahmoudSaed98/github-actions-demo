using github_actions_demo.ConsoleApp.Core.Contracts;
using github_actions_demo.ConsoleApp.Core.Exceptions;
using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;

namespace github_actions_demo.ConsoleApp.Presentation.Console.Commands;

public sealed class RegisterUserCommand : BaseUserConsoleCommand<RegisterUserCommand>
{
    public RegisterUserCommand(IConsoleWriter consoleWriter, IConsoleReader consoleReader,
        IUserService userService) : base(consoleWriter, consoleReader, userService)
    {
    }

    public override string CommandKey => CommandKeys.RegisterUser;
    public override string Description => "Register new user";

    public override async Task ExecuteAsync()
    {

        try
        {
            string username = ConsoleReader.GetInput("Enter username")!;

            string email = ConsoleReader.GetInput("Enter email")!;

            var userDto = new RegisterUserDto(username, email);

            var userId = await UserService.CreateUserAsync(userDto)
                                          .ConfigureAwait(false);

            ConsoleWriter.WriteLine($"new user was successfully registred with Id: {userId}");
            ConsoleReader.WaitForEnterKey();
        }
        catch (Exception e)
        {
            HandleExceptionInternal(e);
        }
    }

    private void HandleExceptionInternal(Exception e)
    {
        if (e is ValidationException ve)
        {

            foreach (var error in ve.Errors)
            {
                ConsoleWriter.WriteLine(error);
            }

            ConsoleReader.WaitForEnterKey();
            return;
        }

        ConsoleWriter.WriteLine($"Error: {e.Message}");
        ConsoleReader.WaitForEnterKey();
    }
}