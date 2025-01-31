using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class ShowBoardCommand(IGameProcessor gameProcessor, IUiRender renderer) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        var gameState = gameProcessor.GetGameState();
        renderer.RenderBoard(gameState.Grid);
        return null;
    }
}