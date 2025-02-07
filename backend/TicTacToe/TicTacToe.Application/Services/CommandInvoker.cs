using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Services;

public class CommandInvoker(IGameProcessor gameProcessor) : ICommandInvoker
{
    public ErrorOr<GameStateDto>? Execute(ICommand command, Dictionary<string, List<Type>> commandsByState)
    {
        var commandType = command.GetType();
        var gameStateModel = GameStateModel.MapToModel(gameProcessor.GetGameState());

        if (commandsByState.ContainsKey(gameStateModel.State.ToString()) &&
            commandsByState[gameStateModel.State.ToString()].Contains(commandType))
            return command.Execute();

        return Error.Validation(
            "ExecuteCommand",
            Messages.Error.CommandNotAllowed
        );
    }
}