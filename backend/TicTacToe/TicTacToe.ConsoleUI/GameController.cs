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
    IInputProvider reader,
    IGameStateService gameStateService)
{
    public void Execute()
    {
        consoleRenderer.RenderWelcome();
        while (true)
        {
            var command = GetCommand();
            var executionResult = commandInvoker.Execute(command);
            if (executionResult.IsError)
            {
                consoleRenderer.RenderError(executionResult.Errors.First().Description);
                continue;
            }

            if (gameStateService.GameMode != GameModes.GameWithAi)
                PlayGameLoop();
        }
    }

    private void PlayGameLoop()
    {
        while (gameStateService.GameMode != GameModes.NotDefined)
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

            if (gameStateService.GameMode != GameModes.GameWithAi) continue;
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
        if (gameStateService.State == GameState.Win)
        {
            consoleRenderer.RenderWin(gameStateService.CurrentTurn);
            consoleRenderer.RenderProposeRestoreGame();
            return true;
        }

        if (gameStateService.State == GameState.Draw)
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