using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.ConsoleUI.InputProcessing;

public class InputReader(IUiRender render) : IInputReader
{
    public string GetValidCommandInput()
    {
        string? commandInput;
        do
        {
            render.RenderPrompt(ConsoleMessages.GameMessages.CommandPrompt);
            commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
                render.RenderError(ConsoleMessages.Error.InvalidInput);
        } while (string.IsNullOrEmpty(commandInput));

        return commandInput;
    }
}