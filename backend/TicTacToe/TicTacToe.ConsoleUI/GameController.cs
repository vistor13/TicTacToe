using TicTacToe.ConsoleUI.InputProcessing;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.ConsoleUI;

public class GameController(
    ParserCommand parserCommand,
    GameProcessor gameProcessor,
    IConsoleRenderer consoleRenderer,
    ICommandInvoker commandInvoker)
{
    private readonly ICommandInvoker _commandInvoker = commandInvoker;
    private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
    private readonly GameProcessor _gameProcessor = gameProcessor;
    private readonly IParseCommand _parserCommand = parserCommand;

    public void Execute()
    {
        _consoleRenderer.RenderWelcome();
        while (true)
        {
            var command = GetCommand();
            if (!_commandInvoker.Execute(command))
            {
                _consoleRenderer.RenderError("An error occurred during execution");
                continue;
            }

            if (command is MoveCommand)
            {
                var showCommand = new ShowBoardCommand(_gameProcessor, _consoleRenderer);
                showCommand.Execute();
            }

            if (_gameProcessor.State is GameState.Win)
            {
                _consoleRenderer.RenderWin(_gameProcessor.CurrentTurn);
                _consoleRenderer.RenderProposeRestoreGame();
            }

            if (_gameProcessor.State is GameState.Draw)
            {
                _consoleRenderer.RenderDraw();
                _consoleRenderer.RenderProposeRestoreGame();
            }
        }
    }

    private ICommand GetCommand()
    {
        ICommand? command = null;
        while (command is null)
        {
            var commandInput = GetValidCommandInput();
            command = _parserCommand.CommandParse(commandInput);
        }

        return command;
    }

    private string GetValidCommandInput()
    {
        string? commandInput;
        do
        {
            _consoleRenderer.RenderPrompt("Write your command: ");
            commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput)) _consoleRenderer.RenderError("Please, write a valid command");
        } while (string.IsNullOrEmpty(commandInput));

        return commandInput;
    }
}