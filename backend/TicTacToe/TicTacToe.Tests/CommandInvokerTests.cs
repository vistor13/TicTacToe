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
    public void Execute_ShouldReturnSuccess_WhenStartCommandIsExecutedAndGameNotStarted()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.State).Returns(GameState.NotStarted);
        var command = new StartCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
        _gameProcessorMock.Verify(g => g.InitializeGame(), Times.Once);
        _consoleRendererMock.Verify(r => r.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenStartCommandIsExecutedAndGameAlreadyStarted()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.State).Returns(GameState.Ongoing);
        var command = new StartCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.True(result.IsError);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenReplayCommandIsExecutedAndGameNotStarted()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.State).Returns(GameState.NotStarted);
        var command = new ReplayCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.True(result.IsError);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenReplayCommandIsExecutedAndGameIsOngoing()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.State).Returns(GameState.Ongoing);
        var command = new ReplayCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
        _consoleRendererMock.Verify(c => c.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenMoveCommandIsExecutedAndGameIsOngoing()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.State).Returns(GameState.Ongoing);
        var moveX = new MoveParameters(0, 0, PlayerTurn.X);
        var command = new MoveCommand(_gameProcessorMock.Object, moveX);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenInstructionCommandIsExecuted()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.State).Returns(GameState.Ongoing);
        var command = new InstructionCommand(_consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.False(result.IsError);
        _consoleRendererMock.Verify(c => c.RenderInstruction(), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenCommandIsNotAllowedInCurrentState()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.State).Returns(GameState.Ongoing);
        var command = new StartCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.True(result.IsError);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenMoveCommandIsExecutedAfterGameIsFinished()
    {
        // Arrange
        _gameProcessorMock.Setup(g => g.State).Returns(GameState.Win);
        var moveX = new MoveParameters(0, 0, PlayerTurn.X);
        var command = new MoveCommand(_gameProcessorMock.Object, moveX);

        // Act
        var result = _commandInvoker.Execute(command);

        // Assert
        Assert.True(result.IsError);
    }
}