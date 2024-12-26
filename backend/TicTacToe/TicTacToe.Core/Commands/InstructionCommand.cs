using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core.Commands;

public class InstructionCommand(IUiRender renderer) : ICommand
{
    public bool Execute()
    {
        renderer.RenderInstruction();
        return true;
    }
}