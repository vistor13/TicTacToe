using TicTacToe.Core.Models;

namespace TicTacToe.Api.GameModels;

/// <summary>
///     Represents a view model for the game.
/// </summary>
public record GameViewModel
{
    /// <summary>
    ///     A unique identifier for the game instance.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     The game mode.
    /// </summary>
    public GameModes GameMode { get; init; }
}