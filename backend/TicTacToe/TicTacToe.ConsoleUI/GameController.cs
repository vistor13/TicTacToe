using TicTacToe.Core.Commands;
using TicTacToe.Core.CoreMessages;
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
        var isEnded = false;
        while (!isEnded)
        {
            var command = GetCommand();
            var executionResult = commandInvoker.Execute(command);
            if (executionResult.IsError)
            {
                consoleRenderer.RenderError(executionResult.Errors.First().Description);
            }

            if (gameProcessor.GameMode != GameModes.NotDefined)
                PlayGameLoop();

            if (!gameProcessor.IsRunning)
                isEnded = true;
        }
    }

    private void PlayGameLoop()
    {
        while (gameProcessor.IsRunning)
        {
            var command = GetCommand();
            var executionResult = commandInvoker.Execute(command);

            if (executionResult.IsError)
            {
                consoleRenderer.RenderError(executionResult.Errors.First().Description);
                continue;
            }

            if (command is not MoveCommand) continue;
            var showCommand = new ShowBoardCommand(gameProcessor, consoleRenderer);
            showCommand.Execute();

            if (IsGameInTerminateState()) break;

            DoAiMove();

            PrintBoard();

            if (IsGameInTerminateState()) break;
        }
    }

    private bool HandleGameEndState()
    private bool IsGameInTerminateState()
    {
        if (gameProcessor.GetBoard().State == GameState.Win)
        {
            consoleRenderer.RenderWin(gameProcessor.GetBoard().CurrentTurn);
            consoleRenderer.RenderProposeRestoreGame();
            return true;
        }

        if (gameProcessor.GetBoard().State == GameState.Draw)
        {
            consoleRenderer.RenderDraw();
            consoleRenderer.RenderProposeRestoreGame();
            return true;
        }

        return false;
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