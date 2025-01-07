using Moq;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Tests;

public class CommandInvokerTests
{
    private readonly CommandInvoker _commandInvoker;
    private readonly Mock<IUiRender> _consoleRendererMock;
    private readonly Mock<IGameProcessor> _gameProcessorMock;

    public CommandInvokerTests()
    {
        _gameProcessorMock = new Mock<IGameProcessor>();
        _consoleRendererMock = new Mock<IUiRender>();
        _commandInvoker = new CommandInvoker(_gameProcessorMock.Object);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenPlayerGameCommandExecutedAndModeNotDefined()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GameMode).Returns(GameModes.NotDefined);
        var command = new PlayerGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
        _gameProcessorMock.Verify(g => g.InitializeGame(true), Times.Once);
        _consoleRendererMock.Verify(r => r.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenAiGameCommandExecutedAndModeNotDefined()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GameMode).Returns(GameModes.NotDefined);
        var command = new AiGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
        _gameProcessorMock.Verify(g => g.InitializeGame(false), Times.Once);
        _consoleRendererMock.Verify(r => r.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenPlayerGameCommandNotExecutedAndModeGameAi()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GameMode).Returns(GameModes.GameWithAi);
        _gameProcessorMock.Setup(g => g.GetBoard())
            .Returns(new Board());

        _gameProcessorMock.Object.GetBoard().SetGameState(GameState.Win);

        var command = new PlayerGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);


        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e.Code == "ExecuteCommand");
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenReplayCommandIsExecutedAndGameNotStarted()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GetBoard())
            .Returns(new Board());
        _gameProcessorMock.Object.GetBoard().SetGameState(GameState.NotStarted);

        var command = new ReplayCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e.Code == "ExecuteCommand");
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenReplayCommandIsExecutedAndGameIsOngoing()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GetBoard())
            .Returns(new Board());
        _gameProcessorMock.Object.GetBoard().SetGameState(GameState.Ongoing);

        var command = new ReplayCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
        _consoleRendererMock.Verify(c => c.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenInstructionCommandIsExucuted()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GetBoard())
            .Returns(new Board());
        _gameProcessorMock.Object.GetBoard().SetGameState(GameState.NotStarted);

        var command = new InstructionCommand(_consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
        _consoleRendererMock.Verify(c => c.RenderInstruction(), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenExitIsExecuted()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GameMode).Returns(GameModes.GameWithAi);
        var command = new ExitCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
        _consoleRendererMock.Verify(c => c.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenEndCommandIsExecuted()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GetBoard())
            .Returns(new Board());
        _gameProcessorMock.Object.GetBoard().SetGameState(GameState.Ongoing);
        var command = new EndGameCommand(_gameProcessorMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenCommandIsNotAllowedInOngoingState()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GetBoard())
            .Returns(new Board());
        _gameProcessorMock.Object.GetBoard().SetGameState(GameState.Ongoing);
        _gameProcessorMock.Setup(g => g.GameMode).Returns(GameModes.GameWithAi);
        var command = new PlayerGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e.Code == "ExecuteCommand");
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenMoveCommandIsExecutedAfterGameIsFinished()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.GetBoard())
            .Returns(new Board());
        _gameProcessorMock.Object.GetBoard().SetGameState(GameState.Win);
        var moveX = new MoveParameters(0, 0, PlayerTurn.X);
        var command = new MoveCommand(_gameProcessorMock.Object, moveX);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e.Code == "ExecuteCommand");
    }
}