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

        public const string UserCreationFailed =
            "Failed to create a new user due to an unexpected error.";

        public const string NoValidRolesFound =
            "No valid roles found.";
    }
}