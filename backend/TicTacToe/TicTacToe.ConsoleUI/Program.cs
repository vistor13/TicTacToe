using Microsoft.Extensions.DependencyInjection;
using TicTacToe.ConsoleUI;
using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.ConsoleUI.InputProcessing;
using TicTacToe.Core.Interfaces;
using TicTacToe.Core.Services;

var services = new ServiceCollection();
services.AddScoped<IGameProcessor, GameProcessor>();
services.AddScoped<IUiRender, ConsoleRenderer>();
services.AddScoped<ICommandInvoker, CommandInvoker>();
services.AddScoped<ICommandParser, CommandParser>();
services.AddScoped<IInputReader, InputReader>();
services.AddScoped<GameController>();

var serviceProvider = services.BuildServiceProvider();
var scopeFactory = serviceProvider.CreateScope();

var gameController = scopeFactory.ServiceProvider.GetRequiredService<GameController>();
gameController.Execute();