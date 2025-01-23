namespace TicTacToe.Application.ApplicationMessages;

public static class Messages
{
    public static class Error
    {
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

        public const string WelcomeMessageGameWithPlayer =
            """
            Welcome to Tic-Tac-Toe: Player vs Player Mode!
            Get ready to challenge your opponent in a classic battle of wits and strategy.
            Take turns placing your marks ('X' or 'O') on the board and aim to get three in a row.

            Type 'Help' to see the game instructions.Good luck to both players!
            """;


        public const string WelcomeMessageGameWithAi =
            """
                                Player vs AI Mode!
            Get ready to test your skills against the AI in a strategic battle.
            Take turns placing your marks ('X' or 'O') on the board, and aim to get three in a row.

            The AI will make its moves, but can you outsmart it? Type 'Help' to see the game instructions.
            Good luck, and may the best player win!
            """;

        public const string AiMove =
            """
            The AI has made its : move row {0}, column {1}.
            """;

        public const string GameModeSelection =
            """
            Choose your game mode:
            - 'Player vs Player': Challenge a friend and prove your strategy!
            - 'Player vs AI': Test your skills against the computer's cunning moves.
            """;
    }
}