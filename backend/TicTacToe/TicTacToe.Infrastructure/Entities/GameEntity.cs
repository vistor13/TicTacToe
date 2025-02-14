using Microsoft.EntityFrameworkCore;
using TicTacToe.Core.Models;
using TicTacToe.Infrastructure.Entities.Configuration;

namespace TicTacToe.Infrastructure.Entities;

[EntityTypeConfiguration(typeof(GameEntityTypeConfiguration))]
public class GameEntity : BaseEntity
{
    public List<MoveEntity> Moves { get; set; } = [];
    public GameState GameState { get; set; }

    public GameModes Mode { get; set; }

    public PlayerTurn CurrentPlayer { get; set; }

    public bool IsRunning { get; set; }

    public bool ShouldAiMove { get; set; }
}