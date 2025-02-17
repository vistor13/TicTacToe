using System.Net;
using System.Net.Http.Json;
using TicTacToe.Api;
using TicTacToe.Api.Contracts.Requests;
using TicTacToe.Api.Contracts.Responses;

namespace TicTacToe.IntegrationTests;

public class GameEndpointTests(CustomWebFactory<Program> factory) : TestsDataBaseIntegrationTests(factory)
{
    [Fact]
    public async Task StartGame_ShouldReturnCreated_WithValidTwoPlayerGameResponse()
    {
        // Act
        var response = await Client.PostAsync("/api/game/start?isTwoPlayerMode=true", null);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var gameResponse = await response.Content.ReadFromJsonAsync<GameResponse>();
        Assert.NotNull(gameResponse);
        Assert.False(string.IsNullOrEmpty(gameResponse.Id.ToString()));
        Assert.Equal("GameWithPlayer", gameResponse.GameMode);
    }

    [Fact]
    public async Task StartGame_ShouldReturnCreated_WithValidSinglePlayerGameResponse()
    {
        // Act
        var response = await Client.PostAsync("/api/game/start?isTwoPlayerMode=false", null);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var gameResponse = await response.Content.ReadFromJsonAsync<GameResponse>();
        Assert.NotNull(gameResponse);
        Assert.False(string.IsNullOrEmpty(gameResponse.Id.ToString()));
        Assert.Equal("GameWithAi", gameResponse.GameMode);
        Assert.Equal(1, gameResponse.Id);
    }

    [Fact]
    public async Task MakeMove_ShouldReturnSuccess_WhenMoveIsValid()
    {
        // Arrange
        var startResponse = await Client.PostAsync("/api/game/start?isTwoPlayerMode=true", null);
        var game = await startResponse.Content.ReadFromJsonAsync<GameResponse>();
        var moveRequest = new MoveRequest(game!.Id, 2, 3);
        // Act
        var response = await Client.PostAsJsonAsync("/api/game/move", moveRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task MakeMove_ShouldReturnBadRequest_WhenMoveOutOfBounds()
    {
        // Arrange
        var startResponse = await Client.PostAsync("/api/game/start?isTwoPlayerMode=true", null);
        var game = await startResponse.Content.ReadFromJsonAsync<GameResponse>();
        var moveRequest = new MoveRequest(game!.Id, 0, 0);

        // Act
        var response = await Client.PostAsJsonAsync("/api/game/move", moveRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task MakeMove_ShouldReturnNotFound_WhenGameIdNotCorrect()
    {
        // Arrange
        var moveRequest = new MoveRequest(1, 1, 1);

        // Act
        var response = await Client.PostAsJsonAsync("/api/game/move", moveRequest);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MakeMove_ShouldReturnBadRequest_WhenMoveAndCellOccupied()
    {
        // Arrange
        var startResponse = await Client.PostAsync("/api/game/start?isTwoPlayerMode=true", null);
        var game = await startResponse.Content.ReadFromJsonAsync<GameResponse>();
        var moveRequest1 = new MoveRequest(game.Id, 1, 1);
        var moveRequest2 = new MoveRequest(game.Id, 1, 1);
        await Client.PostAsJsonAsync("/api/game/move", moveRequest1);

        // Act
        var response = await Client.PostAsJsonAsync("/api/game/move", moveRequest2);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetGameState_ShouldReturnGameState_WhenGameExists()
    {
        // Arrange
        var startResponse = await Client.PostAsync("/api/game/start?isTwoPlayerMode=true", null);
        var game = await startResponse.Content.ReadFromJsonAsync<GameResponse>();

        // Act
        var response = await Client.GetAsync($"/api/game/state?gameId={game!.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var gameState = await response.Content.ReadFromJsonAsync<GameStateResponse>();
        Assert.NotNull(gameState);
    }

    [Fact]
    public async Task GetGameState_ShouldReturnNotFound_WhenGameIdNotCorrect()
    {
        // Arrange
        var id = 1;
        // Act
        var response = await Client.GetAsync($"/api/game/state?gameId={id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}