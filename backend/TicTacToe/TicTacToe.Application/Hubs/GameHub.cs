using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using TicTacToe.Application.Dto;
using TicTacToe.Application.Interfaces;

namespace TicTacToe.Application.Hubs;

public class GameHub(IMemoryCache cache, IGameProcessor gameProcessor) : Hub<IGameHub>
{
    private const string GameRoomsKey = "GameRooms";

    private ConcurrentDictionary<string, GameRoom> GetGameRooms()
    {
        return cache.GetOrCreate(GameRoomsKey, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(30);
            return new ConcurrentDictionary<string, GameRoom>();
        })!;
    }

    public async Task CreateGame(string playerOne)
    {
        var gameRooms = GetGameRooms();

        gameProcessor.InitializeGame();

        var room = new GameRoom
        {
            PlayerOne = playerOne,
            GameState = gameProcessor.GetGameState()
        };
        room.AssignSymbols();

        if (gameRooms.TryAdd(room.Id, room))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room.Id);
            await Clients.Caller.ReceiveRoomId(room.Id);
            await Clients.Caller.ReceiveSymbol("X");
            await UpdateAvailableGames();
        }
    }

    public async Task JoinGame(string roomId, string playerTwo)
    {
        var gameRooms = GetGameRooms();
        if (gameRooms.TryGetValue(roomId, out var room) && room.PlayerTwo == null)
        {
            room.PlayerTwo = playerTwo;
            room.Status = "Playing";
            room.AssignSymbols();

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            await Clients.Group(roomId).GameStarted(roomId);
            await Clients.Caller.ReceiveSymbol("O");
            await UpdateAvailableGames();
        }
        else
        {
            await Clients.Caller.JoinFailed("Room is full or doesn't exist");
        }
    }

    public async Task MakeMove(string roomId, string playerId, int row, int col)
    {
        var gameRooms = GetGameRooms();

        if (gameRooms.TryGetValue(roomId, out var room))
        {
            room.PlayerSymbols.TryGetValue(playerId, out var symbol);

            gameProcessor.LoadGameState(room.GameState);
            var move = new MoveParametersDto(row - 1, col - 1, symbol);

            var result = gameProcessor.MakeMove(move);
            if (result.IsError)
            {
                await Clients.Caller.MoveFailed(result.Errors);
                return;
            }

            await Clients.Group(roomId).MoveMade(symbol, row, col);
        }
    }


    public async Task GetAvailableGames()
    {
        var gameRooms = GetGameRooms().Values.Where(r => r.PlayerTwo == null).Select(r => r.Id);
        await Clients.Caller.ReceiveAvailableGames(gameRooms.FirstOrDefault() ?? string.Empty);
    }

    private async Task UpdateAvailableGames()
    {
        var gameRooms = GetGameRooms().Values.Where(r => r.PlayerTwo == null).Select(r => r.Id);
        await Clients.All.ReceiveAvailableGames(gameRooms.FirstOrDefault() ?? string.Empty);
    }
}