using ErrorOr;
using MediatR;
using TicTacToe.Infrastructure.Interfaces;

namespace TicTacToe.Application.Queries;

public class GetStateByIdHandler(IGameRepository gameRepository)
    : IRequestHandler<GetStateByIdQuery, ErrorOr<GameStateDto>>
{
    public async Task<ErrorOr<GameStateDto>> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
    {
        var gameStateEntity = await gameRepository.GetById(request.Id);

        if (gameStateEntity is null)
            return Error.NotFound(
                "NotFoundGame",
                "Game not found."
            );

        return GameStateDto.ToDto(gameStateEntity);
    }
}