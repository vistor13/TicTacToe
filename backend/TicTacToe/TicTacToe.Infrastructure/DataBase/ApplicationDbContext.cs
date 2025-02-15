using Microsoft.EntityFrameworkCore;
using TicTacToe.Infrastructure.Entities;

namespace TicTacToe.Infrastructure.DataBase;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<GameEntity> Games { get; set; } = null!;
}