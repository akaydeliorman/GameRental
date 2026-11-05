using GameRental.Api.Dtos;
using GameRental.Api.Helpers;
using GameRental.Core.Services;

namespace GameRental.Api.Endpoints;

public static class RentalEndpoints
{
    public static void MapRentalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/rentals");

        group.MapGet("", (IGameRentalService service) =>
            service.GetRentals().Select(RentalResponse.From));

        group.MapPost("", (RentGameRequest request, IGameRentalService service) =>
        {
            var result = service.RentGame(request.CustomerId, request.GameId);
            if (result.IsSuccess)
            {
                return Results.Created($"/api/rentals/{result.Value!.Id}", RentalResponse.From(result.Value!));
            }
            return ErrorResults.From(result.ErrorType, result.Message);
        });

        group.MapPost("/{id}/return", (int id, IGameRentalService service) =>
        {
            var result = service.ReturnGame(id);
            if (result.IsSuccess)
            {
                return Results.NoContent();
            }
            return ErrorResults.From(result.ErrorType, result.Message);
        });
    }
}
