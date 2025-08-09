using github_actions_demo.ConsoleApp.Core.Contracts;
using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;
using github_actions_demo.ConsoleApp.Presentation.Console.Commands;
using Moq;

namespace github_actions_demo.tests.Presentation.Console.Commands;

public sealed class GetAllUsersCommandTests
{
    private readonly Mock<IConsoleWriter> _consoleWriterMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IConsoleReader> _consoleReaderMock;
    private readonly GetAllUsersCommand _command;

    public GetAllUsersCommandTests()
    {
        _consoleWriterMock = new Mock<IConsoleWriter>();
        _userServiceMock = new Mock<IUserService>();
        _consoleReaderMock = new Mock<IConsoleReader>();

        _command = new GetAllUsersCommand(_consoleWriterMock.Object, _consoleReaderMock.Object,
            _userServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUsersExist_ShouldPrintAllUsers()
    {
        //Arrange

        List<UserResponse> users =
        [
         new UserResponse(Guid.NewGuid(), "User1", "Email1@example.com"),
         new UserResponse(Guid.NewGuid(), "User2", "Email2@example.com")
        ];

        _userServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

        //Act

        await _command.ExecuteAsync();

        //Assert

        _consoleWriterMock.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("User1"))), Times.Once);
        _consoleWriterMock.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("User2"))), Times.Once);
        _consoleReaderMock.Verify(x => x.WaitForEnterKey(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenNoUsersExist_ShouldPrintNotFoundMessage()
    {
        // Arrange
        _userServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<UserResponse>());

        // Act
        await _command.ExecuteAsync();

        // Assert
        _consoleWriterMock.Verify(w => w.WriteLine("No users found in the system."), Times.Once);
    }
}
