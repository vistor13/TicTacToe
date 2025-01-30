using ErrorOr;
using Moq;
using TicTacToe.Application.Commands.MoveCommand;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Models;

namespace TicTacToe.Tests.ApplicationTests;

public class MoveHandlerTests
{
    private readonly Mock<IGameProcessor> _gameProcessorMock;
    private readonly Mock<IGameStateManager> _gameStateManagerMock;
    private readonly MoveHandler _handler;

    public MoveHandlerTests()
    {
        _gameProcessorMock = new Mock<IGameProcessor>();
        _gameStateManagerMock = new Mock<IGameStateManager>();
        _handler = new MoveHandler(_gameProcessorMock.Object, _gameStateManagerMock.Object);
    }

    [Fact]
    public async Task Handle_GameNotFound_ReturnsError()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var command = new MoveCommand(gameId, 1, 1);
        _gameStateManagerMock.Setup(gsm => gsm.GetGame(gameId)).Returns((GameStateDto)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var error = Assert.IsType<ErrorOr<Success>>(result);
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e is { Code: "NotFoundGame", Description: "Game not found." });
    }

    [Fact]
    public async Task Handle_GameLoadedSuccessfully_ReturnsSuccess()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var board = new Board();
        var command = new MoveCommand(gameId, 1, 1);
        var move = new MoveParameters(1, 1, board.CurrentTurn);

        var gameState = new GameStateDto(GameModes.GameWithAi, PlayerTurn.X, GameState.Ongoing, new char[3, 3]);
        _gameStateManagerMock.Setup(gsm => gsm.GetGame(gameId)).Returns(gameState);

        _gameProcessorMock.Setup(gp => gp.LoadGameState(It.IsAny<GameStateDto>()));
        _gameProcessorMock.Setup(gp => gp.MakeMove(move)).Returns(Result.Success);
        _gameProcessorMock.Setup(gp => gp.GetBoard()).Returns(board);
        _gameProcessorMock.Setup(gp => gp.GetGameState()).Returns(gameState);
        _gameProcessorMock.Setup(gp => gp.AiMakeMove(out It.Ref<MoveParameters>.IsAny)).Returns(Result.Success);


        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        _gameStateManagerMock.Verify(gsm => gsm.SaveGame(gameId, It.IsAny<GameStateDto>()), Times.Once);
    }

    [Fact]
    public async Task Handle_MoveFails_ReturnsError()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var board = new Board();
        var command = new MoveCommand(gameId, 5, 1);
        var gameState = new GameStateDto(GameModes.GameWithAi, PlayerTurn.X, GameState.Ongoing, new char[3, 3]);

        _gameStateManagerMock.Setup(gsm => gsm.GetGame(gameId)).Returns(gameState);
        _gameProcessorMock.Setup(gp => gp.GetBoard()).Returns(board);
        _gameProcessorMock.Setup(gp => gp.MakeMove(It.IsAny<MoveParameters>())).Returns(Error.Validation("OutOfBounds",
            Messages.Error.OutOfBoundsErrorMessage));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors,
            e => e is { Code: "OutOfBounds", Description: Messages.Error.OutOfBoundsErrorMessage });
    }
}