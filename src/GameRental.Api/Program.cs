using GameRental.Api.Dtos;
using GameRental.Api.Helpers;
using GameRental.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGameRentalService, GameRentalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/games", (IGameRentalService service) =>
    service.GetGames().Select(GameResponse.From));

app.MapPost("/api/games/physical", (AddPhysicalGameRequest request, IGameRentalService service) =>
{
    var result = service.AddPhysicalGame(request.Title, request.Genre, request.AgeLimit, request.DailyPrice, request.StockCount);

    if (result.IsSuccess)
    {
        return Results.Created($"/api/games/{result.Value!.Id}", GameResponse.From(result.Value!));
    }

    return Results.BadRequest(new { error = result.Message });
});

app.MapPost("/api/games/digital", (AddDigitalGameRequest request, IGameRentalService service) =>
{
    var result = service.AddDigitalGame(request.Title, request.Genre, request.AgeLimit, request.DailyPrice, request.Platform);
    if (result.IsSuccess)
    {
        return Results.Created($"/api/games/{result.Value!.Id}", GameResponse.From(result.Value!));
    }

    return Results.BadRequest(new { error = result.Message });
});

app.MapPost("/api/customers", (AddCustomerRequest request, IGameRentalService service) =>
{
    var result = service.AddCustomer(request.Name, request.Age);
    if (result.IsSuccess)
    {
        return Results.Created($"/api/customers/{result.Value!.Id}", CustomerResponse.From(result.Value!));
    }

    return Results.BadRequest(new { error = result.Message });
});
app.MapGet("/api/customers", (IGameRentalService service) =>
    service.GetCustomers().Select(CustomerResponse.From));

app.MapPost("/api/rentals", (RentGameRequest request, IGameRentalService service) =>
{
    var result = service.RentGame(request.CustomerId, request.GameId);
    if (result.IsSuccess)
    {
        return Results.Created($"/api/rentals/{result.Value!.Id}", RentalResponse.From(result.Value!));
    }
    return ErrorResults.From(result.ErrorType, result.Message);
});

app.Run();