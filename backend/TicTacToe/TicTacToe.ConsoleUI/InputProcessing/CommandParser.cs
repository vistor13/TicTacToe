using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI.InputProcessing;

public class CommandParser(IGameProcessor gameProcessor, IUiRender consoleRenderer, IServiceProvider serviceProvider)
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

        if (!Enum.TryParse(input, true, out Command command))
        {
            consoleRenderer.RenderError("Please, write a valid command");
            return null;
        }

        return command switch
        {
            Command.Start => serviceProvider.GetRequiredService<StartCommand>(),
            Command.Help => serviceProvider.GetRequiredService<InstructionCommand>(),
            Command.Replay => serviceProvider.GetRequiredService<ReplayCommand>(),
            Command.Exit => serviceProvider.GetRequiredService<ExitCommand>(),
            _ => null
        };
    }

    private ICommand? ParseMoveCommand(string input)
    {
        var moveData = input.Substring(4).Trim();
        if (TryParseMove(moveData, out var moveParameters))
            return new MoveCommand(gameProcessor, moveParameters);

        return null;
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

        moveParameters = new MoveParameters(row - 1, col - 1, gameProcessor.CurrentTurn);
        return true;
    }
}