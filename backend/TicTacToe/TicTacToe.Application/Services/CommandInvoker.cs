using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Services;

public class CommandInvoker(IGameProcessor gameProcessor) : ICommandInvoker
{
    public ErrorOr<Success> Execute(ICommand command, Dictionary<string, List<Type>> commandsByState)
    {
        var commandType = command.GetType();
        var currentState = gameProcessor.GetBoard().State;


        if (commandsByState.ContainsKey(currentState.ToString()) &&
            commandsByState[currentState.ToString()].Contains(commandType))
            return command.Execute();

        return Error.Validation(
            "ExecuteCommand",
            Messages.Error.CommandNotAllowed
        );
    }
}