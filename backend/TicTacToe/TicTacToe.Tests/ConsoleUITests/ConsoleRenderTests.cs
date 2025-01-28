using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.Core.Models;

namespace TicTacToe.Tests.ConsoleUITests;

public class ConsoleRendererTests
{
    private readonly StringWriter _consoleOutput;
    private readonly ConsoleRenderer _renderer;

    public ConsoleRendererTests()
    {
        _renderer = new ConsoleRenderer();
        _consoleOutput = new StringWriter();
        Console.SetOut(_consoleOutput);
    }

    [Fact]
    public void RenderWelcome_PrintsWelcomeMessage()
    {
        // Act
        _renderer.RenderWelcome();

        // Assert
        var output = _consoleOutput.ToString();
        Assert.Contains(ConsoleMessages.GameMessages.WelcomeMessage, output);
    }

    [Fact]
    public void RenderInstruction_PrintsInstructionMessage()
    {
        // Act
        _renderer.RenderInstruction();

        // Assert
        var output = _consoleOutput.ToString();
        Assert.Contains(ConsoleMessages.GameMessages.Instruction, output);
    }

    [Fact]
    public void RenderPrompt_PrintsPromptWithCorrectColor()
    {
        // Arrange
        const string promptText = "Enter your move:";

        // Act
        _renderer.RenderPrompt(promptText);

        // Assert
        var output = _consoleOutput.ToString();
        Assert.Contains(promptText, output);
    }

    [Fact]
    public void RenderError_PrintsErrorMessage()
    {
        // Arrange
        const string errorMessage = "Invalid input";

        // Act
        _renderer.RenderError(errorMessage);

        // Assert
        var output = _consoleOutput.ToString();
        Assert.Contains("Error : Invalid input", output);
    }

    [Fact]
    public void RenderBoard_ShouldDisplayFormattedBoard()
    {
        // Arrange
        var board = new Board();
        board.Grid[0, 0] = 'X';
        board.Grid[1, 1] = 'O';

        // Act
        _renderer.RenderBoard(board.Grid);

        // Assert
        var output = _consoleOutput.ToString();
        Assert.Contains("X", output);
        Assert.Contains("O", output);
        Assert.Contains("│", output);
        Assert.Contains("─────", output);
    }

    [Fact]
    public void RenderProposeRestoreGame_PrintsEndGamePrompt()
    {
        // Act
        _renderer.RenderProposeRestoreGame();

        // Assert
        var output = _consoleOutput.ToString();
        Assert.Contains(ConsoleMessages.GameMessages.EndGamePrompt, output);
    }
}