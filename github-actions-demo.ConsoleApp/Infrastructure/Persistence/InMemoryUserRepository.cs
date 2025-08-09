using github_actions_demo.ConsoleApp.Core.Models;
using github_actions_demo.ConsoleApp.Core.Repositories;

namespace github_actions_demo.ConsoleApp.Infrastructure.Persistence;

public class InMemoryUserRepository : IUserRepository
{
    private IList<User> _users;

    public InMemoryUserRepository()
    {
        _users = new List<User>();
    }
    public async Task<bool> Delete(Guid id)
    {
        var existingUser = _users.FirstOrDefault(x => x.Id == id);

        if (existingUser != null)
        {
            _users.Remove(existingUser);

            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }

    public async Task<IList<User>> GetAll()
    {
        return await Task.FromResult(_users);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = _users
            .FirstOrDefault(x => 0 == string.Compare(x.Email, email, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(user);
    }

    public async Task<User?> GetByUserName(string userName)
    {
        var user = _users
            .FirstOrDefault(x => 0 == string.Compare(x.UserName, userName, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(user);
    }

    public async Task Insert(User user)
    {
        _users.Add(user);

        await Task.CompletedTask;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        bool result = !_users.Any(x => x.Email == email);

        return await Task.FromResult(result);
    }

    public async Task<bool> IsUsernameUnique(string userName)
    {

        bool result = !_users.Any(x => x.UserName == userName);

        return await Task.FromResult(result);
    }
}