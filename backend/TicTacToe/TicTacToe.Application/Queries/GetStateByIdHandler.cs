using MediatR;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Services;

namespace TicTacToe.Application.Queries;

public class GetStateByIdHandler(GameStateManager gameStateManager) : IRequestHandler<GetStateByIdQuery, GameStateDto>
{
    public async Task<GameStateDto> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
    {
        var gameState = gameStateManager.GetGame(request.Id);
        return gameState ?? null!;
    }
}