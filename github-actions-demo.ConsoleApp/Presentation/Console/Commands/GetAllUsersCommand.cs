using github_actions_demo.ConsoleApp.Core.Contracts;
using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;

namespace github_actions_demo.ConsoleApp.Presentation.Console.Commands;

public sealed class GetAllUsersCommand : BaseUserConsoleCommand<GetAllUsersCommand>
{
    public GetAllUsersCommand(IConsoleWriter consoleWriter, IConsoleReader consoleReader, IUserService userService) : base(consoleWriter, consoleReader, userService)
    {
    }

    public override string CommandKey => CommandKeys.GetAllUsers;
    public override string Description => "Get all users";

    public override async Task ExecuteAsync()
    {

        IEnumerable<UserResponse> users = await UserService.GetAllAsync()
                                                           .ConfigureAwait(false);

        if (!users.Any())
        {
            ConsoleWriter.WriteLine("No users found in the system.");
            ConsoleReader.WaitForEnterKey();
            return;
        }

        DisplayUsers(users);
    }

    private void DisplayUsers(IEnumerable<UserResponse> users)
    {
        ConsoleWriter.WriteLine("\n--- All Users ---\n");

        foreach (var user in users)
        {
            ConsoleWriter.WriteLine($"\nID: {user.Id} | Username: {user.UserName} | Email: {user.Email}");
        }

        ConsoleReader.WaitForEnterKey();
    }
}