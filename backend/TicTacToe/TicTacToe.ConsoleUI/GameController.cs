using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Commands.ConsoleUI;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;
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
    private ICommand _currentCommand = null!;

    public void Execute()
    {
        consoleRenderer.RenderWelcome();

        do
        {
            _currentCommand = GetCommand();
            var executionResult = commandInvoker.Execute(_currentCommand);
            if (executionResult.IsError)
            {
                consoleRenderer.RenderError(executionResult.Errors.First().Description);
            }

            if (gameProcessor.GameMode != GameModes.NotDefined)
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
            var executionResult = commandInvoker.Execute(_currentCommand);

            if (executionResult.IsError)
            {
                consoleRenderer.RenderError(executionResult.Errors.First().Description);
                continue;
            }

            if (_currentCommand is not MoveCommand) continue;

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
        if (gameProcessor.GameMode == GameModes.GameWithAi)
        {
            gameProcessor.AiMakeMove(out var aiMove);
            consoleRenderer.RenderMessage(string.Format(Messages.GameProcess.AiMove, aiMove.Row + 1, aiMove.Col + 1));
        }
    }

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
}