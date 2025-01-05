using ErrorOr;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class EndGameCommand(IGameProcessor gameProcessor) : ICommand
{
    public ErrorOr<Success> Execute()
    {
        gameProcessor.IsRunning = false;
        return Result.Success;
    }
}