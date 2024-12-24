using Moq;
using TicTacToe.ConsoleUI;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;
using TicTacToe.Core.Services;

namespace TicTacToe.Tests.IntegrationTests;

public class GameControllerTests
{
    private readonly Mock<ICommandInvoker> _commandInvokerMock;
    private readonly Mock<IConsoleRenderer> _consoleRendererMock;
    private readonly GameController _gameController;
    private readonly GameProcessor _gameProcessor;
    private readonly Mock<IParseCommand> _parserCommandMock;

    public GameControllerTests()
    {
        _parserCommandMock = new Mock<IParseCommand>();
        _consoleRendererMock = new Mock<IConsoleRenderer>();
        _commandInvokerMock = new Mock<ICommandInvoker>();
        _gameProcessor = new GameProcessor();

        _gameController = new GameController(
            _parserCommandMock.Object,
            _gameProcessor,
            _consoleRendererMock.Object,
            _commandInvokerMock.Object
        );
    }

    [Fact]
    public void GameBoard_ShouldUpdate_AfterEachMove()
    {
        // Arrange
        _gameProcessor.InitializeGame();
        var moveCommand = new MoveCommand(_gameProcessor, new MoveParameters(0, 0, PlayerTurn.X));

        _parserCommandMock
            .Setup(parser => parser.CommandParse(It.IsAny<string>()))
            .Returns(moveCommand);

        _commandInvokerMock
            .Setup(invoker => invoker.Execute(moveCommand))
            .Returns(true);

        // Act
        _gameController.Execute();

        // Assert
        Assert.Equal('X', _gameProcessor.GameBoard.GetCell(0, 0));
        _consoleRendererMock.Verify(renderer => renderer.RenderBoard(It.IsAny<Board>()), Times.Once);
    }
}