using System;

namespace GameRental.Api.Dtos;

public record RentGameRequest (int CustomerId, int GameId);