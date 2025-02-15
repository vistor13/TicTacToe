namespace TicTacToe.Infrastructure.Entities;

public class MoveEntity
{
    public int Row { get; set; }

    public int Col { get; set; }

    public required char MoveSymbol { get; set; }
}