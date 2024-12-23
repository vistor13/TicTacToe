using TicTacToe.Core.Models;

namespace TicTacToe.Core.Interfaces;

public interface IConsoleRenderer
{
    void RenderBoard(Board board);
    void RenderInstruction();
    void RenderError(string invalidCommandTypeHelpForInstructions);
    void RenderPrompt(string text);
    void RenderMessage(string text);
    void RenderWelcome();
}