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

        public const string CommandNotAllowed =
            """
            This command not allowed in this game state
            """;

        public const string InvalidGameState =
            """
            Invalid game state.
            """;

        public const string InvalidCurrentPlayer =
            """
            Invalid player turn.
            """;
    }

    public static class GameProcess
    {
        public const string RestartNotification =
            """
            Game restarted!!!! The player {0} makes a move. 
            """;

        public const string StartNotification =
            """
            The game is started, the player {0} makes a move.
            """;
    }
}