using ErrorOr;
using TicTacToe.Core.BoardValidator;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Models
{
    public class Board
    {
        public const int BoardSize = 3;

        public const char EmptyCell = ' ';

        private readonly List<IValidator> _validators = InitializeValidators();

        public GameState State { get; private set; } = GameState.NotStarted;

        public PlayerTurn CurrentTurn { get; private set; }
        public char[,] Grid { get; } = InitializeBoard();

        public char GetCell(int row, int col)
        {
            return Grid[row, col];
        }

        public void SetGameState(GameState gameState)
        {
            State = gameState;
        }

        public void InitializeGameState()
        {
            State = GameState.Ongoing;
        }

        public ErrorOr<Success> MakeMove(MoveParameters moveParameters)
        {
            var canMakeMoveResult = CanMakeMove(moveParameters);
            if (canMakeMoveResult.IsError)
                return canMakeMoveResult.Errors;

            var (row, col, playerTurn) = moveParameters;
            var currentPlayer = playerTurn is PlayerTurn.X ? 'X' : 'O';
            Grid[row, col] = currentPlayer;
            State = GetGameStatus();

            if (State == GameState.Ongoing) SwitchTurn();

            return Result.Success;
        }

        public List<(int, int)> GetAvailableCells()
        {
            return Enumerable.Range(0, BoardSize)
                .SelectMany(i => Enumerable.Range(0, BoardSize)
                    .Where(j => Grid[i, j] == EmptyCell)
                    .Select(j => (i, j)))
                .ToList();
        }

        private ErrorOr<Success> CanMakeMove(MoveParameters moveParameters)
        {
            foreach (var validationResult in _validators.Select(validator => validator.Validate(moveParameters, this))
                         .Where(validationResult => validationResult.IsError))
                return validationResult.Errors;

            return Result.Success;
        }

        private bool IsBoardFull()
        {
            for (var i = 0; i < BoardSize; i++)
                if (Enumerable.Range(0, BoardSize).Select(j => Grid[i, j]).Any(j => j == EmptyCell))
                    return false;

            return true;
        }

        private List<List<char>> GetAllLines()
        {
            var lines = new List<List<char>>();

            for (var i = 0; i < BoardSize; i++)
            {
                lines.Add(Enumerable.Range(0, BoardSize).Select(j => Grid[i, j]).ToList());
                lines.Add(Enumerable.Range(0, BoardSize).Select(j => Grid[j, i]).ToList());
            }

            lines.Add(Enumerable.Range(0, BoardSize).Select(i => Grid[i, i]).ToList());
            lines.Add(Enumerable.Range(0, BoardSize).Select(i => Grid[i, BoardSize - i - 1]).ToList());

            return lines;
        }

        public GameState GetGameStatus()
        {
            return CheckWin() ? GameState.Win : CheckDraw() ? GameState.Draw : GameState.Ongoing;
        }

        private bool CheckWin()
        {
            return CheckLines(uniqueCells => uniqueCells.Count == 1 && !uniqueCells.Contains(EmptyCell));
        }

        private bool CheckDraw()
        {
            if (IsBoardFull())
                return true;
            var getOpponentSymbol = CurrentTurn == PlayerTurn.X ? 'O' : 'X';
            if (GetAvailableCells().Count is 1)
                return !CheckLines(uniqueCells =>
                    uniqueCells.Count == 2 &&
                    uniqueCells.Contains(getOpponentSymbol) && uniqueCells.Contains(EmptyCell));

            return !CheckLines(uniqueCells => uniqueCells.Count == 2 && uniqueCells.Contains(EmptyCell));
        }

        private bool CheckLines(Predicate<HashSet<char>> condition)
        {
            var lines = GetAllLines();
            return lines.Select(line => new HashSet<char>(line)).Any(uniqueCells => condition(uniqueCells));
        }

        private static char[,] InitializeBoard()
        {
            var board = new char[BoardSize, BoardSize];
            for (var i = 0; i < BoardSize; i++)
            for (var j = 0; j < BoardSize; j++)
                board[i, j] = EmptyCell;

            return board;
        }

        private static List<IValidator> InitializeValidators()
        {
            return
            [
                new BoundsValidator(),
                new OwnerCellValidator()
            ];
        }

        private void SwitchTurn()
        {
            CurrentTurn = CurrentTurn is PlayerTurn.X ? PlayerTurn.О : PlayerTurn.X;
        }
    }
}