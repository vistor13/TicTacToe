using ErrorOr;
using MediatR;

namespace TicTacToe.Application.Commands.WebApi.MoveCommand;

public record MoveCommand(Guid GameId, int Row, int Col) : IRequest<ErrorOr<Success>>;