using TicTacToe.Infrastructure.DataBase.Specifications;
using TicTacToe.Infrastructure.Entities;

namespace TicTacToe.Infrastructure.Interfaces;

public interface IRepositoriesBase<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetById(long id);
    Task Delete(long id);
    Task<TEntity> Create(TEntity entity);
    Task<TEntity> GetFirstBySpecification(Specification<TEntity> specification);
    Task Update(long id, TEntity entity);
}