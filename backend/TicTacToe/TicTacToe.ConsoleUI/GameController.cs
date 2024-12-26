using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.ConsoleUI;

public class GameController(
    IParseCommand parserCommand,
    GameProcessor gameProcessor,
    IUiRender consoleRenderer,
    ICommandInvoker commandInvoker)
{
    public void Execute()
    {
        consoleRenderer.RenderWelcome();
        while (true)
        {
            var command = GetCommand();
            if (!commandInvoker.Execute(command))
            {
                consoleRenderer.RenderError("An error occurred during execution");
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

            if (gameProcessor.State is GameState.Draw)
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
            var commandInput = GetValidCommandInput();
            command = parserCommand.CommandParse(commandInput);
        }

        return command;
    }

    private string GetValidCommandInput()
    {
        string? commandInput;
        do
        {
            consoleRenderer.RenderPrompt("Write your command: ");
            commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
                consoleRenderer.RenderError("Please, write a valid command (cannot be empty or just spaces)");
        } while (string.IsNullOrEmpty(commandInput));

        return commandInput;
    }
}