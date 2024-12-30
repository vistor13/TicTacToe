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
    IMiniMaxAi aiBot)
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

            if (gameProcessor.GameModes == GameModes.GameWithAi)
                PlayWithAi();
            else if (gameProcessor.GameModes == GameModes.GameWithPlayer) PlayTwoPlayers();
        }
    }

    private void PlayWithAi()
    {
        PlayGameLoop(
            () =>
            {
                var aiMove = aiBot.FindBestMove();
                var aiMoveCommand = new MoveCommand(gameProcessor, aiMove);
                consoleRenderer.RenderMessage(
                    string.Format(Messages.GameProcess.AiMove, aiMove.Row + 1, aiMove.Col + 1));
                commandInvoker.Execute(aiMoveCommand);
                var showAiCommand = new ShowBoardCommand(gameProcessor, consoleRenderer);
                showAiCommand.Execute();
            }
        );
    }

    private void PlayTwoPlayers()
    {
        PlayGameLoop(() => { });
    }

    private void PlayGameLoop(Action aiMoveAction)
    {
        while (gameProcessor.GameModes != GameModes.NotDefined)
        {
            var command = GetCommand();
            var executionResult = commandInvoker.Execute(command);

            if (executionResult.IsError)
            {
                consoleRenderer.RenderError(executionResult.Errors.First().Description);
                continue;
            }

            if (command is MoveCommand)
            {
                var showCommand = new ShowBoardCommand(gameProcessor, consoleRenderer);
                showCommand.Execute();

                if (HandleGameEndState()) break;

                aiMoveAction.Invoke();

                if (HandleGameEndState()) break;
            }
        }
    }

    private bool HandleGameEndState()
    {
        if (gameProcessor.State == GameState.Win)
        {
            consoleRenderer.RenderWin(gameProcessor.CurrentTurn);
            consoleRenderer.RenderProposeRestoreGame();
            return true;
        }

        if (gameProcessor.State == GameState.Draw)
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