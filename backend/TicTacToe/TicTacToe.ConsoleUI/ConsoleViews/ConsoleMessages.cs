namespace TicTacToe.ConsoleUI.ConsoleViews;

public static class ConsoleMessages
{
    public static class Error
    {
        public const string InvalidInput =
            """
            Please, write a valid command (cannot be empty or just spaces)
            """;

        public const string InvalidCommand =
            """
            Please, write a valid command
            """;
    }

    public static class GameMessages
    {
        public const string Instruction =
            """
            Welcome to the game of Tic-Tac-Toe!

            Game Instructions:
            1. The game is played by two players: Player 1 plays as 'X', and Player 2 plays as 'O'.
            2. The game board consists of 9 cells arranged in a 3x3 grid.
            3. Players take turns entering the coordinates of a cell to make their move.
            4. The goal is to place three of your symbols ('X' or 'O') in a row, column, or diagonal.
            5. The game ends when one player wins or all cells are filled.

            Game Modes:
            - **Player vs Player**: Two players compete against each other by taking turns.
            - **Player vs AI**: A single player competes against the computer with AI making moves for Player 2 ('O').

            Commands:
            - 'Move row column': Makes a move at the specified cell (e.g., 'Move 2 3').
            - 'Game player': Start Game **Player vs Player**.
            - 'Game Ai': Start Game **Player vs AI**.
            - 'Help': Shows these instructions.
            - 'Exit': Ends the game.
            - 'Replay': Replay the game.

            Example Gameplay:
            Initial game board:
            . . .
            . . .
            . . .

            After a few moves:
            X . .
            . O .
            . . X

            Important Notes:
            - If a player chooses a cell that is already occupied, they will be asked to try again.
            - If all cells are filled without a winner, the game is declared a draw.
            - Before starting the game, players can choose a mode: **Player vs Player** or **Player vs AI**.
            """;

        public const string WelcomeMessage =
            """
            Welcome to Tic-Tac-Toe!
            Prepare for a battle of wits. Will you claim victory or face defeat?
            Type 'Help' to see the game instructions or choose a game mode to begin:
            Good luck
            """;

        public const string WinnerMessage =
            """
            ================================
                       GAME OVER
            ================================
                   Player {0} wins!
            ================================
            """;

        public const string DrawMessage =
            """
            ================================
                       GAME OVER
            ================================
                     It's a draw!
            ================================
            """;

        public const string EndGamePrompt =
            """
            Would you like to play again?
            Command 'replay' to start a new game.
            Command 'exit' to quit the game.*
            """;

        public const string CommandPrompt =
            """
            Write your command:
            """;
    }
}