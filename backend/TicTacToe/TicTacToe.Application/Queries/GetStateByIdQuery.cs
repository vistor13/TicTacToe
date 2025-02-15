using ErrorOr;
using MediatR;
using TicTacToe.Application.Dto;

namespace TicTacToe.Application.Queries;

public sealed record GetStateByIdQuery(long Id) : IRequest<ErrorOr<GameStateDto>>;