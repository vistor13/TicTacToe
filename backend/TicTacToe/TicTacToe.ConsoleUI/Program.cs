using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application;
using TicTacToe.Application.Commands;
using TicTacToe.ConsoleUI;
using TicTacToe.ConsoleUI.Commands;
using TicTacToe.ConsoleUI.ConsoleViews;
using TicTacToe.ConsoleUI.InputProcessing;
using TicTacToe.ConsoleUI.Interfaces;

var services = new ServiceCollection();

//Commands
services.AddScoped<AiGameCommand>();
services.AddScoped<PlayerGameCommand>();
services.AddScoped<InstructionCommand>();
services.AddScoped<ReplayCommand>();
services.AddScoped<ExitCommand>();
services.AddScoped<EndGameCommand>();

services.AddScoped<IUiRender, ConsoleRenderer>();
;
services.AddScoped<ICommandParser, CommandParser>();
services.AddScoped<IInputProvider, InputProvider>();
services.AddScoped<GameController>();
services.AddApplicationLayer();

var serviceProvider = services.BuildServiceProvider();
var scopeFactory = serviceProvider.CreateScope();

var gameController = scopeFactory.ServiceProvider.GetRequiredService<GameController>();
gameController.Execute();