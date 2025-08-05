using github_actions_demo.ConsoleApp.Contracts;
using github_actions_demo.ConsoleApp.Models;
using github_actions_demo.ConsoleApp.Repositories;
using github_actions_demo.ConsoleApp.Services.Interfaces;

namespace github_actions_demo.ConsoleApp.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<Guid> CreateUserAsync(RegisterUserDto userDto)
    {

        var user = new User(userDto.UserName, userDto.Email);

        if (!await repository.IsEmailUnique(userDto.Email))
        {
            throw new Exception("Email already in use.");
        }

        if (!await repository.IsUsernameUnique(userDto.UserName))
        {
            throw new Exception("UserName already in use.");
        }

        await repository.Insert(user);

        return await Task.FromResult(user.Id);
    }

    public async Task<UserResponse?> GetByUsernameAsync(string userName)
    {
        var user = await repository.GetByUserName(userName);

        return user is null ? null :
               new UserResponse(user.Id, user.UserName, user.Email);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        bool isRemoved = await repository.Delete(id);

        return isRemoved;
    }

    public async Task<UserResponse?> GetByEmailAsync(string email)
    {
        var user = await repository.GetByEmailAsync(email);

        return user is null ? null :
               new UserResponse(user.Id, user.UserName, user.Email);
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var users = await repository.GetAll();

        return users.Select(x => new UserResponse(x.Id, x.UserName, x.Email));
    }
}