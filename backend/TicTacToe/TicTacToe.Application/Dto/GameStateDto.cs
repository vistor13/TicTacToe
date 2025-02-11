using TicTacToe.Infrastructure.Entities;

namespace TicTacToe.Application.Dto;

public sealed record GameStateDto(
    string GameModes,
    string CurrentPlayer,
    string GameState,
    char[,] Grid,
    bool IsRunning,
    bool ShouldAiMove)
{
    public static GameStateDto MapToModel(GameEntity dto)
    {
        return new GameStateDto(
            dto.Mode.ToString(),
            dto.CurrentPlayer.ToString(),
            dto.GameState.ToString(),
            ConvertToArrayChar(dto.Moves),
            dto.GameState is Core.Models.GameState.Ongoing,
            dto.Mode is Core.Models.GameModes.GameWithAi);
    }

    private static char[,] ConvertToArrayChar(List<MoveEntity>? moves)
    {
        var result = new char[3, 3];


        for (var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
            result[i, j] = ' ';

        if (moves is { Count: 0 })
            return result;

        foreach (var move in moves) result[move.Row - 1, move.Col - 1] = move.MoveSymbol;

        return result;
    }
}