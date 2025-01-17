using TicTacToe.Core.Models;

namespace TicTacToe.Api.Game;

public class GameViewModel
{
    public Guid Id { get; set; }
    public GameModes GameMode { get; set; }
}