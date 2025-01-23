namespace TicTacToe.Core.Models;

public record GameStateParameters
{
    public GameModes GameMode { get; init; }

    public GameState State { get; init; }

    public char[,] Grid { get; init; }

    public PlayerTurn PlayerTurn { get; init; }
}