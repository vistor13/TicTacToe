using System.Linq.Expressions;

namespace TicTacToe.Infrastructure.DataBase.Specifications;

public abstract class Specification<TEntity> where TEntity : class
{
    public abstract Expression<Func<TEntity, bool>> ToExpression();

    public bool IsSatisfiedBy(TEntity obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        var predicate = ToExpression().Compile();
        return predicate(obj);
    }
}