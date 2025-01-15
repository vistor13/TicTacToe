using TicTacToe.Core.Models;

namespace TicTacToe.Api.Game;

public class GameViewModel
{
    public bool IsRunning { get; set; }

    public GameModes GameMode { get; set; }
}