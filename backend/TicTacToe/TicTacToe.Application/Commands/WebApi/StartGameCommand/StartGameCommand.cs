using MediatR;
using TicTacToe.Application.Dto;

namespace TicTacToe.Application.Commands.WebApi.StartGameCommand;

public class StartGameCommand(bool isTwoPlayerMode) : IRequest<GameInitializationDto>
{
    public bool IsTwoPlayerMode { get; set; } = isTwoPlayerMode;
}