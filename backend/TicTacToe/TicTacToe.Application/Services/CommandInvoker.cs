using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Commands;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Services;

public class CommandInvoker(IGameProcessor gameProcessor) : ICommandInvoker
{
    private readonly Dictionary<GameState, List<Type>> _commandsByState = InitializeCommandsByState();

    public ErrorOr<Success> Execute(ICommand command)
    {
        var commandType = command.GetType();
        var currentState = gameProcessor.GetBoard().State;

        if (_commandsByState.ContainsKey(currentState) &&
            _commandsByState[currentState].Contains(commandType))
            return command.Execute();

        return Error.Validation(
            "ExecuteCommand",
            Messages.Error.CommandNotAllowed
        );
    }

    private static Dictionary<GameState, List<Type>> InitializeCommandsByState()
    {
        return new Dictionary<GameState, List<Type>>
        {
            {
                GameState.NotStarted, InitializeModesCommands()
                    .Concat(InitializeCommonCommands())
                    .ToList()
            },
            {
                GameState.Ongoing, InitializeGameCycleCommands()
                    .Concat(InitializeRestrictedCommands())
                    .Concat(InitializeCommonCommands())
                    .ToList()
            },
            {
                GameState.Draw, InitializeRestrictedCommands()
                    .Concat(InitializeCommonCommands())
                    .ToList()
            },
            {
                GameState.Win, InitializeRestrictedCommands()
                    .Concat(InitializeCommonCommands())
                    .ToList()
            }
        };
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

    private static List<Type> InitializeGameCycleCommands()
    {
        return
        [
            typeof(MoveCommand),
            typeof(ShowBoardCommand)
        ];
    }

    private static List<Type> InitializeRestrictedCommands()
    {
        return
        [
            typeof(ReplayCommand),
            typeof(ExitCommand)
        ];
    }
}