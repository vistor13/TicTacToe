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

    public bool ShouldAiMove => GameMode is GameModes.GameWithAi;

    public GameModes GameMode { get; private set; } = GameModes.NotDefined;

    public ErrorOr<GameStateDto> AiMakeMove(out MoveParametersDto moveParameters)
    {
        moveParameters = aiBot.FindBestMove(GameBoard);
        return MakeMove(moveParameters);
    }

    public GameStateModel GetGameState()
    {
        return new GameStateModel(
            GameMode,
            GameBoard.State,
            GameBoard.CurrentTurn,
            GameBoard.Grid,
            IsRunning,
            ShouldAiMove);
    }

    public void LoadGameState(GameStateModel state)
    {
        GameMode = state.Modes;
        GameBoard.LoadState(new GameStateParameters(state.State, state.Grid, state.CurrentPlayer));
    }

    public ErrorOr<GameStateDto> MakeMove(MoveParametersDto dto)
    {
        var moveParameters = new MoveParameters(dto.Row, dto.Col, Enum.Parse<PlayerTurn>(dto.Player));

        if (!IsRunning)
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

        var gameStateDto = new GameStateDto
        (GameMode.ToString(),
            GameBoard.CurrentTurn.ToString(),
            GameBoard.State.ToString(),
            GameBoard.Grid,
            IsRunning,
            ShouldAiMove);

        return gameStateDto;
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

    public GameResultDto GetGameResult()
    {
        return GameBoard.State switch
        {
            GameState.Win => new GameResultDto(true, GameBoard.CurrentTurn.ToString()),
            GameState.Draw => new GameResultDto(true, null),
            _ => new GameResultDto(false, null)
        };
    }
}