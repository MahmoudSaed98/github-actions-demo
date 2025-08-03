using github_actions_demo.ConsoleApp.Contracts;
using github_actions_demo.ConsoleApp.Models;
using github_actions_demo.ConsoleApp.Repositories;
using github_actions_demo.ConsoleApp.Services.Interfaces;

namespace github_actions_demo.ConsoleApp.Services;

internal class UserService(IUserRepository repository) : IUserService
{
    public void CreateUser(RegisterUserDto userDto)
    {
        repository.Insert(new User(userDto.UserName, userDto.Email));
    }
}
