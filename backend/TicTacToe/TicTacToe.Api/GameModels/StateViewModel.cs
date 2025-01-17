using TicTacToe.Core.Dto;
using TicTacToe.Core.Models;

namespace TicTacToe.Api.GameModels;

public record StateViewModel
{
    public GameModes GameMode { get; init; }
    public GameState State { get; init; }
    public List<List<char>>? Grid { get; init; }
    public PlayerTurn PlayerTurn { get; init; }

    #region Mapping

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