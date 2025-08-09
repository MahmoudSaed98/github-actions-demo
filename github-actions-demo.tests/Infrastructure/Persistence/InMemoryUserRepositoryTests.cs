using github_actions_demo.ConsoleApp.Core.Models;
using github_actions_demo.ConsoleApp.Infrastructure.Persistence;

namespace github_actions_demo.tests.Infrastructure.Persistence;

public class InMemoryUserRepositoryTests
{
    private readonly InMemoryUserRepository _repository;

    public InMemoryUserRepositoryTests()
    {
        _repository = new InMemoryUserRepository();
    }

    [Fact]
    public async Task Insert_ShouldAddUser_ToInternalList()
    {
        //Arrange
        var user = new User("testuser", "testemail@example.com");

        //Act

        await _repository.Insert(user);
        var users = await _repository.GetAll();

        //Assert

        Assert.Single(users);
        Assert.Equal(user.Id, users.First().Id);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllUsers_WhenUsersExist()
    {
        //Arrange

        var user1 = new User("testuser1", "testemail1@example.com");
        var user2 = new User("testuser2", "testemail2@example.com");

        await _repository.Insert(user1);
        await _repository.Insert(user2);

        //Act

        var allUsers = await _repository.GetAll();

        //Assert

        Assert.Equal(2, allUsers.Count);
        Assert.Contains(user1, allUsers);
        Assert.Contains(user2, allUsers);
    }

    [Theory]
    [InlineData("email@example.com")]
    [InlineData("EMAIL@EXAMPLE.COM")]
    public async Task GetByEmail_ShouldReturnUser_WhenUserExists(string searchEmail)
    {
        //Arrange

        var user = new User("username", "email@example.com");

        await _repository.Insert(user);

        //Act

        var foundUser = await _repository.GetByEmailAsync(searchEmail);

        //Assert

        Assert.NotNull(foundUser);
        Assert.Equal(user.Id, foundUser.Id);
    }

    [Fact]
    public async Task GetByEmail_ShouldReturnNull_WhenUserDoesNotExist()
    {
        //Act
        var user = await _repository.GetByEmailAsync("nonexistentEmail@example.com");

        //Assert
        Assert.Null(user);
    }

    [Theory]
    [InlineData("testuser")]
    [InlineData("TESTUSER")]
    public async Task GetByUsername_ShouldReturnUser_WhenUserExists(string username)
    {
        //Arrange

        var user = new User("testuser", "testemail@example.com");

        await _repository.Insert(user);

        //Act

        var foundUser = await _repository.GetByUserName(username);

        //Assert

        Assert.NotNull(foundUser);
        Assert.Equal(user.Id, foundUser.Id);
    }

    [Fact]
    public async Task GetByUserName_ShouldReturnNull_WhenUserDoesNotExist()
    {
        //Act
        var foundUser = await _repository.GetByUserName("nonexistentUsername");

        //Assert
        Assert.Null(foundUser);
    }

    [Fact]
    public async Task Delete_ShouldDeleteUser_WhenUserExists_AndReturnTrue()
    {
        //Arrange

        var user = new User("testusername", "testemail@example.com");

        await _repository.Insert(user);

        var allUsers = await _repository.GetAll();

        //Act

        bool isDeleted = await _repository.Delete(user.Id);

        //Assert

        Assert.True(isDeleted);
        Assert.Empty(allUsers);
    }

    [Fact]
    public async Task Delete_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        //Arrange

        var nonExistentId = Guid.NewGuid();

        //Act

        bool expectedResult = false;

        bool actualResult = await _repository.Delete(nonExistentId);

        //Assert

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task IsUsernameUnique_ShouldReturnTrue_WhenUsernameIsUnique()
    {
        //Act

        bool isUnique = await _repository.IsUsernameUnique("uniqueUsername");

        //Assert

        Assert.True(isUnique);
    }

    [Fact]
    public async Task IsUsernameUnique_ShouldReturnFalse_WhenUsernameIsNotUnique()
    {
        //Arrange

        var user = new User("username", "email@example.com");

        await _repository.Insert(user);

        //Act
        bool isUnique = await _repository.IsUsernameUnique(user.UserName);

        //Assert
        Assert.False(isUnique);
    }

    [Fact]
    public async Task IsEmailUnique_ShouldReturnTrue_WhenEmailIsUnique()
    {
        //Act

        bool isUnique = await _repository.IsEmailUnique("uniqueEmail");

        //Assert

        Assert.True(isUnique);
    }

    [Fact]
    public async Task IsEmailUnique_ShouldReturnFalse_WhenEmailIsNotUnique()
    {
        //Arrange

        var user = new User("username", "email@example.com");

        await _repository.Insert(user);

        //Act
        bool isUnique = await _repository.IsEmailUnique(user.Email);

        //Assert
        Assert.False(isUnique);
    }
}
