using github_actions_demo.ConsoleApp.Core.Models;

namespace github_actions_demo.ConsoleApp.Core.Repositories;

public interface IUserRepository
{
    Task Insert(User user);
    Task<bool> Delete(Guid id);
    Task<User?> GetByUserName(string userName);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> IsUsernameUnique(string userName);
    Task<bool> IsEmailUnique(string email);
    Task<IList<User>> GetAll();
}