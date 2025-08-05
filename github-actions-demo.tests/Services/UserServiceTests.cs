using github_actions_demo.ConsoleApp.Contracts;
using github_actions_demo.ConsoleApp.Models;
using github_actions_demo.ConsoleApp.Repositories;
using github_actions_demo.ConsoleApp.Services;
using github_actions_demo.ConsoleApp.Services.Interfaces;
using Moq;

namespace github_actions_demo.tests.Services;

public sealed class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockRepository.Object);
    }


    [Fact]
    public async Task CreateUserAsync_WithUniqueCredentials_ShouldCallInsertMethod_AndReturnUserId()
    {
        //Arrange
        var userDto = new RegisterUserDto("newuser", "new@example.com");

        _mockRepository.Setup(x => x.IsUsernameUnique(userDto.UserName))
                           .ReturnsAsync(true);

        _mockRepository.Setup(x => x.IsEmailUnique(userDto.Email))
                           .ReturnsAsync(true);

        _mockRepository.Setup(x => x.Insert(It.IsAny<User>()))
                           .Returns(Task.CompletedTask);

        //Act

        var userId = await _userService.CreateUserAsync(userDto);

        //Assert

        Assert.NotEqual(Guid.Empty, userId);

        // verifiy that the repository methods were called as expected

        _mockRepository.Verify(x => x.IsUsernameUnique(userDto.UserName), Times.Once);
        _mockRepository.Verify(x => x.IsEmailUnique(userDto.Email), Times.Once);
        _mockRepository.Verify(x => x.Insert(It.Is<User>(u => u.UserName == userDto.UserName && u.Email == userDto.Email)));
    }

    [Fact]
    public async Task CreateUserAsync_ShouldThrowException_WhenEmailIsNotUnique()
    {
        var userDto = new RegisterUserDto("uniqueUsername", "existingEmail@example.com");

        _mockRepository.Setup(x => x.IsEmailUnique(userDto.Email))
                           .ReturnsAsync(false);

        await Assert.ThrowsAsync<Exception>(() => _userService.CreateUserAsync(userDto));

        _mockRepository.Verify(x => x.Insert(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldThrowException_WhenUsernameIsNotUnique()
    {
        var userDto = new RegisterUserDto("existingUsername", "UniqueEmail@example.com");

        _mockRepository.Setup(x => x.IsUsernameUnique(userDto.UserName))
                           .ReturnsAsync(false);

        await Assert.ThrowsAsync<Exception>(() => _userService.CreateUserAsync(userDto));

        _mockRepository.Verify(x => x.Insert(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task GetUserByUserName_ShouldReturnNull_WhenUserDoesNotExist()
    {
        //Arrange

        var username = "username";

        _mockRepository.Setup(x => x.GetByUserName(username))
                           .ReturnsAsync((User?)null);

        //Act

        var result = await _userService.GetByUsernameAsync(username);

        //Assert

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByUsername_ShouldReturnUserResponse_WhenUserExists()
    {
        //Arrange

        var user = new User("username", "email@example.com");

        _mockRepository.Setup(x => x.GetByUserName(user.UserName))
                                .ReturnsAsync(user);
        //Act

        var result = await _userService.GetByUsernameAsync(user.UserName);

        //Assert

        Assert.NotNull(result);
        Assert.Equal(user.UserName, result.UserName);
        Assert.Equal(user.Email, result.Email);

        _mockRepository.Verify(x => x.GetByUserName(user.UserName), Times.Once);
    }

    [Fact]
    public async Task GetByUsernameAsync_WhenUserDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var userName = "nonexistent";
        _mockRepository.Setup(r => r.GetByUserName(userName)).ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetByUsernameAsync(userName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenUsersExist_ShouldReturnListOfUserResponses()
    {
        // Arrange
        var users = new List<User>
        {
            new User( "user1", "user1@example.com"),
            new User( "user2", "user2@example.com")
        };

        _mockRepository.Setup(r => r.GetAll()).ReturnsAsync(users);

        // Act

        var result = await _userService.GetAllAsync();

        // Assert

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(users.First().Id, result.First().Id);
        Assert.Equal(users.Last().Email, result.Last().Email);
    }

    [Fact]
    public async Task DeleteAsync_WhenDeleteSucceeds_ShouldReturnTrue()
    {
        // Arrange

        var userId = Guid.NewGuid();
        _mockRepository.Setup(r => r.Delete(userId)).ReturnsAsync(true);

        // Act

        var result = await _userService.DeleteAsync(userId);

        // Assert

        Assert.True(result);
        _mockRepository.Verify(r => r.Delete(userId), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenDeleteFails_ShouldReturnFalse()
    {
        // Arrange

        var userId = Guid.NewGuid();
        _mockRepository.Setup(r => r.Delete(userId)).ReturnsAsync(false);

        // Act

        var result = await _userService.DeleteAsync(userId);

        // Assert

        Assert.False(result);
        _mockRepository.Verify(r => r.Delete(userId), Times.Once);
    }

}
