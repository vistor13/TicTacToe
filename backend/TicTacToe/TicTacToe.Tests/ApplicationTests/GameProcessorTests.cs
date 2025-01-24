using Moq;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Services;
using TicTacToe.Core.Models;

namespace TicTacToe.Tests.ApplicationTests;

public class GameProcessorTests
{
    private readonly Mock<IMiniMaxAi> _aiBotMock;
    private readonly GameProcessor _gameProcessor;

    public GameProcessorTests()
    {
        _aiBotMock = new Mock<IMiniMaxAi>();
        _gameProcessor = new GameProcessor(_aiBotMock.Object);
    }

    [Fact]
    public void InitializeGame_ShouldSetCorrectGameMode()
    {
        // Act
        _gameProcessor.InitializeGame(false);

        // Assert
        Assert.Equal(GameModes.GameWithAi, _gameProcessor.GameMode);
        Assert.Equal(GameState.Ongoing, _gameProcessor.GetBoard().State);
    }

    [Fact]
    public void Reset_ShouldResetGameBoardAndGameMode()
    {
        // Arrange
        _gameProcessor.InitializeGame(false);

        // Act
        _gameProcessor.Reset();

        // Assert
        Assert.Equal(GameModes.NotDefined, _gameProcessor.GameMode);
        Assert.Equal(GameState.NotStarted, _gameProcessor.GetBoard().State);
    }

    [Fact]
    public void MakeMove_ShouldMakeMoveAndUpdateState()
    {
        // Arrange
        _gameProcessor.InitializeGame();

        var move = new MoveParameters(0, 0, PlayerTurn.X);

        // Act
        var result = _gameProcessor.MakeMove(move);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal('X', _gameProcessor.GetBoard().GetCell(0, 0));
        Assert.Equal(PlayerTurn.О, _gameProcessor.GetBoard().CurrentTurn);
    }

    [Fact]
    public void MakeMove_ShouldReturnError_WhenGameStateIsNotOngoing()
    {
        // Arrange
        _gameProcessor.InitializeGame();
        _gameProcessor.GetBoard().SetGameState(GameState.Draw);

        var move = new MoveParameters(0, 0, PlayerTurn.X);

        // Act
        var result = _gameProcessor.MakeMove(move);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e.Code == "InvalidGameState");
    }

    [Fact]
    public void MakeMove_ShouldReturnError_WhenPlayerTurnIsInvalid()
    {
        // Arrange
        _gameProcessor.InitializeGame();
        var move = new MoveParameters(0, 0, PlayerTurn.О);

        // Act
        var result = _gameProcessor.MakeMove(move);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e.Code == "InvalidCurrentPlayer");
    }

    [Fact]
    public void AiMakeMove_ShouldCallAiAndMakeMove()
    {
        // Arrange
        var move = new MoveParameters(0, 0, PlayerTurn.X);

        var expectedMove = new MoveParameters(1, 1, PlayerTurn.О);

        _aiBotMock.Setup(ai => ai.FindBestMove(It.IsAny<Board>())).Returns(expectedMove);

        _gameProcessor.InitializeGame(false);
        _gameProcessor.MakeMove(move);

        // Act
        var result = _gameProcessor.AiMakeMove(out var actualMove);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(expectedMove.Row, actualMove.Row);
        Assert.Equal(expectedMove.Col, actualMove.Col);
        Assert.Equal('O', _gameProcessor.GetBoard().GetCell(1, 1));

        _aiBotMock.Verify(ai => ai.FindBestMove(It.IsAny<Board>()), Times.Once);
    }
}