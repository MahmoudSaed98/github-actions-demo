using github_actions_demo.ConsoleApp.Core.Contracts;
using github_actions_demo.ConsoleApp.Core.Exceptions;
using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;
using github_actions_demo.ConsoleApp.Presentation.Console.Commands;
using Moq;

namespace github_actions_demo.tests.Presentation.Console.Commands;

public sealed class RegisterUserCommandTests
{
    private readonly Mock<IConsoleReader> _consoleReaderMock;
    private readonly Mock<IConsoleWriter> _consoleWriterMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly RegisterUserCommand _command;

    public RegisterUserCommandTests()
    {
        _consoleReaderMock = new Mock<IConsoleReader>();
        _consoleWriterMock = new Mock<IConsoleWriter>();
        _userServiceMock = new Mock<IUserService>();

        _command = new RegisterUserCommand(_consoleWriterMock.Object, _consoleReaderMock.Object,
            _userServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserProvidesValidData_ShouldCallCreateUserAndPrintSuccess()
    {
        // Arrange
        var username = "testuser";
        var email = "test@example.com";
        var newId = Guid.NewGuid();

        _consoleReaderMock.SetupSequence(r => r.GetInput(It.IsAny<string>()))
            .Returns(username)
            .Returns(email);

        _userServiceMock.Setup(s => s.CreateUserAsync(It.IsAny<RegisterUserDto>()))
                        .ReturnsAsync(newId);

        // Act
        await _command.ExecuteAsync();

        // Assert
        _userServiceMock.Verify(s => s.CreateUserAsync(
            It.Is<RegisterUserDto>(dto => dto.UserName == username && dto.Email == email)),
            Times.Once);

        _consoleWriterMock.Verify(w => w.WriteLine($"new user was successfully registred with Id: {newId}"), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidationExceptionIsThrown_ShouldPrintAllValidationErrors()
    {
        // Arrange
        var validationErrors = new List<string> { "Invalid Username.", "Invalid Email." };
        var validationException = new ValidationException(validationErrors);

        _consoleReaderMock.Setup(r => r.GetInput(It.IsAny<string>())).Returns("any invalid value");

        _userServiceMock.Setup(s => s.CreateUserAsync(It.IsAny<RegisterUserDto>()))
                        .ThrowsAsync(validationException);

        // Act
        await _command.ExecuteAsync();

        // Assert
        _consoleWriterMock.Verify(w => w.WriteLine("Invalid Username."), Times.Once);
        _consoleWriterMock.Verify(w => w.WriteLine("Invalid Email."), Times.Once);
        _consoleWriterMock.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Exactly(2));
    }


}
