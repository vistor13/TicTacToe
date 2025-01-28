using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.InputProcessing;

public class InputProvider(IUiRender render) : IInputProvider
{
    public string GetCommandInput()
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