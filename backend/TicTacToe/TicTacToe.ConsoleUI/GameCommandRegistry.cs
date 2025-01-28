using TicTacToe.Application.Commands;
using TicTacToe.ConsoleUI.Commands;

namespace TicTacToe.ConsoleUI;

public static class GameCommandRegistry
{
    public static readonly Dictionary<string, List<Type>> CommandsByState = InitializeCommandsByState();

    private static Dictionary<string, List<Type>> InitializeCommandsByState()
    {
        return new Dictionary<string, List<Type>>
        {
            {
                "NotStarted", InitializeModesCommands()
                    .Concat(InitializeCommonCommands())
                    .ToList()
            },
            {
                "Ongoing", InitializeGameCycleCommands()
                    .Concat(InitializeRestrictedCommands())
                    .Concat(InitializeCommonCommands())
                    .ToList()
            },
            {
                "Draw", InitializeRestrictedCommands()
                    .Concat(InitializeCommonCommands())
                    .ToList()
            },
            {
                "Win", InitializeRestrictedCommands()
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
            typeof(MakeMoveCommand),
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