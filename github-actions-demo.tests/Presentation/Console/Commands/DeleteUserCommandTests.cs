using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Services;
using github_actions_demo.ConsoleApp.Presentation.Console.Commands;
using Moq;

namespace github_actions_demo.tests.Presentation.Console.Commands;

public sealed class DeleteUserCommandTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IConsoleReader> _consoleReaderMock;
    private readonly Mock<IConsoleWriter> _consoleWriterMock;
    private readonly DeleteUserCommand _command;

    public DeleteUserCommandTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _consoleReaderMock = new Mock<IConsoleReader>();
        _consoleWriterMock = new Mock<IConsoleWriter>();

        _command = new DeleteUserCommand(_consoleWriterMock.Object, _consoleReaderMock.Object,
            _userServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidIdAndSuccessfulDeletion_ShouldPrintSuccess()
    {
        //Arrange

        var userId = Guid.NewGuid();

        _consoleReaderMock.Setup(x => x.GetInput(It.IsAny<string>()))
                          .Returns(userId.ToString());

        _userServiceMock.Setup(x => x.DeleteAsync(userId)).ReturnsAsync(true);

        //Act

        await _command.ExecuteAsync();

        //Assert

        _userServiceMock.Verify(x => x.DeleteAsync(userId), Times.Once);
        _consoleWriterMock.Verify(x => x.WriteLine("user deleted sucessfully."), Times.Once);
        _consoleReaderMock.Verify(x => x.WaitForEnterKey(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidIdAndFailedDeletion_ShouldPrintFailure()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _consoleReaderMock.Setup(x => x.GetInput(It.IsAny<string>())).Returns(userId.ToString());
        _userServiceMock.Setup(x => x.DeleteAsync(userId)).ReturnsAsync(false);

        // Act
        await _command.ExecuteAsync();

        // Assert
        _consoleWriterMock.Verify(x => x.WriteLine(It.Is<string>(s => s.Contains("not found"))), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidIdFormat_ShouldPrintErrorAndNotCallService()
    {
        // Arrange
        _consoleReaderMock.Setup(x => x.GetInput(It.IsAny<string>())).Returns("this-is-not-a-guid");

        // Act
        await _command.ExecuteAsync();

        // Assert
        _consoleWriterMock.Verify(x => x.WriteLine("Invalid Id format."), Times.Once);
        _userServiceMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Never);
    }
}
