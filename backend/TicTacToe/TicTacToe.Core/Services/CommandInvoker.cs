using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services;

public class CommandInvoker(IGameProcessor gameProcessor) : ICommandInvoker
{
    private readonly List<Type> _commonCommands = InitializeCommonCommands();

    public bool Execute(ICommand command)
    {
        var commandType = command.GetType();

        if (gameProcessor.State == GameState.NotStarted && commandType == typeof(StartCommand))
            return command.Execute();

        if (gameProcessor.State != GameState.NotStarted && commandType == typeof(ReplayCommand))
            return command.Execute();

        if ((gameProcessor.State == GameState.Ongoing && commandType == typeof(MoveCommand)) ||
            _commonCommands.Contains(commandType))
            return command.Execute();

        return false;
    }

    private static List<Type> InitializeCommonCommands()
    {
        return
        [
            typeof(InstructionCommand),
            typeof(ExitCommand)
        ];
    }
}