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
        var isEnded = true;
        while (isEnded)
        {
            var command = GetCommand();
            var executionResult = commandInvoker.Execute(command);
            if (executionResult.IsError)
            {
                consoleRenderer.RenderError(executionResult.Errors.First().Description);
                continue;
            }

            if (gameProcessor.GameMode != GameModes.NotDefined)
                PlayGameLoop();
            if (!gameProcessor.IsRunning) isEnded = false;
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

            if (HandleGameEndState()) break;

            if (gameProcessor.GameMode != GameModes.GameWithAi) continue;
            gameProcessor.AiMakeMove(out var aiMove);
            consoleRenderer.RenderMessage(
                string.Format(Messages.GameProcess.AiMove, aiMove.Row + 1, aiMove.Col + 1));
            var showAiCommand = new ShowBoardCommand(gameProcessor, consoleRenderer);
            showAiCommand.Execute();
            if (HandleGameEndState()) break;
        }
    }

    private bool HandleGameEndState()
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