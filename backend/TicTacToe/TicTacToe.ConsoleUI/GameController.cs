using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI;

public class GameController(
    ICommandParser parserCommandParser,
    IGameProcessor gameProcessor,
    IUiRender consoleRenderer,
    ICommandInvoker commandInvoker,
    IInputReader reader)
{
    public void Execute()
    {
        consoleRenderer.RenderWelcome();
        while (true)
        {
            var command = GetCommand();
            if (!commandInvoker.Execute(command))
            {
                consoleRenderer.RenderError(ConsoleMessages.Error.ExecutionError);
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
            var commandInput = reader.GetValidCommandInput();
            command = parserCommandParser.CommandParse(commandInput);
        }

        return command;
    }
}