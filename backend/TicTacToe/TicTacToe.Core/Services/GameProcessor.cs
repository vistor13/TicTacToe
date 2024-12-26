using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services
{
    public class GameProcessor : IGameProcessor
    {
        public Board GameBoard { get; private set; } = null!;
        public GameState State { get; private set; }
        public PlayerTurn CurrentTurn { get; private set; }

        public bool MakeMove(MoveParameters moveParameters)
        {
            if (State != GameState.Ongoing || CurrentTurn != moveParameters.PlayerTurn)
                return false;

            if (!GameBoard.CanMakeMove(moveParameters))
                return false;

            GameBoard.MakeMove(moveParameters);

            State = GameBoard.GetGameStatus();

            if (State == GameState.Ongoing)
                SwitchTurn();

            return true;
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