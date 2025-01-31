using ErrorOr;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.ConsoleUI.Commands;

public class InstructionCommand(IUiRender renderer) : ICommand
{
    public ErrorOr<GameStateDto>? Execute()
    {
        renderer.RenderInstruction();
        return null;
    }
}