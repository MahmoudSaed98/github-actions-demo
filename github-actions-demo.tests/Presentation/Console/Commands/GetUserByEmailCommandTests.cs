using github_actions_demo.ConsoleApp.Core.Contracts;
using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;
using github_actions_demo.ConsoleApp.Presentation.Console.Commands;
using Moq;

namespace github_actions_demo.tests.Presentation.Console.Commands;

public sealed class GetUserByEmailCommandTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IConsoleReader> _consoleReaderMock;
    private readonly Mock<IConsoleWriter> _consoleWriterMock;
    private readonly GetUserByEmailCommand _command;

    public GetUserByEmailCommandTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _consoleReaderMock = new Mock<IConsoleReader>();
        _consoleWriterMock = new Mock<IConsoleWriter>();
        _command = new GetUserByEmailCommand(_consoleWriterMock.Object, _consoleReaderMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserExists_ShouldPrintUserDetails()
    {
        // Arrange
        var email = "found@example.com";
        var userResponse = new UserResponse(Guid.NewGuid(), "found_user", email);

        _consoleReaderMock.Setup(x => x.GetInput(It.IsAny<string>())).Returns(email);

        _userServiceMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(userResponse);

        // Act
        await _command.ExecuteAsync();

        // Assert
        _consoleWriterMock.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(userResponse.Email))), Times.Once);
        _consoleWriterMock.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(userResponse.UserName))), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserDoesNotExist_ShouldPrintNotFoundMessage()
    {
        // Arrange
        var email = "notfound@example.com";

        _consoleReaderMock.Setup(x => x.GetInput(It.IsAny<string>())).Returns(email);

        _userServiceMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync((UserResponse?)null);

        // Act
        await _command.ExecuteAsync();

        // Assert
        _consoleWriterMock.Verify(x => x.WriteLine($"User with email: '{email}' was not found."), Times.Once);
    }
}
