using System;

namespace GameRental.Api.Dtos;

public record AddPhysicalGameRequest(string Title, string Genre, int AgeLimit, decimal DailyPrice, int StockCount);
