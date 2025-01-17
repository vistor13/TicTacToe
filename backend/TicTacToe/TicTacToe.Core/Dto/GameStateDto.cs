using TicTacToe.Core.Models;

namespace TicTacToe.Core.Dto;

public class GameStateDto
{
    public GameModes GameMode { get; set; }

    public GameState State { get; set; }

    public char[,] Grid { get; set; }

    public PlayerTurn PlayerTurn { get; set; }
}