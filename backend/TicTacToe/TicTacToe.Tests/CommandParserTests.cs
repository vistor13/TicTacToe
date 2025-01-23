using Moq;
using TicTacToe.Application.Commands;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.InputProcessing;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Tests;

public class CommandParserTests
{
    private readonly CommandParser _commandParser;
    private readonly Mock<IUiRender> _consoleRendererMock;
    private readonly Mock<IGameProcessor> _gameProcessorMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;

    public CommandParserTests()
    {
        _gameProcessorMock = new Mock<IGameProcessor>();
        _consoleRendererMock = new Mock<IUiRender>();
        _serviceProviderMock = new Mock<IServiceProvider>();

        _commandParser = new CommandParser(
            _gameProcessorMock.Object,
            _consoleRendererMock.Object,
            _serviceProviderMock.Object
        );
    }

    [Fact]
    public void CommandParse_ShouldReturnPlayerGameCommand_WhenInputIsStart()
    {
        // Arrange
        _serviceProviderMock.Setup(s => s.GetService(typeof(PlayerGameCommand)))
            .Returns(new PlayerGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object));

        // Act
        var result = _commandParser.CommandParse("game player");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PlayerGameCommand>(result);
    }

    [Fact]
    public void CommandParse_ShouldReturnReplayCommand_WhenInputIsReplay()
    {
        // Arrange
        _serviceProviderMock.Setup(s => s.GetService(typeof(ReplayCommand)))
            .Returns(new ReplayCommand(_gameProcessorMock.Object, _consoleRendererMock.Object));

        // Act
        var result = _commandParser.CommandParse("replay");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ReplayCommand>(result);
    }

    [Fact]
    public void CommandParse_ShouldReturnError_WhenInputIsInvalidCommand()
    {
        // Arrange
        var invalidCommand = "invalidCommand";

        // Act
        var result = _commandParser.CommandParse(invalidCommand);

        // Assert
        Assert.Null(result);
        _consoleRendererMock.Verify(c => c.RenderError(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void CommandParse_ShouldReturnMoveCommand_WhenInputIsValidMove()
    {
        // Arrange
        var board = new Board();
        _gameProcessorMock.Setup(g => g.GetBoard()).Returns(board);

        var moveInput = "move 1 2";

        // Act
        var result = _commandParser.CommandParse(moveInput);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MoveCommand>(result);

        var moveCommand = result as MoveCommand;
        Assert.NotNull(moveCommand);
    }

    [Fact]
    public void CommandParse_ShouldReturnNull_WhenMoveCommandHasInvalidParameters()
    {
        // Arrange
        var invalidMoveInput = "move 1 invalid";

        // Act
        var result = _commandParser.CommandParse(invalidMoveInput);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CommandParse_ShouldReturnExitCommand_WhenInputIsExit()
    {
        // Arrange
        _serviceProviderMock.Setup(s => s.GetService(typeof(ExitCommand)))
            .Returns(new ExitCommand(_gameProcessorMock.Object, _consoleRendererMock.Object));

        // Act
        var result = _commandParser.CommandParse("exit");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ExitCommand>(result);
    }

    [Fact]
    public void CommandParse_ShouldReturnInstructionCommand_WhenInputIsHelp()
    {
        // Arrange
        _serviceProviderMock.Setup(s => s.GetService(typeof(InstructionCommand)))
            .Returns(new InstructionCommand(_consoleRendererMock.Object));

        // Act
        var result = _commandParser.CommandParse("help");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<InstructionCommand>(result);
    }
}