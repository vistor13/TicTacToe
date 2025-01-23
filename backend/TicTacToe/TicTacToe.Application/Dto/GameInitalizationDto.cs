using TicTacToe.Core.Models;

namespace TicTacToe.Application.Dto;

public sealed record GameInitializationDto(Guid Id, GameModes Modes);