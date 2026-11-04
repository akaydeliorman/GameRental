using GameRental.Core.Models;

namespace GameRental.Api.Dtos;

public record RentalResponse(
    int Id,
    int CustomerId,
    int GameId,
    bool IsReturned)
{
    public static RentalResponse From(Rental rental)
    {
        return new RentalResponse(
            rental.Id,
            rental.CustomerId,
            rental.GameId,
            rental.IsReturned);
    }
}
