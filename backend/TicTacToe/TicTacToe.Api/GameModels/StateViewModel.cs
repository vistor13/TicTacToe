using TicTacToe.Core.Models;

namespace TicTacToe.Api.GameModels;

public class StateViewModel
{
    public GameModes GameMode { get; set; }
    public GameState State { get; set; }
    public List<List<char>>? Grid { get; set; }
    public PlayerTurn PlayerTurn { get; set; }
}