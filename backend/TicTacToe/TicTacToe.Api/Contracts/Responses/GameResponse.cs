namespace TicTacToe.Api.Contracts.Responses;

/// <summary>
///     Represents a view model for the game.
/// </summary>
public record GameResponse
{
    /// <summary>
    ///     A unique identifier for the game instance.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     The game mode.
    /// </summary>
    public required string GameMode { get; init; }
}