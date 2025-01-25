using MediatR;
using TicTacToe.Application.Dto;

namespace TicTacToe.Application.Commands.WebApi.StartGameCommand;

public sealed record StartGameCommand(bool IsTwoPlayerMode) : IRequest<GameInitializationDto>;