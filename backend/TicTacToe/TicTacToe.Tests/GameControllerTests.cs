using ErrorOr;
using Moq;
using TicTacToe.ConsoleUI;
using TicTacToe.ConsoleUI.Interfaces;
using TicTacToe.Core.Commands;
using TicTacToe.Core.CoreMessages;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Models;

namespace TicTacToe.Tests;

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
        _mockReader.SetupSequence(r => r.GetCommandInput())
            .Returns("end");
        _mockCommandParser.Setup(p => p.CommandParse("end"))
            .Returns(new Mock<ICommand>().Object);
        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<ICommand>()))
            .Returns(Result.Success);
        _mockGameProcessor.Setup(g => g.IsRunning)
            .Returns(false);

        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(r => r.RenderWelcome(), Times.Once);
    }

    [Fact]
    public void PlayGameLoop_ShouldRenderWinMessage_WhenGameEndsWithWin()
    {
        // Arrange
        var move = new MoveParameters(1, 1, PlayerTurn.X);
        _mockReader.SetupSequence(r => r.GetCommandInput())
            .Returns("game player")
            .Returns("move 1 1");

        _mockCommandParser.Setup(p => p.CommandParse("game player"))
            .Returns(new PlayerGameCommand(_mockGameProcessor.Object, _mockConsoleRenderer.Object));

        _mockCommandParser.Setup(p => p.CommandParse("move 1 1"))
            .Returns(new MoveCommand(_mockGameProcessor.Object, move));

        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<ICommand>()))
            .Returns(Result.Success);

        _mockGameProcessor.Setup(g => g.GameMode)
            .Returns(GameModes.GameWithPlayer);

        _mockGameProcessor.SetupSequence(g => g.IsRunning)
            .Returns(true)
            .Returns(false);

        _mockGameProcessor.Setup(g => g.GetBoard())
            .Returns(new Board());

        _mockGameProcessor.Object.GetBoard().SetGameState(GameState.Win);

        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(r => r.RenderWin(It.IsAny<PlayerTurn>()), Times.Once);
        _mockConsoleRenderer.Verify(r => r.RenderProposeRestoreGame(), Times.Once);
    }

    [Fact]
    public void PlayGameLoop_ShouldRenderDrawMessage_WhenGameEndsWithDraw()
    {
        // Arrange
        var board = new Board();

        board.SetGameState(GameState.Draw);

        var move = new MoveParameters(1, 1, PlayerTurn.X);
        _mockReader.SetupSequence(r => r.GetCommandInput())
            .Returns("game player")
            .Returns("move 1 1");

        _mockCommandParser.Setup(p => p.CommandParse("game player"))
            .Returns(new PlayerGameCommand(_mockGameProcessor.Object, _mockConsoleRenderer.Object));

        _mockCommandParser.Setup(p => p.CommandParse("move 1 1"))
            .Returns(new MoveCommand(_mockGameProcessor.Object, move));

        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<ICommand>()))
            .Returns(Result.Success);

        _mockGameProcessor.SetupSequence(g => g.IsRunning)
            .Returns(true)
            .Returns(false);

        _mockGameProcessor.Setup(g => g.GetBoard())
            .Returns(board);


        _mockGameProcessor.Setup(g => g.GameMode)
            .Returns(GameModes.GameWithPlayer);

        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(r => r.RenderDraw(), Times.Once);
        _mockConsoleRenderer.Verify(r => r.RenderProposeRestoreGame(), Times.Once);
    }

    [Fact]
    public void PlayGameLoop_ShouldAllowAiToMakeMove_WhenGameModeIsGameWithAi()
    {
        // Arrange
        var aiMove = new MoveParameters(0, 0, PlayerTurn.О);
        var board = new Board();

        board.SetGameState(GameState.Ongoing);

        var move = new MoveParameters(1, 1, PlayerTurn.X);
        _mockReader.SetupSequence(r => r.GetCommandInput())
            .Returns("game ai")
            .Returns("move 1 1");

        _mockCommandParser.Setup(p => p.CommandParse("game ai"))
            .Returns(new AiGameCommand(_mockGameProcessor.Object, _mockConsoleRenderer.Object));

        _mockCommandParser.Setup(p => p.CommandParse("move 1 1"))
            .Returns(new MoveCommand(_mockGameProcessor.Object, move));

        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<ICommand>()))
            .Returns(Result.Success);

        _mockGameProcessor.SetupSequence(g => g.IsRunning)
            .Returns(true)
            .Returns(false);

        _mockGameProcessor.Setup(g => g.GetBoard())
            .Returns(board);

        _mockGameProcessor.Setup(g => g.GameMode)
            .Returns(GameModes.GameWithAi);

        _mockGameProcessor.Setup(g => g.AiMakeMove(out aiMove))
            .Returns(() => Result.Success);


        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(r => r.RenderMessage(
            string.Format(Messages.GameProcess.AiMove, aiMove.Row + 1, aiMove.Col + 1)), Times.Once);

        _mockConsoleRenderer.Verify(r => r.RenderWin(It.IsAny<PlayerTurn>()), Times.Never);
        _mockConsoleRenderer.Verify(r => r.RenderDraw(), Times.Never);
    }

    [Fact]
    public void Execute_ShouldRenderError_WhenCommandExecutionFails()
    {
        // Arrange
        var board = new Board();

        board.SetGameState(GameState.Ongoing);
        var executionResultWithError = Error.Validation(
            "OutOfBounds",
            Messages.Error.OutOfBoundsErrorMessage
        );
        var move = new MoveParameters(5, 1, PlayerTurn.X);
        _mockReader.SetupSequence(r => r.GetCommandInput())
            .Returns("game ai")
            .Returns("move 5 1");

        _mockCommandParser.Setup(p => p.CommandParse("game ai"))
            .Returns(new AiGameCommand(_mockGameProcessor.Object, _mockConsoleRenderer.Object));

        _mockCommandParser.Setup(p => p.CommandParse("move 5 1"))
            .Returns(new MoveCommand(_mockGameProcessor.Object, move));

        _mockGameProcessor.SetupSequence(g => g.IsRunning)
            .Returns(true)
            .Returns(false);

        _mockGameProcessor.Setup(g => g.GetBoard())
            .Returns(board);

        _mockGameProcessor.Setup(g => g.GameMode)
            .Returns(GameModes.GameWithAi);

        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<AiGameCommand>()))
            .Returns(Result.Success);

        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<MoveCommand>()))
            .Returns(executionResultWithError);


        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(
            r => r.RenderError(It.Is<string>(s => s.Contains(executionResultWithError.Description))), Times.Once);
    }

    [Fact]
    public void Execute_ShouldRenderError_WhenCommandExecutionFailsWithError()
    {
        // Arrange
        var board = new Board();

        board.SetGameState(GameState.NotStarted);
        var executionResultWithError = Error.Validation(
            "ExecuteCommand",
            Messages.Error.CommandNotAllowed
        );

        var move = new MoveParameters(1, 1, PlayerTurn.X);
        _mockReader.Setup(r => r.GetCommandInput())
            .Returns("move 1 1");

        _mockCommandParser.Setup(p => p.CommandParse("move 1 1"))
            .Returns(new MoveCommand(_mockGameProcessor.Object, move));

        _mockGameProcessor.Setup(g => g.IsRunning)
            .Returns(false);

        _mockGameProcessor.Setup(g => g.GetBoard())
            .Returns(board);

        _mockGameProcessor.Setup(g => g.GameMode)
            .Returns(GameModes.NotDefined);

        _mockCommandInvoker.Setup(i => i.Execute(It.IsAny<MoveCommand>()))
            .Returns(executionResultWithError);


        // Act
        _gameController.Execute();

        // Assert
        _mockConsoleRenderer.Verify(
            r => r.RenderError(It.Is<string>(s => s.Contains(executionResultWithError.Description))), Times.Once);
    }
}