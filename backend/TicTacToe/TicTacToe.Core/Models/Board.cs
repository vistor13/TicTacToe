using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Validators;

namespace TicTacToe.Core.Models
{
	public class Board
	{
		private const int BoardSize = 3;
		private const char EmptyCell = ' ';
		private readonly List<IValidator> _validators;
		public char[,] Grid { get; private set; }

        public Board()
        {
			_validators = InitializeValidators();
			Grid = InitializeBoard();
		}
        public bool CanMakeMove(MoveParameters moveParameters)
		{
			foreach (var validator in _validators)
			{
				if(!validator.Validate(moveParameters,this))
					return false;
			}
			return true;
		}
		public void MakeMove(MoveParameters moveParameters) 
		{
			(var row,var col,var playerTurn) = moveParameters;
			char currentPlayer = playerTurn is PlayerTurn.X ? 'X' : 'Y';
			Grid[row, col] = currentPlayer;
		}
		public bool IsBoardFull()
		{
			for (int i = 0; i < BoardSize; i++)
			{
				if (Enumerable.Range(0, BoardSize).Select(j => Grid[i, j]).Where(j => j == ' ').Any())
					return false;
			}
			return true;
				
		}
		public List<List<char>> GetAllLines()
		{
			var lines = new List<List<char>>();

			for (int i = 0; i < BoardSize; i++)
			{
				lines.Add(Enumerable.Range(0, BoardSize).Select(j => Grid[i, j]).ToList());
				lines.Add(Enumerable.Range(0, BoardSize).Select(j => Grid[j, i]).ToList()); 
			}

			lines.Add(Enumerable.Range(0, BoardSize).Select(i => Grid[i, i]).ToList());
			lines.Add(Enumerable.Range(0, BoardSize).Select(i => Grid[i, BoardSize - i - 1]).ToList());

			return lines;
		}
		private static char[,] InitializeBoard()
		{
			var board = new char[BoardSize, BoardSize];
			for (int i = 0; i < BoardSize; i++)
			{ 
			   for (int j = 0; j < BoardSize; j++)
			   {
					board[i, j] = EmptyCell;
			   }
			}
			return board;

		}
		private static List<IValidator> InitializeValidators()
		{
			return new List<IValidator>
			{
				new BoundsValidator(),
				new OwnerCellValidator()
		    };
		}
	}
}
