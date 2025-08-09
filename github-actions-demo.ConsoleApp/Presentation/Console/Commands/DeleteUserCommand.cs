using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;

namespace github_actions_demo.ConsoleApp.Presentation.Console.Commands;

public sealed class DeleteUserCommand : BaseUserConsoleCommand<DeleteUserCommand>
{
    public DeleteUserCommand(IConsoleWriter consoleWriter, IConsoleReader consoleReader, IUserService userService)
        : base(consoleWriter, consoleReader, userService)
    {
    }

    public override string CommandKey => CommandKeys.DeleteUser;

    public override string Description => "Delete user";

    public override async Task ExecuteAsync()
    {

        string? userId = ConsoleReader.GetInput("Enter user id");

        if (!Guid.TryParse(userId, out Guid id))
        {
            ConsoleWriter.WriteLine("Invalid Id format.");
            ConsoleReader.WaitForEnterKey();
            return;
        }

        bool isDeleted = await UserService.DeleteAsync(id)
                                          .ConfigureAwait(false);

        if (isDeleted)
        {
            ConsoleWriter.WriteLine("user deleted sucessfully.");
            ConsoleReader.WaitForEnterKey();
        }
        else
        {
            ConsoleWriter.WriteLine($"User with Id '{userId}' not found or could not be deleted.");
            ConsoleReader.WaitForEnterKey();
        }
    }
}