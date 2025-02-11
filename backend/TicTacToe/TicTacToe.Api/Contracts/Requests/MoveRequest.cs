namespace TicTacToe.Api.Contracts.Requests;

/// <summary>
///     Represents a request to make a move in a game.
/// </summary>
/// <param name="GameId"></param>
/// <param name="Row"></param>
/// <param name="Col"></param>
public sealed record MoveRequest(long GameId, int Row, int Col);