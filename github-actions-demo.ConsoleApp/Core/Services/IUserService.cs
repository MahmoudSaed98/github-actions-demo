using github_actions_demo.ConsoleApp.Core.Contracts;

namespace github_actions_demo.ConsoleApp.Core.Services;

public interface IUserService
{
    Task<Guid> CreateUserAsync(RegisterUserDto userDto);
    Task<UserResponse?> GetByUsernameAsync(string? userName);
    Task<UserResponse?> GetByEmailAsync(string? email);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<UserResponse>> GetAllAsync();
}