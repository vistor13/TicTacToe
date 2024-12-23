using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI.ConsoleViews;

public class ConsoleRenderer : IConsoleRenderer
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

    public void RenderBoard(Board board)
    {
        for (var i = 0; i < Board.BoardSize; i++)
        {
            RenderRow(board, i);
            if (i < Board.BoardSize - 1) RenderRowDivider();
        }
    }

    public void RenderWelcome()
    {
        PrintColoredText(ConsoleMessages.WelcomeMessage, MessagesColor, true);
    }

    public void RenderInstruction()
    {
        PrintColoredText(ConsoleMessages.Instruction, HelpColor, true);
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

    private void RenderRow(Board board, int rowIndex)
    {
        for (var j = 0; j < Board.BoardSize; j++)
        {
            RenderCell(board.GetCell(rowIndex, j));
            if (j < Board.BoardSize - 1) Console.Write(VerticalLine);
        }

        Console.WriteLine();
    }

    private void RenderRowDivider()
    {
        var divider = string.Join(CornerLine, Enumerable.Repeat(HorizontalLine, Board.BoardSize));
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