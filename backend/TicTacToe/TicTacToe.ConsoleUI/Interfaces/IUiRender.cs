using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI.Interfaces;

public interface IUiRender
{
    void RenderBoard(Board board);
    void RenderInstruction();
    void RenderError(string invalidCommandTypeHelpForInstructions);
    void RenderPrompt(string text);
    void RenderMessage(string text);
    void RenderWelcome();
    void RenderWin(PlayerTurn playerTurn);
    void RenderProposeRestoreGame();
    void RenderDraw();
}