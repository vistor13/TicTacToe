using ErrorOr;
using MediatR;

namespace TicTacToe.Application.Commands.MoveCommand;

public record MoveCommand(long GameId, int Row, int Col) : IRequest<ErrorOr<Success>>;