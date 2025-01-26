using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.ConsoleViews;

public class ConsoleRenderer : IUiRender
{
    private const string VerticalLine = "│";
    private const string HorizontalLine = "─────";
    private const string CornerLine = "┼";

    private const ConsoleColor XColor = ConsoleColor.Magenta;
    private const ConsoleColor OColor = ConsoleColor.DarkCyan;
    private const ConsoleColor HelpColor = ConsoleColor.Cyan;
    private const ConsoleColor ErrorColor = ConsoleColor.Red;
    private const ConsoleColor InputTextColor = ConsoleColor.Blue;
    private const ConsoleColor MessagesColor = ConsoleColor.Green;

    public void RenderBoard(char[,] grid)
    {
        var boardSize = grid.Length;
        for (var i = 0; i < boardSize; i++)
        {
            RenderRow(grid, i, boardSize);
            if (i < boardSize - 1) RenderRowDivider(boardSize);
        }
    }

    public void RenderProposeRestoreGame()
    {
        RenderMessage(ConsoleMessages.GameMessages.EndGamePrompt);
    }

    public void RenderDraw()
    {
        Console.Clear();
        RenderMessage(ConsoleMessages.GameMessages.DrawMessage);
    }


    public void RenderWin(string currentPlayer)
    {
        Console.Clear();
        RenderMessage(string.Format(ConsoleMessages.GameMessages.WinnerMessage, currentPlayer));
    }

    public void RenderWelcome()
    {
        PrintColoredText(ConsoleMessages.GameMessages.WelcomeMessage, MessagesColor, true);
    }

    public void RenderInstruction()
    {
        PrintColoredText(ConsoleMessages.GameMessages.Instruction, HelpColor, true);
    }

    public void RenderPrompt(string text)
    {
        PrintColoredText(text, InputTextColor);
    }

    public void RenderMessage(string text)
    {
        PrintColoredText(text, MessagesColor, true);
    }

    public void RenderError(string text)
    {
        PrintColoredText($"Error : {text}", ErrorColor, true);
    }

    private void RenderRow(char[,] grid, int rowIndex, int boardSize)
    {
        for (var j = 0; j < boardSize; j++)
        {
            RenderCell(grid[rowIndex, j]);
            if (j < boardSize - 1) Console.Write(VerticalLine);
        }

        Console.WriteLine();
    }

    private void RenderRowDivider(int boardSize)
    {
        var divider = string.Join(CornerLine, Enumerable.Repeat(HorizontalLine, boardSize));
        Console.WriteLine(divider);
    }

    private void RenderCell(char cell)
    {
        Console.Write("  ");
        switch (cell)
        {
            case 'X':
                PrintColoredText(cell.ToString(), XColor);
                break;
            case 'O':
                PrintColoredText(cell.ToString(), OColor);
                break;
            default:
                Console.Write(cell);
                break;
        }

        Console.Write("  ");
    }

    private void PrintColoredText(string text, ConsoleColor color, bool isWriteLine = false)
    {
        Console.ForegroundColor = color;
        if (isWriteLine)
            Console.WriteLine(text);
        else
            Console.Write(text);

        Console.ResetColor();
    }
}