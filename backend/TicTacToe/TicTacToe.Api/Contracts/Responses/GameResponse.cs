namespace TicTacToe.Api.Contracts.Responses;

/// <summary>
///     Represents a view model for the game.
/// </summary>
public sealed record GameResponse(long Id, string GameMode);