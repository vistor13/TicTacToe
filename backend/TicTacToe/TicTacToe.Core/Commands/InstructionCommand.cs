using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Core.Commands;

public class InstructionCommand(IUiRender renderer) : ICommand
{
    public OperationResult Execute()
    {
        renderer.RenderInstruction();
        return OperationResult.Success();
    }
}