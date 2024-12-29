using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI;

public class GameController(
    ICommandParser parserCommandParser,
    IGameProcessor gameProcessor,
    IUiRender consoleRenderer,
    ICommandInvoker commandInvoker,
    IInputProvider reader)
{
    public void Execute()
    {
        consoleRenderer.RenderWelcome();
        while (true)
        {
            var command = GetCommand();
            var executionResult = commandInvoker.Execute(command);
            if (!executionResult.IsSuccess)
            {
                consoleRenderer.RenderError(executionResult.ErrorMessage);
                continue;
            }

            if (command is MoveCommand)
            {
                var showCommand = new ShowBoardCommand(gameProcessor, consoleRenderer);
                showCommand.Execute();
            }

            if (gameProcessor.State is GameState.Win)
            {
                consoleRenderer.RenderWin(gameProcessor.CurrentTurn);
                consoleRenderer.RenderProposeRestoreGame();
            }
            else if (gameProcessor.State is GameState.Draw)
            {
                consoleRenderer.RenderDraw();
                consoleRenderer.RenderProposeRestoreGame();
            }
        }
    }

    private ICommand GetCommand()
    {
        ICommand? command = null;
        while (command is null)
        {
            var commandInput = reader.GetCommandInput();
            command = parserCommandParser.CommandParse(commandInput);
        }

        return command;
    }
}