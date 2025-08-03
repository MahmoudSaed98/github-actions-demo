using github_actions_demo.ConsoleApp.Models;

namespace github_actions_demo.ConsoleApp.Repositories;

public interface IUserRepository
{
    void Insert(User user);
    void Update(User user);
    void Delete(User user);
    User? GetByUserName(string userName);
}