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

    public ServiceResult<Game> AddDigitalGame(string title, string genre, int ageLimit, decimal dailyPrice, string platform)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Oyun adı boş olamaz.");
        }
        if (dailyPrice <= 0)
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Günlük fiyat pozitif olmalı.");
        }
        if (string.IsNullOrWhiteSpace(platform))
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Platform adı boş olamaz.");
        }

        DigitalGame game = new DigitalGame(_nextGameId, title, genre, ageLimit, dailyPrice, platform);
        
        _nextGameId++;
        _games.Add(game);
        _gameById[game.Id] = game;

        return ServiceResult<Game>.Success(game);
    }
    public ServiceResult<Customer> AddCustomer(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return ServiceResult<Customer>.Fail(ErrorType.Validation, "İsim boş olamaz.");
        }
        if (age <= 6)
        {
            return ServiceResult<Customer>.Fail(ErrorType.Validation, "Kullanıcının yaşı 7 veya daha büyük olmalı.");
        }

        Customer customer = new Customer(_nextCustomerId, name, age);
        _nextCustomerId++;
        _customer.Add(customer);
        _customerById[customer.Id] = customer;

        return ServiceResult<Customer>.Success(customer);
    }
}
