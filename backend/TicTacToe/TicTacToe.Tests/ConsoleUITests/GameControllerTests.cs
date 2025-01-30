using ErrorOr;
using Moq;
using TicTacToe.Application.Commands;
using TicTacToe.Application.Interfaces;
using TicTacToe.ConsoleUI;
using TicTacToe.ConsoleUI.Commands;
using TicTacToe.ConsoleUI.Interfaces;
using TicTacToe.Core.Models;
using Messages = TicTacToe.Application.ApplicationMessages.Messages;

namespace TicTacToe.Tests.ConsoleUITests;

public class GameControllerTests
{
    private readonly GameController _gameController;
    private readonly Mock<ICommandInvoker> _mockCommandInvoker;
    private readonly Mock<ICommandParser> _mockCommandParser;
    private readonly Mock<IUiRender> _mockConsoleRenderer;
    private readonly Mock<IGameProcessor> _mockGameProcessor;
    private readonly Mock<IInputProvider> _mockReader;

    public GameControllerTests()
    {
        _mockCommandParser = new Mock<ICommandParser>();
        _mockGameProcessor = new Mock<IGameProcessor>();
        _mockConsoleRenderer = new Mock<IUiRender>();
        _mockCommandInvoker = new Mock<ICommandInvoker>();
        _mockReader = new Mock<IInputProvider>();

        _gameController = new GameController(
            _mockCommandParser.Object,
            _mockGameProcessor.Object,
            _mockConsoleRenderer.Object,
            _mockCommandInvoker.Object,
            _mockReader.Object);
    }

    [Fact]
    public void Execute_ShouldRenderWelcomeMessage_WhenGameStarts()
    {
        // Arrange
        _mockReader.Setup(r => r.GetCommandInput())
            .Returns("end");
        _mockCommandParser.Setup(p => p.CommandParse("end"))
            .Returns(new EndGameCommand(_mockGameProcessor.Object));
        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<ICommand>(), It.IsAny<Dictionary<string, List<Type>>>()))
            .Returns(Result.Success);

        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(r => r.RenderWelcome(), Times.Once);
    }

    [Fact]
    public void Execute_ShouldExitLoop_WhenEndGameCommandIsExecuted()
    {
        // Arrange
        _mockReader.SetupSequence(r => r.GetCommandInput())
            .Returns("game player")
            .Returns("end");
        _mockCommandParser.Setup(p => p.CommandParse("game player"))
            .Returns(new PlayerGameCommand(_mockGameProcessor.Object, _mockConsoleRenderer.Object));
        _mockCommandParser.Setup(p => p.CommandParse("end"))
            .Returns(new EndGameCommand(_mockGameProcessor.Object));
        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<ICommand>(), It.IsAny<Dictionary<string, List<Type>>>()))
            .Returns(Result.Success);

        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(r => r.RenderWelcome(), Times.Once);
        _mockCommandParser.Verify(p => p.CommandParse(It.IsAny<string>()), Times.Exactly(2));
        _mockCommandInvoker.Verify(i => i.Execute(It.IsAny<ICommand>(), It.IsAny<Dictionary<string, List<Type>>>()),
            Times.Exactly(2));
    }

    [Fact]
    public void Execute_ShouldRenderError_WhenCommandNotAllowed()
    {
        // Arrange
        var error = Error.Validation(
            "ExecuteCommand",
            Messages.Error.CommandNotAllowed
        );

        _mockReader.SetupSequence(r => r.GetCommandInput())
            .Returns("replay")
            .Returns("end");

        _mockCommandParser.Setup(p => p.CommandParse("replay"))
            .Returns(new ReplayCommand(_mockGameProcessor.Object, _mockConsoleRenderer.Object));
        _mockCommandParser.Setup(p => p.CommandParse("end"))
            .Returns(new EndGameCommand(_mockGameProcessor.Object));

        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<ReplayCommand>(), It.IsAny<Dictionary<string, List<Type>>>()))
            .Returns(error);

        _mockCommandInvoker
            .Setup(i => i.Execute(It.IsAny<EndGameCommand>(), It.IsAny<Dictionary<string, List<Type>>>()))
            .Returns(Result.Success);

        // Act
        _gameController.Execute(); // Assert
        _mockConsoleRenderer.Verify(
            r => r.RenderError(Messages.Error.CommandNotAllowed), Times.Once);
    }

    [Fact]
    public void Execute_ShouldRenderError_WhenCommandExecutionFails()
    {
        // Arrange
        var board = new Board();
        board.SetGameState(GameState.Ongoing);
        var executionResultWithError = Error.Validation(
            "OutOfBounds",
            Core.CoreMessages.Messages.Error.OutOfBoundsErrorMessage
        );
        var move = new MoveParameters(5, 1, PlayerTurn.X);
        _mockReader.SetupSequence(r => r.GetCommandInput())
            .Returns("game ai")
            .Returns("move 5 1")
            .Returns("end");

        _mockCommandParser.Setup(p => p.CommandParse("game ai"))
            .Returns(new AiGameCommand(_mockGameProcessor.Object, _mockConsoleRenderer.Object));

        _mockCommandParser.Setup(p => p.CommandParse("move 5 1"))
            .Returns(new MakeMoveCommand(_mockGameProcessor.Object, move));

        _mockCommandParser.Setup(p => p.CommandParse("end"))
            .Returns(new EndGameCommand(_mockGameProcessor.Object));

        _mockGameProcessor.SetupSequence(g => g.IsRunning)
            .Returns(false);

        _mockGameProcessor.Setup(g => g.GetBoard())
            .Returns(board);

        _mockGameProcessor.Setup(g => g.GameMode)
            .Returns(GameModes.GameWithAi);

        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<AiGameCommand>(), It.IsAny<Dictionary<string, List<Type>>>()))
            .Returns(Result.Success);

        _mockCommandInvoker
            .Setup(i => i.Execute(It.IsAny<MakeMoveCommand>(), It.IsAny<Dictionary<string, List<Type>>>()))
            .Returns(executionResultWithError);

        _mockCommandInvoker
            .Setup(i => i.Execute(It.IsAny<EndGameCommand>(), It.IsAny<Dictionary<string, List<Type>>>()))
            .Returns(Result.Success);
        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(
            r => r.RenderError(executionResultWithError.Description), Times.Once);
    }
}