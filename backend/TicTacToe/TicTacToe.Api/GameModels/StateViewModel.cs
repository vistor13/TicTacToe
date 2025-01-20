using TicTacToe.Core.Dto;
using TicTacToe.Core.Models;

namespace TicTacToe.Api.GameModels;

/// <summary>
///     Represents the state view model.
/// </summary>
public record StateViewModel
{
    /// <summary>
    ///     The current game mode.
    /// </summary>
    public GameModes GameMode { get; init; }

    /// <summary>
    ///     The current state of the game (draw, ongoing, win).
    /// </summary>
    public GameState State { get; init; }

    /// <summary>
    ///     The game grid, represented as a list of lists of characters.
    ///     It contains the positions of the game pieces.
    /// </summary>
    public List<List<char>>? Grid { get; init; }

    /// <summary>
    ///     The current player's turn .
    /// </summary>
    public PlayerTurn PlayerTurn { get; init; }

    #region Mapping

    /// <summary>
    ///     Converts a GameStateDto object to a StateViewModel.
    /// </summary>
    /// <param name="gameState">The GameStateDto to convert.</param>
    /// <returns> <see cref="StateViewModel" /> containing the relevant data from the GameStateDto.</returns>
    public static StateViewModel ToViewModel(GameStateDto gameState)
    {
        var gridList = Enumerable.Range(0, Board.BoardSize)
            .Select(i => Enumerable.Range(0, Board.BoardSize)
                .Select(j => gameState.Grid[i, j])
                .ToList())
            .ToList();

        return new StateViewModel
        {
            State = gameState.State,
            GameMode = gameState.GameMode,
            Grid = gridList,
            PlayerTurn = gameState.PlayerTurn
        };
    }

    #endregion
}