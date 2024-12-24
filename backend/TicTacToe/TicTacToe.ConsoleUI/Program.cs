using TicTacToe.ConsoleUI;
using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.ConsoleUI.InputProcessing;
using TicTacToe.Core.Services;

var gameProcessor = new GameProcessor();
var consoleRenderer = new ConsoleRenderer();
var commandInvoker = new CommandInvoker(gameProcessor);
var parserCommand = new ParserCommand(gameProcessor, consoleRenderer);

var gameController = new GameController(parserCommand, gameProcessor, consoleRenderer, commandInvoker);

gameController.Execute();