using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Infrastructure.Entities;

public class MoveEntity : BaseEntity
{
    [Required] public long GameId { get; set; }

    public GameEntity? GameEntity { get; set; }

    [Required] [Range(1, 3)] public int Row { get; set; }

    [Required] [Range(1, 3)] public int Col { get; set; }

    [Required] public required char MoveSymbol { get; set; }
}