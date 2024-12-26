using TicTacToe.Core.Interfaces;

namespace TicTacToe.ConsoleUI.InputProcessing;

public class InputReader(IUiRender render) : IInputReader
{
    public string GetValidCommandInput()
    {
        string? commandInput;
        do
        {
            render.RenderPrompt("Write your command: ");
            commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
                render.RenderError("Please, write a valid command (cannot be empty or just spaces)");
        } while (string.IsNullOrEmpty(commandInput));

        return commandInput;
    }
}