using TicTacToe.Infrastructure.Entities;

namespace TicTacToe.Infrastructure.Interfaces;

public interface IRepositoriesBase<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetById(long id);
    Task Delete(long id);
    Task<TEntity> Create(TEntity entity);
    Task Update(long id, TEntity entity);
}