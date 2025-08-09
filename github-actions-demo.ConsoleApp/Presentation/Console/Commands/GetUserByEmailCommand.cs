using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;

namespace github_actions_demo.ConsoleApp.Presentation.Console.Commands;

public sealed class GetUserByEmailCommand : BaseUserConsoleCommand<GetUserByEmailCommand>
{
    public GetUserByEmailCommand(IConsoleWriter consoleWriter, IConsoleReader consoleReader, IUserService userService)
        : base(consoleWriter, consoleReader, userService)
    {
    }

    public override string CommandKey => CommandKeys.GetUserByEmail;

    public override string Description => "Get user by email";

    public override async Task ExecuteAsync()
    {

        try
        {
            string? email = ConsoleReader.GetInput("Enter user email");

            var userResponse = await UserService.GetByEmailAsync(email)
                                                .ConfigureAwait(false);

            if (userResponse is not null)
            {
                ConsoleWriter.WriteLine($"{userResponse.UserName}, {userResponse.Email}");
                ConsoleReader.WaitForEnterKey();
                return;
            }

            ConsoleWriter.WriteLine($"User with email: '{email}' was not found.");
            ConsoleReader.WaitForEnterKey();
        }
        catch (Exception e)
        {
            ConsoleWriter.WriteLine($"Error: {e.Message}");
            ConsoleReader.WaitForEnterKey();
        }
    }

}