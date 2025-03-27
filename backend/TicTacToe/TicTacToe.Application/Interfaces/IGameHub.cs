using ErrorOr;

namespace TicTacToe.Application.Interfaces;

public interface IGameHub
{
    Task ReceiveSymbol(string symbol);
    Task ReceiveRoomId(string roomId);
    Task GameStarted(string roomId);
    Task JoinFailed(string error);
    Task ReceiveAvailableGames(string roomId);
    Task MoveFailed(ErrorOr<Success> errorOr);
    Task MoveMade(string symbol, int row, int col);
}