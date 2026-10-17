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
app.MapGet("/api/games", (IGameRentalService service) => service.GetGames());

app.MapPost("/api/games/physical", (AddPhysicalGameRequest request, IGameRentalService service) =>
{
    var result = service.AddPhysicalGame(request.Title, request.Genre, request.AgeLimit, request.DailyPrice, request.StockCount);
    if (result.IsSuccess)
    {
        return Results.Created($"/api/games/{result.Value!.Id}", result.Value);
    }
    return Results.BadRequest(new { error = result.Message });
});

app.Run();


record AddPhysicalGameRequest(string Title, string Genre, int AgeLimit, decimal DailyPrice, int StockCount);
