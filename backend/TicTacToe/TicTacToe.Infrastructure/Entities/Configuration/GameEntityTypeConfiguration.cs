using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Infrastructure.Entities.Configuration;

public class GameEntityTypeConfiguration : IEntityTypeConfiguration<GameEntity>
{
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder
            .Property(b => b.GameState)
            .IsRequired()
            .HasConversion<string>();
        builder
            .Property(b => b.CurrentPlayer)
            .IsRequired()
            .HasConversion<string>();
        builder
            .Property(b => b.Mode)
            .IsRequired()
            .HasConversion<string>();
    }
}