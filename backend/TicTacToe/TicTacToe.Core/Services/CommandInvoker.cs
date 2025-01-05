using ErrorOr;
using TicTacToe.Core.Commands;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services;

public class CommandInvoker(IGameProcessor gameProcessor) : ICommandInvoker
{
    private readonly List<Type> _commonCommands = InitializeCommonCommands();
    private readonly List<Type> _modesCommands = InitializeModesCommands();

    public ErrorOr<Success> Execute(ICommand command)
    {
        var commandType = command.GetType();

        if (gameProcessor.GameMode == GameModes.NotDefined && _modesCommands.Contains(commandType))
            return command.Execute();

        if (gameProcessor.GameMode != GameModes.NotDefined && commandType == typeof(ExitCommand))
            return command.Execute();

        if (gameProcessor.GetBoard().State != GameState.NotStarted && commandType == typeof(ReplayCommand))
            return command.Execute();

        if ((gameProcessor.GetBoard().State == GameState.Ongoing && commandType == typeof(MoveCommand)) ||
            _commonCommands.Contains(commandType))
            return command.Execute();

        return Error.Validation(
            "ExecuteCommand",
            Messages.Error.CommandNotAllowed
        );
    }

    private static List<Type> InitializeModesCommands()
    {
        return
        [
            typeof(AiGameCommand),
            typeof(PlayerGameCommand)
        ];
    }

    private static List<Type> InitializeCommonCommands()
    {
        return
        [
            typeof(InstructionCommand),
            typeof(EndGameCommand)
        ];
    }
}