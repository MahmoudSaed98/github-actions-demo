using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Presentation.Console;
using Moq;

namespace github_actions_demo.tests.Presentation.Console;

public sealed class ConsoleViewTests
{
    private readonly Mock<IConsoleWriter> _mockWriter;
    private readonly IConsoleView _view;

    public ConsoleViewTests()
    {
        _mockWriter = new Mock<IConsoleWriter>();
        _mockWriter.SetupAllProperties();
        _view = new ConsoleView(_mockWriter.Object);
    }

    [Fact]
    public void ShowMenu_WhenCommandsProvided_ShouldPrintAllCommands()
    {
        // Arrange
        var mockCommand = new Mock<IConsoleCommand>();
        mockCommand.Setup(c => c.CommandKey).Returns("1");
        mockCommand.Setup(c => c.Description).Returns("Test Command");
        var commands = new[] { mockCommand.Object };

        // Act
        _view.ShowMenu(commands);

        // Assert
        _mockWriter.Verify(w => w.Clear(), Times.Once);
        _mockWriter.Verify(w => w.WriteLine("1: Test Command"), Times.Once);
    }
}
