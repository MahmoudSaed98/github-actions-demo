using github_actions_demo.ConsoleApp.Models;
using github_actions_demo.ConsoleApp.Repositories;

namespace github_actions_demo.ConsoleApp.Data.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private IList<User> _users;

    public InMemoryUserRepository()
    {
        this._users = new List<User>();
    }
    public async Task<bool> Delete(Guid id)
    {
        var existingUser = this._users.FirstOrDefault(x => x.Id == id);

        if (existingUser != null)
        {
            this._users.Remove(existingUser);

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
        bool result = !this._users.Any(x => x.Email == email);

        return await Task.FromResult(result);
    }

    public async Task<bool> IsUsernameUnique(string userName)
    {

        bool result = !this._users.Any(x => x.UserName == userName);

        return await Task.FromResult(result);
    }
}