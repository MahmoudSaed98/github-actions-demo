using github_actions_demo.ConsoleApp.Contracts;

namespace github_actions_demo.ConsoleApp.Services.Interfaces;

public interface IUserService
{
    Task<Guid> CreateUserAsync(RegisterUserDto userDto);
    Task<UserResponse?> GetByUsernameAsync(string userName);
    Task<UserResponse?> GetByEmailAsync(string email);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<UserResponse>> GetAllAsync();
}