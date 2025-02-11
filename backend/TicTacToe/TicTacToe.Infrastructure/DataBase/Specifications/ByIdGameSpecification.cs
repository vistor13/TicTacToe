using System.Linq.Expressions;
using TicTacToe.Infrastructure.Entities;

namespace TicTacToe.Infrastructure.DataBase.Specifications;

public class ByIdGameSpecification : Specification<GameEntity>
{
    private readonly long _id;

    public ByIdGameSpecification(long id)
    {
        _id = id;
        AddInclude(game => game.Moves);
    }

    public override Expression<Func<GameEntity, bool>> ToExpression()
    {
        return game => game.Id == _id;
    }
}