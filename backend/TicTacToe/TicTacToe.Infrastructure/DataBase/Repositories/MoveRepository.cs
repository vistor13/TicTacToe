using TicTacToe.Infrastructure.Entities;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Infrastructure.DataBase.Repositories;

public class MoveRepository(ApplicationDbContext context) : RepositoriesBase<MoveEntity>(context), IMoveRepository;