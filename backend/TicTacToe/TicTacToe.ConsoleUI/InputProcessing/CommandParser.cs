using Microsoft.Extensions.DependencyInjection;
using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI.InputProcessing;

public class CommandParser(
    IGameProcessor gameProcessor,
    IUiRender consoleRenderer,
    IServiceProvider serviceProvider)
    : ICommandParser
{
    public ICommand? CommandParse(string? input)
    {
        if (input!.StartsWith("move", StringComparison.OrdinalIgnoreCase))
        {
            var moveCommand = ParseMoveCommand(input);
            if (moveCommand != null)
                return moveCommand;
        }

        if (input.StartsWith("game", StringComparison.OrdinalIgnoreCase))
            input = string.Concat(input.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        if (Enum.TryParse(input, true, out Command command))
            return command switch
            {
                Command.GamePlayer => serviceProvider.GetRequiredService<PlayerGameCommand>(),
                Command.GameAi => serviceProvider.GetRequiredService<AiGameCommand>(),
                Command.Help => serviceProvider.GetRequiredService<InstructionCommand>(),
                Command.Replay => serviceProvider.GetRequiredService<ReplayCommand>(),
                Command.Exit => serviceProvider.GetRequiredService<ExitCommand>(),
                Command.End => serviceProvider.GetRequiredService<EndGameCommand>(),
                _ => null
            };
        consoleRenderer.RenderError(ConsoleMessages.Error.InvalidCommand);
        return null;
    }

    private ICommand? ParseMoveCommand(string input)
    {
        var moveData = input[4..].Trim();
        return TryParseMove(moveData, out var moveParameters)
            ? new MoveCommand(gameProcessor, moveParameters)
            : null;
    }

    private bool TryParseMove(string input, out MoveParameters moveParameters)
    {
        moveParameters = null!;

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 ||
            !int.TryParse(parts[0], out var row) ||
            !int.TryParse(parts[1], out var col))
        {
            return false;
        }

        moveParameters = new MoveParameters(row - 1, col - 1, gameProcessor.GetBoard().CurrentTurn);
        return true;
    }
}