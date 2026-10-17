using GameRental.Core.Models;

namespace GameRental.Api.Dtos;

public record GameResponse(
    int Id,
    string Title,
    string Genre,
    int AgeLimit,
    decimal DailyPrice,
    string Type,
    int? StockCount,
    string? Platform)
{
    public static GameResponse From(Game game)
    {
        if (game is PhysicalGame physical)
        {
            return new GameResponse(
                game.Id,
                game.Title,
                game.Genre,
                game.AgeLimit,
                game.DailyPrice,
                "Physical",
                physical.StockCount,
                null);
        }

        if (game is DigitalGame digital)
        {
            return new GameResponse(
                game.Id,
                game.Title,
                game.Genre,
                game.AgeLimit,
                game.DailyPrice,
                "Digital",
                null,
                digital.Platform);
        }
        throw new InvalidOperationException($"Bilinmeyen oyun türü: {game.GetType().Name}");
    }
}
