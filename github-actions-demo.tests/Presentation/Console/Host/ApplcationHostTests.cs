using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Presentation.Console;
using github_actions_demo.ConsoleApp.Presentation.Console.Host;
using Moq;

namespace github_actions_demo.tests.Presentation.Console.Host;

public sealed class ApplcationHostTests
{
    private readonly Mock<IConsoleReader> _consoleReaderMock;
    private readonly Mock<IConsoleView> _consoleViewMock;
    private readonly Mock<IConsoleCommand> _commandOneMock;
    private readonly List<IConsoleCommand> _commands;

    public ApplcationHostTests()
    {
        _consoleReaderMock = new Mock<IConsoleReader>();
        _consoleViewMock = new Mock<IConsoleView>();
        _commandOneMock = new Mock<IConsoleCommand>();

        _commandOneMock.Setup(c => c.CommandKey).Returns(CommandKeys.RegisterUser);

        _commands = new List<IConsoleCommand>() { _commandOneMock.Object };
    }

    [Fact]
    public async Task RunAsync_WhenUserSelectsValidCommand_ShouldExecuteTheCommand()
    {
        //Arrange

        _consoleReaderMock.SetupSequence(x => x.GetInput(It.IsAny<string>()))
                          .Returns(CommandKeys.RegisterUser)
                          .Returns(CommandKeys.Exit);

        IApplicationHost appHost = new ApplicationHost(_commands, _consoleViewMock.Object,
            _consoleReaderMock.Object);

        //Act

        await appHost.RunAsync();

        //Assert

        _commandOneMock.Verify(x => x.ExecuteAsync(), Times.Once);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    [InlineData("99")]
    public async Task RunAsync_WhenUserChoiceIsInValid_ShouldShowErrorMessage(string userInput)
    {
        //Arrange
        _consoleReaderMock.SetupSequence(x => x.GetInput(It.IsAny<string>()))
                          .Returns(userInput)
                          .Returns(CommandKeys.Exit);

        IApplicationHost appHost = new ApplicationHost(_commands, _consoleViewMock.Object,
            _consoleReaderMock.Object);

        //Act         

        await appHost.RunAsync();

        //Act

        _consoleViewMock.Verify(x => x.ShowMessage("Unknown command."), Times.Once);
        _commandOneMock.Verify(x => x.ExecuteAsync(), Times.Never);
    }

}
