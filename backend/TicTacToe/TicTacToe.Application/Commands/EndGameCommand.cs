using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Commands;

public class EndGameCommand(IGameProcessor gameProcessor) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        gameProcessor.Reset();
        return null;
    }
}