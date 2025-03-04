namespace TicTacToe.Application.Dto;

public sealed record GameResultDto(bool IsGameOver, string? Winner);