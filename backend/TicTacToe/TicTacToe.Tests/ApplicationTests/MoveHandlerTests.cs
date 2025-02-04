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
        _gameStateManagerMock.Setup(gsm => gsm.GetGame(gameId)).Returns((GameStateModel)null!);

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
        var command = new MoveCommand(gameId, 1, 1);
        var move = new MoveParametersDto(1, 1, "X");

        var gameStateDto = new GameStateDto(
            GameModes.GameWithPlayer.ToString(),
            PlayerTurn.X.ToString(),
            GameState.Ongoing.ToString(),
            new char[3, 3],
            true,
            false
        );
        var gameState = GameStateModel.MapToModel(gameStateDto);
        _gameStateManagerMock.Setup(gsm => gsm.GetGame(gameId)).Returns(gameState);

        _gameProcessorMock.Setup(gp => gp.LoadGameState(It.IsAny<GameStateModel>()));
        _gameProcessorMock.Setup(gp => gp.MakeMove(It.IsAny<MoveParametersDto>())).Returns(gameStateDto);
        _gameProcessorMock.Setup(gp => gp.GetGameState()).Returns(gameStateDto);
        _gameProcessorMock.Setup(gp => gp.AiMakeMove(out It.Ref<MoveParametersDto>.IsAny)).Returns(gameStateDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        _gameStateManagerMock.Verify(gsm => gsm.SaveGame(gameId, It.IsAny<GameStateModel>()), Times.Once);
    }

    [Fact]
    public async Task Handle_MoveFails_ReturnsError()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var command = new MoveCommand(gameId, 5, 1);
        var gameStateDto = new GameStateDto(
            GameModes.GameWithPlayer.ToString(),
            PlayerTurn.X.ToString(),
            GameState.Ongoing.ToString(),
            new char[3, 3],
            true,
            false
        );
        var gameState = GameStateModel.MapToModel(gameStateDto);

        _gameStateManagerMock.Setup(gsm => gsm.GetGame(gameId)).Returns(gameState);


        _gameProcessorMock.Setup(gp => gp.MakeMove(It.IsAny<MoveParametersDto>()))
            .Returns(Error.Validation("OutOfBounds", Messages.Error.OutOfBoundsErrorMessage));
        _gameProcessorMock.Setup(gp => gp.GetGameState()).Returns(gameStateDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, 
            e => e is { Code: "OutOfBounds", Description: Messages.Error.OutOfBoundsErrorMessage });
    }

}
