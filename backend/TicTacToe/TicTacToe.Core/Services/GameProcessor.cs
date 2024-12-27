using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services
{
    public class GameProcessor : IGameProcessor
    {
        public Board GameBoard { get; private set; } = null!;
        public GameState State { get; private set; } = GameState.NotStarted;
        public PlayerTurn CurrentTurn { get; private set; }


        public OperationResult MakeMove(MoveParameters moveParameters)
        {
            if (State != GameState.Ongoing || CurrentTurn != moveParameters.PlayerTurn)
                return OperationResult.Failure("Invalid game state or player turn.");

            var canMakeMoveResult = GameBoard.CanMakeMove(moveParameters);
            if (!canMakeMoveResult.IsSuccess)
                return canMakeMoveResult;

            GameBoard.MakeMove(moveParameters);
            State = GameBoard.GetGameStatus();

            if (State == GameState.Ongoing)
                SwitchTurn();

            return OperationResult.Success();
        }

        public void InitializeGame()
        {
            GameBoard = new Board();
            State = GameState.Ongoing;
            CurrentTurn = PlayerTurn.X;
        }

        public Board GetBoard()
        {
            return GameBoard;
        }

        private void SwitchTurn()
        {
            CurrentTurn = CurrentTurn is PlayerTurn.X ? PlayerTurn.О : PlayerTurn.X;
        }
    }
}