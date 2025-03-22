namespace TicTacToe.Api.Contracts.Responses;

/// <summary>
///     Represents the state view model.
/// </summary>
public sealed record GameStateResponse(string GameMode, string State, List<List<char>> Grid, string PlayerTurn)
{
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

        return new GameStateResponse(gameState.GameModes, gameState.GameState, gridList, gameState.CurrentPlayer);
    }

    #endregion
}