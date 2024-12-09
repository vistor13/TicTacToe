namespace TicTacToe.Core.Models
{
	public class Board
	{
		private const int BOARD_SIZE = 3;
		public char[,] Grid {  get; set; }
        public Board()
        {
            InitializeBoard();
        }
        private void InitializeBoard()
		{
			Grid = new char[BOARD_SIZE, BOARD_SIZE];
			for (int i = 0; i < BOARD_SIZE; i++)
			{ 
			   for (int j = 0; j < BOARD_SIZE; j++)
				{
					Grid[i, j] = ' ';
				}
			}

		}
	}
}
