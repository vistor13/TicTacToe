namespace TicTacToe.ConsoleUI.Interfaces;

public interface IUiRender
{
    void RenderBoard(char[,] grid);
    void RenderProposeRestoreGame();
    void RenderDraw();
    void RenderWin(string currentPlayer);
    void RenderWelcome();
    void RenderInstruction();
    void RenderPrompt(string text);
    void RenderMessage(string text);
    void RenderError(string text);
}