using TicTacToe.Core.Models;

namespace TicTacToe.Application.Dto;

public sealed record GameStateModel
{
    public GameModes Modes { get; private init; }

    public GameState State { get; private init; }

    public PlayerTurn CurrentPlayer { get; private init; }

    public char[,] Grid { get; private init; }

    public static GameStateModel MapToModel(GameStateDto dto)
    {
        return new GameStateModel
        {
            Modes = Enum.Parse<GameModes>(dto.GameModes),
            State = Enum.Parse<GameState>(dto.GameState),
            CurrentPlayer = Enum.Parse<PlayerTurn>(dto.CurrentPlayer),
            Grid = dto.Grid
        };
    }
}