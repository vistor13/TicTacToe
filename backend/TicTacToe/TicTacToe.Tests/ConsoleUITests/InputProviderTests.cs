using Moq;
using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.ConsoleUI.InputProcessing;
using TicTacToe.ConsoleUI.Interfaces;

namespace TicTacToe.Tests.ConsoleUITests;

public class InputProviderTests
{
    private readonly Mock<IUiRender> _mockRender = new();

    [Fact]
    public void GetCommandInput_ReturnsValidInput_AfterInvalidAttempts()
    {
        // Arrange
        var inputs = new Queue<string>(new[] { "", string.Empty, "ValidCommand" });
        Console.SetIn(new StringReader(string.Join(Environment.NewLine, inputs)));
        var inputReader = new InputProvider(_mockRender.Object);

        // Act
        var result = inputReader.GetCommandInput();

        // Assert
        Assert.Equal("ValidCommand", result);
        _mockRender.Verify(r => r.RenderPrompt(ConsoleMessages.GameMessages.CommandPrompt), Times.Exactly(3));
        _mockRender.Verify(r => r.RenderError(ConsoleMessages.Error.InvalidInput), Times.Exactly(2));
    }

    [Fact]
    public void GetCommandInput_ReturnsInput_WhenFirstTryIsValid()
    {
        // Arrange
        Console.SetIn(new StringReader("ValidInput"));
        var inputReader = new InputProvider(_mockRender.Object);

        // Act
        var result = inputReader.GetCommandInput();

        // Assert
        Assert.Equal("ValidInput", result);
        _mockRender.Verify(r => r.RenderPrompt(ConsoleMessages.GameMessages.CommandPrompt), Times.Once);
        _mockRender.Verify(r => r.RenderError(It.IsAny<string>()), Times.Never);
    }
}