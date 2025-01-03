using ErrorOr;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Services
{
    public class GameProcessor(IMiniMaxAi aiBot, IGameStateService gameStateService) : IGameProcessor
    {
        private Board GameBoard { get; set; } = null!;

        public ErrorOr<Success> AiMakeMove(out MoveParameters moveParameters)
        {
            moveParameters = aiBot.FindBestMove();
            return MakeMove(moveParameters);
        }

        public ErrorOr<Success> MakeMove(MoveParameters moveParameters)
        {
            if (gameStateService.State != GameState.Ongoing)
                return Error.Validation(
                    "InvalidGameState",
                    Messages.Error.InvalidGameState
                );

            if (gameStateService.CurrentTurn != moveParameters.PlayerTurn)
                return Error.Validation(
                    "InvalidCurrentPlayer",
                    Messages.Error.InvalidCurrentPlayer
                );

            var canMakeMoveResult = GameBoard.CanMakeMove(moveParameters);
            if (canMakeMoveResult.IsError)
                return canMakeMoveResult.Errors;

            GameBoard.MakeMove(moveParameters);
            gameStateService.SetState(GameBoard.GetGameStatus());

            if (gameStateService.State == GameState.Ongoing) SwitchTurn();

            return Result.Success;
        }

        public void Reset()
        {
            GameBoard = new Board();
            gameStateService.Reset();
        }

        public GameProcessor Clone()
        {
            var clonedProcessor = new GameProcessor(aiBot, gameStateService)
            {
                GameBoard = GameBoard.Clone()
            };
            return clonedProcessor;
        }

        public void InitializeGame(bool twoPlayerGame = true)
        {
            GameBoard = new Board();
            gameStateService.SetState(GameState.Ongoing);
            gameStateService.SetCurrentTurn(PlayerTurn.X);
            gameStateService.SetGameMode(twoPlayerGame ? GameModes.GameWithPlayer : GameModes.GameWithAi);
        }

        public Board GetBoard()
        {
            return GameBoard;
        }

        private void SwitchTurn()
        {
            var nextTurn = gameStateService.CurrentTurn == PlayerTurn.X ? PlayerTurn.О : PlayerTurn.X;
            gameStateService.SetCurrentTurn(nextTurn);
        }
    }
}