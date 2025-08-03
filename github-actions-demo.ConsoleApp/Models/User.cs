namespace github_actions_demo.ConsoleApp.Models;

public class User
{
    public User(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }

    public string UserName { get; set; }
    public string Email { get; set; }
}
