using ErrorOr;
using TicTacToe.Application.ApplicationMessages;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Application.Services;

public class GameProcessor(IMiniMaxAi aiBot) : IGameProcessor
{
    private Board GameBoard { get; set; } = new();

    public bool IsRunning => GameBoard.State is GameState.Ongoing;
    public GameModes GameMode { get; private set; } = GameModes.NotDefined;

    public ErrorOr<Success> AiMakeMove(out MoveParameters moveParameters)
    {
        moveParameters = aiBot.FindBestMove(GameBoard);
        return MakeMove(moveParameters);
    }

    public GameStateDto GetGameState()
    {
        return new GameStateDto(GameMode, GameBoard.CurrentTurn, GameBoard.State, GameBoard.Grid);
    }

    public void LoadGameState(GameStateParameters state)
    {
        GameMode = state.GameMode;
        GameBoard.LoadState(state);
    }

    public ErrorOr<Success> MakeMove(MoveParameters moveParameters)
    {
        if (GameBoard.State != GameState.Ongoing)
            return Error.Validation(
                "InvalidGameState",
                Messages.Error.InvalidGameState
            );

        if (GameBoard.CurrentTurn != moveParameters.PlayerTurn)
            return Error.Validation(
                "InvalidCurrentPlayer",
                Messages.Error.InvalidCurrentPlayer
            );

        var makeMove = GameBoard.MakeMove(moveParameters);
        if (makeMove.IsError)
            return makeMove.Errors;

        return Result.Success;
    }

    public void Reset()
    {
        GameBoard = new Board();
        GameMode = GameModes.NotDefined;
    }

    public void InitializeGame(bool twoPlayerGame = true)
    {
        GameBoard = new Board();
        GameMode = twoPlayerGame ? GameModes.GameWithPlayer : GameModes.GameWithAi;
        GameBoard.InitializeGameState();
    }

    public Board GetBoard()
    {
        return GameBoard;
    }
}