using github_actions_demo.ConsoleApp.Core.Contracts;
using github_actions_demo.ConsoleApp.Core.Models;
using github_actions_demo.ConsoleApp.Core.Repositories;

namespace github_actions_demo.ConsoleApp.Core.Services;

public class UserService(IUserRepository repository) : IUserService
{
    private IList<string> validationErrors = new List<string>();

    public async Task<Guid> CreateUserAsync(RegisterUserDto userDto)
    {

        ValidateDto(userDto);

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

    private void ValidateDto(RegisterUserDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.UserName))
        {
            this.validationErrors.Add("Invalid Username.");
        }

        if (string.IsNullOrWhiteSpace(userDto.Email))
        {
            this.validationErrors.Add("Invalid Email.");
        }

        if (this.validationErrors.Any())
        {
            throw new Exceptions.ValidationException(this.validationErrors);
        }
    }

    public async Task<UserResponse?> GetByUsernameAsync(string? userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("Invalid username.");
        }

        var user = await repository.GetByUserName(userName);

        return user is null ? null :
               new UserResponse(user.Id, user.UserName, user.Email);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        bool isRemoved = await repository.Delete(id);

        return isRemoved;
    }

    public async Task<UserResponse?> GetByEmailAsync(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Invalid email.");
        }

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