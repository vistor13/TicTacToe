using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services
{
    public class GameProcessor
    {
        public Board GameBoard { get; } = new();
        public GameState State { get; private set; } = GameState.Ongoing;
        public PlayerTurn CurrentTurn { get; private set; } = PlayerTurn.X;

        public bool MakeMove(MoveParameters moveParameters)
        {
            if (State != GameState.Ongoing || CurrentTurn != moveParameters.PlayerTurn)
                return false;

            if (!GameBoard.CanMakeMove(moveParameters))
                return false;

            GameBoard.MakeMove(moveParameters);

            State = GameBoard.GetGameStatus();

            if (State != GameState.Ongoing)
                return false;

            SwitchTurn();

            return true;
        }

        private void SwitchTurn()
        {
            CurrentTurn = CurrentTurn is PlayerTurn.X ? PlayerTurn.О : PlayerTurn.X;
        }

        public Board GetBoard()
        {
            return GameBoard;
        }
    }
}