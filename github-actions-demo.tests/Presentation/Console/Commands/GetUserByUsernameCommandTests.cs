using github_actions_demo.ConsoleApp.Core.Contracts;
using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;
using github_actions_demo.ConsoleApp.Presentation.Console.Commands;
using Moq;

namespace github_actions_demo.tests.Presentation.Console.Commands;

public sealed class GetUserByUsernameCommandTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IConsoleReader> _consoleReaderMock;
    private readonly Mock<IConsoleWriter> _consoleWriterMock;
    private readonly GetUserByUsernameCommand _command;

    public GetUserByUsernameCommandTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _consoleReaderMock = new Mock<IConsoleReader>();
        _consoleWriterMock = new Mock<IConsoleWriter>();
        _command = new GetUserByUsernameCommand(_consoleWriterMock.Object, _consoleReaderMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserExists_ShouldPrintUserDetails()
    {
        // Arrange
        var username = "found_user";
        var userResponse = new UserResponse(Guid.NewGuid(), username, "found@example.com");

        _consoleReaderMock.Setup(x => x.GetInput(It.IsAny<string>())).Returns(username);

        _userServiceMock.Setup(x => x.GetByUsernameAsync(username))
                        .ReturnsAsync(userResponse);

        // Act
        await _command.ExecuteAsync();

        // Assert
        _consoleWriterMock.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(username))), Times.Once);
    }
}
