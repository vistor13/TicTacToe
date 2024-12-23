namespace TicTacToe.ConsoleUI.ConsoleViews;

public abstract class ConsoleMessages
{
    public const string Instruction = @"
Welcome to the game of Tic-Tac-Toe!

Game Instructions:
1. The game is played by two players: Player 1 plays as 'X', and Player 2 plays as 'O'.
2. The game board consists of 9 cells arranged in a 3x3 grid.
3. Players take turns entering the coordinates of a cell to make their move.
4. The goal is to place three of your symbols ('X' or 'O') in a row, column, or diagonal.
5. The game ends when one player wins or all cells are filled.

Commands:
- 'Move row column': Makes a move at the specified cell (e.g., 'Move 2 3').
- 'Start': Displays the current game board.
- 'Help': Shows these instructions.
- 'Exit': Ends the game.

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
";

    public const string WelcomeMessage = @"
Welcome to Tic-Tac-Toe!
Prepare for a battle of wits. Will you claim victory or face defeat?
Type 'Help' to see the game instructions or 'Start' to begin the game. Good luck!
";

    public const string WinnerMessage = @"
    ================================
             GAME OVER
    ================================
          Player {0} wins!
    ================================
    ";

    public const string DrawMessage = @"
    ================================
             GAME OVER
    ================================
             It's a draw!
    ================================
";

    public const string EndGamePrompt = @"
    Would you like to play again?
    Type 'replay' to start a new game.
    Type 'exit' to quit the game.
";
}