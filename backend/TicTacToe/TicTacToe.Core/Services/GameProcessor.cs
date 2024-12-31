using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services
{
    public class GameProcessor : IGameProcessor
    {
        public Board GameBoard { get; private set; } = null!;
        public GameState State { get; private set; } = GameState.NotStarted;
        public PlayerTurn CurrentTurn { get; private set; }

        public ErrorOr<Success> MakeMove(MoveParameters moveParameters)
        {
            if (State != GameState.Ongoing)
                return Error.Validation(
                    "InvalidGameState",
                    Messages.Error.InvalidGameState
                );

            if (CurrentTurn != moveParameters.PlayerTurn)
                return Error.Validation(
                    "InvalidCurrentPlayer",
                    Messages.Error.InvalidCurrentPlayer
                );

            var canMakeMoveResult = GameBoard.CanMakeMove(moveParameters);
            if (canMakeMoveResult.IsError)
                return canMakeMoveResult.Errors;

            GameBoard.MakeMove(moveParameters);
            State = GameBoard.GetGameStatus();

            if (State == GameState.Ongoing) SwitchTurn();

            return Result.Success;
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