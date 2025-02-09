using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Infrastructure.DataBase;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options);