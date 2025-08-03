using github_actions_demo.ConsoleApp.Contracts;

namespace github_actions_demo.ConsoleApp.Services.Interfaces;

internal interface IUserService
{
    void CreateUser(RegisterUserDto userDto);
}
