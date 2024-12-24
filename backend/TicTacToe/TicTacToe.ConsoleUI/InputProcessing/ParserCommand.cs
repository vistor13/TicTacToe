using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.ConsoleUI.InputProcessing;

public class ParserCommand : IParseCommand
{
    private readonly Dictionary<Command, ICommand> _commands;
    private readonly IConsoleRenderer _consoleRenderer;
    private readonly GameProcessor _gameProcessor;

    public ParserCommand(GameProcessor gameProcessor, IConsoleRenderer consoleRenderer)
    {
        _gameProcessor = gameProcessor;
        _consoleRenderer = consoleRenderer;
        _commands = InitializeDictionary();
    }

    public ICommand? CommandParse(string? input)
    {
        if (input!.StartsWith("move", StringComparison.OrdinalIgnoreCase))
        {
            var moveCommand = ParseMoveCommand(input);
            if (moveCommand != null)
                return moveCommand;
        }

        if (Enum.TryParse(input, true, out Command command) &&
            _commands.TryGetValue(command, out var executableCommand))
            return executableCommand;

        _consoleRenderer.RenderError("Please, write a valid command");
        return null;
    }

    private ICommand? ParseMoveCommand(string input)
    {
        var moveData = input.Substring(4).Trim();
        if (TryParseMove(moveData, out var moveParameters))
            return new MoveCommand(_gameProcessor, moveParameters);

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

        moveParameters = new MoveParameters(row - 1, col - 1, _gameProcessor.CurrentTurn);
        return true;
    }

    private Dictionary<Command, ICommand> InitializeDictionary()
    {
        return new Dictionary<Command, ICommand>
        {
            { Command.Start, new StartCommand(_gameProcessor, _consoleRenderer) },
            { Command.Help, new InstructionCommand(_consoleRenderer) },
            { Command.Replay, new ReplayCommand(_gameProcessor, _consoleRenderer) },
            { Command.Exit, new ExitCommand() }
        };
    }
}