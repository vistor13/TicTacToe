using TicTacToe.Core.Models;
using TicTacToe.Infrastructure.Entities;

namespace TicTacToe.Application.Dto;

public sealed record GameStateModel(
    GameModes Modes,
    GameState State,
    PlayerTurn CurrentPlayer,
    char[,] Grid,
    bool IsRunning,
    bool ShouldAiMove)
{
    public static GameStateModel MapToModel(GameEntity dto)
    {
        return new GameStateModel
        (
            dto.Mode,
            dto.GameState,
            dto.CurrentPlayer,
            ConvertToArrayChar(dto.Moves),
            dto.GameState is GameState.Ongoing,
            dto.Mode is GameModes.GameWithAi
        );
    }

    private static char[,] ConvertToArrayChar(List<MoveEntity>? moves)
    {
        var result = new char[3, 3];


        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++) result[i, j] = ' ';

        if (moves is null)
            return result;

        foreach (var move in moves) result[move.Row - 1, move.Col - 1] = move.MoveSymbol;

        return result;
    }

    public static GameEntity MapToEntity(GameStateModel stateModel)
    {
        var moves = new List<MoveEntity>();

        for (var i = 0; i < stateModel.Grid.GetLength(0); i++)
        for (var j = 0; j < stateModel.Grid.GetLength(1); j++)
        {
            var symbol = stateModel.Grid[i, j];
            if (symbol == 'X' || symbol == 'O')
                moves.Add(new MoveEntity
                {
                    Row = i + 1,
                    Col = j + 1,
                    MoveSymbol = symbol
                });
        }

        return new GameEntity
        {
            GameState = stateModel.State,
            Mode = stateModel.Modes,
            CurrentPlayer = stateModel.CurrentPlayer,
            Moves = moves
        };
    }
}