using Moq;
using TicTacToe.Application.Commands;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Services;
using TicTacToe.ConsoleUI.Commands;
using TicTacToe.ConsoleUI.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Tests.ApplicationTests;

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

    private void SetupGameState(GameState state, GameModes gameMode = GameModes.NotDefined, string currentTurn = "X")
    {
        var grid = new char[3, 3];
        var isRunning = state == GameState.Ongoing;
        var shouldAiMove = gameMode == GameModes.GameWithAi;
        var gameStateDto = new GameStateDto(
            gameMode.ToString(),
            currentTurn,
            state.ToString(),
            grid,
            isRunning,
            shouldAiMove
        );

        _gameProcessorMock.Setup(g => g.GetGameState()).Returns(gameStateDto);
        _gameProcessorMock.Setup(g => g.GameMode).Returns(gameMode);
    }
    [Fact]
    public void Execute_ShouldReturnSuccess_WhenPlayerGameCommandExecutedAndModeNotDefined()
    {
        // Arrange
        SetupGameState(GameState.NotStarted);
        var command = new PlayerGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.NotStarted.ToString(), [typeof(PlayerGameCommand)] }
        };

        // Act
        _commandInvoker.Execute(command, commandsByState);

        // Assert
        _gameProcessorMock.Verify(g => g.InitializeGame(true), Times.Once);
        _consoleRendererMock.Verify(r => r.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenAiGameCommandExecutedAndModeNotDefined()
    {
        // Arrange
        SetupGameState(GameState.NotStarted);
        var command = new AiGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.NotStarted.ToString(), [typeof(AiGameCommand)] }
        };
        
        // Act
        var result = _commandInvoker.Execute(command, commandsByState);

        // Assert
        Assert.Null(result);
        _gameProcessorMock.Verify(g => g.InitializeGame(false), Times.Once);
        _consoleRendererMock.Verify(r => r.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenPlayerGameCommandNotExecutedAndModeGameAi()
    {
        // Arrange
        SetupGameState(GameState.Ongoing, GameModes.GameWithAi);
        var command = new PlayerGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.NotStarted.ToString(), [typeof(PlayerGameCommand)] }
        };
        
        // Act
        var result = _commandInvoker.Execute(command, commandsByState);
        
        // Assert
        Assert.True(result!.Value.IsError);
        Assert.Contains(result.Value.Errors, e => e.Code == "ExecuteCommand");
    }
    

    [Fact]
    public void Execute_ShouldReturnError_WhenReplayCommandIsExecutedAndGameNotStarted()
    {
        // Arrange
        SetupGameState(GameState.NotStarted);

        var command = new ReplayCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.Ongoing.ToString(), [typeof(ReplayCommand)] }
        };

        // Act
        var result = _commandInvoker.Execute(command, commandsByState);

        // Assert
        Assert.True(result!.Value.IsError);
        Assert.Contains(result.Value.Errors, e => e.Code == "ExecuteCommand");
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenReplayCommandIsExecutedAndGameIsOngoing()
    {
        // Arrange
        SetupGameState(GameState.Ongoing);

        var command = new ReplayCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.Ongoing.ToString(), [typeof(ReplayCommand)] }
        };

        // Act
        _commandInvoker.Execute(command, commandsByState);

        // Assert
        _gameProcessorMock.Verify(c => c.InitializeGame(It.IsAny<bool>()), Times.Once);
        _consoleRendererMock.Verify(c => c.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenInstructionCommandIsExecuted()
    {
        // Arrange
        SetupGameState(GameState.NotStarted);

        var command = new InstructionCommand(_consoleRendererMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.NotStarted.ToString(), [typeof(InstructionCommand)] }
        };

        // Act
        _commandInvoker.Execute(command, commandsByState);

        // Assert
        _consoleRendererMock.Verify(c => c.RenderInstruction(), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenExitCommandIsExecuted()
    {
        // Arrange
        SetupGameState(GameState.Ongoing, GameModes.GameWithAi);

        var command = new ExitCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.Ongoing.ToString(), [typeof(ExitCommand)] }
        };

        // Act
        var result = _commandInvoker.Execute(command, commandsByState);

        // Assert
        Assert.Null(result);
        _consoleRendererMock.Verify(c => c.RenderMessage(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenEndCommandIsExecuted()
    {
        // Arrange
        SetupGameState(GameState.NotStarted);

        var command = new EndGameCommand(_gameProcessorMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.NotStarted.ToString(), [typeof(EndGameCommand)] }
        };

        // Act
        var result = _commandInvoker.Execute(command, commandsByState);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void Execute_ShouldReturnError_WhenCommandIsNotAllowedInOngoingState()
    {
        // Arrange
        SetupGameState(GameState.Ongoing, GameModes.GameWithAi);

        var command = new PlayerGameCommand(_gameProcessorMock.Object, _consoleRendererMock.Object);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.NotStarted.ToString(), [typeof(PlayerGameCommand)] }
        };

        // Act
        var result = _commandInvoker.Execute(command, commandsByState);

        // Assert
        Assert.True(result!.Value.IsError);
        Assert.Contains(result.Value.Errors, e => e.Code == "ExecuteCommand");
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenMoveCommandIsExecutedAfterGameIsFinished()
    {
        // Arrange
        SetupGameState(GameState.Win);

        var moveX = new MoveParametersDto(0, 0, PlayerTurn.X.ToString());
        var command = new MakeMoveCommand(_gameProcessorMock.Object, moveX);
        var commandsByState = new Dictionary<string, List<Type>>
        {
            { GameState.Ongoing.ToString(), [typeof(MakeMoveCommand)] }
        };

        // Act
        var result = _commandInvoker.Execute(command, commandsByState);

        // Assert
        Assert.True(result!.Value.IsError);
        Assert.Contains(result.Value.Errors, e => e.Code == "ExecuteCommand");
    }
}
