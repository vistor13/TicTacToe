using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.ConsoleUI.ConsoleViews;

public class ConsoleRenderer : IConsoleRenderer
{
    private const string VerticalLine = "│";
    private const string HorizontalLine = "────";
    private const string CornerLine = "┼";

    private const ConsoleColor XColor = ConsoleColor.Magenta;
    private const ConsoleColor OColor = ConsoleColor.DarkCyan;

    public void RenderBoard(Board board)
    {
        for (var i = 0; i < Board.BoardSize; i++)
        {
            RenderRow(board, i);
            if (i < Board.BoardSize - 1) RenderRowDivider();
        }
    }

    public void RenderInstruction()
    {
        Console.WriteLine(ConsoleMessages.Instruction);
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
                Console.ForegroundColor = XColor;
                Console.Write(cell);
                break;
            case 'O':
                Console.ForegroundColor = OColor;
                Console.Write(cell);
                break;
            default:
                Console.Write(cell);
                break;
        }

        Console.ResetColor();
        Console.Write("  ");
    }
}