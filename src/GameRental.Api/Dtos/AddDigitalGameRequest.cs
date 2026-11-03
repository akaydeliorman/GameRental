namespace GameRental.Api.Dtos;

public record AddDigitalGameRequest(string Title, string Genre, int AgeLimit, decimal DailyPrice, string Platform);