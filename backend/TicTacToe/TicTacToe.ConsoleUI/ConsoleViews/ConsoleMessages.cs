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

    How to Play:
    - To choose a cell, enter its coordinates in the format 'row column' (e.g., '1 2').
    - Rows and columns are numbered from 1 to 3.
        - For example:
            - Entering '1 1' corresponds to the top-left corner.
            - Entering '3 3' corresponds to the bottom-right corner.

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
    - If all cells are filled without a winner, the game is declared a draw.";
}