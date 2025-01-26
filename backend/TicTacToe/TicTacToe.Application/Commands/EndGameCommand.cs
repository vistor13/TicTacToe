using ErrorOr;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Commands;

public class EndGameCommand(IGameProcessor gameProcessor) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.Reset();
        return Result.Success;
    }
}