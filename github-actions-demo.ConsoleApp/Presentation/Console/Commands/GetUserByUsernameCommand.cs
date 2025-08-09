using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;

namespace github_actions_demo.ConsoleApp.Presentation.Console.Commands;

public class GetUserByUsernameCommand : BaseUserConsoleCommand<GetUserByUsernameCommand>
{
    public GetUserByUsernameCommand(IConsoleWriter consoleWriter, IConsoleReader consoleReader,
        IUserService userService) : base(consoleWriter, consoleReader, userService)
    {
    }

    public override string CommandKey => CommandKeys.GetUserByUsername;

    public override string Description => "Get user by username";

    public override async Task ExecuteAsync()
    {

        try
        {
            string? username = ConsoleReader.GetInput("Enter username");

            var userResponse = await UserService.GetByUsernameAsync(username)
                                                .ConfigureAwait(false);

            if (userResponse is not null)
            {
                ConsoleWriter.WriteLine($"{userResponse.UserName}, {userResponse.Email}");
                ConsoleReader.WaitForEnterKey();
                return;
            }

            ConsoleWriter.WriteLine($"User with username: '{username}' was not found.");
            ConsoleReader.WaitForEnterKey();
        }
        catch (Exception e)
        {
            ConsoleWriter.WriteLine($"Error: {e.Message}");
            ConsoleReader.WaitForEnterKey();
        }
    }
}