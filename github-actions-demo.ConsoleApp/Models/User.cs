namespace github_actions_demo.ConsoleApp.Models;

public class User
{
    public User(string userName, string email)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = email;
    }

    public Guid Id { get; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
