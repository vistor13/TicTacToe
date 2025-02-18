using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using TicTacToe.Api.Contracts.Requests;
using TicTacToe.Api.Contracts.Responses;

namespace TicTacToe.IntegrationTests.Endpoints;

internal static class GameEndpoints
{
    public static async Task<GameResponse?> POST_Start(this HttpClient client, bool isTwoPlayerMode)
    {
        const string endpoint = "/api/game/start?isTwoPlayerMode={0}";
        
        var requestUri = string.Format(endpoint, isTwoPlayerMode);
        var response = await client.PostAsync(requestUri, null);

        return await response.Content.ReadFromJsonAsync<GameResponse>();
    }

    public static async Task<IResult?> POST_Move(this HttpClient client, MoveRequest moveRequest)
    {
        const string endpoint = "/api/game/move";

        var jsonContent = JsonContent.Create(moveRequest);
        var response = await client.PostAsync(endpoint, jsonContent);

        return await response.Content.ReadFromJsonAsync<IResult>();
    }

    public static async Task<GameStateResponse?> GET_State(this HttpClient client, long gameId)
    {
        const string endpoint = "/api/game/state?gameId={0}";

        var requestUri = string.Format(endpoint, gameId);
        var response = await client.PostAsync(requestUri, null);

        return await response.Content.ReadFromJsonAsync<GameStateResponse>();
    }
}
