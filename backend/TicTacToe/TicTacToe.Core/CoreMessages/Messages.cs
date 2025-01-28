namespace TicTacToe.Core.CoreMessages;

public static class Messages
{
    public static class Error
    {
        public const string OutOfBoundsErrorMessage =
            """
            Coordinates are out of bounds! Please enter numbers between 1 and 3 for both row and column.
            """;

        public const string CellOccupied =
            """
            The selected cell is already occupied. Please choose another cell.
            """;
    }
}