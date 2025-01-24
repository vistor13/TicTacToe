using MediatR;
using TicTacToe.Application.Dto;

namespace TicTacToe.Application.Queries;

public sealed record GetStateByIdQuery(Guid Id) : IRequest<GameStateDto>;