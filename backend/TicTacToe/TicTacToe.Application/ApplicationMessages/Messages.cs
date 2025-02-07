namespace TicTacToe.Application.ApplicationMessages;

public static class Messages
{
    public static class Error
    {
        public const string CommandNotAllowed =
            "This command not allowed in this game state";

        public const string InvalidGameState =
            "Invalid game state.";

        public const string InvalidCurrentPlayer =
            "Invalid player turn.";
    }
}