using TicTacToe.Application.Dto;

namespace TicTacToe.Api.Contracts.Responses;

/// <summary>
///     Represents the state view model.
/// </summary>
public record GameStateResponse
{
    /// <summary>
    ///     The current game mode.
    /// </summary>
    public required string GameMode { get; init; }

    /// <summary>
    ///     The current state of the game (draw, ongoing, win).
    /// </summary>
    public required string State { get; init; }

    /// <summary>
    ///     The game grid, represented as a list of lists of characters.
    ///     It contains the positions of the game pieces.
    /// </summary>
    public required List<List<char>> Grid { get; init; }

    /// <summary>
    ///     The current player's turn .
    /// </summary>
    public required string PlayerTurn { get; init; }

    #region Mapping

    /// <summary>
    ///     Converts a GameStateDto object to a StateViewModel.
    /// </summary>
    /// <param name="gameState">The GameStateDto to convert.</param>
    /// <returns> <see cref="GameStateResponse" /> containing the relevant data from the GameStateDto.</returns>
    public static GameStateResponse ToViewModel(GameStateDto gameState)
    {
        var gridList = Enumerable.Range(0, gameState.Grid.GetLength(0))
            .Select(i => Enumerable.Range(0, gameState.Grid.GetLength(1))
                .Select(j => gameState.Grid[i, j])
                .ToList())
            .ToList();

        return new GameStateResponse
        {
            State = gameState.GameState,
            Grid = gridList,
            GameMode = gameState.GameModes,
            PlayerTurn = gameState.CurrentPlayer
        };
    }

    #endregion
}