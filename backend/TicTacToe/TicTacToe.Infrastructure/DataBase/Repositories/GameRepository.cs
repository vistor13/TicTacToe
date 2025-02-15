using TicTacToe.Infrastructure.Entities;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Infrastructure.DataBase.Repositories;

public class GameRepository(ApplicationDbContext context) : RepositoriesBase<GameEntity>(context), IGameRepository;