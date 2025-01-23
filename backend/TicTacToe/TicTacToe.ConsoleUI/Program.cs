using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Commands;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Services;
using TicTacToe.ConsoleUI;
using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.ConsoleUI.InputProcessing;
using TicTacToe.ConsoleUI.Interfaces;
using TicTacToe.Core.Commands;
using TicTacToe.Core.Interfaces;

var services = new ServiceCollection();

//Commands
services.AddScoped<AiGameCommand>();
services.AddScoped<PlayerGameCommand>();
services.AddScoped<InstructionCommand>();
services.AddScoped<ReplayCommand>();
services.AddScoped<ExitCommand>();
services.AddScoped<EndGameCommand>();

services.AddScoped<IGameProcessor, GameProcessor>();
services.AddScoped<IUiRender, ConsoleRenderer>();
services.AddScoped<ICommandInvoker, CommandInvoker>();
services.AddScoped<ICommandParser, CommandParser>();
services.AddScoped<IInputProvider, InputProvider>();
services.AddScoped<IMiniMaxAi, MiniMaxAi>();
services.AddScoped<GameController>();

var serviceProvider = services.BuildServiceProvider();
var scopeFactory = serviceProvider.CreateScope();

var gameController = scopeFactory.ServiceProvider.GetRequiredService<GameController>();
gameController.Execute();