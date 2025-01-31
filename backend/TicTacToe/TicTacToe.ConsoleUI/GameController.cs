using TicTacToe.Application.Commands;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Commands;
using TicTacToe.ConsoleUI.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI;

public class GameController(
    ICommandParser parserCommandParser,
    IGameProcessor gameProcessor,
    IUiRender consoleRenderer,
    ICommandInvoker commandInvoker,
    IInputProvider reader)
{
    private ICommand _currentCommand = null!;

    public void Execute()
    {
        consoleRenderer.RenderWelcome();

        do
        {
            _currentCommand = GetCommand();
            var executionResult = commandInvoker.Execute(_currentCommand, GameCommandRegistry.CommandsByState);
            if (executionResult!.Value.IsError)
            {
                consoleRenderer.RenderError(executionResult.Value.Errors.First().Description);
            }

            if (gameProcessor.IsRunning)
                PlayGameLoop();
        } while (_currentCommand is not EndGameCommand);
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

    private void PlayGameLoop()
    {
        while (gameProcessor.IsRunning)
        {
            _currentCommand = GetCommand();
            var executionResult = commandInvoker.Execute(_currentCommand, GameCommandRegistry.CommandsByState);

            if (executionResult!.Value.IsError)
            {
                consoleRenderer.RenderError(executionResult.Value.Errors.First().Description);
                continue;
            }

            if (_currentCommand is not MakeMoveCommand) continue;

            PrintBoard();

            if (IsGameInTerminateState()) break;

            DoAiMove();

            PrintBoard();

            if (IsGameInTerminateState()) break;
        }
    }

    private void PrintBoard()
    {
        var showCommand = new ShowBoardCommand(gameProcessor, consoleRenderer);
        showCommand.Execute();
    }

    private void DoAiMove()
    {
        if (gameProcessor.GameMode != GameModes.GameWithAi) return;
        gameProcessor.AiMakeMove(out var aiMove);
        consoleRenderer.RenderMessage(string.Format(Constants.Messages.GameProcess.AiMove, aiMove.Row + 1,
            aiMove.Col + 1));
    }

    private bool IsGameInTerminateState()
    {
        var gameState = gameProcessor.GetGameResult();
        if (!gameState.IsGameOver)
            return false;

        if (gameState.Winner is not null)
            consoleRenderer.RenderWin(gameState.Winner);
        else
            consoleRenderer.RenderDraw();

        consoleRenderer.RenderProposeRestoreGame();
        return true;
    }
}