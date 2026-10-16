using GameRental.Core.Models;
using GameRental.Core.Results;
namespace GameRental.Core.Services;

public class GameRentalService
{
    private readonly List<Game> _games = new();
    private readonly Dictionary<int, Game> _gameById = new();
    private readonly List<Customer> _customer = new();
    private readonly Dictionary<int, Customer> _customerById = new();
    private readonly List<Rental> _rental = new();
    private readonly Dictionary<int, Rental> _rentalById = new();

    private int _nextGameId = 1;
    private int _nextCustomerId = 1;
    private int _nextRentalId = 1;

    public ServiceResult<Game> AddPhysicalGame(string title, string genre, int ageLimit, decimal dailyPrice, int stockCount)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Oyun adı boş olamaz.");
        }
        if (dailyPrice <= 0)
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Günlük fiyat pozitif olmalı.");
        }
        if (stockCount < 0)
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Stok negatif olamaz.");
        }
        
        PhysicalGame game = new PhysicalGame(_nextGameId, title, genre, ageLimit, dailyPrice, stockCount);
        
        _nextGameId++;
        _games.Add(game);
        _gameById[game.Id] = game;

        return ServiceResult<Game>.Success(game);
    }
}
