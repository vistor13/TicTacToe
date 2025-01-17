using ErrorOr;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class EndGameCommand(IGameProcessor gameProcessor) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.Reset();
        return Result.Success;
    }
}